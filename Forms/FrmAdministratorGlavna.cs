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
    public partial class FrmAdministratorGlavna : Form
    {
        public FrmAdministratorGlavna()
        {
            InitializeComponent();
            lblUser.Text = Sesija.Instance.Zaposleni.ToString();
        }
        

        private void panel7_Click(object sender, EventArgs e)
        {
            try
            {
                FrmLajsne f = new FrmLajsne();
                this.Dispose();
                f.ShowDialog();
            }
            catch (Exception ex)
            {
                FrmPoruka f = new FrmPoruka(ex.Message);
                f.ShowDialog();
                Environment.Exit(Environment.ExitCode);
            }
        }

        private void panel8_Click(object sender, EventArgs e)
        {
            try
            {
                FrmKompanije f = new FrmKompanije();
                this.Dispose();
                f.ShowDialog();
            }
            catch (Exception ex)
            {
                FrmPoruka f = new FrmPoruka(ex.Message);
                f.ShowDialog();
                Environment.Exit(Environment.ExitCode);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            try
            {
                FrmLajsne f = new FrmLajsne();
                this.Dispose();
                f.ShowDialog();
            }
            catch (Exception ex)
            {
                FrmPoruka f = new FrmPoruka(ex.Message);
                f.ShowDialog();
                Environment.Exit(Environment.ExitCode);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            try
            {
                FrmKompanije f = new FrmKompanije();
                this.Dispose();
                f.ShowDialog();
            }
            catch (Exception ex)
            {
                FrmPoruka f = new FrmPoruka(ex.Message);
                f.ShowDialog();
                Environment.Exit(Environment.ExitCode);
            }
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                FrmLajsne f = new FrmLajsne();
                this.Dispose();
                f.ShowDialog();
            }
            catch (Exception ex)
            {
                FrmPoruka f = new FrmPoruka(ex.Message);
                f.ShowDialog();
                Environment.Exit(Environment.ExitCode);
            }
        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            try
            {
                FrmKompanije f = new FrmKompanije();
                this.Dispose();
                f.ShowDialog();
            }
            catch (Exception ex)
            {
                FrmPoruka f = new FrmPoruka(ex.Message);
                f.ShowDialog();
                Environment.Exit(Environment.ExitCode);
            }
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            try
            {
                FrmLajsne f = new FrmLajsne();
                this.Dispose();
                f.ShowDialog();
            }
            catch (Exception ex)
            {
                FrmPoruka f = new FrmPoruka(ex.Message);
                f.ShowDialog();
                Environment.Exit(Environment.ExitCode);
            }
        }


        private void panel7_MouseHover(object sender, EventArgs e)
        {
            panel7.BackColor = Color.FromArgb(247,230,210);
            textBox2.BackColor = Color.FromArgb(247, 230, 210);
        }
        private void textBox2_MouseHover(object sender, EventArgs e)
        {
            panel7.BackColor = Color.FromArgb(247, 230, 210);
            textBox2.BackColor = Color.FromArgb(247, 230, 210);
        }
        private void panel7_MouseLeave(object sender, EventArgs e)
        {
            panel7.BackColor = Color.FromArgb(186, 226, 218);
            textBox2.BackColor = Color.FromArgb(186, 226, 218);
        }
        private void textBox2_MouseLeave(object sender, EventArgs e)
        {
            panel7.BackColor = Color.FromArgb(186, 226, 218);
            textBox2.BackColor = Color.FromArgb(186, 226, 218);
        }


        private void panel8_MouseHover(object sender, EventArgs e)
        {
            panel8.BackColor = Color.FromArgb(247, 230, 210);
            textBox3.BackColor = Color.FromArgb(247, 230, 210);
        }
        private void textBox3_MouseHover(object sender, EventArgs e)
        {
            panel8.BackColor = Color.FromArgb(247, 230, 210);
            textBox3.BackColor = Color.FromArgb(247, 230, 210);
        }
        private void panel8_MouseLeave(object sender, EventArgs e)
        {
            panel8.BackColor = Color.FromArgb(186, 226, 218);
            textBox3.BackColor = Color.FromArgb(186, 226, 218);
        }
        private void textBox3_MouseLeave(object sender, EventArgs e)
        {
            panel8.BackColor = Color.FromArgb(186, 226, 218);
            textBox3.BackColor = Color.FromArgb(186, 226, 218);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Komunikacija.Instance.OdjaviMe();
            Environment.Exit(Environment.ExitCode);
        }
    }
}
