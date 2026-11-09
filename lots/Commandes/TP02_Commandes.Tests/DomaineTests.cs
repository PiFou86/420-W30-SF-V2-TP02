using Commandes.Domaine;
using Xunit;
namespace TP02_Commandes.Tests;
public sealed class DomaineTests
{
    [Fact]
    public void calculerTotal_PlusieursLignes_22SansTaxesNiPourboire()
    {
        Commande commande = new Commande(101);
        commande.AjouterLigne(new LigneCommande("Soupe", 6.50m, 2));
        commande.AjouterLigne(new LigneCommande("Sandwich", 9m, 1));
        Assert.Equal(22m, commande.Total);
        commande.Confirmer();
        Assert.True(commande.EstConfirmee);
    }
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void ctor_NumeroOuQuantiteNonPositif_Exception(int valeur)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Commande(valeur));
        Assert.Throws<ArgumentOutOfRangeException>(() => new LigneCommande("Soupe", 1m, valeur));
    }
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void ctor_LibelleInvalide_Exception(string? libelle)
    {
        Assert.ThrowsAny<ArgumentException>(() => new LigneCommande(libelle!, 1m, 1));
    }
    [Fact]
    public void ctor_PrixNegatif_ExceptionEtZeroAccepte()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new LigneCommande("Eau", -0.01m, 1));
        Assert.Equal(0m, new LigneCommande("Eau", 0m, 1).Total);
    }
    [Fact]
    public void confirmer_Vide_ExceptionEtEtatConserve()
    {
        Commande commande = new Commande(101);
        Assert.Throws<InvalidOperationException>(() => commande.Confirmer());
        Assert.False(commande.EstConfirmee);
        Assert.Equal(0m, commande.Total);
    }
    [Fact]
    public void ajouterLigne_Null_ExceptionSansMutation()
    {
        Commande commande = new Commande(101);
        Assert.Throws<ArgumentNullException>(() => commande.AjouterLigne(null!));
        Assert.Empty(commande.Lignes);
    }
    [Fact]
    public void ajouterOuConfirmer_DejaConfirmee_ExceptionSansMutation()
    {
        Commande commande = new Commande(101);
        commande.AjouterLigne(new LigneCommande("Soupe", 6.50m, 2));
        commande.Confirmer();
        Assert.Throws<InvalidOperationException>(() => commande.AjouterLigne(new LigneCommande("Autre", 1m, 1)));
        Assert.Throws<InvalidOperationException>(() => commande.Confirmer());
        Assert.Single(commande.Lignes);
        Assert.Equal(13m, commande.Total);
    }
    [Fact]
    public void lignes_CollectionExposee_InstantaneNonModifiable()
    {
        Commande commande = new Commande(101);
        IReadOnlyList<LigneCommande> avant = commande.Lignes;
        commande.AjouterLigne(new LigneCommande("Soupe", 6.50m, 2));
        Assert.Empty(avant);
        Assert.Throws<NotSupportedException>(() => ((ICollection<LigneCommande>)commande.Lignes).Clear());
        Assert.Single(commande.Lignes);
    }
    [Fact]
    public void copier_ConfirmeeOuPreparation_ConserveEtatEtIndependance()
    {
        Commande commande = new Commande(101);
        Commande copieVide = commande.Copier();
        Assert.False(copieVide.EstConfirmee);
        commande.AjouterLigne(new LigneCommande("Soupe", 6.50m, 2));
        commande.Confirmer();
        Commande copie = commande.Copier();
        Assert.NotSame(commande, copie);
        Assert.True(copie.EstConfirmee);
        Assert.Equal(13m, copie.Total);
        Assert.Empty(copieVide.Lignes);
    }
    [Fact]
    public void ligneDtoOuPersistance_ReferenceNulle_Exception()
    {
        Assert.Throws<ArgumentNullException>(() => new Commandes.Application.LigneCommandeDto(null!));
        Assert.Throws<ArgumentNullException>(() => new Commandes.Infrastructure.LigneCommandePersistance(null!));
    }
}
