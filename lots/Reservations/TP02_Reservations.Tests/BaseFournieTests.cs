using Xunit;
using Reservations.Application;
using Reservations.Domaine;
using Reservations.Infrastructure;
namespace TP02_Reservations.Tests;
public sealed class BaseFournieTests
{
    private static Reservation Element(int numero) => new Reservation(numero, "Salle " + numero, 4, new DateTime(2026, 11, 23, 10, 0, 0), new DateTime(2026, 11, 23, 11, 0, 0));
    private static SaisieDto Saisie(int numero) => new SaisieDto { Numero = numero, Ressource = "Salle " + numero, NombrePersonnes = 4, Debut = new DateTime(2026, 11, 23, 10, 0, 0), Fin = new DateTime(2026, 11, 23, 11, 0, 0) };
    [Fact]
    public void Memoire_ProtegeLesObjetsRetournes()
    {
        IDepotReservations depot = new DepotReservationsMemoire(new[] { Element(1) });
        Reservation sortie = depot.Obtenir(1)!;
        sortie.Annuler();
        Assert.False(depot.Obtenir(1)!.EstAnnulee);
    }
    [Fact]
    public void AjouterEtRemplacer_SeRelisentAvecUneNouvelleInstanceJson()
    {
        string dossier = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(dossier);
        try
        {
            string fichier = Path.Combine(dossier, "test.json");
            DepotReservationsJson depot = new DepotReservationsJson(fichier);
            depot.Ajouter(Element(1));
            depot.Remplacer(Element(1));
            Assert.Equal(1, new DepotReservationsJson(fichier).Obtenir(1)!.Numero);
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
        DepotReservationsMemoire depot = new DepotReservationsMemoire(Array.Empty<Reservation>());
        Assert.Throws<ArgumentOutOfRangeException>(() => depot.Rechercher(new CriteresRecherche { Debut = debut, Taille = taille }));
    }
    [Fact]
    public void Recherche_DefaultEtStrategieControlee_AvantPagination()
    {
        DepotReservationsMemoire depot = new DepotReservationsMemoire(new[] { Element(3), Element(1), Element(2) });
        Assert.Equal(new[] { 1, 2 }, depot.Rechercher(new CriteresRecherche { Taille = 2 }).Select(e => e.Numero));
        Assert.Equal(new[] { 2 }, depot.Rechercher(new CriteresRecherche { Debut = 1, Taille = 1 }, new TriInverse()).Select(e => e.Numero));
        Assert.Empty(depot.Rechercher(new CriteresRecherche { Debut = 9, Taille = 2 }));
    }
    [Fact]
    public void CreationEtModification_UtilisentLesServicesFournis()
    {
        DepotReservationsMemoire depot = new DepotReservationsMemoire(Array.Empty<Reservation>());
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
        public IEnumerable<Reservation> Ordonner(IEnumerable<Reservation> elements) => elements.OrderByDescending(e => e.Numero);
    }
}
