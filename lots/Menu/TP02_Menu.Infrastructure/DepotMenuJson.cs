using System.Text;
using System.Text.Json;
using Menu.Application;
using Menu.Domaine;
namespace Menu.Infrastructure;
public sealed class DepotMenuJson : IDepotMenu
{
    private readonly string m_chemin;
    private readonly JsonSerializerOptions m_options = new JsonSerializerOptions { WriteIndented = true };
    public DepotMenuJson(string chemin)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(chemin);
        m_chemin = Path.GetFullPath(chemin);
    }
    public IReadOnlyCollection<ElementMenu> ObtenirToutes()
    {
        return Lire().AsReadOnly();
    }
    public ElementMenu? Obtenir(int numero)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(numero);
        return Lire().SingleOrDefault(e => e.Numero == numero);
    }
    public void Ajouter(ElementMenu element)
    {
        ArgumentNullException.ThrowIfNull(element);
        List<ElementMenu> elements = Lire();
        elements.Add(ProtectionDonnees.Copier(element));
        ProtectionDonnees.Verifier(elements);
        Ecrire(elements);
    }
    public void Remplacer(ElementMenu element)
    {
        ArgumentNullException.ThrowIfNull(element);
        List<ElementMenu> elements = Lire();
        ElementMenu actuel = elements.SingleOrDefault(e => e.Numero == element.Numero)
            ?? throw new InvalidOperationException("Numéro absent.");
        ProtectionDonnees.VerifierRemplacement(actuel);
        List<ElementMenu> candidats = elements.Where(e => e.Numero != element.Numero).ToList();
        candidats.Add(ProtectionDonnees.Copier(element));
        ProtectionDonnees.Verifier(candidats);
        Ecrire(candidats);
    }
    public IReadOnlyList<ElementMenu> Rechercher(CriteresRecherche criteres, IStrategieTri? strategie = null)
    {
        ArgumentNullException.ThrowIfNull(criteres);
        criteres.Valider();
        IEnumerable<ElementMenu> candidats = ObtenirToutes();
        candidats = candidats.Where(e => criteres.InclureHistorique || !e.EstRetire);
        string fragment = criteres.Texte.Trim();
        candidats = candidats.Where(e => e.Nom.Contains(fragment, StringComparison.OrdinalIgnoreCase));
        if (criteres.Montant.HasValue)
        {
            candidats = candidats.Where(e => e.Prix <= criteres.Montant.Value);
        }
        IEnumerable<ElementMenu> tries = strategie is null
            ? candidats.OrderBy(e => e.Numero)
            : strategie.Ordonner(candidats);
        return tries.Skip(criteres.Debut).Take(criteres.Taille).ToList().AsReadOnly();
    }
    private void Ecrire(IEnumerable<ElementMenu> elements)
    {
        string json = JsonSerializer.Serialize(elements.Select(e => new ElementMenuPersistance(e)).ToList(), m_options);
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
        private List<ElementMenu> Lire()
    {
        string json;
        try
        {
            json = File.ReadAllText(m_chemin, Encoding.UTF8);
        }
        catch (FileNotFoundException) when (Directory.Exists(Path.GetDirectoryName(m_chemin)))
        {
            return new List<ElementMenu>();
        }
        List<ElementMenuPersistance?>? donnees =
            JsonSerializer.Deserialize<List<ElementMenuPersistance?>>(json, m_options);
        if (donnees is null)
        {
            throw new JsonException("Le document doit contenir une liste.");
        }
        List<ElementMenu> elements = new List<ElementMenu>();
        HashSet<int> numeros = new HashSet<int>();
        foreach (ElementMenuPersistance? donnee in donnees)
        {
            if (donnee is null)
            {
                throw new JsonException("Une entrée ne peut pas être nulle.");
            }
            ElementMenu element;
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
