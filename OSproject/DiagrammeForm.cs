using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OSproject
{
    public partial class DiagrammeForm : Form
    {
        public DiagrammeForm(List<Processus> list)
        {
            InitializeComponent();
            int loc_init = 30;
            Label lvlinit=new Label();
            int wtest = 0;
            int tat = 0,tam;
            foreach (Processus item in list)
            {
                //calcul des temps d'attente moyen/total
                tat += item.tempsattente;



                Label lbl = new Label();

                lbl.Location = new Point(loc_init, 120);
                //loc_init = item.DureeProcessus+loc_init+1;
                lbl.Left = lvlinit.Right;
               
                lbl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
                lbl.ForeColor = Color.White;
                lbl.Text = item.NomProcessus;
                lbl.Font = new Font("Segoe UI", 11, FontStyle.Bold);
                lbl.AutoSize = false;
                lbl.Height = 40;
                lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                lbl.Width = item.DureeProcessus * 15;
                lbl.MouseEnter+= delegate { lbl.BackColor = Color.FromArgb(174, 180, 183); };
                lbl.MouseLeave+= delegate { lbl.BackColor = Color.FromArgb(55, 71, 79); };
                lbl.Click += delegate { 
                    tempsatt.Text = item.tempsattente.ToString();
                    textBox1.Text=item.tempssejour.ToString();
                    textBox3.Text = item.entree.ToString();
                    textBox2.Text = item.sortie.ToString();
                };
                this.Controls.Add(lbl);
                lvlinit = lbl;
                wtest += lbl.Width;
                

            }

            tam = tat / list.Count();
            label2.Text += tam;
            label4.Text += tat;
            

        }
        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;

        ///
        /// Handling the window messages
        ///
        protected override void WndProc(ref Message message)
        {
            base.WndProc(ref message);

            if (message.Msg == WM_NCHITTEST && (int)message.Result == HTCLIENT)
                message.Result = (IntPtr)HTCAPTION;
        }

        private void DiagrammeForm_Load(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
