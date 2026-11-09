namespace Commandes.Application;
public sealed class ConsulterCommandes
{
    private readonly IDepotCommandes m_depot;
    public ConsulterCommandes(IDepotCommandes depot)
    {
        ArgumentNullException.ThrowIfNull(depot);
        m_depot = depot;
    }
    public IReadOnlyList<CommandeDto> ObtenirToutes()
    {
        return m_depot.ObtenirToutes().Select(element => new CommandeDto(element)).ToList().AsReadOnly();
    }
}
