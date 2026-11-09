using Commandes.Domaine;
namespace Commandes.Application;

public interface IStrategieTri
{
    IEnumerable<Commande> Ordonner(IEnumerable<Commande> elements);
}
