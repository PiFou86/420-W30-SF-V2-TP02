#nullable disable
namespace TP02_Commandes.WinForms;
partial class FormCommandes
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
        cbMontant = new CheckBox();
        nudMontant = new NumericUpDown();
        cbEtat = new ComboBox();
        cbOrdre = new ComboBox();
        bRechercher = new Button();
        bTous = new Button();
        bSuivi = new Button();
        dgvResultats = new DataGridView();
        colNumero = new DataGridViewTextBoxColumn();
        colDescription = new DataGridViewTextBoxColumn();
        colEtat = new DataGridViewTextBoxColumn();
        colReference = new DataGridViewTextBoxColumn();
        colDetail = new DataGridViewTextBoxColumn();
        bPrecedent = new Button();
        bSuivant = new Button();
        bNouveau = new Button();
        bModifier = new Button();
        bAction = new Button();
        nudNouveauNumero = new NumericUpDown();
        bConfirmer = new Button();
        lEtat = new Label();
        ((System.ComponentModel.ISupportInitialize)nudMontant).BeginInit();
        ((System.ComponentModel.ISupportInitialize)dgvResultats).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudNouveauNumero).BeginInit();
        SuspendLayout();
        // 
        // cbMontant
        // 
        cbMontant.Location = new Point(196, 12);
        cbMontant.Name = "cbMontant";
        cbMontant.Size = new Size(90, 26);
        cbMontant.TabIndex = 2;
        cbMontant.Text = "Total min.";
        // 
        // nudMontant
        // 
        nudMontant.DecimalPlaces = 2;
        nudMontant.Location = new Point(286, 12);
        nudMontant.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
        nudMontant.Name = "nudMontant";
        nudMontant.Size = new Size(100, 23);
        nudMontant.TabIndex = 3;
        // 
        // cbEtat
        // 
        cbEtat.DropDownStyle = ComboBoxStyle.DropDownList;
        cbEtat.Items.AddRange(new object[] { "Tous", "Brouillon", "Confirmée" });
        cbEtat.Location = new Point(396, 12);
        cbEtat.Name = "cbEtat";
        cbEtat.Size = new Size(120, 23);
        cbEtat.TabIndex = 4;
        // 
        // cbOrdre
        // 
        cbOrdre.DropDownStyle = ComboBoxStyle.DropDownList;
        cbOrdre.Items.AddRange(new object[] { "Numéro", "Total" });
        cbOrdre.Location = new Point(696, 12);
        cbOrdre.Name = "cbOrdre";
        cbOrdre.Size = new Size(130, 23);
        cbOrdre.TabIndex = 6;
        // 
        // bRechercher
        // 
        bRechercher.Location = new Point(536, 52);
        bRechercher.Name = "bRechercher";
        bRechercher.Size = new Size(100, 30);
        bRechercher.TabIndex = 10;
        bRechercher.Text = "Rechercher";
        // 
        // bTous
        // 
        bTous.Location = new Point(646, 52);
        bTous.Name = "bTous";
        bTous.Size = new Size(80, 30);
        bTous.TabIndex = 11;
        bTous.Text = "Tous";
        // 
        // bSuivi
        // 
        bSuivi.Location = new Point(736, 52);
        bSuivi.Name = "bSuivi";
        bSuivi.Size = new Size(110, 30);
        bSuivi.TabIndex = 12;
        bSuivi.Text = "Ouvrir suivi";
        // 
        // dgvResultats
        // 
        dgvResultats.AllowUserToAddRows = false;
        dgvResultats.AllowUserToDeleteRows = false;
        dgvResultats.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvResultats.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvResultats.Columns.AddRange(new DataGridViewColumn[] { colNumero, colDescription, colEtat, colReference, colDetail });
        dgvResultats.Location = new Point(16, 88);
        dgvResultats.MultiSelect = false;
        dgvResultats.Name = "dgvResultats";
        dgvResultats.ReadOnly = true;
        dgvResultats.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvResultats.Size = new Size(920, 372);
        dgvResultats.TabIndex = 13;
        // 
        // colNumero
        // 
        colNumero.DataPropertyName = "Numero";
        colNumero.HeaderText = "Numero";
        colNumero.Name = "colNumero";
        colNumero.ReadOnly = true;
        // 
        // colDescription
        // 
        colDescription.DataPropertyName = "Description";
        colDescription.HeaderText = "Description";
        colDescription.Name = "colDescription";
        colDescription.ReadOnly = true;
        // 
        // colEtat
        // 
        colEtat.DataPropertyName = "Etat";
        colEtat.HeaderText = "Etat";
        colEtat.Name = "colEtat";
        colEtat.ReadOnly = true;
        // 
        // colReference
        // 
        colReference.DataPropertyName = "Reference";
        colReference.HeaderText = "Reference";
        colReference.Name = "colReference";
        colReference.ReadOnly = true;
        // 
        // colDetail
        // 
        colDetail.DataPropertyName = "Detail";
        colDetail.HeaderText = "Detail";
        colDetail.Name = "colDetail";
        colDetail.ReadOnly = true;
        // 
        // bPrecedent
        // 
        bPrecedent.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        bPrecedent.Location = new Point(16, 480);
        bPrecedent.Name = "bPrecedent";
        bPrecedent.Size = new Size(95, 32);
        bPrecedent.TabIndex = 14;
        bPrecedent.Text = "Précédent";
        // 
        // bSuivant
        // 
        bSuivant.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        bSuivant.Location = new Point(120, 480);
        bSuivant.Name = "bSuivant";
        bSuivant.Size = new Size(95, 32);
        bSuivant.TabIndex = 15;
        bSuivant.Text = "Suivant";
        // 
        // bNouveau
        // 
        bNouveau.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        bNouveau.Location = new Point(230, 480);
        bNouveau.Name = "bNouveau";
        bNouveau.Size = new Size(95, 32);
        bNouveau.TabIndex = 16;
        bNouveau.Text = "Nouveau";
        // 
        // bModifier
        // 
        bModifier.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        bModifier.Location = new Point(335, 480);
        bModifier.Name = "bModifier";
        bModifier.Size = new Size(95, 32);
        bModifier.TabIndex = 17;
        bModifier.Text = "Modifier";
        // 
        // bAction
        // 
        bAction.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        bAction.Location = new Point(440, 480);
        bAction.Name = "bAction";
        bAction.Size = new Size(95, 32);
        bAction.TabIndex = 18;
        bAction.Text = "Dupliquer";
        // 
        // nudNouveauNumero
        // 
        nudNouveauNumero.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        nudNouveauNumero.Location = new Point(550, 480);
        nudNouveauNumero.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
        nudNouveauNumero.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        nudNouveauNumero.Name = "nudNouveauNumero";
        nudNouveauNumero.Size = new Size(100, 23);
        nudNouveauNumero.TabIndex = 19;
        nudNouveauNumero.Value = new decimal(new int[] { 105, 0, 0, 0 });
        // 
        // bConfirmer
        // 
        bConfirmer.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        bConfirmer.Location = new Point(670, 480);
        bConfirmer.Name = "bConfirmer";
        bConfirmer.Size = new Size(110, 32);
        bConfirmer.TabIndex = 20;
        bConfirmer.Text = "Confirmer";
        // 
        // lEtat
        // 
        lEtat.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        lEtat.Location = new Point(16, 525);
        lEtat.Name = "lEtat";
        lEtat.Size = new Size(920, 55);
        lEtat.TabIndex = 21;
        lEtat.Text = "Maquette fournie — raccordement MVC à réaliser.";
        // 
        // FormCommandes
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(960, 600);
        Controls.Add(cbMontant);
        Controls.Add(nudMontant);
        Controls.Add(cbEtat);
        Controls.Add(cbOrdre);
        Controls.Add(bRechercher);
        Controls.Add(bTous);
        Controls.Add(bSuivi);
        Controls.Add(dgvResultats);
        Controls.Add(bPrecedent);
        Controls.Add(bSuivant);
        Controls.Add(bNouveau);
        Controls.Add(bModifier);
        Controls.Add(bAction);
        Controls.Add(nudNouveauNumero);
        Controls.Add(bConfirmer);
        Controls.Add(lEtat);
        MinimumSize = new Size(960, 600);
        Name = "FormCommandes";
        StartPosition = FormStartPosition.CenterParent;
        Text = "TP02 — Commandes — FormCommandes";
        ((System.ComponentModel.ISupportInitialize)nudMontant).EndInit();
        ((System.ComponentModel.ISupportInitialize)dgvResultats).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudNouveauNumero).EndInit();
        ResumeLayout(false);
    }
    private DataGridViewTextBoxColumn colNumero;
    private DataGridViewTextBoxColumn colDescription;
    private DataGridViewTextBoxColumn colEtat;
    private DataGridViewTextBoxColumn colReference;
    private DataGridViewTextBoxColumn colDetail;
    private CheckBox cbMontant;
    private NumericUpDown nudMontant;
    private ComboBox cbEtat;
    private ComboBox cbOrdre;
    private Button bRechercher;
    private Button bTous;
    private Button bSuivi;
    private DataGridView dgvResultats;
    private Button bPrecedent;
    private Button bSuivant;
    private Button bNouveau;
    private Button bModifier;
    private Button bAction;
    private NumericUpDown nudNouveauNumero;
    private Button bConfirmer;
    private Label lEtat;
}
