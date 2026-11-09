using Menu.Application;
namespace TP02_Menu.WinForms;
public partial class FormSaisie : Form
{
    private Func<SaisieDto, Result<OperationDto>>? m_enregistrer;
    public FormSaisie()
    {
        InitializeComponent();
        bValider.Click += Valider;
    }
    public void Configurer(SaisieDto saisie, bool modification, Func<SaisieDto, Result<OperationDto>> enregistrer)
    {
        ArgumentNullException.ThrowIfNull(saisie);
        ArgumentNullException.ThrowIfNull(enregistrer);
        m_enregistrer = enregistrer;
        tbNumero.Text = saisie.Numero.ToString();
        tbNumero.ReadOnly = modification;
        tbNom.Text = saisie.Nom; tbPrix.Text = saisie.Prix.ToString();
    }
    private SaisieDto LireSaisie()
    {
        if (!int.TryParse(tbNumero.Text, out int numero))
        {
            throw new FormatException("Numéro illisible.");
        }
        SaisieDto saisie = new SaisieDto { Numero = numero };
        saisie.Nom = tbNom.Text;
        if (!decimal.TryParse(tbPrix.Text, out decimal prix))
        {
            throw new FormatException("Prix illisible.");
        }
        saisie.Prix = prix;
        return saisie;
    }
    private void Valider(object? sender, EventArgs e)
    {
        if (m_enregistrer is null)
        {
            lMessage.Text = "Raccordement au contrôleur à réaliser.";
            return;
        }
        try
        {
            Result<OperationDto> resultat = m_enregistrer(LireSaisie());
            if (resultat.EstSucces)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                lMessage.Text = resultat.MessageErreur;
            }
        }
        catch (FormatException exception)
        {
            lMessage.Text = exception.Message;
        }
    }
}
