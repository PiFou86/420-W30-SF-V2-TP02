namespace Commandes.Application;
public sealed class SaisieDto
{
    public int Numero { get; set; }
    public List<LigneSaisieDto> Lignes { get; set; } = new List<LigneSaisieDto>();
}
public sealed class LigneSaisieDto
{
    public string Libelle { get; set; } = "";
    public decimal PrixUnitaire { get; set; }
    public int Quantite { get; set; } = 1;
}
