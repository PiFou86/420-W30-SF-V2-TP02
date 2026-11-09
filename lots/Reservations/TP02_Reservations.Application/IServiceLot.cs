namespace Reservations.Application;

public interface IServiceLot
{
    SaisieDto? Obtenir(int numero);
    IReadOnlyList<LigneConsultationDto> Rechercher(CriteresRecherche criteres, IStrategieTri? strategie = null);
    Result<OperationDto> Creer(SaisieDto saisie);
    Result<OperationDto> Modifier(SaisieDto saisie);
    Result<OperationDto> ExecuterAction(int numero, int? nouveauNumero = null);
}
