using System.Text.Json;
using Commandes.Application;
using Commandes.Infrastructure;
using Xunit;

namespace TP02_Commandes.Tests;

public sealed class LectureJsonTests
{
    private static string Chemin(string fichier) =>
        Path.Combine(AppContext.BaseDirectory, "donnees-exemples", fichier);

    [Fact]
    public void Lire_FichierValide_ProjetteLesNumerosAttendus()
    {
        ConsulterCommandes consulter = new ConsulterCommandes(
            new DepotCommandesJson(Chemin("valide.json")));
        Assert.Equal(new[] { 101 }, consulter.ObtenirToutes().Select(x => x.Numero));
    }

    [Theory]
    [InlineData("invalide.json")]
    [InlineData("doublon.json")]
    [InlineData("cas-nul.json")]
    public void Lire_DocumentIncoherent_SignaleUnIncidentSansModifierLeFichier(string fichier)
    {
        string chemin = Chemin(fichier);
        byte[] avant = File.ReadAllBytes(chemin);
        DepotCommandesJson depot = new DepotCommandesJson(chemin);

        Assert.Throws<JsonException>(() => depot.ObtenirToutes());
        Assert.Equal(avant, File.ReadAllBytes(chemin));
    }

    [Fact]
    public void Lire_ListeVide_RetourneUneCollectionVide()
    {
        DepotCommandesJson depot = new DepotCommandesJson(Chemin("vide.json"));
        Assert.Empty(depot.ObtenirToutes());
    }
}
