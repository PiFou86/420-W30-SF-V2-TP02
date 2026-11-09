using Reservations.Application;
using Reservations.Infrastructure;

namespace TP02_Reservations.Terminal;

internal static class Program
{
    public static void Main(string[] args)
    {
        string chemin = args.Length == 1
            ? args[0]
            : Path.Combine(AppContext.BaseDirectory, "donnees-exemples", "valide.json");
        IDepotReservations depot = new DepotReservationsJson(chemin);
        ConsulterReservations consulter = new ConsulterReservations(depot);

        foreach (ReservationDto element in consulter.ObtenirToutes())
        {
            Console.Out.WriteLine($"Numéro {element.Numero}");
        }
    }
}
