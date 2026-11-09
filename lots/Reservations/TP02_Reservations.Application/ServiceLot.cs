using Reservations.Domaine;
namespace Reservations.Application;
public sealed class ServiceLot : IServiceLot
{
    private readonly IDepotReservations m_depot;
    private readonly IDepotReferences m_references;
    public ServiceLot(IDepotReservations depot, IDepotReferences references)
    {
        ArgumentNullException.ThrowIfNull(depot);
        ArgumentNullException.ThrowIfNull(references);
        m_depot = depot; m_references = references;
    }
    public SaisieDto? Obtenir(int numero)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(numero);
        Reservation? entite = m_depot.Obtenir(numero);
        if (entite is null)
        {
            return null;
        }
        return new SaisieDto { Numero = entite.Numero, Ressource = entite.Ressource, NombrePersonnes = entite.NombrePersonnes, Debut = entite.Debut, Fin = entite.Fin };
    }
    public IReadOnlyList<LigneConsultationDto> Rechercher(CriteresRecherche criteres, IStrategieTri? strategie = null)
    {
        ArgumentNullException.ThrowIfNull(criteres);
        criteres.Valider();
        IReadOnlyList<Reservation> page = m_depot.Rechercher(criteres, strategie);
        m_references.ObtenirToutes(); // Validation technique fournie; jointure à réaliser.
        return page.Select(e => VersLigne(e, "À enrichir")).ToList().AsReadOnly();
    }
    private static LigneConsultationDto VersLigne(Reservation e, string reference)
    {
        return new LigneConsultationDto(e.Numero, e.Ressource, e.EstAnnulee ? "Annulée" : "Active", reference, $"{e.Debut:yyyy-MM-dd HH:mm} – {e.Fin:HH:mm}, {e.NombrePersonnes} personnes");
    }
    private static Reservation Construire(SaisieDto saisie)
    {
        return new Reservation(saisie.Numero, saisie.Ressource, saisie.NombrePersonnes, saisie.Debut, saisie.Fin);
    }
    public Result<OperationDto> Creer(SaisieDto saisie)
    {
        ArgumentNullException.ThrowIfNull(saisie);
        Reservation entite;
        try
        {
            entite = Construire(saisie);
        }
        catch (ArgumentException)
        {
            return Result<OperationDto>.Echec("Les données saisies ne respectent pas les règles du lot.");
        }
        if (m_depot.Obtenir(entite.Numero) is not null)
        {
            return Result<OperationDto>.Echec("Ce numéro est déjà réservé.");
        }
        if (m_depot.ObtenirToutes().Any(r => !r.EstAnnulee && r.Numero != entite.Numero && r.Chevauche(entite)))
        {
            return Result<OperationDto>.Echec("La ressource est déjà réservée sur cette période.");
        }
        m_depot.Ajouter(entite);
        return Result<OperationDto>.Succes(new OperationDto(entite.Numero, "Création", VersLigne(entite, "").Detail));
    }
    public Result<OperationDto> Modifier(SaisieDto saisie)
    {
        ArgumentNullException.ThrowIfNull(saisie);
        Reservation entite;
        try
        {
            entite = Construire(saisie);
        }
        catch (ArgumentException)
        {
            return Result<OperationDto>.Echec("Les données saisies ne respectent pas les règles du lot.");
        }
        Reservation? actuel = m_depot.Obtenir(saisie.Numero);
        if (actuel is null || actuel.EstAnnulee)
        {
            return Result<OperationDto>.Echec("Cet élément est absent ou ne peut plus être modifié.");
        }
        if (m_depot.ObtenirToutes().Any(r => !r.EstAnnulee && r.Numero != entite.Numero && r.Chevauche(entite)))
        {
            return Result<OperationDto>.Echec("La ressource est déjà réservée sur cette période.");
        }
        m_depot.Remplacer(entite);
        return Result<OperationDto>.Succes(new OperationDto(entite.Numero, "Modification", VersLigne(entite, "").Detail));
    }
    public Result<OperationDto> ExecuterAction(int numero, int? nouveauNumero = null)
    {
        if (numero <= 0)
        {
            return Result<OperationDto>.Echec("Numéro invalide.");
        }
        return Result<OperationDto>.Echec("Action du lot à réaliser.");
    }

}
