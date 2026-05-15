using MediaTek86.bddmanager;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

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

        /// <summary>
        /// On transforme le mot de passe en chaine SHA256
        /// </summary>
        public static string GetHashSHA256(string leMotDePasse)
        {
            // On convertit d'abord la chaine en tableau d'octets
            byte[] octets = Encoding.UTF8.GetBytes(leMotDePasse);
            SHA256 sha = SHA256.Create();

            // On calcule le hachage
            byte[] resultat = sha.ComputeHash(octets);

            // On transforme le tableau de bytes en une chaine de caractères
            string chaineHachee = "";
            for (int i = 0; i < resultat.Length; i++)
            {
                chaineHachee = chaineHachee + resultat[i].ToString("x2");
            }

            return chaineHachee;
        }

        /// <summary>
        /// On vérifie si les identifiants sont bons dans la table responsable
        /// </summary>
        public static bool VerifAuthentification(string login, string pwd)
        {
            // On hache le mdp reçu pour comparer avec la base
            string pwdHache = GetHashSHA256(pwd);

            // On prépare la requête SQL
            string requete = "select * from responsable where login = '" + login + "' and pwd = '" + pwdHache + "';";

            // On récupère le résultat via notre manager
            List<Object[]> liste = manager.ReqSelect(requete, chaineConnexion);

            // On regarde si on a trouvé une ligne
            bool trouve = false;
            if (liste.Count > 0)
            {
                trouve = true;
            }

            return trouve;
        }

        /// <summary>
        /// Récupère la liste complète du personnel depuis la BDD
        /// </summary>
        public static List<modele.Personnel> GetLePersonnel()
        {
            List<modele.Personnel> lesPersonnels = new List<modele.Personnel>();

            // On liste les colonnes explicitement pour être SUR de l'ordre des index [0, 1, 2...]
            string req = "select idpersonnel, idservice, nom, prenom, tel, mail from personnel order by nom, prenom;";

            List<Object[]> lignes = manager.ReqSelect(req, chaineConnexion);

            for (int i = 0; i < lignes.Count; i++)
            {
                Object[] uneLigne = lignes[i];

                // Maintenant, on est sûr que index 0 = id et index 1 = idservice
                int id = Convert.ToInt32(uneLigne[0]);
                int idservice = Convert.ToInt32(uneLigne[1]);
                string nom = (string)uneLigne[2];
                string prenom = (string)uneLigne[3];
                string tel = (string)uneLigne[4];
                string mail = (string)uneLigne[5];

                modele.Personnel unPersonnel = new modele.Personnel(id, idservice, nom, prenom, tel, mail);
                lesPersonnels.Add(unPersonnel);
            }
            return lesPersonnels;
        }

        public static List<modele.Service> GetLesServices()
        {
            List<modele.Service> lesServices = new List<modele.Service>();
            string req = "select idservice, nom from service order by nom;";
            List<Object[]> lignes = manager.ReqSelect(req, chaineConnexion);

            foreach (Object[] ligne in lignes)
            {
                lesServices.Add(new modele.Service(Convert.ToInt32(ligne[0]), (string)ligne[1]));
            }
            return lesServices;
        }

        /// <summary>
        /// Ajoute un nouveau personnel dans la base de données
        /// </summary>
        public static void AddPersonnel(modele.Personnel personnel)
        {
            /* TEST échoué : Erreur de syntaxe SQL sur les IDs.
            Cause : J'avais mis des ' ' autour des nombres (idservice). 
            Saut que en SQL, les nombres ne prennent pas de guillemets...
             */
            string req = "insert into personnel (idservice, nom, prenom, tel, mail) ";
            req += "values (" + personnel.Idservice + ", '" + personnel.Nom + "', '" + personnel.Prenom + "', ";
            req += "'" + personnel.Tel + "', '" + personnel.Mail + "');";

            // On utilise ReqUpdate car c'est une modification de la base
            manager.ReqUpdate(req, chaineConnexion);
        }

        public static void DeletePersonnel(modele.Personnel personnel)
        {
            // Requête simple basée sur l'ID (clé primaire)
            string req = "delete from personnel where idpersonnel = " + personnel.Idpersonnel + ";";
            manager.ReqUpdate(req, chaineConnexion);
        }

        public static void UpdatePersonnel(modele.Personnel personnel)
        {
            // On utilise UPDATE et on cible l'ID avec WHERE
            string req = "update personnel set idservice = " + personnel.Idservice + ", ";
            req += "nom = '" + personnel.Nom + "', prenom = '" + personnel.Prenom + "', ";
            req += "tel = '" + personnel.Tel + "', mail = '" + personnel.Mail + "' ";
            req += "where idpersonnel = " + personnel.Idpersonnel + ";";

            manager.ReqUpdate(req, chaineConnexion);
        }

        public static List<modele.Motif> GetLesMotifs()
        {
            List<modele.Motif> lesMotifs = new List<modele.Motif>();
            string req = "select idmotif, libelle from motif order by libelle;";
            List<Object[]> lignes = manager.ReqSelect(req, chaineConnexion);

            foreach (Object[] ligne in lignes)
            {
                lesMotifs.Add(new modele.Motif(Convert.ToInt32(ligne[0]), (string)ligne[1]));
            }
            return lesMotifs;
        }


        public static List<modele.Absence> GetLesAbsences(int idpersonnel)
        {
            List<modele.Absence> lesAbsences = new List<modele.Absence>();
            // Note le "order by datedebut desc" pour respecter la consigne du PDF
            string req = "select idpersonnel, datedebut, datefin, idmotif from absence ";
            req += "where idpersonnel = " + idpersonnel + " order by datedebut desc;";

            List<Object[]> lignes = manager.ReqSelect(req, chaineConnexion);

            foreach (Object[] ligne in lignes)
            {
                lesAbsences.Add(new modele.Absence(Convert.ToInt32(ligne[0]), (DateTime)ligne[1], (DateTime)ligne[2], Convert.ToInt32(ligne[3])));
            }
            return lesAbsences;
        }

        /// <summary>
        /// Ajoute une nouvelle absence dans la base de données
        /// </summary>
        /// <param name="absence">L'objet absence à ajouter</param>
        /// <summary>
        /// Ajoute une nouvelle absence (Cas n°6)
        /// </summary>
        public static void AddAbsence(modele.Absence absence)
        {
            // On convertit les dates au format MySQL (YYYY-MM-DD HH:MM:SS)
            string req = "insert into absence (idpersonnel, datedebut, datefin, idmotif) ";
            req += "values (" + absence.Idpersonnel + ", '" + absence.Datedebut.ToString("yyyy-MM-dd HH:mm:ss") + "', ";
            req += "'" + absence.Datefin.ToString("yyyy-MM-dd HH:mm:ss") + "', " + absence.Idmotif + ");";

            manager.ReqUpdate(req, chaineConnexion);
        }

        /// <summary>
        /// Supprime une absence (Cas n°7)
        /// </summary>
        public static void DeleteAbsence(modele.Absence absence)
        {
            // La clé primaire est (idpersonnel + datedebut)
            string req = "delete from absence where idpersonnel = " + absence.Idpersonnel + " ";
            req += "and datedebut = '" + absence.Datedebut.ToString("yyyy-MM-dd HH:mm:ss") + "';";

            manager.ReqUpdate(req, chaineConnexion);
        }

        /// <summary>
        /// Modifie une absence (Cas n°8)
        /// </summary>
        public static void UpdateAbsence(modele.Absence absence)
        {
            // On met à jour la date de fin et le motif pour une absence précise
            string req = "update absence set datefin = '" + absence.Datefin.ToString("yyyy-MM-dd HH:mm:ss") + "', ";
            req += "idmotif = " + absence.Idmotif + " where idpersonnel = " + absence.Idpersonnel + " ";
            req += "and datedebut = '" + absence.Datedebut.ToString("yyyy-MM-dd HH:mm:ss") + "';";

            manager.ReqUpdate(req, chaineConnexion);
        }
    }
}


