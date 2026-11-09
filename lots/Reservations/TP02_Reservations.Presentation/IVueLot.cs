using Reservations.Application;
namespace Reservations.Presentation;
public interface IVueLot
{
    void Afficher(IReadOnlyList<LigneConsultationDto> lignes);
    void AfficherMessage(string message);
}
