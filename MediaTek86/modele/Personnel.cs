namespace MediaTek86.modele
{
    /// <summary>
    ///On vient déclarer les informations pour le personnel
    /// </summary>
    public class Personnel
    {
        private int idpersonnel;
        private int idservice;
        private string nom;
        private string prenom;
        private string tel;
        private string mail;

        public Personnel(int idpersonnel, int idservice, string nom, string prenom, string tel, string mail)
        {
            this.idpersonnel = idpersonnel;
            this.idservice = idservice;
            this.nom = nom;
            this.prenom = prenom;
            this.tel = tel;
            this.mail = mail;
        }

        public int Idpersonnel { get => idpersonnel; set => idpersonnel = value; }
        public int Idservice { get => idservice; set => idservice = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public string Tel { get => tel; set => tel = value; }
        public string Mail { get => mail; set => mail = value; }
    }
}