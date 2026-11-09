using Commandes.Domaine;
namespace Commandes.Application;
public sealed class LigneCommandeDto
{
    public string Libelle { get; }
    public decimal PrixUnitaire { get; }
    public int Quantite { get; }
    public LigneCommandeDto(LigneCommande ligne)
    {
        ArgumentNullException.ThrowIfNull(ligne);
        Libelle = ligne.Libelle;
        PrixUnitaire = ligne.PrixUnitaire;
        Quantite = ligne.Quantite;
    }
}
