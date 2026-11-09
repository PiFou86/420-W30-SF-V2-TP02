namespace Commandes.Application;

public sealed class LigneConsultationDto
{
    public LigneConsultationDto(int numero, string description, string etat, string reference, string detail)
    {
        Numero = numero; Description = description; Etat = etat; Reference = reference; Detail = detail;
    }
    public int Numero { get; }
    public string Description { get; }
    public string Etat { get; }
    public string Reference { get; }
    public string Detail { get; }
}
public sealed class OperationDto
{
    public OperationDto(int numero, string type, string detail)
    {
        Numero = numero; Type = type; Detail = detail;
    }
    public int Numero { get; }
    public string Type { get; }
    public string Detail { get; }
}
