using System.Text;
using System.Text.Json;
using Commandes.Application;
using Commandes.Domaine;
namespace Commandes.Infrastructure;
public sealed class DepotCommandesJson : IDepotCommandes
{
    private readonly string m_chemin;
    private readonly JsonSerializerOptions m_options = new JsonSerializerOptions { WriteIndented = true };
    public DepotCommandesJson(string chemin)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(chemin);
        m_chemin = Path.GetFullPath(chemin);
    }
    public IReadOnlyCollection<Commande> ObtenirToutes()
    {
        return Lire().AsReadOnly();
    }
    public Commande? Obtenir(int numero)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(numero);
        return Lire().SingleOrDefault(e => e.Numero == numero);
    }
    public void Ajouter(Commande element)
    {
        ArgumentNullException.ThrowIfNull(element);
        List<Commande> elements = Lire();
        elements.Add(ProtectionDonnees.Copier(element));
        ProtectionDonnees.Verifier(elements);
        Ecrire(elements);
    }
    public void Remplacer(Commande element)
    {
        ArgumentNullException.ThrowIfNull(element);
        List<Commande> elements = Lire();
        Commande actuel = elements.SingleOrDefault(e => e.Numero == element.Numero)
            ?? throw new InvalidOperationException("Numéro absent.");
        ProtectionDonnees.VerifierRemplacement(actuel);
        List<Commande> candidats = elements.Where(e => e.Numero != element.Numero).ToList();
        candidats.Add(ProtectionDonnees.Copier(element));
        ProtectionDonnees.Verifier(candidats);
        Ecrire(candidats);
    }
    public IReadOnlyList<Commande> Rechercher(CriteresRecherche criteres, IStrategieTri? strategie = null)
    {
        ArgumentNullException.ThrowIfNull(criteres);
        criteres.Valider();
        IEnumerable<Commande> candidats = ObtenirToutes();
        if (criteres.Confirmee.HasValue)
        {
            candidats = candidats.Where(e => e.EstConfirmee == criteres.Confirmee.Value);
        }
        if (criteres.Montant.HasValue)
        {
            candidats = candidats.Where(e => e.Total >= criteres.Montant.Value);
        }
        IEnumerable<Commande> tries = strategie is null
            ? candidats.OrderBy(e => e.Numero)
            : strategie.Ordonner(candidats);
        return tries.Skip(criteres.Debut).Take(criteres.Taille).ToList().AsReadOnly();
    }
    private void Ecrire(IEnumerable<Commande> elements)
    {
        string json = JsonSerializer.Serialize(elements.Select(e => new CommandePersistance(e)).ToList(), m_options);
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
        private List<Commande> Lire()
    {
        string json;
        try
        {
            json = File.ReadAllText(m_chemin, Encoding.UTF8);
        }
        catch (FileNotFoundException) when (Directory.Exists(Path.GetDirectoryName(m_chemin)))
        {
            return new List<Commande>();
        }
        List<CommandePersistance?>? donnees =
            JsonSerializer.Deserialize<List<CommandePersistance?>>(json, m_options);
        if (donnees is null)
        {
            throw new JsonException("Le document doit contenir une liste.");
        }
        List<Commande> elements = new List<Commande>();
        HashSet<int> numeros = new HashSet<int>();
        foreach (CommandePersistance? donnee in donnees)
        {
            if (donnee is null)
            {
                throw new JsonException("Une entrée ne peut pas être nulle.");
            }
            Commande element;
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
