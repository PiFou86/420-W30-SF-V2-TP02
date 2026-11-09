using Reservations.Application;
using Reservations.Domaine;
namespace Reservations.Infrastructure;
public sealed class DepotReservationsMemoire : IDepotReservations
{
    private List<Reservation> m_elements;
    public DepotReservationsMemoire(IEnumerable<Reservation> elements)
    {
        ArgumentNullException.ThrowIfNull(elements);
        m_elements = elements.Select(ProtectionDonnees.Copier).ToList();
        ProtectionDonnees.Verifier(m_elements);
    }
    public IReadOnlyCollection<Reservation> ObtenirToutes()
    {
        return m_elements.Select(ProtectionDonnees.Copier).ToList().AsReadOnly();
    }
    public Reservation? Obtenir(int numero)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(numero);
        return ObtenirToutes().SingleOrDefault(e => e.Numero == numero);
    }
    public void Ajouter(Reservation element)
    {
        ArgumentNullException.ThrowIfNull(element);
        List<Reservation> candidats = m_elements.Select(ProtectionDonnees.Copier).ToList();
        candidats.Add(ProtectionDonnees.Copier(element));
        ProtectionDonnees.Verifier(candidats);
        m_elements = candidats;
    }
    public void Remplacer(Reservation element)
    {
        ArgumentNullException.ThrowIfNull(element);
        Reservation actuel = Obtenir(element.Numero) ?? throw new InvalidOperationException("Numéro absent.");
        ProtectionDonnees.VerifierRemplacement(actuel);
        List<Reservation> candidats = m_elements.Where(e => e.Numero != element.Numero).Select(ProtectionDonnees.Copier).ToList();
        candidats.Add(ProtectionDonnees.Copier(element));
        ProtectionDonnees.Verifier(candidats);
        m_elements = candidats;
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
}
