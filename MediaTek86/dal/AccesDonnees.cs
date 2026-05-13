using System;
using System.Collections.Generic;
using MediaTek86.bddmanager;

namespace MediaTek86.dal
{
    // Classe de liaison entre le code et la base de données
    public class AccesDonnees
    {
        // On définit la chaine de connexion ici comme ca on pourra la changer facilement
        // On vien utiliser l'utilisateur créé à l'étape 1 du projet soit A2 avec le mot de passe associé. 
        private static string chaineConnexion = "server=localhost;user=A2;password=mdp!;database=gestion_absences;";

        // On crée un objet manager pour utiliser les méthodes de bddmanager
        private static BddManager manager = BddManager.GetInstance();

        /// <summary>
        /// Méthode simple qui permet de récupérer la chaine de connexion
        /// </summary>
        /// <returns>La chaine de connexion</returns>
        public static string GetChaineConnexion()
        {
            string laChaine = chaineConnexion;
            return laChaine;
        }
    }
}