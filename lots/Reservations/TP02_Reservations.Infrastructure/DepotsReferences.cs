using System.Text.Json;
using Reservations.Application;
namespace Reservations.Infrastructure;
public sealed class DepotReferencesMemoire : IDepotReferences
{
    private readonly IReadOnlyList<ReferenceDto> m_references;
    public DepotReferencesMemoire(IEnumerable<ReferenceDto> references)
    {
        ArgumentNullException.ThrowIfNull(references);
        ReferenceDto[] copie = references.ToArray();
        if (copie.Any(r => r is null) || copie.Select(r => r.Cle).Distinct(StringComparer.Ordinal).Count() != copie.Length)
        {
            throw new ArgumentException("Référence nulle ou clé dupliquée.", nameof(references));
        }
        m_references = Array.AsReadOnly(copie);
    }
    public IReadOnlyList<ReferenceDto> ObtenirToutes() => m_references;
}
public sealed class DepotReferencesJson : IDepotReferences
{
    private readonly string m_chemin;
    public DepotReferencesJson(string chemin)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(chemin);
        m_chemin = Path.GetFullPath(chemin);
    }
    public IReadOnlyList<ReferenceDto> ObtenirToutes()
    {
        List<ReferencePersistance?> donnees = JsonSerializer.Deserialize<List<ReferencePersistance?>>(File.ReadAllText(m_chemin))
            ?? throw new JsonException("Une liste de références est attendue.");
        List<ReferenceDto> references = new List<ReferenceDto>();
        HashSet<string> cles = new HashSet<string>(StringComparer.Ordinal);
        foreach (ReferencePersistance? donnee in donnees)
        {
            if (donnee is null)
            {
                throw new JsonException("Référence nulle.");
            }
            string? cle = donnee.Ressource;
            if (string.IsNullOrWhiteSpace(cle) || string.IsNullOrWhiteSpace(donnee.Libelle) || !cles.Add(cle))
            {
                throw new JsonException("Clé ou libellé invalide, ou clé dupliquée.");
            }
            references.Add(new ReferenceDto(cle, donnee.Libelle));
        }
        return references.AsReadOnly();
    }
}
public sealed class ReferencePersistance
{
    public int? Numero { get; set; }
    public string? Ressource { get; set; }
    public string? Libelle { get; set; }
}
