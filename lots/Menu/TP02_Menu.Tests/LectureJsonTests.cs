using System.Text.Json;
using Menu.Application;
using Menu.Infrastructure;
using Xunit;

namespace TP02_Menu.Tests;

public sealed class LectureJsonTests
{
    private static string Chemin(string fichier) =>
        Path.Combine(AppContext.BaseDirectory, "donnees-exemples", fichier);

    [Fact]
    public void Lire_FichierValide_ProjetteLesNumerosAttendus()
    {
        ConsulterMenu consulter = new ConsulterMenu(
            new DepotMenuJson(Chemin("valide.json")));
        Assert.Equal(new[] { 1, 2, 3 }, consulter.ObtenirToutes().Select(x => x.Numero));
    }

    [Theory]
    [InlineData("invalide.json")]
    [InlineData("doublon.json")]
    [InlineData("cas-nul.json")]
    public void Lire_DocumentIncoherent_SignaleUnIncidentSansModifierLeFichier(string fichier)
    {
        string chemin = Chemin(fichier);
        byte[] avant = File.ReadAllBytes(chemin);
        DepotMenuJson depot = new DepotMenuJson(chemin);

        Assert.Throws<JsonException>(() => depot.ObtenirToutes());
        Assert.Equal(avant, File.ReadAllBytes(chemin));
    }

    [Fact]
    public void Lire_ListeVide_RetourneUneCollectionVide()
    {
        DepotMenuJson depot = new DepotMenuJson(Chemin("vide.json"));
        Assert.Empty(depot.ObtenirToutes());
    }
}
