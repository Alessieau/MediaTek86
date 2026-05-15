using MediaTek86.dal;
using MediaTek86.vue;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MediaTek86.controleur
{
    public class Controle
    {
        private FrmLogin laVueLogin;

        public Controle()
        {
            // On instancie la vue et on lui passe le controleur
            laVueLogin = new FrmLogin(this);
            laVueLogin.ShowDialog();
        }

        /// <summary>
        /// Appelée par le bouton de connexion de la vue
        /// </summary>
        public void TentativeConnexion(string login, string mdp)
        {
            // On demande à la DAL de vérifier les identifiants
            if (AccesDonnees.VerifAuthentification(login, mdp))
            {
                laVueLogin.Hide(); // On cache le login
                FrmMain laVueMain = new FrmMain(this);

                // On ouvre la fenêtre principale en mode bloquant
                laVueMain.ShowDialog();

                // Dès que FrmMain est fermée (= Déconnexion) :
                laVueLogin.ViderChamps(); // On vide les cases
                laVueLogin.Show(); // On réaffiche le login
            }
            else
            {
                MessageBox.Show("Identifiants incorrects !", "Erreur de connexion"); //
            }
        }

        /// <summary>
        /// Récupère la liste des personnels via la DAL
        /// </summary>
        public List<modele.Personnel> GetLePersonnel()
        {
            return AccesDonnees.GetLePersonnel();
        }

        public List<modele.Service> GetLesServices()
        {
            return AccesDonnees.GetLesServices();
        }

        public void AjouterPersonnel(modele.Personnel personnel)
        {
            // On demande l'ajout
            AccesDonnees.AddPersonnel(personnel);
        }

        public void SupprimerPersonnel(modele.Personnel personnel)
        {
            AccesDonnees.DeletePersonnel(personnel);
        }

        public void OuvrirAbsences(modele.Personnel personnel)
        {
            // On appelle la vue en lui passant 'this' (le contrôleur) et le personnel choisi
            FrmAbsences laVueAbsences = new FrmAbsences(this, personnel);
            laVueAbsences.ShowDialog();
        }

        public void ModifierPersonnel(modele.Personnel personnel)
        {
            AccesDonnees.UpdatePersonnel(personnel);
        }

        public List<modele.Motif> GetLesMotifs() => AccesDonnees.GetLesMotifs();

        public List<modele.Absence> GetLesAbsences(int idpersonnel)
        {
            return AccesDonnees.GetLesAbsences(idpersonnel);
        }
        public void AjouterAbsence(modele.Absence nouvelleAbsence)
        {
            AccesDonnees.AddAbsence(nouvelleAbsence);
        }

        public void SupprimerAbsence(modele.Absence absenceASupprimer)
        {
            AccesDonnees.DeleteAbsence(absenceASupprimer);
        }

        public void ModifierAbsence(modele.Absence absenceAModifier)
        {
            AccesDonnees.UpdateAbsence(absenceAModifier);
        }

    }
}

/* public void TentativeConnexion(string login, string mdp)
{
    if (AccesDonnees.VerifAuthentification(login, mdp))
    {
   
         TEST échoué : J'avais fait laVueMain.Show() sans cacher laVueLogin.
         Résultat : On se retrouvait avec deux fenêtres ouvertes en même temps.
        
laVueLogin.Hide();
FrmMain laVueMain = new FrmMain();
laVueMain.ShowDialog();
    }
    else
{
    System.Windows.Forms.MessageBox.Show("Identifiants incorrects !");
}
}
 */