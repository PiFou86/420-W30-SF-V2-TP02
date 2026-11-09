using Commandes.Application;
namespace Commandes.Presentation;
public interface IVueLot
{
    void Afficher(IReadOnlyList<LigneConsultationDto> lignes);
    void AfficherMessage(string message);
}
