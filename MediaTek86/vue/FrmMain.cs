using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaTek86.vue
{
    public partial class FrmMain : Form
    {
        // On déclare le contrôleur pour pouvoir l'utiliser partout dans la classe
        private controleur.Controle leControleur;

        public FrmMain(controleur.Controle controleur)
        {
            InitializeComponent();
            this.leControleur = controleur;

            // On lance le remplissage dès l'ouverture
            RemplirListePersonnel();
            RemplirComboBoxServices();
        }

        private void RemplirListePersonnel()
        {
            /* TEST échoué : J'avais essayé de faire dgvPersonnel.DataSource = laListe;
              sans avoir rglé les propiétés 'DataPropertyName'
              Résultat : Le tableau affichait le bon nombre de lignes mais elles étaient vides
              Solution : Mapper chaque colone du DataGridView sur les propriété (Nom, Prenom etc) de la classe Personnel
             */

            // On récupère les données via le contrôleur
            List<modele.Personnel> laListe = leControleur.GetLePersonnel();

            // On lie la liste au DataGridView
            dgvPersonnel.DataSource = laListe;
        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            // On vérifie d'abord si un service est sélectionné
            if (cboServices.SelectedValue != null)
            {
                // On récupère l'ID du service
                int idService = (int)cboServices.SelectedValue;

                // On crée l'objet Personnel
                modele.Personnel nouveau = new modele.Personnel(0, idService, txtNom.Text, txtPrenom.Text, txtTel.Text, txtMail.Text);

                // On envoie au contrôleur
                leControleur.AjouterPersonnel(nouveau);

                // On rafraîchit l'affichage
                RemplirListePersonnel();

                // On vide les champs (optionnel)
                txtNom.Text = "";
                txtPrenom.Text = "";
                txtTel.Text = "";
                txtMail.Text = "";
            }
            else
            {
                // Petit message si l'utilisateur a oublié de choisir un service
                MessageBox.Show("Veuillez sélectionner un service avant d'ajouter un personnel.");
            }
        }

        private void btnModifier_Click(object sender, EventArgs e)
        {
            if (dgvPersonnel.CurrentRow != null && cboServices.SelectedValue != null)
            {
                // 1 : On récupère la personne sélectionnée pour avoir son ID
                modele.Personnel selectionne = (modele.Personnel)dgvPersonnel.CurrentRow.DataBoundItem;

                // 2 : On met à jour ses infos avec ce qu'il y a dans les TextBox
                selectionne.Idservice = (int)cboServices.SelectedValue;
                selectionne.Nom = txtNom.Text;
                selectionne.Prenom = txtPrenom.Text;
                selectionne.Tel = txtTel.Text;
                selectionne.Mail = txtMail.Text;

                // 3. On envoie au contrôleur
                leControleur.ModifierPersonnel(selectionne);

                // 4. On rafraîchit
                RemplirListePersonnel();
                MessageBox.Show("Modifications enregistrées !");
            }
        }


        /// <summary>
        /// Remplit la liste déroulante des services
        /// </summary>
        private void RemplirComboBoxServices()
        {
            /* TEST échoué : Erreur 'Le nom RemplirComboBoxServices n'existe pas'.
              Cause : J'appelais la méthode dans le constructeur avant même de l'avoir créée.
              Solution : Définition de la méthode qui récupère les services via le contrôleur.
             */

            // On demande la liste au contrôleur
            List<modele.Service> lesServices = leControleur.GetLesServices();

            // On lie les données à la ComboBox
            cboServices.DataSource = lesServices;

            // On définit ce qui est affiché et ce qui est caché
            cboServices.DisplayMember = "Nom"; // On affiche le nom du service
            cboServices.ValueMember = "Idservice"; // On garde l'ID en valeur invisible
        }



        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            // On vérifie qu'une ligne est bien sélectionnée
            if (dgvPersonnel.CurrentRow != null)
            {
                // On récupère l'objet Personnel lié à la ligne sélectionnée
                modele.Personnel lePersonnel = (modele.Personnel)dgvPersonnel.CurrentRow.DataBoundItem;

                // Petite confirmation pour éviter les bêtises
                if (MessageBox.Show("Voulez-vous vraiment supprimer " + lePersonnel.Nom + " ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    /* TEST échoué : Erreur de suppression.
                      Cause : J'essayais de supprimer un personnel qui avait des absences enregistrées.
                      Solution : (Pour ton dossier) Normalement, il faudrait supprimer les absences avant, 
                      ou utiliser une suppression en cascade en SQL.
                     */
                    leControleur.SupprimerPersonnel(lePersonnel);
                    RemplirListePersonnel(); // On rafraîchit le tableau
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un membre du personnel dans la liste.");
            }
        }

        private void cboServices_SelectedIndexChanged(object sender, EventArgs e)
        {
            // On récupère la liste complète
            List<modele.Personnel> laListe = leControleur.GetLePersonnel();

            if (cboServices.SelectedValue != null && cboServices.SelectedValue is int)
            {
                int idFiltre = (int)cboServices.SelectedValue;

                // On filtre la liste pour ne garder que ceux du service choisi
                // (Nécessite 'using System.Linq;' en haut du fichier)
                List<modele.Personnel> listeFiltree = laListe.Where(p => p.Idservice == idFiltre).ToList();

                // On affiche la liste filtrée
                dgvPersonnel.DataSource = listeFiltree;
            }
        }

        private void btnAbsences_Click(object sender, EventArgs e)
        {
            if (dgvPersonnel.CurrentRow != null)
            {
                modele.Personnel lePersonnel = (modele.Personnel)dgvPersonnel.CurrentRow.DataBoundItem;
                leControleur.OuvrirAbsences(lePersonnel);
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un membre du personnel pour gérer ses absences.");
            }
        }

        private void btnDeconnexion_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Voulez-vous vous déconnecter ?", "Déconnexion", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // En fermant cette fenêtre, le contrôle revient à la méthde
                this.Close();
            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {

        }

        private void dgvPersonnel_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (!dgvPersonnel.Focused)
                {
                    return;
                }

                // On vérifie d'abord si la grille a des lignes avant de toucher à CurrentRow 
                if (dgvPersonnel.Rows.Count > 0 && dgvPersonnel.CurrentRow != null && dgvPersonnel.CurrentRow.Index != -1)
                {
                    if (dgvPersonnel.CurrentRow.DataBoundItem != null)
                    {
                        modele.Personnel lePersonnel = (modele.Personnel)dgvPersonnel.CurrentRow.DataBoundItem;
                        txtNom.Text = lePersonnel.Nom;
                        txtPrenom.Text = lePersonnel.Prenom;
                        txtTel.Text = lePersonnel.Tel;
                        txtMail.Text = lePersonnel.Mail;

                        cboServices.SelectedValue = lePersonnel.Idservice;
                    }
                }
            }
            catch (Exception)
            {
            }
        }


        private void dgvPersonnel_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // On laisse vide pour éviter que la boîte de dialogue s'affiche en boucle
            e.ThrowException = false;
        }
    }
}


