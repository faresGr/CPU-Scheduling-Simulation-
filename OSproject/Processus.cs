using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSproject
{
    public class Processus
    {
        public String NomProcessus { get; set; }
        public int arrivProcessus { get; set; }
        public int DureeProcessus { get; set; }
        public int priorité { get; set; }
        public int tempsattente { get; set; }
        public int tempssejour { get; set; }
        public int entree { get; set; }
        public int sortie { get; set; }

        public Processus(String NomProcessus,int arrivProcessus,int DureeProcessus)
        {
            this.arrivProcessus = arrivProcessus;
            this.DureeProcessus = DureeProcessus;
            this.NomProcessus = NomProcessus;

        }

        public Processus(String NomProcessus, int arrivProcessus, int DureeProcessus,int priorité)
        {
            this.arrivProcessus = arrivProcessus;
            this.DureeProcessus = DureeProcessus;
            this.NomProcessus = NomProcessus;
            this.priorité = priorité;

        }


    }
}
