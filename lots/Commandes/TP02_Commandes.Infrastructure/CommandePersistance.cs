using System.Text.Json;
using Commandes.Domaine;
namespace Commandes.Infrastructure;
public sealed class CommandePersistance
{
    public int? Numero { get; set; }
    public bool? EstConfirmee { get; set; }
    public List<LigneCommandePersistance?>? Lignes { get; set; }
    public CommandePersistance(Commande element)
    {
        ArgumentNullException.ThrowIfNull(element);
        Numero = element.Numero;
        EstConfirmee = element.EstConfirmee;
        Lignes = element.Lignes.Select(l => (LigneCommandePersistance?)new LigneCommandePersistance(l)).ToList();
    }
    public CommandePersistance()
    {
    }
    public Commande VersEntite()
    {
        if (Numero is null)
        {
            throw new JsonException("Numéro manquant.");
        }
        if (EstConfirmee is null)
        {
            throw new JsonException("État manquant.");
        }

        Commande commande = new Commande(Numero.Value);
        bool confirmer = EstConfirmee.Value;
        if (Lignes is null)
        {
            throw new JsonException("Lignes manquantes.");
        }
        foreach (LigneCommandePersistance? ligne in Lignes)
        {
            if (ligne is null)
            {
                throw new JsonException("Une ligne ne peut pas être nulle.");
            }
            commande.AjouterLigne(ligne.VersEntite());
        }
        if (confirmer)
        {
            commande.Confirmer();
        }
        return commande;
    }
}
