# Contrats partiels et points de vérification

Les types fournis se trouvent dans le projet Application de chaque lot. Les espaces de noms restent `Commandes`, `Menu` et `Reservations`. Conserver les signatures publiques ci-dessous; les classes privées et l’organisation interne restent libres. Un adaptateur public équivalent convient si vous choisissez d’autres noms internes.

## Application et Infrastructure

`SaisieDto` porte les champs propres au lot. `LigneConsultationDto` expose `Numero`, `Description`, `Etat`, `Reference` et `Detail`. `OperationDto` est l’instantané immuable d’une écriture réussie : `Numero`, `Type`, `Detail`.

`IServiceLot` et `ServiceLot` exposent :

```csharp
SaisieDto? Obtenir(int numero);
IReadOnlyList<LigneConsultationDto> Rechercher(
    CriteresRecherche criteres, IStrategieTri? strategie = null);
Result<OperationDto> Creer(SaisieDto saisie);
Result<OperationDto> Modifier(SaisieDto saisie);
Result<OperationDto> ExecuterAction(int numero, int? nouveauNumero = null);
```

`nouveauNumero` est utilisé seulement par Commandes. Une action sur un numéro invalide/absent ou un état interdit retourne un refus. Le contrat Commandes possède aussi `Confirmer(int numero)`, fourni. Un objet de saisie nul viole une précondition; des champs convertibles mais invalides donnent un refus métier. Les incidents de fichier se propagent jusqu’à la frontière de présentation.

`CriteresRecherche` fournit `Texte`, `Montant`, `InclureHistorique`, `Confirmee`, les deux bornes de période, `Debut` et `Taille`. Ses propriétés inutiles à un lot ne sont pas affichées. Sans période, toutes les périodes sont admises; avec période, fournir deux bornes ordonnées. Les règles précises sont dans le sujet et dans `Valider`.

`IStrategieTri.Ordonner` reçoit et retourne `IEnumerable<Commande>`, `IEnumerable<ElementMenu>` ou `IEnumerable<Reservation>` selon le lot. Le départ accepte une stratégie nulle pour son ordre de référence par numéro. Le résultat final expose `TriParNumero` et `TriSecondaire` et permet une autre implantation de test.

Le port spécifique fourni déclare les opérations de base et `Rechercher`. En S12, extraire dans Application :

```csharp
public interface IRepository<T> where T : class
{
    void Ajouter(T element);
    T? Obtenir(int numero);
    IReadOnlyCollection<T> ObtenirToutes();
    void Remplacer(T element);
}
```

Le port spécifique hérite du contrat générique et conserve la recherche. Les deux dépôts gardent leurs constructeurs : mémoire reçoit une séquence d’entités; JSON reçoit un chemin. Les références ont leur contrat de lecture `IDepotReferences`. Leurs implantations sont fournies.

## Présentation à construire

Le projet Presentation contient déjà `IVueLot.Afficher(lignes)` et `AfficherMessage(message)`.

| Type à créer | Membres publics attendus |
|---|---|
| `ModeleLot : IObservable<OperationDto>` | `OperationDto? DerniereOperation { get; }`, `Subscribe(IObserver<OperationDto>)`, `Publier(OperationDto)` |
| `ControleurLot` | Constructeur `(IServiceLot service, ModeleLot modele, IVueLot vue)` |
| Consultation du contrôleur | `Obtenir(int numero)`, `Appliquer(CriteresRecherche, IStrategieTri)`, `Actualiser()`, `Suivante()`, `Precedente()` |
| Écritures du contrôleur | `Creer(SaisieDto)`, `Modifier(SaisieDto)`, `ExecuterAction(int, int? = null)` retournent `Result<OperationDto>`; Commandes ajoute `Confirmer(int)` |

`Appliquer` copie les critères, remet le début à zéro et relance la consultation. `Subscribe` ne publie pas automatiquement l’état initial. `Dispose` de l’abonnement est idempotent. Les notifications suivent les succès du service, pas les clics des boutons.

Le fichier `FormSaisie.cs` fourni accepte `Configurer(SaisieDto, bool modification, Func<SaisieDto, Result<OperationDto>> enregistrer)`. Brancher cette fonction au contrôleur. La fenêtre ferme seulement après un succès; Annuler ne l’appelle pas.

## Fichiers concernés

- Modifier `ServiceLot.cs` pour l’action et la jointure; créer les deux classes de tri dans Application.
- Modifier le port de dépôt lors de l’extraction d’`IRepository<T>`.
- Réusiner les deux dépôts Infrastructure en S13; créer le lecteur JSON et la requête partagée.
- Créer le contrôleur et le modèle dans Presentation; raccorder les deux vues dans les fichiers `.cs` WinForms et l’assemblage dans `Program.cs`.
- Ajouter vos tests au projet Tests. Les modèles et maquettes fournis compilent dès le départ.
