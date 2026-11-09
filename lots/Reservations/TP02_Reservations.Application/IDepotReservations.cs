using Reservations.Domaine;
namespace Reservations.Application;
public interface IDepotReservations
{
    void Ajouter(Reservation element);
    Reservation? Obtenir(int numero);
    IReadOnlyCollection<Reservation> ObtenirToutes();
    void Remplacer(Reservation element);
    IReadOnlyList<Reservation> Rechercher(CriteresRecherche criteres, IStrategieTri? strategie = null);
}
