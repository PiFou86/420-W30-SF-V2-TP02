namespace Reservations.Domaine;
public sealed class Reservation
{
    public bool EstAnnulee { get; private set; }
    public void Annuler()
    {
        if (EstAnnulee)
        {
            throw new InvalidOperationException("L’opération a déjà été réalisée.");
        }
        EstAnnulee = true;
    }
    public int Numero { get; }
    public string Ressource { get; }
    public int NombrePersonnes { get; }
    public DateTime Debut { get; }
    public DateTime Fin { get; }
    public Reservation(int numero, string ressource, int nombrePersonnes, DateTime debut, DateTime fin, bool estAnnulee = false)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(numero);
        ArgumentException.ThrowIfNullOrWhiteSpace(ressource);
        if (nombrePersonnes < 1 || nombrePersonnes > 12)
        {
            throw new ArgumentOutOfRangeException(nameof(nombrePersonnes));
        }
        if (debut >= fin)
        {
            throw new ArgumentException("Le début doit précéder strictement la fin.", nameof(fin));
        }
        Numero = numero;
        EstAnnulee = estAnnulee;
        Ressource = ressource;
        NombrePersonnes = nombrePersonnes;
        Debut = debut;
        Fin = fin;
    }
    public bool Chevauche(Reservation autre)
    {
        ArgumentNullException.ThrowIfNull(autre);
        return string.Equals(Ressource, autre.Ressource, StringComparison.Ordinal)
            && Debut < autre.Fin && autre.Debut < Fin;
    }
}