/*
private void RemplirComboBoxServices()
{
     TEST échoué : Erreur 'Le nom cboServices n'existe pas'
     Cause : J'ai oublié de renommer la ComboBox dans le Designer
     Elle s'appelait encore comboBox1 alors que mon code cherchait cboServices
     Solution : Changement du (Name) en cboServices dans les propriétés
     

List<modele.Service> lesServices = leControleur.GetLesServices();

    cboServices.DataSource = lesServices;
    cboServices.DisplayMember = "Nom";
    cboServices.ValueMember = "Idservice";
}
*/

/* TEST échoué : System.NullReferenceException sur cboServices.SelectedValue
  Cause : L'utilisateur cliquait sur Ajouter sans avoir sélectionné de service, 
  ou la ComboBox n'était pas encore initialisée.
  Solution : Ajout d'une condition 'if (cboServices.SelectedValue != null)' 
  pour empêcher le cast d'une valeur nulle.
 */

/* TEST échoué : Le bouton Modifier créait des doublons
  Cause : J'utilisais une requête 'INSERT' au lieu de 'UPDATE' dans la DAL, 
 ce qui ajoutait une novele ligne à chaque fois
 Solution : Création d'une méthode UpdatePersonnel utilisant la clause WHERE sur l'idpersonnel
 */

/* TEST échoué : La ComboBox de service ne changeait pas l'affichage du tableau
 Cause : Aucun code n'était lié à l'événement de changement de sélection de la liste
 Solution : Impléentation du filtrage LINQ dans l'événement SelectedIndexChanged
 */

/* TEST échoué: L'utilisateur devait retaper toutes les informations pour modifier un profil
  Cause : Les TextBox ne se mettaient pas à jour lors du clic sur le tableau
  Solution : Utilisation de l'évéement SelectionChanged du DataGridView pour 
  pré-remplir automatiquement les champs avec les propriétés de l'objet sélectionné
 */

/* TEST échoué : Apparition d'une System.IndexOutOfRangeException en boucle lors du filtrage
 Cause : L'événement SelectionChanged tentait d'accéder à DataBoundItem alors que le 
 CurrencyManager de WinForms était en trai de reconstruire l'index du tableau suite au changement de DataSource
 Solution : Ajout de vérifications de nullité sur CurrentRow et gestion de l'événement DataError pour empêcher le blocage de l'UI
 */