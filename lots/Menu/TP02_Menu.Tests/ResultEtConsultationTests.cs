using Menu.Application;
using Xunit;
namespace TP02_Menu.Tests;
public sealed class ResultEtConsultationTests
{
    [Fact]
    public void succes_Valeur_ConserveValeurEtEtat()
    {
        object valeur = new object();
        Result<object> resultat = Result<object>.Succes(valeur);
        Assert.True(resultat.EstSucces);
        Assert.Same(valeur, resultat.Valeur);
        Assert.Null(resultat.MessageErreur);
        Assert.Throws<ArgumentNullException>(() => Result<object>.Succes(null!));
    }
    [Fact]
    public void echec_Message_ConserveMessageSansValeur()
    {
        Result<object> resultat = Result<object>.Echec("Refus attendu.");
        Assert.False(resultat.EstSucces);
        Assert.Null(resultat.Valeur);
        Assert.Equal("Refus attendu.", resultat.MessageErreur);
    }
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void echec_MessageInvalide_Exception(string? message)
    {
        Assert.ThrowsAny<ArgumentException>(() => Result<object>.Echec(message!));
    }
}
