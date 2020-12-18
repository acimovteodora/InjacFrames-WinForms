using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Domen;
using System.Globalization;

namespace Forms
{
    public partial class KreirajPor : UserControl
    {
        private List<Kompanija> kompanije = Komunikacija.Instance.VratiSveKompanije();
        private List<Lajsna> lajsne = Komunikacija.Instance.VratiSveLajsne();
        private BindingList<StavkaPorudzbenice> stavke = new BindingList<StavkaPorudzbenice>();
        public KreirajPor()
        {
            InitializeComponent();
        }

        private void KreirajPor_Load(object sender, EventArgs e)
        {
            dgvKompanijePor.DataSource = kompanije;
            dgvLajsnePor.DataSource = lajsne;
            dgvPoruceneLajsne.DataSource = stavke;
            dtOd.Format = DateTimePickerFormat.Custom;
            dtOd.CustomFormat = "dd. MMMM yyyy.";
            dtOd.Value = DateTime.Today;
            dtDo.Format = DateTimePickerFormat.Custom;
            dtDo.CustomFormat = "dd. MMMM yyyy.";
            dtDo.Value = DateTime.Today;
            if (txtUkupno.Enabled == false && txtUkupno.ReadOnly == true)
            {
                txtUkupno.BackColor = Color.White;
            }
            SrediFormu();
        }

        private void SrediFormu()
        {
            dgvKompanijePor.Columns["Drzava"].Visible = false;
            dgvKompanijePor.Columns["Kontakt"].Visible = false;
            dgvKompanijePor.Columns["Email"].Visible = false;
            dgvKompanijePor.Columns["Adresa"].Visible = false;
            dgvKompanijePor.Columns["Vlasnik"].Visible = false;
            dgvKompanijePor.Columns["Grad"].Visible = false;
            dgvPoruceneLajsne.Columns["CenaMetra"].Visible = false;
            dgvLajsnePor.Columns["CenaMetra"].Visible = false;
            dgvLajsnePor.Columns["UkupnaDuzina"].Visible = false;
        }

        private int VratiIdStavke()
        {
            return stavke.Count() + 1;
        }
        private void PromeniIdStavkama()
        {
            int broj = 1;
            foreach (StavkaPorudzbenice stavka in stavke)
            {
                stavka.Id = broj++;
            }
        }
        private void OcistiPoljaStavke()
        {
            txtDuzinaLa.Text = string.Empty;
            txtPretraziKompaniju.Text = string.Empty;
            txtPretraziLajsnu.Text = string.Empty;
        }
        private bool Provera()
        {
            string poruka = string.Empty;
            bool provera = true;
               
            if (dgvKompanijePor.SelectedRows.Count != 1)
            {
                poruka += "Odaberite kompaniju za koju je ova porudžbina! \n\n";
            }
            if (dgvPoruceneLajsne.Rows.Count == 0)
            {
                poruka += "Unesite lajsne koje su poručene! \n\n";
            }
            if (dtOd.Value >= dtDo.Value)
            {
                poruka += "Datum od kada je moguce dostaviti porudzbenicu mora biti pre datuma do kada je to moguce odraditi!";
                provera = false;
            }
            if (!string.IsNullOrEmpty(poruka))
            {
                FrmPoruka f = new FrmPoruka(poruka);
                f.ShowDialog();
                provera = false;
            }
            return provera;
        }
        private bool ProveraLajsne()
        {
            bool provera = true;
            if (dgvLajsnePor.SelectedRows.Count != 1)
            {
                FrmPoruka f = new FrmPoruka("Morate odabrati lajsnu koja je porucena!");
                f.ShowDialog();
                return false;
            }
            
            if (string.IsNullOrEmpty(txtDuzinaLa.Text))
            {
                lblDuzina.Text = "Popunite!";
                lblDuzina.ForeColor = Color.DarkRed;
                provera = false;
            }
            else
            {
                if (!double.TryParse(txtDuzinaLa.Text, out double p1))
                {
                    lblDuzina.Text = "Mora broj!";
                    lblDuzina.ForeColor = Color.DarkRed;
                    provera = false;
                }
                else
                {
                    lblDuzina.Text = string.Empty;
                    lblDuzina.ForeColor = Color.White;
                }
            }
            return provera;
        }

