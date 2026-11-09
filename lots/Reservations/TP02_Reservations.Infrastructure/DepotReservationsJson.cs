using System.Text;
using System.Text.Json;
using Reservations.Application;
using Reservations.Domaine;
namespace Reservations.Infrastructure;
public sealed class DepotReservationsJson : IDepotReservations
{
    private readonly string m_chemin;
    private readonly JsonSerializerOptions m_options = new JsonSerializerOptions { WriteIndented = true };
    public DepotReservationsJson(string chemin)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(chemin);
        m_chemin = Path.GetFullPath(chemin);
    }
    public IReadOnlyCollection<Reservation> ObtenirToutes()
    {
        return Lire().AsReadOnly();
    }
    public Reservation? Obtenir(int numero)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(numero);
        return Lire().SingleOrDefault(e => e.Numero == numero);
    }
    public void Ajouter(Reservation element)
    {
        ArgumentNullException.ThrowIfNull(element);
        List<Reservation> elements = Lire();
        elements.Add(ProtectionDonnees.Copier(element));
        ProtectionDonnees.Verifier(elements);
        Ecrire(elements);
    }
    public void Remplacer(Reservation element)
    {
        ArgumentNullException.ThrowIfNull(element);
        List<Reservation> elements = Lire();
        Reservation actuel = elements.SingleOrDefault(e => e.Numero == element.Numero)
            ?? throw new InvalidOperationException("Numéro absent.");
        ProtectionDonnees.VerifierRemplacement(actuel);
        List<Reservation> candidats = elements.Where(e => e.Numero != element.Numero).ToList();
        candidats.Add(ProtectionDonnees.Copier(element));
        ProtectionDonnees.Verifier(candidats);
        Ecrire(candidats);
    }
    public IReadOnlyList<Reservation> Rechercher(CriteresRecherche criteres, IStrategieTri? strategie = null)
    {
        ArgumentNullException.ThrowIfNull(criteres);
        criteres.Valider();
        IEnumerable<Reservation> candidats = ObtenirToutes();
        candidats = candidats.Where(e => criteres.InclureHistorique || !e.EstAnnulee);
        if (!string.IsNullOrWhiteSpace(criteres.Texte))
        {
            candidats = candidats.Where(e => string.Equals(e.Ressource, criteres.Texte, StringComparison.Ordinal));
        }
        if (criteres.DebutPeriode.HasValue)
        {
            candidats = candidats.Where(e => e.Debut < criteres.FinPeriode!.Value && criteres.DebutPeriode.Value < e.Fin);
        }
        IEnumerable<Reservation> tries = strategie is null
            ? candidats.OrderBy(e => e.Numero)
            : strategie.Ordonner(candidats);
        return tries.Skip(criteres.Debut).Take(criteres.Taille).ToList().AsReadOnly();
    }
    private void Ecrire(IEnumerable<Reservation> elements)
    {
        string json = JsonSerializer.Serialize(elements.Select(e => new ReservationPersistance(e)).ToList(), m_options);
        string temporaire = m_chemin + "." + Guid.NewGuid().ToString("N") + ".tmp";
        try
        {
            File.WriteAllText(temporaire, json, Encoding.UTF8);
            File.Move(temporaire, m_chemin, true);
        }
        finally
        {
            if (File.Exists(temporaire))
            {
                File.Delete(temporaire);
            }
        }
    }
        private List<Reservation> Lire()
    {
        string json;
        try
        {
            json = File.ReadAllText(m_chemin, Encoding.UTF8);
        }
        catch (FileNotFoundException) when (Directory.Exists(Path.GetDirectoryName(m_chemin)))
        {
            return new List<Reservation>();
        }
        List<ReservationPersistance?>? donnees =
            JsonSerializer.Deserialize<List<ReservationPersistance?>>(json, m_options);
        if (donnees is null)
        {
            throw new JsonException("Le document doit contenir une liste.");
        }
        List<Reservation> elements = new List<Reservation>();
        HashSet<int> numeros = new HashSet<int>();
        foreach (ReservationPersistance? donnee in donnees)
        {
            if (donnee is null)
            {
                throw new JsonException("Une entrée ne peut pas être nulle.");
            }
            Reservation element;
            try
            {
                element = donnee.VersEntite();
            }
            catch (ArgumentException exception)
            {
                throw new JsonException("Les données violent un invariant du domaine.", exception);
            }
            catch (InvalidOperationException exception)
            {
                throw new JsonException("L'état enregistré est incohérent.", exception);
            }
            if (!numeros.Add(element.Numero))
            {
                throw new JsonException("Un numéro est répété dans le document.");
            }
            elements.Add(element);
        }
        try
        {
            ProtectionDonnees.Verifier(elements);
        }
        catch (InvalidOperationException exception)
        {
            throw new JsonException("Le document contient des états incompatibles.", exception);
        }
        return elements;
    }
}
