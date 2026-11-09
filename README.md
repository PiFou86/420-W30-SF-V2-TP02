# TP02 — Restaurant, MVC et Observateur

## Mandat et échéance

Compléter un lot Windows autonome avec MVC, Observateur et Stratégie, puis réusiner sa recherche. **Remise finale en fin de semaine 13**; la plateforme précise le jour et l’heure. Budget de travail : **environ 13 h par personne, dont 8 h en classe et 5 h à la maison**. Il n’y a aucune vidéo à remettre.

En binôme, attribuer Commandes et Menu. À trois, ajouter Réservations. Chaque personne possède son lot compilable et testable seul. Les solutions `TP02_Equipe2.slnx` et `TP02_Equipe3.slnx` regroupent les projets. Remplir [AUTHORS.md](AUTHORS.md).

## Départ fourni et travail à réaliser

Ouvrir la solution de son lot dans Visual Studio, avec .NET 10 et Développement Desktop .NET. Générer et lancer les tests. Le Terminal affiche 101 pour Commandes, 1/2/3 pour Menu, 301 pour Réservations. Le projet WinForms ouvre une **maquette non raccordée** : les commandes de la fenêtre principale sont à brancher. Son absence de comportement initial est normale.

| Fourni et testé | À réaliser par chaque personne |
|---|---|
| Domaine, DTO de saisie et opérations de création/modification | Service de l’action propre au lot et ses refus |
| Dépôts mémoire/JSON avec écritures et protection des références | Généralisation du contrat avec `IRepository<T>` |
| Recherche fonctionnelle répétée dans les deux dépôts | Deux stratégies de tri puis recherche partagée après réusinage |
| Références en mémoire et JSON, validation et anciens JSON | Jointure et projection enrichie dans Application |
| Trois formulaires disposés dans le designer; saisie et conversion fournies | Contrôleur, modèle observable, abonnements et raccordement des vues |
| Tests du code fourni et données | Tests des ajouts, doubles manuels, deux PR soumises et deux relues |

Le code fourni doit continuer à fonctionner. Les points évaluent vos ajouts, l’utilisation correcte des contrats et les preuves de compréhension; la simple présence du départ ne rapporte pas de points.

Les [contrats publics](CONTRATS.md) fixent les points de vérification. Le choix des méthodes privées, de leur organisation et des tests supplémentaires reste libre. Un contrat dans le sujet ou la solution n’est pas un corrigé de son implantation.

## Maquettes fournies

Chaque lot contient réellement les trois formulaires suivants, leurs fichiers `.Designer.cs` et `.resx` :

- `FormCommandes`, `FormMenu` ou `FormReservations` : filtres propres au lot, ordre, grille en lecture seule, navigation, création/modification, action et accès au suivi;
- `FormSaisie` : saisie modale; l’éditeur de lignes de Commandes est fourni. Sa méthode `Configurer` reçoit un DTO et une fonction de sauvegarde; elle ne connaît ni dépôt ni entité du domaine;
- `FormSuivi` : maquette de suivi non modal à transformer en observateur.

~~~text
Fenêtre principale
+----------------------------------------------------------------+
| Filtres du lot                  Ordre : Numéro / second ordre   |
| [Rechercher] [Tous] [Ouvrir suivi]                               |
|----------------------------------------------------------------|
| Numéro | Description | État | Référence | Détail                 |
| Grille en lecture seule                                        |
|----------------------------------------------------------------|
| [Précédent] [Suivant] [Nouveau] [Modifier] [Action du lot]        |
| Message                                                        |
+----------------------------------------------------------------+
Saisie modale                         Suivi non modal
+------------------------------+      +--------------------------+
| Numéro et champs du lot      |      | Dernière opération       |
| Lignes / nom-prix / période  |      | Numéro et détail         |
| Message de refus             |      +--------------------------+
| [Enregistrer] [Annuler]       |
+------------------------------+
~~~

Les contrôles et les ancrages sont fournis. Vérifier leur redimensionnement et leur réouverture dans le designer sous Windows. Ne pas reconstruire les formulaires à partir de zéro.

## Contrats fonctionnels communs

