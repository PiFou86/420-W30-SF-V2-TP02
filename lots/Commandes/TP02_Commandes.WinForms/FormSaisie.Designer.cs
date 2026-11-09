#nullable disable
namespace TP02_Commandes.WinForms;
partial class FormSaisie
{
    private System.ComponentModel.IContainer components = null;
    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }
    private void InitializeComponent()
    {
        SuspendLayout();
        lNumero = new Label();
        lNumero.Name = "lNumero";
        lNumero.Location = new Point(16, 15);
        lNumero.Size = new Size(100, 24);
        lNumero.Text = "Numéro";
        tbNumero = new TextBox();
        tbNumero.Name = "tbNumero";
        tbNumero.Location = new Point(130, 15);
        tbNumero.Size = new Size(180, 26);
        lMessage = new Label();
        lMessage.Name = "lMessage";
        lMessage.Location = new Point(16, 385);
        lMessage.Size = new Size(670, 45);
        lMessage.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        bValider = new Button();
        bValider.Name = "bValider";
        bValider.Location = new Point(440, 440);
        bValider.Size = new Size(110, 32);
        bValider.Text = "Enregistrer"; bValider.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        bAnnuler = new Button();
        bAnnuler.Name = "bAnnuler";
        bAnnuler.Location = new Point(560, 440);
        bAnnuler.Size = new Size(110, 32);
        bAnnuler.Text = "Annuler"; bAnnuler.DialogResult = DialogResult.Cancel; bAnnuler.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        lInstruction = new Label();
        lInstruction.Name = "lInstruction";
        lInstruction.Location = new Point(16, 55);
        lInstruction.Size = new Size(660, 35);
        lInstruction.Text = "Lignes : libellé, prix unitaire, quantité. Suppr retire la ligne sélectionnée.";
        dgvLignes = new DataGridView();
        dgvLignes.Name = "dgvLignes";
        dgvLignes.Location = new Point(16, 95);
        dgvLignes.Size = new Size(660, 270);
        dgvLignes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right; dgvLignes.AllowUserToAddRows = true; dgvLignes.AllowUserToDeleteRows = true; dgvLignes.AutoGenerateColumns = false; dgvLignes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        AcceptButton = bValider; CancelButton = bAnnuler;
        colLibelle = new DataGridViewTextBoxColumn();
        colLibelle.Name = "colLibelle";
        colLibelle.HeaderText = "Libelle";
        dgvLignes.Columns.Add(colLibelle);
        colPrixUnitaire = new DataGridViewTextBoxColumn();
        colPrixUnitaire.Name = "colPrixUnitaire";
        colPrixUnitaire.HeaderText = "PrixUnitaire";
        dgvLignes.Columns.Add(colPrixUnitaire);
        colQuantite = new DataGridViewTextBoxColumn();
        colQuantite.Name = "colQuantite";
        colQuantite.HeaderText = "Quantite";
        dgvLignes.Columns.Add(colQuantite);
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(700, 490);
        MinimumSize = new Size(700, 490);
        StartPosition = FormStartPosition.CenterParent;
        Name = "FormSaisie";
        Text = "TP02 — Commandes — FormSaisie";
        Controls.Add(lNumero);
        Controls.Add(tbNumero);
        Controls.Add(lMessage);
        Controls.Add(bValider);
        Controls.Add(bAnnuler);
        Controls.Add(lInstruction);
        Controls.Add(dgvLignes);
        ResumeLayout(false);
        PerformLayout();
    }
    private DataGridViewTextBoxColumn colLibelle;
    private DataGridViewTextBoxColumn colPrixUnitaire;
    private DataGridViewTextBoxColumn colQuantite;
    private Label lNumero;
    private TextBox tbNumero;
    private Label lMessage;
    private Button bValider;
    private Button bAnnuler;
    private Label lInstruction;
    private DataGridView dgvLignes;
}
