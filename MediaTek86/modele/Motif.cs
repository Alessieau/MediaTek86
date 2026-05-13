namespace MediaTek86.modele
{
    /// <summary>
    /// Représente la table "motif" (c'est pour les absences)
    /// </summary>
    public class Motif
    {
        private int idmotif;
        private string libelle;

        public Motif(int idmotif, string libelle)
        {
            this.idmotif = idmotif;
            this.libelle = libelle;
        }

        public int Idmotif { get => idmotif; set => idmotif = value; }
        public string Libelle { get => libelle; set => libelle = value; }
    }
}