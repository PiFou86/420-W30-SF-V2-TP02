using Menu.Application;
using Menu.Domaine;
namespace Menu.Infrastructure;
public sealed class DepotMenuMemoire : IDepotMenu
{
    private List<ElementMenu> m_elements;
    public DepotMenuMemoire(IEnumerable<ElementMenu> elements)
    {
        ArgumentNullException.ThrowIfNull(elements);
        m_elements = elements.Select(ProtectionDonnees.Copier).ToList();
        ProtectionDonnees.Verifier(m_elements);
    }
    public IReadOnlyCollection<ElementMenu> ObtenirToutes()
    {
        return m_elements.Select(ProtectionDonnees.Copier).ToList().AsReadOnly();
    }
    public ElementMenu? Obtenir(int numero)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(numero);
        return ObtenirToutes().SingleOrDefault(e => e.Numero == numero);
    }
    public void Ajouter(ElementMenu element)
    {
        ArgumentNullException.ThrowIfNull(element);
        List<ElementMenu> candidats = m_elements.Select(ProtectionDonnees.Copier).ToList();
        candidats.Add(ProtectionDonnees.Copier(element));
        ProtectionDonnees.Verifier(candidats);
        m_elements = candidats;
    }
    public void Remplacer(ElementMenu element)
    {
        ArgumentNullException.ThrowIfNull(element);
        ElementMenu actuel = Obtenir(element.Numero) ?? throw new InvalidOperationException("Numéro absent.");
        ProtectionDonnees.VerifierRemplacement(actuel);
        List<ElementMenu> candidats = m_elements.Where(e => e.Numero != element.Numero).Select(ProtectionDonnees.Copier).ToList();
        candidats.Add(ProtectionDonnees.Copier(element));
        ProtectionDonnees.Verifier(candidats);
        m_elements = candidats;
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
}
