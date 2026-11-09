namespace Commandes.Domaine;
public sealed class Commande
{
    private readonly List<LigneCommande> m_lignes = new List<LigneCommande>();
    public int Numero { get; }
    public bool EstConfirmee { get; private set; }
    public IReadOnlyList<LigneCommande> Lignes
    {
        get
        {
            return m_lignes.ToList().AsReadOnly();
        }
    }
    public decimal Total
    {
        get
        {
            decimal total = 0m;
            foreach (LigneCommande ligne in m_lignes)
            {
                total += ligne.Total;
            }
            return total;
        }
    }
    public Commande(int numero)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(numero);
        Numero = numero;
    }
    public void AjouterLigne(LigneCommande ligne)
    {
        ArgumentNullException.ThrowIfNull(ligne);
        if (EstConfirmee)
        {
            throw new InvalidOperationException("Une commande confirmée ne peut plus être modifiée.");
        }
        m_lignes.Add(ligne);
    }
    public void Confirmer()
    {
        if (EstConfirmee || m_lignes.Count == 0)
        {
            throw new InvalidOperationException("La commande doit être en préparation et non vide.");
        }
        EstConfirmee = true;
    }
    public Commande Copier()
    {
        Commande copie = new Commande(Numero);
        foreach (LigneCommande ligne in m_lignes)
        {
            // LigneCommande est immuable : partager sa référence est sans risque.
            copie.AjouterLigne(ligne);
        }
        if (EstConfirmee)
        {
            copie.Confirmer();
        }
        return copie;
    }
}
