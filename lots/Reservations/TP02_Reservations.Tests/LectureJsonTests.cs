using System.Text.Json;
using Reservations.Application;
using Reservations.Infrastructure;
using Xunit;

namespace TP02_Reservations.Tests;

public sealed class LectureJsonTests
{
    private static string Chemin(string fichier) =>
        Path.Combine(AppContext.BaseDirectory, "donnees-exemples", fichier);

    [Fact]
    public void Lire_FichierValide_ProjetteLesNumerosAttendus()
    {
        ConsulterReservations consulter = new ConsulterReservations(
            new DepotReservationsJson(Chemin("valide.json")));
        Assert.Equal(new[] { 301 }, consulter.ObtenirToutes().Select(x => x.Numero));
    }

    [Theory]
    [InlineData("invalide.json")]
    [InlineData("doublon.json")]
    [InlineData("nul.json")]
    public void Lire_DocumentIncoherent_SignaleUnIncidentSansModifierLeFichier(string fichier)
    {
        string chemin = Chemin(fichier);
        byte[] avant = File.ReadAllBytes(chemin);
        DepotReservationsJson depot = new DepotReservationsJson(chemin);

        Assert.Throws<JsonException>(() => depot.ObtenirToutes());
        Assert.Equal(avant, File.ReadAllBytes(chemin));
    }

    [Fact]
    public void Lire_ListeVide_RetourneUneCollectionVide()
    {
        DepotReservationsJson depot = new DepotReservationsJson(Chemin("vide.json"));
        Assert.Empty(depot.ObtenirToutes());
    }
}
