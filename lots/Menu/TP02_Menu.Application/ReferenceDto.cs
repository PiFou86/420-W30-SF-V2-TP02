namespace Menu.Application;

public sealed class ReferenceDto
{
    public ReferenceDto(string cle, string libelle)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(cle);
        ArgumentException.ThrowIfNullOrWhiteSpace(libelle);
        if (!int.TryParse(cle, out int numero) || numero <= 0)
        {
            throw new ArgumentException("La clé doit être un numéro positif.", nameof(cle));
        }
        Cle = numero.ToString(System.Globalization.CultureInfo.InvariantCulture);
        Libelle = libelle;
    }
    public string Cle { get; }
    public string Libelle { get; }
}
public interface IDepotReferences
{
    IReadOnlyList<ReferenceDto> ObtenirToutes();
}