- Créer et modifier via WinForms en réutilisant les services fournis. Le numéro reste verrouillé en modification. Annuler la saisie n’écrit rien; un refus garde les champs et affiche le motif. Relire après succès.
- Filtrer, trier puis paginer : décalage `Debut` depuis zéro, taille de 1 à 50, page hors résultats vide. Les filtres sont conservés en changeant de page. Appliquer de nouveaux critères ou un autre tri ramène à zéro.
- Écrire `TriParNumero` et `TriSecondaire` derrière `IStrategieTri` dans Application. Le contexte utilise le contrat, sans tester le type concret. Le second ordre est le total, le prix ou le début, croissant, puis le numéro croissant. Une troisième stratégie de test doit pouvoir être injectée sans modifier le contexte.
- Compléter `ServiceLot.Rechercher` : joindre la page aux références par clé et projeter les lignes. Une référence absente affiche « Non renseigné » et conserve la ligne. Les dépôts de références et leurs validations sont fournis.
- Employer LINQ pour les filtres, tris, existence, jointure, projection et pagination. Les effets de bord peuvent utiliser des boucles. Matérialiser les résultats avant retour.
- Préserver les protections fournies : remplacement absent refusé, numéros historiques réservés, copies des objets mutables, écritures refusées sans modification des octets, relecture par une nouvelle instance. Les incidents techniques ne deviennent pas des résultats vides.

### MVC et Observateur à implanter

Construire `ControleurLot`, testable sans fenêtre, avec injection de `IServiceLot`, `ModeleLot` et `IVueLot`. Les formulaires manipulent les contrôles et DTO; le contrôleur orchestre les appels Application. Les formulaires ne lisent pas JSON et ne construisent pas les entités du domaine.

Construire `ModeleLot : IObservable<OperationDto>`. La liste principale et le suivi implantent `IObserver<OperationDto>`, directement ou par un adaptateur mince. Après chaque écriture réussie, publier une seule fois le même instantané immuable aux deux vues. La liste demande son actualisation au contrôleur. Le suivi affiche le type, le numéro et le détail. Aucun événement n’est publié après refus, annulation de saisie ou incident d’écriture.

À l’ouverture du suivi, afficher le dernier instantané puis s’abonner. À sa fermeture, libérer l’abonnement `IDisposable`; la liste reste abonnée. Réouvrir sans doubler les notifications. Les services ne dépendent pas des formulaires. Le domaine ne dépend d’aucune autre couche.

## Tâches par lot

