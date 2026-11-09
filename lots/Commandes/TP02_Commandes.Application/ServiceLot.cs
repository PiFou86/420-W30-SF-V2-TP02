using Commandes.Domaine;
namespace Commandes.Application;
public sealed class ServiceLot : IServiceLot
{
    private readonly IDepotCommandes m_depot;
    private readonly IDepotReferences m_references;
    public ServiceLot(IDepotCommandes depot, IDepotReferences references)
    {
        ArgumentNullException.ThrowIfNull(depot);
        ArgumentNullException.ThrowIfNull(references);
        m_depot = depot; m_references = references;
    }
    public SaisieDto? Obtenir(int numero)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(numero);
        Commande? entite = m_depot.Obtenir(numero);
        if (entite is null)
        {
            return null;
        }
        return new SaisieDto { Numero = entite.Numero, Lignes = entite.Lignes.Select(l => new LigneSaisieDto { Libelle = l.Libelle, PrixUnitaire = l.PrixUnitaire, Quantite = l.Quantite }).ToList() };
    }
    public IReadOnlyList<LigneConsultationDto> Rechercher(CriteresRecherche criteres, IStrategieTri? strategie = null)
    {
        ArgumentNullException.ThrowIfNull(criteres);
        criteres.Valider();
        IReadOnlyList<Commande> page = m_depot.Rechercher(criteres, strategie);
        m_references.ObtenirToutes(); // Validation technique fournie; jointure à réaliser.
        return page.Select(e => VersLigne(e, "À enrichir")).ToList().AsReadOnly();
    }
    private static LigneConsultationDto VersLigne(Commande e, string reference)
    {
        return new LigneConsultationDto(e.Numero, $"Commande {e.Numero}", e.EstConfirmee ? "Confirmée" : "Brouillon", reference, $"Total : {e.Total:F2} $");
    }
    private static Commande Construire(SaisieDto saisie)
    {
        ArgumentNullException.ThrowIfNull(saisie.Lignes);
        Commande entite = new Commande(saisie.Numero);
        foreach (LigneSaisieDto ligne in saisie.Lignes)
        {
            ArgumentNullException.ThrowIfNull(ligne);
            entite.AjouterLigne(new LigneCommande(ligne.Libelle, ligne.PrixUnitaire, ligne.Quantite));
        }
        return entite;
    }
    public Result<OperationDto> Creer(SaisieDto saisie)
    {
        ArgumentNullException.ThrowIfNull(saisie);
        Commande entite;
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
        Commande entite;
        try
        {
            entite = Construire(saisie);
        }
        catch (ArgumentException)
        {
            return Result<OperationDto>.Echec("Les données saisies ne respectent pas les règles du lot.");
        }
        Commande? actuel = m_depot.Obtenir(saisie.Numero);
        if (actuel is null || actuel.EstConfirmee)
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
    public Result<OperationDto> Confirmer(int numero)
    {
        if (numero <= 0)
        {
            return Result<OperationDto>.Echec("Numéro invalide.");
        }
        Commande? commande = m_depot.Obtenir(numero);
        if (commande is null || commande.EstConfirmee || commande.Lignes.Count == 0)
        {
            return Result<OperationDto>.Echec("Un brouillon non vide est requis.");
        }
        commande.Confirmer();
        m_depot.Remplacer(commande);
        return Result<OperationDto>.Succes(new OperationDto(numero, "Confirmation", $"Total : {commande.Total:F2} $"));
    }
}
