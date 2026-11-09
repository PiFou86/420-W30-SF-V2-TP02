using Reservations.Domaine;
namespace Reservations.Application;
public sealed class ReservationDto
{
    public int Numero { get; }
    public string Ressource { get; }
    public int NombrePersonnes { get; }
    public DateTime Debut { get; }
    public DateTime Fin { get; }
    public ReservationDto(Reservation entite)
    {
        ArgumentNullException.ThrowIfNull(entite);
        Numero = entite.Numero;
        Ressource = entite.Ressource;
        NombrePersonnes = entite.NombrePersonnes;
        Debut = entite.Debut;
        Fin = entite.Fin;
    }
}