Réaliser les [tâches communes](#contrats-fonctionnels-communs), puis appliquer les consignes de votre lot. Dans les trois lots, le filtrage et la pagination de départ fonctionnent déjà dans les dépôts mémoire et JSON. Ajouter les deux stratégies de tri et compléter la jointure dans `ServiceLot.Rechercher`. Pour un filtre de montant, une valeur absente désactive ce filtre.

### Commandes

**Fourni**

- Le formulaire permet de saisir les lignes avec leur libellé, leur prix unitaire et leur quantité. Les services permettent de créer, modifier et confirmer un brouillon. La confirmation exige des lignes valides et non vides. Une commande confirmée reste consultable et sa modification est refusée. Les prix excluent les taxes, les frais et le pourboire.
- La recherche filtre par état et total minimum inclusif.

**À faire**

- Trier par numéro ou par total croissant, puis par numéro croissant. Joindre le **canal** par numéro de commande. Dans le suivi, afficher le total et l’état.
- Implanter la **duplication** dans `ServiceLot.ExecuterAction` et raccorder le bouton. Créer un brouillon sous un numéro positif et libre, avec une collection de lignes indépendante. Une modification de la copie laisse la source intacte. Refuser une source absente ou confirmée et un nouveau numéro invalide ou déjà pris. Joindre le canal de la copie à partir de son propre numéro.
- Raccorder aussi le bouton de confirmation au service fourni.

Dans `atelier.json`, le brouillon 102 contient Salade à 8,00 $. Dupliquer vers 105, modifier 105 et vérifier que 102 conserve son total. La duplication de 101, qui est confirmée, doit être refusée.

### Menu

**Fourni**

- Le formulaire recueille le nom et le prix. Les services créent et modifient les articles. Un nom blanc ou un prix négatif est refusé. Un prix de 0,00 $ est valide.
- Le domaine fournit `ElementMenu.Retirer()`. Les anciens fichiers JSON où `EstRetire` est absent se lisent avec l’état actif.
- La recherche filtre par fragment de nom, prix maximum inclusif et inclusion de l’historique. Le fragment ignore la casse et les espaces à ses extrémités. Un fragment vide désigne tous les noms.

**À faire**

- Trier par numéro ou par prix croissant, puis par numéro croissant. Joindre la **catégorie** par numéro d’article. Dans le suivi, afficher le nom et l’état.
- Implanter le **retrait** dans `ServiceLot.ExecuterAction` et raccorder le bouton. Réutiliser `ElementMenu.Retirer()` et persister l’état retiré. L’article quitte la liste active, reste dans l’historique et garde son numéro réservé. Vérifier le refus d’un second retrait et celui d’une modification d’un article retiré.

Retirer l’article 1. Il doit disparaître de la liste active et rester dans l’historique après relecture JSON. La réutilisation du numéro 1 doit être refusée. L’article 3 conserve son prix de 0,00 $ et affiche « Non renseigné » si sa catégorie manque.

### Réservations

**Fourni**

- Le formulaire recueille la ressource, le nombre de personnes de 1 à 12, le début et la fin. Les services de création et de modification vérifient les conflits entre réservations actives sur la même ressource. Les intervalles sont semi-ouverts. En modification, l’entrée de même numéro est exclue de la vérification.
- Le domaine fournit `Reservation.Annuler()`. Les anciens fichiers JSON où `EstAnnulee` est absent se lisent avec l’état actif. La lecture d’un fichier contenant deux réservations actives en conflit produit un incident technique.
- La recherche filtre par ressource facultative, période et inclusion de l’historique. Un filtre de ressource vide ou composé seulement d’espaces désigne toutes les ressources. Une autre valeur est comparée avec `StringComparison.Ordinal`, sans changer la casse ni les espaces. Pour une période valide, la recherche retient les chevauchements et exclut la simple contiguïté. L’historique inclut les réservations actives et annulées. Sans ce filtre, seules les actives sont retenues.

**À faire**

- Trier par numéro ou par début croissant, puis par numéro croissant. Joindre le **lieu** par nom de ressource. Dans le suivi, afficher la ressource et l’état.
- Implanter l’**annulation** dans `ServiceLot.ExecuterAction` et raccorder le bouton. Réutiliser `Reservation.Annuler()` et persister l’état annulé. La réservation quitte la liste active, reste dans l’historique et garde son numéro réservé. Vérifier que la période libérée peut être utilisée sous un nouveau numéro. Une deuxième annulation et toute modification de la réservation annulée doivent être refusées.

Dans `atelier.json`, prolonger la réservation 301 jusqu’à 11 h 30 doit être refusé à cause de 303. Annuler 301, relire le JSON puis réserver Salle A de 10 h à 11 h sous un nouveau numéro. Le numéro 301 reste réservé.

## Réusinage obligatoire en S13

Le commit **avant réusinage** doit déjà contenir une recherche fonctionnelle dans les deux dépôts, avec les deux tris et les tests qui vérifient les résultats et l’ordre des pages. Le commit **après réusinage** doit utiliser une même requête pour le filtrage, le tri et la pagination dans les dépôts mémoire et JSON. Isoler la lecture, la validation et la conversion JSON dans un composant distinct du dépôt qui orchestre la persistance et la recherche. Conserver les formats, les résultats et les dépendances externes.

Joindre les deux commits, les résultats des mêmes tests et un schéma de responsabilités annoté. Renommer une méthode ou déplacer du code sans supprimer la duplication ne suffit pas.

## Tests et preuves

Conserver les tests initiaux. Ajouter des tests sur l’action du lot, la jointure manquante, les deux tris, les égalités, les pages limites et la parité mémoire/JSON. Vérifier refus sans mutation et action persistée avec une nouvelle instance JSON.

Construire directement les sujets testés. Avec une doublure manuelle, vérifier séparément une valeur retournée et une interaction. Avec deux observateurs espions, vérifier notification unique, absence après refus et désabonnement d’un seul observateur. Les tests doivent échouer si le comportement qu’ils prétendent vérifier est retiré.

| Jalon | Travail |
|---|---|
| S11 | Prise en main, contrats, début du raccordement |
| S12 | Contrat générique, tris, jointure, MVC/Observateur, première PR |
| S13 | Action, réusinage, seconde PR, validation et intégration |

Les activités de dépôt générique et de PR de S12 se font directement dans le TP. La simulation de conflit Git reste séparée.

## Deux revues et contrôle en classe

Chaque personne ouvre et relit deux PR, en S12 puis S13, de `fonctionnalite/*` vers `dev`. En binôme, revue réciproque; à trois, revue circulaire. Le relecteur vérifie le diff et un scénario, motive sa décision, puis revalide le dernier commit après correction. Après fusion, relancer les tests sur `dev`.

En S13, présenter son lot pendant **2–3 minutes**, pendant que les autres travaillent : action avec deux vues, refus conservant la saisie, fermeture et réouverture du suivi. L’enseignant consigne ce qu’il a observé. Documenter un retour client concret, le correctif et sa vérification avant remise. Une nouvelle observation est nécessaire si le comportement montré change ensuite.

## Remise

Livrer les lots autonomes, tests et données, `AUTHORS.md`, le diagramme Mermaid d’équipe et une procédure de compilation/lancement au commit final. Chaque lot possède un `JOURNAL.md` bref reliant les critères aux fichiers, tests, PR et commits. Indiquer explicitement le code fourni et le travail ajouté. Ne pas livrer `bin` ni `obj`.

La [grille](GRILLE.md) distingue les points individuels et communs. Un lot d’un coéquipier défaillant ne retire pas de points individuels.
