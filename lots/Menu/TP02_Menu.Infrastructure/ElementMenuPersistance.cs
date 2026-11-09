using System.Text.Json;
using Menu.Domaine;
namespace Menu.Infrastructure;
public sealed class ElementMenuPersistance
{
    public bool EstRetire { get; set; }
    public int? Numero { get; set; }
    public string? Nom { get; set; }
    public decimal? Prix { get; set; }
    public ElementMenuPersistance(ElementMenu element)
    {
        ArgumentNullException.ThrowIfNull(element);
        Numero = element.Numero; Nom = element.Nom; Prix = element.Prix; EstRetire = element.EstRetire;
    }
    public ElementMenuPersistance()
    {
    }
    public ElementMenu VersEntite()
    {
        if (Numero is null)
        {
            throw new JsonException("Numéro manquant.");
        }
        if (Nom is null)
        {
            throw new JsonException("Nom manquant.");
        }
        if (Prix is null)
        {
            throw new JsonException("Prix manquant.");
        }

        return new ElementMenu(
            Numero.Value,
            Nom,
            Prix.Value, EstRetire);
    }
}
