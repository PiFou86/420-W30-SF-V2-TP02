using Commandes.Application;
namespace TP02_Commandes.WinForms;
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
        foreach (LigneSaisieDto ligne in saisie.Lignes)
        {
            dgvLignes.Rows.Add(ligne.Libelle, ligne.PrixUnitaire, ligne.Quantite);
        }
    }
    private SaisieDto LireSaisie()
    {
        if (!int.TryParse(tbNumero.Text, out int numero))
        {
            throw new FormatException("Numéro illisible.");
        }
        SaisieDto saisie = new SaisieDto { Numero = numero };
        dgvLignes.EndEdit();
        foreach (DataGridViewRow ligne in dgvLignes.Rows)
        {
            if (ligne.IsNewRow)
            {
                continue;
            }
            if (!decimal.TryParse(Convert.ToString(ligne.Cells[1].Value), out decimal prix)
                || !int.TryParse(Convert.ToString(ligne.Cells[2].Value), out int quantite))
            {
                throw new FormatException("Prix ou quantité illisible.");
            }
            saisie.Lignes.Add(new LigneSaisieDto { Libelle = Convert.ToString(ligne.Cells[0].Value) ?? "", PrixUnitaire = prix, Quantite = quantite });
        }
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
