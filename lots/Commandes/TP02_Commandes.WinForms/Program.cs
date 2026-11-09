namespace TP02_Commandes.WinForms;

internal static class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        ApplicationConfiguration.Initialize();
        // TP02 : assembler la vue, le contrôleur et les services dans ce point d'entrée.
        System.Windows.Forms.Application.Run(new FormCommandes());
    }
}
