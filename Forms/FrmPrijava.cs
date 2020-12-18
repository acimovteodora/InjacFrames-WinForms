using Domen;
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
    public partial class FrmPrijava : Form
    {
        public FrmPrijava()
        {
            InitializeComponent();
        }
        private void OcistiFormu()
        {
            txtKorisnickoIme.Clear();
            txtLozinka.Clear();
        }
        private bool ProveraLogin()
        {
            bool provera = true;
            if (string.IsNullOrEmpty(txtKorisnickoIme.Text))
            {
                lblUser.Text = "korisnicko ime";
                provera = false;
            }
            else
            {
                lblUser.Text = string.Empty;
            }
            if (string.IsNullOrEmpty(txtLozinka.Text))
            {
                lblPass.Text = "lozinka";
                provera = false;
            }
            else
            {
                lblPass.Text = string.Empty;
            }
            return provera;
        }
        

        private void btnPrijava_Click(object sender, EventArgs e)
        {
            try
            {
                if (ProveraLogin())
                {
                    if (Komunikacija.Instance.PoveziSe())
                    {
                        OdgovorServera odg = Komunikacija.Instance.PrijaviZaposlenog(txtKorisnickoIme.Text, txtKorisnickoIme.Text);
                        if (odg.Signal == Signal.Error)
                        {
                            FrmPoruka por = new FrmPoruka(odg.Poruka);
                            por.ShowDialog();
                            OcistiFormu();
                            return;
                        }
                        Zaposleni z = (Zaposleni)odg.Objekat;
                        AdministratorBazePodataka abp = Komunikacija.Instance.PrijaviAdministratora(z);
                        MenadzerProdaje mp = Komunikacija.Instance.PrijavaMenadzera(z);
                        if (abp != null)
                        {
                            abp.Ime = z.Ime;
                            abp.Prezime = z.Prezime;
                            abp.KorisnickoIme = z.KorisnickoIme;
                            abp.Lozinka = z.Lozinka;
                            Sesija.Instance.Zaposleni = abp;
                            //FrmPoruka f = new FrmPoruka("Uspesno ste prijavljeni!");
                            //f.ShowDialog();
                            FrmAdministratorGlavna f1 = new FrmAdministratorGlavna();
                            f1.Show();
                            this.Hide();
                            return;
                        }
                        mp.Ime = z.Ime;
                        mp.Prezime = z.Prezime;
                        mp.KorisnickoIme = z.KorisnickoIme;
                        mp.Lozinka = z.Lozinka;
                        Sesija.Instance.Zaposleni = mp;
                        //FrmPoruka f3 = new FrmPoruka("Uspesno ste prijavljeni!");
                        //f3.ShowDialog();
                        FrmMenadzerProdajeGlavna f2 = new FrmMenadzerProdajeGlavna();
                        f2.Show();
                        this.Hide();
                    }
                }
            }
            catch (Exception ex)
            {
                FrmPoruka f = new FrmPoruka(ex.Message);
                f.ShowDialog();
                Environment.Exit(Environment.ExitCode);
            }
        }

        private void btnPrijava_MouseHover_1(object sender, EventArgs e)
        {
            btnPrijava.BackColor = Color.FromArgb(186, 226, 218);
            btnPrijava.ForeColor = Color.FromArgb(255, 255, 255);
            panel4.BackColor = Color.FromArgb(186, 130, 106);
            panel5.BackColor = Color.FromArgb(186, 130, 106);
        }

        private void btnPrijava_MouseLeave_1(object sender, EventArgs e)
        {
            btnPrijava.BackColor = Color.FromArgb(255, 255, 255);
            btnPrijava.ForeColor = Color.FromArgb(186, 130, 106);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }

        private void txtKorisnickoIme_Click(object sender, EventArgs e)
        {
            panel5.BackColor = Color.FromArgb(247, 230, 210);
            panel4.BackColor = Color.FromArgb(186, 130, 106);
        }

        private void txtLozinka_Click(object sender, EventArgs e)
        {
            panel4.BackColor = Color.FromArgb(247, 230, 210);
            panel5.BackColor = Color.FromArgb(186, 130, 106);
        }
    }
}
