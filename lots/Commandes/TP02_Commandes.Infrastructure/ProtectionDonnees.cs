using Commandes.Domaine;
namespace Commandes.Infrastructure;
internal static class ProtectionDonnees
{
    public static Commande Copier(Commande element)
    {
        ArgumentNullException.ThrowIfNull(element);
        return element.Copier();
    }
    public static void Verifier(IReadOnlyCollection<Commande> elements)
    {
        if (elements.Any(e => e is null) || elements.Select(e => e.Numero).Distinct().Count() != elements.Count)
        {
            throw new InvalidOperationException("Entrée nulle ou numéro dupliqué.");
        }

    }
    public static void VerifierRemplacement(Commande element)
    {
        if (element.EstConfirmee)
        {
            throw new InvalidOperationException("Cet élément est verrouillé.");
        }
    }
}
