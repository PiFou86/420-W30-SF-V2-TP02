namespace Commandes.Application;

public sealed class CriteresRecherche
{
    public decimal? Montant { get; set; }
    public bool? Confirmee { get; set; }
    // Pagination :
    public int Debut { get; set; }
    public int Taille { get; set; } = 5;
    public CriteresRecherche Copier()
    {
        return new CriteresRecherche { Montant = Montant,
            Confirmee = Confirmee,
            Debut = Debut, Taille = Taille };
    }
    public void Valider()
    {
        if (Debut < 0 || Taille < 1 || Taille > 50)
        {
            throw new ArgumentOutOfRangeException(nameof(Debut), "Début positif ou nul; taille de 1 à 50.");
        }
        if (Montant < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(Montant));
        }
    }
}
