namespace Reservations.Application;

public sealed class ReferenceDto
{
    public ReferenceDto(string cle, string libelle)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(cle);
        ArgumentException.ThrowIfNullOrWhiteSpace(libelle);
        Cle = cle; Libelle = libelle;
    }
    public string Cle { get; }
    public string Libelle { get; }
}
public interface IDepotReferences
{
    IReadOnlyList<ReferenceDto> ObtenirToutes();
}
