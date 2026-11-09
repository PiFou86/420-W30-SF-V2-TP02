using Menu.Domaine;
namespace Menu.Application;
public sealed class ElementMenuDto
{
    public int Numero { get; }
    public string Nom { get; }
    public decimal Prix { get; }
    public ElementMenuDto(ElementMenu entite)
    {
        ArgumentNullException.ThrowIfNull(entite);
        Numero = entite.Numero;
        Nom = entite.Nom;
        Prix = entite.Prix;
    }
}
