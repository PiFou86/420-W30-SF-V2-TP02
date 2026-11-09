using Menu.Domaine;
namespace Menu.Infrastructure;
internal static class ProtectionDonnees
{
    public static ElementMenu Copier(ElementMenu element)
    {
        ArgumentNullException.ThrowIfNull(element);
        return new ElementMenu(element.Numero, element.Nom, element.Prix, element.EstRetire);
    }
    public static void Verifier(IReadOnlyCollection<ElementMenu> elements)
    {
        if (elements.Any(e => e is null) || elements.Select(e => e.Numero).Distinct().Count() != elements.Count)
        {
            throw new InvalidOperationException("Entrée nulle ou numéro dupliqué.");
        }

    }
    public static void VerifierRemplacement(ElementMenu element)
    {
        if (element.EstRetire)
        {
            throw new InvalidOperationException("Cet élément est verrouillé.");
        }
    }
}
