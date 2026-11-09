using Reservations.Domaine;
using Xunit;
namespace TP02_Reservations.Tests;
public sealed class DomaineTests
{
    private static readonly DateTime s_debut = new DateTime(2026, 10, 21, 10, 0, 0);
    [Theory]
    [InlineData(1)]
    [InlineData(12)]
    public void ctor_BornesCapacite_Acceptees(int personnes)
    {
        Assert.Equal(personnes, new Reservation(301, "Salle A", personnes, s_debut, s_debut.AddHours(1)).NombrePersonnes);
    }
    [Theory]
    [InlineData(0)]
    [InlineData(13)]
    public void ctor_CapaciteHorsBornes_Exception(int personnes)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Reservation(301, "Salle A", personnes, s_debut, s_debut.AddHours(1)));
    }
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void ctor_NumeroNonPositif_Exception(int numero)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Reservation(numero, "Salle A", 4, s_debut, s_debut.AddHours(1)));
    }
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void ctor_RessourceInvalide_Exception(string? ressource)
    {
        Assert.ThrowsAny<ArgumentException>(() => new Reservation(301, ressource!, 4, s_debut, s_debut.AddHours(1)));
    }
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void ctor_FinEgaleOuAnterieure_Exception(int minutes)
    {
        Assert.Throws<ArgumentException>(() => new Reservation(301, "Salle A", 4, s_debut, s_debut.AddMinutes(minutes)));
    }
    [Theory]
    [InlineData(30, 90, true)]
    [InlineData(15, 45, true)]
    [InlineData(-30, 90, true)]
    [InlineData(0, 60, true)]
    [InlineData(-30, 0, false)]
    [InlineData(60, 120, false)]
    [InlineData(-120, -60, false)]
    public void chevauche_Periodes_IntervallesSemiOuvertsSymetriques(int debut, int fin, bool attendu)
    {
        Reservation premiere = new Reservation(301, "Salle A", 4, s_debut, s_debut.AddHours(1));
        Reservation autre = new Reservation(302, "Salle A", 4, s_debut.AddMinutes(debut), s_debut.AddMinutes(fin));
        Assert.Equal(attendu, premiere.Chevauche(autre));
        Assert.Equal(attendu, autre.Chevauche(premiere));
    }
    [Theory]
    [InlineData("Salle B")]
    [InlineData("salle a")]
    public void chevauche_RessourceDistincte_AbsenceConflit(string ressource)
    {
        Reservation premiere = new Reservation(301, "Salle A", 4, s_debut, s_debut.AddHours(1));
        Assert.False(premiere.Chevauche(new Reservation(302, ressource, 4, s_debut, s_debut.AddHours(1))));
        Assert.Throws<ArgumentNullException>(() => premiere.Chevauche(null!));
    }
}
