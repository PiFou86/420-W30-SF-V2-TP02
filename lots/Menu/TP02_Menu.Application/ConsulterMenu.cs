namespace Menu.Application;
public sealed class ConsulterMenu
{
    private readonly IDepotMenu m_depot;
    public ConsulterMenu(IDepotMenu depot)
    {
        ArgumentNullException.ThrowIfNull(depot);
        m_depot = depot;
    }
    public IReadOnlyList<ElementMenuDto> ObtenirToutes()
    {
        return m_depot.ObtenirToutes().Select(element => new ElementMenuDto(element)).ToList().AsReadOnly();
    }

}
