using Commandes.Domaine;
namespace Commandes.Application;
public interface IDepotCommandes
{
    void Ajouter(Commande element);
    Commande? Obtenir(int numero);
    IReadOnlyCollection<Commande> ObtenirToutes();
    void Remplacer(Commande element);
    IReadOnlyList<Commande> Rechercher(CriteresRecherche criteres, IStrategieTri? strategie = null);
}
