using Reservations.Application;
namespace TP02_Reservations.WinForms;
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
        tbRessource.Text = saisie.Ressource; tbPersonnes.Text = saisie.NombrePersonnes.ToString(); dtpDebut.Value = saisie.Debut; dtpFin.Value = saisie.Fin;
    }
    private SaisieDto LireSaisie()
    {
        if (!int.TryParse(tbNumero.Text, out int numero))
        {
            throw new FormatException("Numéro illisible.");
        }
        SaisieDto saisie = new SaisieDto { Numero = numero };
        saisie.Ressource = tbRessource.Text; saisie.Debut = dtpDebut.Value; saisie.Fin = dtpFin.Value;
        if (!int.TryParse(tbPersonnes.Text, out int personnes))
        {
            throw new FormatException("Nombre de personnes illisible.");
        }
        saisie.NombrePersonnes = personnes;
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
