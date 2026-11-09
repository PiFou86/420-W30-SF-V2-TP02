using Commandes.Application;
using Commandes.Infrastructure;

namespace TP02_Commandes.Terminal;

internal static class Program
{
    public static void Main(string[] args)
    {
        string chemin = args.Length == 1
            ? args[0]
            : Path.Combine(AppContext.BaseDirectory, "donnees-exemples", "valide.json");
        IDepotCommandes depot = new DepotCommandesJson(chemin);
        ConsulterCommandes consulter = new ConsulterCommandes(depot);

        foreach (CommandeDto element in consulter.ObtenirToutes())
        {
            Console.Out.WriteLine($"Numéro {element.Numero}");
        }
    }
}
