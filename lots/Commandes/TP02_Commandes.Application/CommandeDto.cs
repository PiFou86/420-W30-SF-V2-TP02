using Commandes.Domaine;
namespace Commandes.Application;
public sealed class CommandeDto
{
    public int Numero { get; }
    public bool EstConfirmee { get; }
    public decimal Total { get; }
    public IReadOnlyList<LigneCommandeDto> Lignes { get; }
    public CommandeDto(Commande entite)
    {
        ArgumentNullException.ThrowIfNull(entite);
        Numero = entite.Numero;
        EstConfirmee = entite.EstConfirmee;
        Total = entite.Total;
        Lignes = entite.Lignes.Select(ligne => new LigneCommandeDto(ligne)).ToList().AsReadOnly();
    }
}
