namespace Reservations.Application;

public sealed class CriteresRecherche
{
    public string Texte { get; set; } = "";
    public decimal? Montant { get; set; }
    public bool InclureHistorique { get; set; }
    public bool? Confirmee { get; set; }
    public DateTime? DebutPeriode { get; set; }
    public DateTime? FinPeriode { get; set; }
    public int Debut { get; set; }
    public int Taille { get; set; } = 5;
    public CriteresRecherche Copier()
    {
        return new CriteresRecherche { Texte = Texte, Montant = Montant,
            InclureHistorique = InclureHistorique, Confirmee = Confirmee,
            DebutPeriode = DebutPeriode, FinPeriode = FinPeriode, Debut = Debut, Taille = Taille };
    }
    public void Valider()
    {
        ArgumentNullException.ThrowIfNull(Texte);
        if (Debut < 0 || Taille < 1 || Taille > 50)
        {
            throw new ArgumentOutOfRangeException(nameof(Debut), "Début positif ou nul; taille de 1 à 50.");
        }
        if (Montant < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(Montant));
        }
        if (DebutPeriode.HasValue != FinPeriode.HasValue || DebutPeriode >= FinPeriode)
        {
            throw new ArgumentException("La période doit avoir deux bornes et un début strictement avant la fin.");
        }
    }
}
