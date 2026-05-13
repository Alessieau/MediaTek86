namespace MediaTek86.modele
{
    /// <summary>
    ///  Représente la table "service"
    /// </summary>
    public class Service
    {
        private int idservice;
        private string nom;

        public Service(int idservice, string nom)
        {
            this.idservice = idservice;
            this.nom = nom;
        }

        public int Idservice { get => idservice; set => idservice = value; }
        public string Nom { get => nom; set => nom = value; }
    }
}