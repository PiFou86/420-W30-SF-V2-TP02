namespace Commandes.Domaine;
public sealed class LigneCommande
{
    public string Libelle { get; }
    public decimal PrixUnitaire { get; }
    public int Quantite { get; }
    public decimal Total
    {
        get
        {
            return PrixUnitaire * Quantite;
        }
    }
    public LigneCommande(string libelle, decimal prixUnitaire, int quantite)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(libelle);
        ArgumentOutOfRangeException.ThrowIfNegative(prixUnitaire);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantite);
        Libelle = libelle;
        PrixUnitaire = prixUnitaire;
        Quantite = quantite;
    }
}
