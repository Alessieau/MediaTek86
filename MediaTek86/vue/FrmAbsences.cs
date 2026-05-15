using MediaTek86.modele;
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
    public partial class FrmAbsences : Form
    {
        private controleur.Controle leControleur;
        private modele.Personnel lePersonnel;
        public FrmAbsences(controleur.Controle controleur, modele.Personnel personnel)
        {
            InitializeComponent();
            this.leControleur = controleur;
            this.lePersonnel = personnel;
            this.Text = "Absences de " + lePersonnel.Nom + " " + lePersonnel.Prenom;
            RemplirComboBoxMotifs();
            RemplirListeAbsences();
        }

        private void RemplirListeAbsences()
        {
            List<modele.Absence> lesAbsences = leControleur.GetLesAbsences(lePersonnel.Idpersonnel);
            dgvAbsences.DataSource = lesAbsences.OrderByDescending(a => a.Datedebut).ToList();
        }

        private void RemplirComboBoxMotifs()
        {
            // On charge les motifs (Cas n°6)
            cboMotifs.DataSource = leControleur.GetLesMotifs();
            cboMotifs.DisplayMember = "Libelle";
            cboMotifs.ValueMember = "Idmotif";
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            // 1. Vérification chronologique (Scénario 4b du PDF)
            if (dtpFin.Value < dtpDebut.Value)
            {
                MessageBox.Show("La date de fin ne peut pas être antérieure à la date de début.");
                return;
            }

            // 2. Vérification du chevauchement (Scénario 4c du PDF)
            if (ChevauchementAbsence(dtpDebut.Value, dtpFin.Value))
            {
                MessageBox.Show("Une absence est déjà programmée dans ce créneau.");
                return;
            }

            // 3. Enregistrement (Scénario nominal)
            if (cboMotifs.SelectedValue != null)
            {
                modele.Absence nouvelle = new modele.Absence(lePersonnel.Idpersonnel, dtpDebut.Value, dtpFin.Value, (int)cboMotifs.SelectedValue);
                leControleur.AjouterAbsence(nouvelle); // On enregistre
                RemplirListeAbsences(); // On rafraîchit le tableau
                MessageBox.Show("Absence ajoutée !");
            }
        }

        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            if (dgvAbsences.CurrentRow != null)
            {
                // On Demande la confirmation 
                if (MessageBox.Show("Supprimer cette absence ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    modele.Absence abs = (modele.Absence)dgvAbsences.CurrentRow.DataBoundItem;
                    leControleur.SupprimerAbsence(abs);
                    RemplirListeAbsences();
                }
            }
        }

        private void dgvAbsences_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAbsences.CurrentRow != null)
            {
                modele.Absence abs = (modele.Absence)dgvAbsences.CurrentRow.DataBoundItem;
                dtpDebut.Value = abs.Datedebut;
                dtpFin.Value = abs.Datefin;
                cboMotifs.SelectedValue = abs.Idmotif;
            }
        }
        private void btnModifier_Click(object sender, EventArgs e)
        {
            // On vérifie qu'une absence est bien sélectionnée
            if (dgvAbsences.CurrentRow != null)
            {
                if (dtpFin.Value < dtpDebut.Value)
                {
                    MessageBox.Show("La date de fin ne peut pas être antérieure à la date de début.", "Erreur de saisie");
                    return;
                }

                if (cboMotifs.SelectedValue != null)
                {
                    modele.Absence absModifiee = new modele.Absence(lePersonnel.Idpersonnel, dtpDebut.Value, dtpFin.Value, (int)cboMotifs.SelectedValue);

                    if (MessageBox.Show("Voulez-vous enregistrer les modifications ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        leControleur.ModifierAbsence(absModifiee);
                        RemplirListeAbsences();
                    }
                }
            }
        }
        private bool ChevauchementAbsence(DateTime debut, DateTime fin)
        {
            foreach (DataGridViewRow row in dgvAbsences.Rows)
            {
                modele.Absence abs = (modele.Absence)row.DataBoundItem;

                // Logique mathématique d'un chevauchement de dates
                if (debut <= abs.Datefin && fin >= abs.Datedebut)
                {
                    return true;
                }
            }
            return false;
        }

        // Pour le bouton retour (n'oublie pas de le lier dans le designer)
        private void btnRetour_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}

/* TEST échoué : Possibilité d'ajouter une absence avec une date de fin 

   antérieure à la date de début (Scénario 4b)

   Cause : Manque de vérification logique dans le bouton Ajouter

   Solution : Ajout d'un test 'if (dtpFin.Value < dtpDebut.Value)' avant l'enregistrement

 */