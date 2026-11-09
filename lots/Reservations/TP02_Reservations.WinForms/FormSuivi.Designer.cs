#nullable disable
namespace TP02_Reservations.WinForms;
partial class FormSuivi
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
        lDerniereOperation = new Label();
        lDerniereOperation.Name = "lDerniereOperation";
        lDerniereOperation.Location = new Point(20, 20);
        lDerniereOperation.Size = new Size(450, 130);
        lDerniereOperation.Text = "Aucune opération observée."; lDerniereOperation.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(500, 180);
        MinimumSize = new Size(500, 180);
        StartPosition = FormStartPosition.CenterParent;
        Name = "FormSuivi";
        Text = "TP02 — Reservations — FormSuivi";
        Controls.Add(lDerniereOperation);
        ResumeLayout(false);
        PerformLayout();
    }
    private Label lDerniereOperation;
}
