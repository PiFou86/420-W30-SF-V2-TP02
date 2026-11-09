namespace Reservations.Application;
public sealed class ConsulterReservations
{
    private readonly IDepotReservations m_depot;
    public ConsulterReservations(IDepotReservations depot)
    {
        ArgumentNullException.ThrowIfNull(depot);
        m_depot = depot;
    }
    public IReadOnlyList<ReservationDto> ObtenirToutes()
    {
        return m_depot.ObtenirToutes().Select(element => new ReservationDto(element)).ToList().AsReadOnly();
    }
}
