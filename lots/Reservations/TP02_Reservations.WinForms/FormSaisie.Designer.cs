#nullable disable
namespace TP02_Reservations.WinForms;
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
        lRessource = new Label();
        lRessource.Name = "lRessource";
        lRessource.Location = new Point(16, 65);
        lRessource.Size = new Size(100, 26);
        lRessource.Text = "Ressource";
        tbRessource = new TextBox();
        tbRessource.Name = "tbRessource";
        tbRessource.Location = new Point(130, 65);
        tbRessource.Size = new Size(400, 26);
        lPersonnes = new Label();
        lPersonnes.Name = "lPersonnes";
        lPersonnes.Location = new Point(16, 115);
        lPersonnes.Size = new Size(100, 26);
        lPersonnes.Text = "Personnes";
        tbPersonnes = new TextBox();
        tbPersonnes.Name = "tbPersonnes";
        tbPersonnes.Location = new Point(130, 115);
        tbPersonnes.Size = new Size(180, 26);
        lDebut = new Label();
        lDebut.Name = "lDebut";
        lDebut.Location = new Point(16, 165);
        lDebut.Size = new Size(100, 26);
        lDebut.Text = "Début";
        dtpDebut = new DateTimePicker();
        dtpDebut.Name = "dtpDebut";
        dtpDebut.Location = new Point(130, 165);
        dtpDebut.Size = new Size(250, 26);
        dtpDebut.Format = DateTimePickerFormat.Custom; dtpDebut.CustomFormat = "yyyy-MM-dd HH:mm";
        lFin = new Label();
        lFin.Name = "lFin";
        lFin.Location = new Point(16, 215);
        lFin.Size = new Size(100, 26);
        lFin.Text = "Fin";
        dtpFin = new DateTimePicker();
        dtpFin.Name = "dtpFin";
        dtpFin.Location = new Point(130, 215);
        dtpFin.Size = new Size(250, 26);
        dtpFin.Format = DateTimePickerFormat.Custom; dtpFin.CustomFormat = "yyyy-MM-dd HH:mm";
        AcceptButton = bValider; CancelButton = bAnnuler;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(700, 490);
        MinimumSize = new Size(700, 490);
        StartPosition = FormStartPosition.CenterParent;
        Name = "FormSaisie";
        Text = "TP02 — Reservations — FormSaisie";
        Controls.Add(lNumero);
        Controls.Add(tbNumero);
        Controls.Add(lMessage);
        Controls.Add(bValider);
        Controls.Add(bAnnuler);
        Controls.Add(lRessource);
        Controls.Add(tbRessource);
        Controls.Add(lPersonnes);
        Controls.Add(tbPersonnes);
        Controls.Add(lDebut);
        Controls.Add(dtpDebut);
        Controls.Add(lFin);
        Controls.Add(dtpFin);
        ResumeLayout(false);
        PerformLayout();
    }
    private Label lNumero;
    private TextBox tbNumero;
    private Label lMessage;
    private Button bValider;
    private Button bAnnuler;
    private Label lRessource;
    private TextBox tbRessource;
    private Label lPersonnes;
    private TextBox tbPersonnes;
    private Label lDebut;
    private DateTimePicker dtpDebut;
    private Label lFin;
    private DateTimePicker dtpFin;
}
