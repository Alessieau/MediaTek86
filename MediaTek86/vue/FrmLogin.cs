using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaTek86.vue
{
    public partial class FrmLogin : Form
    {
        private controleur.Controle leControleur;

        /// <summary>
        /// Constructeur de la fenêtre de login
        /// </summary>
        public FrmLogin(controleur.Controle controleur)
        {
            InitializeComponent();
            this.leControleur = controleur;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // On récupère la saisie
            string log = txtLogin.Text;
            string pass = txtMdp.Text;

            // On délègue la vérification au controleur
            leControleur.TentativeConnexion(log, pass);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnQuitter_Click(object sender, EventArgs e)
        {
            // Ferme proprement toute l'app
            Application.Exit();
        }

        /// <summary>
        /// Vide les zones de saisie pour une nouvelle tentative
        /// </summary>
        public void ViderChamps()
        {
            // Remplace les textes de login et mt de passe
            txtLogin.Text = "";
            txtMdp.Text = "";
            txtLogin.Focus(); 
        }
    }
}
