using Reservations.Domaine;
namespace Reservations.Infrastructure;
internal static class ProtectionDonnees
{
    public static Reservation Copier(Reservation element)
    {
        ArgumentNullException.ThrowIfNull(element);
        return new Reservation(element.Numero, element.Ressource, element.NombrePersonnes, element.Debut, element.Fin, element.EstAnnulee);
    }
    public static void Verifier(IReadOnlyCollection<Reservation> elements)
    {
        if (elements.Any(e => e is null) || elements.Select(e => e.Numero).Distinct().Count() != elements.Count)
        {
            throw new InvalidOperationException("Entrée nulle ou numéro dupliqué.");
        }
        if (elements.Any(a => !a.EstAnnulee && elements.Any(b => b.Numero != a.Numero && !b.EstAnnulee && a.Chevauche(b))))
        {
            throw new InvalidOperationException("Des réservations actives se chevauchent.");
        }
    }
    public static void VerifierRemplacement(Reservation element)
    {
        if (element.EstAnnulee)
        {
            throw new InvalidOperationException("Cet élément est verrouillé.");
        }
    }
}
