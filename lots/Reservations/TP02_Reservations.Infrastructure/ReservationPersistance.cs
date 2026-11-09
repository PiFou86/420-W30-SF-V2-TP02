using System.Text.Json;
using Reservations.Domaine;
namespace Reservations.Infrastructure;
public sealed class ReservationPersistance
{
    public bool EstAnnulee { get; set; }
    public int? Numero { get; set; }
    public string? Ressource { get; set; }
    public int? NombrePersonnes { get; set; }
    public DateTime? Debut { get; set; }
    public DateTime? Fin { get; set; }
    public ReservationPersistance(Reservation element)
    {
        ArgumentNullException.ThrowIfNull(element);
        Numero = element.Numero; Ressource = element.Ressource; NombrePersonnes = element.NombrePersonnes; Debut = element.Debut; Fin = element.Fin; EstAnnulee = element.EstAnnulee;
    }
    public ReservationPersistance()
    {
    }
    public Reservation VersEntite()
    {
        if (Numero is null)
        {
            throw new JsonException("Numéro manquant.");
        }
        if (Ressource is null)
        {
            throw new JsonException("Ressource manquante.");
        }
        if (NombrePersonnes is null)
        {
            throw new JsonException("Capacité manquante.");
        }
        if (Debut is null)
        {
            throw new JsonException("Début manquant.");
        }
        if (Fin is null)
        {
            throw new JsonException("Fin manquante.");
        }

        return new Reservation(
            Numero.Value,
            Ressource,
            NombrePersonnes.Value,
            Debut.Value,
            Fin.Value, EstAnnulee);
    }
}
