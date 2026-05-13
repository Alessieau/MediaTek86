using System;

namespace MediaTek86.modele
{
    /// <summary>
    /// Classe métier représentant une absence d'un membre du personnel
    /// </summary>
    public class Absence
    {
        private int idpersonnel;
        private DateTime datedebut;
        private DateTime datefin;
        private int idmotif;

        public Absence(int idpersonnel, DateTime datedebut, DateTime datefin, int idmotif)
        {
            this.idpersonnel = idpersonnel;
            this.datedebut = datedebut;
            this.datefin = datefin;
            this.idmotif = idmotif;
        }

        public int Idpersonnel { get => idpersonnel; set => idpersonnel = value; }
        public DateTime Datedebut { get => datedebut; set => datedebut = value; }
        public DateTime Datefin { get => datefin; set => datefin = value; }
        public int Idmotif { get => idmotif; set => idmotif = value; }
    }
}