using Menu.Domaine;
namespace Menu.Application;

public interface IStrategieTri
{
    IEnumerable<ElementMenu> Ordonner(IEnumerable<ElementMenu> elements);
}
