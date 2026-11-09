using Xunit;
using Commandes.Application;
using Commandes.Domaine;
using Commandes.Infrastructure;
namespace TP02_Commandes.Tests;
public sealed class BaseFournieTests
{
    private static Commande Element(int numero) => new Commande(numero);
    private static SaisieDto Saisie(int numero) => new SaisieDto { Numero = numero, Lignes = new List<LigneSaisieDto> { new LigneSaisieDto { Libelle = "Ligne", PrixUnitaire = 8m, Quantite = 1 } } };
    [Fact]
    public void Memoire_ProtegeLesObjetsRetournes()
    {
        IDepotCommandes depot = new DepotCommandesMemoire(new[] { Element(1) });
        Commande sortie = depot.Obtenir(1)!;
        sortie.AjouterLigne(new LigneCommande("Autre", 2m, 1));
        Assert.Empty(depot.Obtenir(1)!.Lignes);
    }
    [Fact]
    public void AjouterEtRemplacer_SeRelisentAvecUneNouvelleInstanceJson()
    {
        string dossier = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(dossier);
        try
        {
            string fichier = Path.Combine(dossier, "test.json");
            DepotCommandesJson depot = new DepotCommandesJson(fichier);
            depot.Ajouter(Element(1));
            depot.Remplacer(Element(1));
            Assert.Equal(1, new DepotCommandesJson(fichier).Obtenir(1)!.Numero);
            byte[] avant = File.ReadAllBytes(fichier);
            Assert.Throws<InvalidOperationException>(() => depot.Ajouter(Element(1)));
            Assert.Throws<InvalidOperationException>(() => depot.Remplacer(Element(99)));
            Assert.Equal(avant, File.ReadAllBytes(fichier));
        }
        finally
        {
            Directory.Delete(dossier, true);
        }
    }
    [Theory]
    [InlineData(-1, 5)]
    [InlineData(0, 0)]
    [InlineData(0, 51)]
    public void Recherche_ParametresInvalides_Refuses(int debut, int taille)
    {
        DepotCommandesMemoire depot = new DepotCommandesMemoire(Array.Empty<Commande>());
        Assert.Throws<ArgumentOutOfRangeException>(() => depot.Rechercher(new CriteresRecherche { Debut = debut, Taille = taille }));
    }
    [Fact]
    public void Recherche_DefaultEtStrategieControlee_AvantPagination()
    {
        DepotCommandesMemoire depot = new DepotCommandesMemoire(new[] { Element(3), Element(1), Element(2) });
        Assert.Equal(new[] { 1, 2 }, depot.Rechercher(new CriteresRecherche { Taille = 2 }).Select(e => e.Numero));
        Assert.Equal(new[] { 2 }, depot.Rechercher(new CriteresRecherche { Debut = 1, Taille = 1 }, new TriInverse()).Select(e => e.Numero));
        Assert.Empty(depot.Rechercher(new CriteresRecherche { Debut = 9, Taille = 2 }));
    }
    [Fact]
    public void CreationEtModification_UtilisentLesServicesFournis()
    {
        DepotCommandesMemoire depot = new DepotCommandesMemoire(Array.Empty<Commande>());
        ServiceLot service = new ServiceLot(depot, new DepotReferencesMemoire(Array.Empty<ReferenceDto>()));
        Assert.True(service.Creer(Saisie(1)).EstSucces);
        Assert.True(service.Modifier(Saisie(1)).EstSucces);
        Assert.False(service.Creer(Saisie(1)).EstSucces);
        Assert.False(service.Modifier(Saisie(99)).EstSucces);
        Assert.Equal(1, service.Obtenir(1)!.Numero);
    }
    [Theory]
    [InlineData("references.json", false)]
    [InlineData("references-doublon.json", true)]
    public void References_ValidentLesCles(string fichier, bool invalide)
    {
        DepotReferencesJson depot = new DepotReferencesJson(Path.Combine(AppContext.BaseDirectory, "donnees-exemples", fichier));
        if (invalide)
        {
            Assert.Throws<System.Text.Json.JsonException>(() => depot.ObtenirToutes());
        }
        else
        {
            Assert.NotEmpty(depot.ObtenirToutes());
        }
    }
    private sealed class TriInverse : IStrategieTri
    {
        public IEnumerable<Commande> Ordonner(IEnumerable<Commande> elements) => elements.OrderByDescending(e => e.Numero);
    }
}
