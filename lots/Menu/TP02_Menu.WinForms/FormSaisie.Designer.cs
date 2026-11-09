#nullable disable
namespace TP02_Menu.WinForms;
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
        lNom = new Label();
        lNom.Name = "lNom";
        lNom.Location = new Point(16, 65);
        lNom.Size = new Size(100, 26);
        lNom.Text = "Nom";
        tbNom = new TextBox();
        tbNom.Name = "tbNom";
        tbNom.Location = new Point(130, 65);
        tbNom.Size = new Size(500, 26);
        lPrix = new Label();
        lPrix.Name = "lPrix";
        lPrix.Location = new Point(16, 115);
        lPrix.Size = new Size(100, 26);
        lPrix.Text = "Prix";
        tbPrix = new TextBox();
        tbPrix.Name = "tbPrix";
        tbPrix.Location = new Point(130, 115);
        tbPrix.Size = new Size(180, 26);
        AcceptButton = bValider; CancelButton = bAnnuler;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(700, 490);
        MinimumSize = new Size(700, 490);
        StartPosition = FormStartPosition.CenterParent;
        Name = "FormSaisie";
        Text = "TP02 — Menu — FormSaisie";
        Controls.Add(lNumero);
        Controls.Add(tbNumero);
        Controls.Add(lMessage);
        Controls.Add(bValider);
        Controls.Add(bAnnuler);
        Controls.Add(lNom);
        Controls.Add(tbNom);
        Controls.Add(lPrix);
        Controls.Add(tbPrix);
        ResumeLayout(false);
        PerformLayout();
    }
    private Label lNumero;
    private TextBox tbNumero;
    private Label lMessage;
    private Button bValider;
    private Button bAnnuler;
    private Label lNom;
    private TextBox tbNom;
    private Label lPrix;
    private TextBox tbPrix;
}