/*public static bool VerifAuthentification(string login, string pwd)
{
    string pwdHache = GetHashSHA256(pwd);

     TEST écoué : J'avais oublié les simples quotes autour des variables
     string requete = "select * from responsable where login = " + login + " and pwd = " + pwdHache;
     Résultat : Erreur MySQL car il cherchait une colonne portant le nom du login.
     Correction : Ajout des ' ' pour entourer les chaines.

    string requete = "select * from responsable where login = '" + login + "' and pwd = '" + pwdHache + "';";

    List<Object[]> liste = manager.ReqSelect(requete, chaineConnexion);
    return (liste.Count > 0);
}
    2e soucis : 
        TEST échoué : La connexion échouait malgré le bon login/mdp.
        Cause : Le hash SHA256 dans la base de données (240be518...) ne correspondait pas 
        au hash généré par mon code C# pour le mot 'admin'.
        Solution : Mise à jour de la table 'responsable' avec le bon hash via une requête SQL UPDATE.

    3e soucis : 
          TEST échoué : Erreur System.FormatException sur Convert.ToInt32(uneLigne[1]).
          Cause : L'utilisation de 'SELECT *' renvoyait les colonnes dans l'ordre de la BDD.
          Mon code attendait l'idservice en index 1, mais recevait le 'nom' (du texte).
          Solution : Spécifier l'ordre des colonnes directement dans la requête SQL SELECT.


    TEST échoué : La liste des abseces était mélangé.

    Cause : Oubli du "ORDER BY datedebut DESC" dans la requête SQL

    Solution : Correction de la requête dans la DAL pour trier par date décroisante.

*/




