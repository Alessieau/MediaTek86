using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

// Classe pour la gestion d'accès à la database MySQL.(utilisation de Singleton pour centraliser la connexion)


namespace MediaTek86.bddmanager
{
    /// <summary>
    /// Classe technique de gestion des accès à la base de données
    /// Basée sur le modèle Singleton
    /// </summary>
    public class BddManager
    {
        // Instance unique de la classe (Singleton)
        private static BddManager instance = null;
        private MySqlConnection connection;

        /// <summary>
        /// Constructeur privé pour empêcher l'instanciation externe
        /// </summary>
        private BddManager() { }

        /// <summary>
        /// Récupération de l'instance unique
        /// </summary>
        /// <returns>Instance de BddManager</returns>
        public static BddManager GetInstance()
        {
            if (instance == null)
            {
                instance = new BddManager();
            }
            return instance;
        }

        /// <summary>
        /// Exécution d'une requête de type SELECT (lecture)
        /// </summary>
        /// <param name="txtQuery">La requête SQL</param>
        /// <param name="infoConnexion">La chaîne de connexion</param>
        /// <returns>Une liste d'objets (les lignes de la table)</returns>
        public List<Object[]> ReqSelect(string txtQuery, string infoConnexion)
        {
            List<Object[]> resultat = new List<Object[]>();
            try
            {
                connection = new MySqlConnection(infoConnexion);
                connection.Open();
                MySqlCommand command = new MySqlCommand(txtQuery, connection);
                MySqlDataReader reader = command.ExecuteReader();

                // On parcourt les lignes du résultat
                while (reader.Read())
                {
                    Object[] ligne = new Object[reader.FieldCount];
                    reader.GetValues(ligne);
                    resultat.Add(ligne);
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                // Simple affichage en console pour le debug étudiant
                Console.WriteLine("Erreur de lecture : " + ex.Message);
            }
            return resultat;
        }

        /// <summary>
        /// Exécution d'une requête de type INSERT, UPDATE ou DELETE (écriture)
        /// </summary>
        /// <param name="txtQuery">La requête SQL</param>
        /// <param name="infoConnexion">La chaîne de connexion</param>
        public void ReqUpdate(string txtQuery, string infoConnexion)
        {
            try
            {
                connection = new MySqlConnection(infoConnexion);
                connection.Open();
                MySqlCommand command = new MySqlCommand(txtQuery, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur de mise à jour : " + ex.Message);
            }
        }
    }
}