namespace Menu.Domaine;
public sealed class ElementMenu
{
    public bool EstRetire { get; private set; }
    public void Retirer()
    {
        if (EstRetire)
        {
            throw new InvalidOperationException("L’opération a déjà été réalisée.");
        }
        EstRetire = true;
    }
    public int Numero { get; }
    public string Nom { get; }
    public decimal Prix { get; }
    public ElementMenu(int numero, string nom, decimal prix, bool estRetire = false)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(numero);
        ArgumentException.ThrowIfNullOrWhiteSpace(nom);
        ArgumentOutOfRangeException.ThrowIfNegative(prix);
        Numero = numero;
        EstRetire = estRetire;
        Nom = nom;
        Prix = prix;
    }
    public bool CorrespondAuNom(string fragment)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(fragment);
        return Nom.Contains(fragment, StringComparison.OrdinalIgnoreCase);
    }
}
