using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OSproject
{
    public partial class menu : UserControl
    {
        private List<Processus> list;



        public menu()
        {
            InitializeComponent();
             list = new List<Processus>();
            
        }

       
        public List<Processus> SJF(List<Processus> list)
        {
            List<Processus> Sortedlist = list.OrderBy(o => o.arrivProcessus).ThenBy(n=>n.DureeProcessus).ToList();
            Processus init = Sortedlist[0];
            List<Processus> queue = new List<Processus>();
            List<Processus> list2 = new List<Processus>();
      
            
            int x = Sortedlist[0].arrivProcessus+Sortedlist[0].DureeProcessus;
            Sortedlist[0].tempsattente = 0;
            list2.Add(Sortedlist[0]);
            
            for(int i=1;i<Sortedlist.Count;i++)
            {
                
                
                IEnumerable<Processus> filteringQuery =
                     from proc in Sortedlist
                     where (proc.arrivProcessus<= x) && (!list2.Contains(proc))
                     select proc;
                    queue = filteringQuery.ToList<Processus>();
                 
                 
            queue = queue.OrderBy(o => o.DureeProcessus).ToList();

                if (queue.Count == 0)
                {
                    Sortedlist[i].tempsattente = 0;
                    queue[0].tempssejour = queue[0].tempsattente + queue[0].DureeProcessus;
                    Sortedlist[i].entree = Sortedlist[i].arrivProcessus;
                    Sortedlist[i].sortie = Sortedlist[i].entree + Sortedlist[i].DureeProcessus;
                    list2.Add(Sortedlist[i]);
                    
                }
                else
                {
                    MessageBox.Show((queue[0].arrivProcessus + " " + x).ToString());
                    queue[0].tempsattente = x-queue[0].arrivProcessus;
                    queue[0].tempssejour = queue[0].tempsattente + queue[0].DureeProcessus;
                    queue[0].entree = queue[0].arrivProcessus + queue[0].tempsattente;
                    queue[0].sortie = queue[0].entree + queue[0].DureeProcessus;
                    list2.Add(queue[0]);

                    if ((queue[0].arrivProcessus > x))
                    {
                        x += queue[0].DureeProcessus + (queue[0].arrivProcessus - x);
                        
                    }
                    else x += queue[0].DureeProcessus;
                    
                }
                    queue.Clear();
                



            }
            return list2;
        }

        public List<Processus> priority(List<Processus> list)
        {
            List<Processus> Sortedlist = list.OrderBy(o => o.arrivProcessus).ThenBy(n => n.priorité).ToList();
            Processus init = Sortedlist[0];
            List<Processus> queue = new List<Processus>();
            List<Processus> list2 = new List<Processus>();


            int x = Sortedlist[0].arrivProcessus + Sortedlist[0].DureeProcessus;
            list2.Add(Sortedlist[0]);

            for (int i = 1; i < Sortedlist.Count; i++)
            {


                IEnumerable<Processus> filteringQuery =
                     from proc in Sortedlist
                     where (proc.arrivProcessus <= x) && (!list2.Contains(proc))
                     select proc;
                queue = filteringQuery.ToList<Processus>();


                queue = queue.OrderBy(o => o.priorité).ToList();

                if (queue.Count == 0)
                {
                    Sortedlist[i].tempssejour = Sortedlist[i].DureeProcessus;
                    Sortedlist[i].tempsattente = 0;
                    Sortedlist[i].entree = Sortedlist[i].arrivProcessus;
                    Sortedlist[i].sortie = Sortedlist[i].entree + Sortedlist[i].DureeProcessus;
                    list2.Add(Sortedlist[i]);
                }

                else
                {
                    queue[0].tempsattente = x - queue[0].arrivProcessus;
                    queue[0].tempssejour = queue[0].tempsattente + queue[0].DureeProcessus;
                    queue[0].entree = queue[0].arrivProcessus + queue[0].tempsattente;
                    queue[0].sortie = queue[0].entree + queue[0].DureeProcessus;
                    list2.Add(queue[0]);

                    if ((queue[0].arrivProcessus > x))
                    {
                        x += queue[0].DureeProcessus + (queue[0].arrivProcessus - x);
                        
                    }
                    else x += queue[0].DureeProcessus;
                    
                }
                queue.Clear();




            }
            return list2;
        }

        public List<Processus> FCFS(List<Processus> list)
        {
            List<Processus> Sortedlist = list.OrderBy(o => o.arrivProcessus).ThenBy(r=>r.DureeProcessus).ToList();
            int ta = Sortedlist[0].arrivProcessus;
            foreach (Processus item in Sortedlist)
            {
                if ((item.arrivProcessus > ta))
                {
                    item.tempsattente = 0;
                    
                    ta += item.DureeProcessus + (item.arrivProcessus - ta);

                }
                else
                {
                    item.tempsattente = ta - item.arrivProcessus;
                    ta += item.DureeProcessus;
                }
                item.tempssejour = item.tempsattente + item.DureeProcessus;
                item.entree = item.tempsattente + item.arrivProcessus;
                item.sortie = item.entree + item.DureeProcessus;

            }
            return Sortedlist;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            List<Processus> Sortedlist = new List<Processus>();
            if (comboBox1.Text == "SJF")
                Sortedlist = SJF(list);
            else if (comboBox1.Text == "FCFS")
                Sortedlist = FCFS(list);
            else if (comboBox1.Text == "Priorité")
                Sortedlist = priority(list);
            else
            {
                MessageBox.Show("Veulliez choisir un algorithme !");
                return;
            }
            DiagrammeForm diagrammeForm = new DiagrammeForm(Sortedlist);
            diagrammeForm.Show();
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            int n;
            bool isNumeric = int.TryParse(arrproc.Text,out n);
            bool isNumeric2 = int.TryParse(durproc.Text, out n);
            bool isNumeric3 = int.TryParse(textBox1.Text, out n);
            if ((!isNumeric) || (!isNumeric2))
            { MessageBox.Show("Format d'input invalide !");
                return;
            }
            DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
            row.Cells[0].Value = nomproc.Text;
            row.Cells[1].Value = arrproc.Text;
            row.Cells[2].Value = durproc.Text;
            row.Cells[3].Value = textBox1.Text;
            Processus p;
            dataGridView1.Rows.Add(row);
            if (textBox1.Text!="")
                 p = new Processus(nomproc.Text, Convert.ToInt32(arrproc.Text), Convert.ToInt32(durproc.Text), Convert.ToInt32(textBox1.Text));
            else p = new Processus(nomproc.Text, Convert.ToInt32(arrproc.Text), Convert.ToInt32(durproc.Text));
            list.Add(p);
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            
        }
    }
}
