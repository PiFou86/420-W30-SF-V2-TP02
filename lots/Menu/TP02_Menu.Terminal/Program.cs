using Menu.Application;
using Menu.Infrastructure;

namespace TP02_Menu.Terminal;

internal static class Program
{
    public static void Main(string[] args)
    {
        string chemin = args.Length == 1
            ? args[0]
            : Path.Combine(AppContext.BaseDirectory, "donnees-exemples", "valide.json");
        IDepotMenu depot = new DepotMenuJson(chemin);
        ConsulterMenu consulter = new ConsulterMenu(depot);

        foreach (ElementMenuDto element in consulter.ObtenirToutes())
        {
            Console.Out.WriteLine($"Numéro {element.Numero}");
        }
    }
}
