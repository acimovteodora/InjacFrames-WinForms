using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms
{
    public partial class FrmMenadzerProdajeGlavna : Form
    {
        private KreirajPor kreirajUC;
        private IzmeniPor izmeniUC;
        private ObrisiPor obrisiUC;
        public FrmMenadzerProdajeGlavna()
        {
            InitializeComponent();
        }

        private void FrmMenadzerProdajeGlavna_Load(object sender, EventArgs e)
        {
            kreirajUC = new KreirajPor();
            panel4.Controls.Add(kreirajUC);
            kreirajUC.Visible = true;
            kreirajUC.BringToFront();
            button1.BackColor = Color.FromArgb(186, 226, 218);
            button1.ForeColor = Color.White;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                lblNaslov.Text = "Kreiranje nove porudzbine";
                kreirajUC = new KreirajPor();
                panel4.Controls.Add(kreirajUC);
                kreirajUC.Visible = true;
                kreirajUC.BringToFront();

                //izmena designa dugmica
                button1.BackColor = Color.FromArgb(186, 226, 218);
                button1.ForeColor = Color.White;

                button2.BackColor = Color.FromArgb(247, 230, 210);
                button2.ForeColor = Color.FromArgb(186, 130, 106);

                button3.BackColor = Color.FromArgb(247, 230, 210);
                button3.ForeColor = Color.FromArgb(186, 130, 106);
            }
            catch (Exception ex)
            {
                FrmPoruka f = new FrmPoruka(ex.Message);
                f.ShowDialog();
                Environment.Exit(Environment.ExitCode);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                lblNaslov.Text = "Izmena unete porudzbine";
                izmeniUC = new IzmeniPor();
                panel4.Controls.Add(izmeniUC);
                izmeniUC.Visible = true;
                izmeniUC.BringToFront();
                //izmena designa dugmica
                button2.BackColor = Color.FromArgb(186, 226, 218);
                button2.ForeColor = Color.White;

                button3.BackColor = Color.FromArgb(247, 230, 210);
                button3.ForeColor = Color.FromArgb(186, 130, 106);

                button1.BackColor = Color.FromArgb(247, 230, 210);
                button1.ForeColor = Color.FromArgb(186, 130, 106);
            }
            catch (Exception ex)
            {
                FrmPoruka f = new FrmPoruka(ex.Message);
                f.ShowDialog();
                Environment.Exit(Environment.ExitCode);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                lblNaslov.Text = "Brisanje unete porudzbine";
                obrisiUC = new ObrisiPor();
                panel4.Controls.Add(obrisiUC);
                obrisiUC.Visible = true;
                obrisiUC.BringToFront();
                //izmena designa dugmica
                button3.BackColor = Color.FromArgb(186, 226, 218);
                button3.ForeColor = Color.White;

                button2.BackColor = Color.FromArgb(247, 230, 210);
                button2.ForeColor = Color.FromArgb(186, 130, 106);

                button1.BackColor = Color.FromArgb(247, 230, 210);
                button1.ForeColor = Color.FromArgb(186, 130, 106);
            }
            catch (Exception ex)
            {
                FrmPoruka f = new FrmPoruka(ex.Message);
                f.ShowDialog();
                Environment.Exit(Environment.ExitCode);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Komunikacija.Instance.OdjaviMe();
            Environment.Exit(Environment.ExitCode);
        }
    }
}
