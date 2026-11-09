using Reservations.Domaine;
namespace Reservations.Application;

public interface IStrategieTri
{
    IEnumerable<Reservation> Ordonner(IEnumerable<Reservation> elements);
}
