using System.Text.Json;
using Commandes.Domaine;
namespace Commandes.Infrastructure;
public sealed class LigneCommandePersistance
{
    public string? Libelle { get; set; }
    public decimal? PrixUnitaire { get; set; }
    public int? Quantite { get; set; }
    public LigneCommandePersistance()
    {
    }
    public LigneCommandePersistance(LigneCommande ligne)
    {
        ArgumentNullException.ThrowIfNull(ligne);
        Libelle = ligne.Libelle;
        PrixUnitaire = ligne.PrixUnitaire;
        Quantite = ligne.Quantite;
    }
    public LigneCommande VersEntite()
    {
        if (Libelle is null)
        {
            throw new JsonException("Libellé manquant.");
        }
        if (PrixUnitaire is null)
        {
            throw new JsonException("Prix manquant.");
        }
        if (Quantite is null)
        {
            throw new JsonException("Quantité manquante.");
        }

        return new LigneCommande(
            Libelle,
            PrixUnitaire.Value,
            Quantite.Value);
    }
}
