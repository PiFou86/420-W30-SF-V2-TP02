using Menu.Domaine;
using Xunit;
namespace TP02_Menu.Tests;
public sealed class DomaineTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void ctor_NumeroNonPositif_Exception(int numero)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new ElementMenu(numero, "Soupe", 6.50m));
    }
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void ctor_NomInvalide_Exception(string? nom)
    {
        Assert.ThrowsAny<ArgumentException>(() => new ElementMenu(1, nom!, 6.50m));
    }
    [Fact]
    public void ctor_PrixZero_ValideEtPrixNegatifRefuse()
    {
        Assert.Equal(0m, new ElementMenu(3, "Eau", 0m).Prix);
        Assert.Throws<ArgumentOutOfRangeException>(() => new ElementMenu(3, "Eau", -0.01m));
    }
    [Theory]
    [InlineData("sOu", true)]
    [InlineData("SOUPE", true)]
    [InlineData("Pizza", false)]
    public void correspondAuNom_Fragment_ComparaisonOrdinaleSansCasse(string fragment, bool attendu)
    {
        Assert.Equal(attendu, new ElementMenu(1, "Soupe", 6.50m).CorrespondAuNom(fragment));
    }
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void correspondAuNom_FragmentInvalide_Exception(string? fragment)
    {
        Assert.ThrowsAny<ArgumentException>(() => new ElementMenu(1, "Soupe", 6.50m).CorrespondAuNom(fragment!));
    }
}
