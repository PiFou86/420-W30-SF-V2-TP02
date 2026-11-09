namespace Reservations.Application;
public sealed class SaisieDto
{
    public int Numero { get; set; }
    public string Ressource { get; set; } = "";
    public int NombrePersonnes { get; set; } = 1;
    public DateTime Debut { get; set; } = new DateTime(2026, 11, 23, 10, 0, 0);
    public DateTime Fin { get; set; } = new DateTime(2026, 11, 23, 11, 0, 0);
}
