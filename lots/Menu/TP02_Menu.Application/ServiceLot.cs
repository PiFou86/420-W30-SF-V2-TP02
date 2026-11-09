using Menu.Domaine;
namespace Menu.Application;
public sealed class ServiceLot : IServiceLot
{
    private readonly IDepotMenu m_depot;
    private readonly IDepotReferences m_references;
    public ServiceLot(IDepotMenu depot, IDepotReferences references)
    {
        ArgumentNullException.ThrowIfNull(depot);
        ArgumentNullException.ThrowIfNull(references);
        m_depot = depot; m_references = references;
    }
    public SaisieDto? Obtenir(int numero)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(numero);
        ElementMenu? entite = m_depot.Obtenir(numero);
        if (entite is null)
        {
            return null;
        }
        return new SaisieDto { Numero = entite.Numero, Nom = entite.Nom, Prix = entite.Prix };
    }
    public IReadOnlyList<LigneConsultationDto> Rechercher(CriteresRecherche criteres, IStrategieTri? strategie = null)
    {
        ArgumentNullException.ThrowIfNull(criteres);
        criteres.Valider();
        IReadOnlyList<ElementMenu> page = m_depot.Rechercher(criteres, strategie);
        m_references.ObtenirToutes(); // Validation technique fournie; jointure à réaliser.
        return page.Select(e => VersLigne(e, "À enrichir")).ToList().AsReadOnly();
    }
    private static LigneConsultationDto VersLigne(ElementMenu e, string reference)
    {
        return new LigneConsultationDto(e.Numero, e.Nom, e.EstRetire ? "Retiré" : "Actif", reference, $"Prix : {e.Prix:F2} $");
    }
    private static ElementMenu Construire(SaisieDto saisie)
    {
        return new ElementMenu(saisie.Numero, saisie.Nom, saisie.Prix);
    }
    public Result<OperationDto> Creer(SaisieDto saisie)
    {
        ArgumentNullException.ThrowIfNull(saisie);
        ElementMenu entite;
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

        m_depot.Ajouter(entite);
        return Result<OperationDto>.Succes(new OperationDto(entite.Numero, "Création", VersLigne(entite, "").Detail));
    }
    public Result<OperationDto> Modifier(SaisieDto saisie)
    {
        ArgumentNullException.ThrowIfNull(saisie);
        ElementMenu entite;
        try
        {
            entite = Construire(saisie);
        }
        catch (ArgumentException)
        {
            return Result<OperationDto>.Echec("Les données saisies ne respectent pas les règles du lot.");
        }
        ElementMenu? actuel = m_depot.Obtenir(saisie.Numero);
        if (actuel is null || actuel.EstRetire)
        {
            return Result<OperationDto>.Echec("Cet élément est absent ou ne peut plus être modifié.");
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