        private void txtPretraziKompaniju_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPretraziKompaniju.Text))
                    dgvKompanijePor.DataSource = kompanije;
                else
                {
                    OdgovorServera odg = Komunikacija.Instance.VratiKompanijeKriterijum(txtPretraziKompaniju.Text);
                    try
                    {
                        if (odg.Signal == Signal.Error) throw new Exception("Nisu pronadjene kompanije po ovom kriterijumu!");
                    }
                    catch (Exception exx)
                    {
                        FrmPoruka f1 = new FrmPoruka(exx.Message);
                        f1.ShowDialog();
                        dgvKompanijePor.DataSource = kompanije;
                        txtPretraziKompaniju.Clear();
                        return;
                    }
                    dgvKompanijePor.DataSource = (List<Kompanija>)odg.Objekat;
                    //FrmPoruka f = new FrmPoruka(odg.Poruka);
                    //f.ShowDialog();
                }
                SrediFormu();
            }
            catch (Exception ex)
            {
                FrmPoruka f = new FrmPoruka(ex.Message);
                f.ShowDialog();
                Environment.Exit(Environment.ExitCode);
            }
        }

        private void txtPretraziLajsnu_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPretraziLajsnu.Text))
                    dgvLajsnePor.DataSource = lajsne;
                else {
                    OdgovorServera odg = Komunikacija.Instance.VratiLajsneKriterijum(txtPretraziLajsnu.Text);
                    if(odg.Signal == Signal.Ok)
                        dgvLajsnePor.DataSource = (List<Lajsna>)odg.Objekat;
                    else
                    {
                        FrmPoruka f = new FrmPoruka(odg.Poruka);
                        f.ShowDialog();
                        dgvLajsnePor.DataSource = lajsne;
                        txtPretraziLajsnu.Clear();
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
        private void PostaviUkupnuSumu()
        {
            double suma = 0.0;
            foreach (StavkaPorudzbenice stavka in stavke)
            {
                suma += stavka.CenaLajsne;
            }
            txtUkupno.Text = suma.ToString();
        }

        private void btnSacuvaj_Click(object sender, EventArgs e)
        {
            try
            {
                if (Provera())
                {
                    Porudzbenica p = new Porudzbenica();
                    p.Kompanija = (Komunikacija.Instance.UcitajKompaniju(((Kompanija)dgvKompanijePor.SelectedRows[0].DataBoundItem).Id)).Objekat as Kompanija;
                    p.Zaposleni = Sesija.Instance.Zaposleni;
                    p.Stavke = stavke.ToList();
                    p.UkupnaCena = double.Parse(txtUkupno.Text);
                    p.DatumOd = dtOd.Value;
                    p.DatumDo = dtDo.Value;
                    p = Komunikacija.Instance.ZapamtiPorudzbenicu(p);
                    if (p != null)
                    {
                        FrmPoruka f = new FrmPoruka("Uspesno sacuvana porudzbenica!");
                        f.ShowDialog();
                    }
                    else
                    {
                        FrmPoruka f = new FrmPoruka("Doslo je do greske prilikom cuvanja porudzbenice!");
                        f.ShowDialog();
                    }
                    Ocisti();
                }
            }
            catch (Exception ex)
            {
                FrmPoruka f = new FrmPoruka(ex.Message);
                f.ShowDialog();
                Environment.Exit(Environment.ExitCode);
            }
        }

        private void Ocisti()
        {
            txtUkupno.Clear();
            OcistiPoljaStavke();
            stavke.Clear();
            dgvPoruceneLajsne.Refresh();
            dgvLajsnePor.ClearSelection();
            dgvKompanijePor.ClearSelection();
        }

        private void btnObrisiStavku_Click(object sender, EventArgs e)
        {
            if (dgvPoruceneLajsne.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvPoruceneLajsne.SelectedRows)
                {
                    stavke.Remove((StavkaPorudzbenice)row.DataBoundItem);
                }
                PromeniIdStavkama();
                OcistiPoljaStavke();
            }
            else
            {
                FrmPoruka f = new FrmPoruka("Odaberite stavke/stavku za brisanje.");
                f.ShowDialog();
            }
        }

        private void btnDodaj_MouseHover(object sender, EventArgs e)
        {
            btnDodaj.BackColor = Color.FromArgb(186, 226, 218);
            btnDodaj.ForeColor = Color.FromArgb(255, 255, 255);
        }

        private void btnObrisiStavku_MouseHover(object sender, EventArgs e)
        {
            btnObrisiStavku.BackColor = Color.FromArgb(186, 226, 218);
            btnObrisiStavku.ForeColor = Color.FromArgb(255, 255, 255);
        }

        private void btnSacuvaj_MouseHover(object sender, EventArgs e)
        {
            btnSacuvaj.BackColor = Color.FromArgb(186, 226, 218);
            btnSacuvaj.ForeColor = Color.FromArgb(255, 255, 255);
        }

        private void btnOcisti_MouseHover(object sender, EventArgs e)
        {
            btnOcisti.BackColor = Color.FromArgb(186, 226, 218);
            btnOcisti.ForeColor = Color.FromArgb(255, 255, 255);
        }

        private void btnOcisti_MouseLeave(object sender, EventArgs e)
        {
            btnOcisti.BackColor = Color.FromArgb(255, 255, 255);
            btnOcisti.ForeColor = Color.FromArgb(186, 130, 106);
        }

        private void btnSacuvaj_MouseLeave(object sender, EventArgs e)
        {
            btnSacuvaj.BackColor = Color.FromArgb(255, 255, 255);
            btnSacuvaj.ForeColor = Color.FromArgb(186, 130, 106);
        }

        private void btnObrisiStavku_MouseLeave(object sender, EventArgs e)
        {
            btnObrisiStavku.BackColor = Color.FromArgb(255, 255, 255);
            btnObrisiStavku.ForeColor = Color.FromArgb(186, 130, 106);
        }

        private void btnDodaj_MouseLeave(object sender, EventArgs e)
        {
            btnDodaj.BackColor = Color.FromArgb(255, 255, 255);
            btnDodaj.ForeColor = Color.FromArgb(186, 130, 106);
        }

        private void btnDodaj_Click(object sender, EventArgs e)
        {
            try
            {
                if (ProveraLajsne())
                {
                    StavkaPorudzbenice stavka = new StavkaPorudzbenice();
                    Lajsna l = Komunikacija.Instance.UcitajLajsnu(((Lajsna)dgvLajsnePor.SelectedRows[0].DataBoundItem).Id);
                    stavka.Lajsna = l;
                    stavka.DuzinaLajsne = double.Parse(txtDuzinaLa.Text);
                    if (stavka.DuzinaLajsne > l.UkupnaDuzina)
                    {
                        FrmPoruka f = new FrmPoruka($"Na stanju ima {l.UkupnaDuzina} metara lajsne {l.NazivLajsne}!");
                        f.ShowDialog();
                        return;
                    }
                    stavka.CenaMetra = l.CenaMetra;
                    stavka.CenaLajsne = stavka.DuzinaLajsne * stavka.CenaMetra;
                    stavka.Id = VratiIdStavke();
                    foreach (StavkaPorudzbenice s in stavke)
                    {
                        if (s.Equals(stavka))
                        {
                            if (stavka.DuzinaLajsne + s.DuzinaLajsne > l.UkupnaDuzina)
                            {
                                FrmPoruka f = new FrmPoruka($"Na stanju ima {l.UkupnaDuzina} metara lajsne {l.NazivLajsne}. Ne mozete poruciti {stavka.DuzinaLajsne + s.DuzinaLajsne} metara!");
                                f.ShowDialog();
                                return;
                            }
                            s.DuzinaLajsne += stavka.DuzinaLajsne;
                            s.CenaLajsne = s.DuzinaLajsne * s.CenaMetra;
                            dgvPoruceneLajsne.Refresh();
                            PostaviUkupnuSumu();
                            OcistiPoljaStavke();
                            return;
                        }
                    }
                    stavke.Add(stavka);
                    PostaviUkupnuSumu();
                    OcistiPoljaStavke();
                }
            }
            catch (Exception ex)
            {
                FrmPoruka f = new FrmPoruka(ex.Message);
                f.ShowDialog();
                Environment.Exit(Environment.ExitCode);
            }
        }

        private void btnOcisti_Click(object sender, EventArgs e)
        {
            Ocisti();
        }

        private void txtDuzinaLa_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
