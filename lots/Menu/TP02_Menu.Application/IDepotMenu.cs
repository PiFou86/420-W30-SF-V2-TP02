using Menu.Domaine;
namespace Menu.Application;
public interface IDepotMenu
{
    void Ajouter(ElementMenu element);
    ElementMenu? Obtenir(int numero);
    IReadOnlyCollection<ElementMenu> ObtenirToutes();
    void Remplacer(ElementMenu element);
    IReadOnlyList<ElementMenu> Rechercher(CriteresRecherche criteres, IStrategieTri? strategie = null);
}
