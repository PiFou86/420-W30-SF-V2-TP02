using Commandes.Application;
using Commandes.Domaine;
namespace Commandes.Infrastructure;
public sealed class DepotCommandesMemoire : IDepotCommandes
{
    private List<Commande> m_elements;
    public DepotCommandesMemoire(IEnumerable<Commande> elements)
    {
        ArgumentNullException.ThrowIfNull(elements);
        m_elements = elements.Select(ProtectionDonnees.Copier).ToList();
        ProtectionDonnees.Verifier(m_elements);
    }
    public IReadOnlyCollection<Commande> ObtenirToutes()
    {
        return m_elements.Select(ProtectionDonnees.Copier).ToList().AsReadOnly();
    }
    public Commande? Obtenir(int numero)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(numero);
        return ObtenirToutes().SingleOrDefault(e => e.Numero == numero);
    }
    public void Ajouter(Commande element)
    {
        ArgumentNullException.ThrowIfNull(element);
        List<Commande> candidats = m_elements.Select(ProtectionDonnees.Copier).ToList();
        candidats.Add(ProtectionDonnees.Copier(element));
        ProtectionDonnees.Verifier(candidats);
        m_elements = candidats;
    }
    public void Remplacer(Commande element)
    {
        ArgumentNullException.ThrowIfNull(element);
        Commande actuel = Obtenir(element.Numero) ?? throw new InvalidOperationException("Numéro absent.");
        ProtectionDonnees.VerifierRemplacement(actuel);
        List<Commande> candidats = m_elements.Where(e => e.Numero != element.Numero).Select(ProtectionDonnees.Copier).ToList();
        candidats.Add(ProtectionDonnees.Copier(element));
        ProtectionDonnees.Verifier(candidats);
        m_elements = candidats;
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
}
