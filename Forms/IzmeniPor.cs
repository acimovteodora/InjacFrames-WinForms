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

namespace Forms
{
    public partial class IzmeniPor : UserControl
    {
        private Porudzbenica p;
        private List<Kompanija> kompanije = Komunikacija.Instance.VratiSveKompanije();
        private List<Lajsna> lajsne = Komunikacija.Instance.VratiSveLajsne();
        private List<Porudzbenica> porudzbenice = Komunikacija.Instance.VratiSvePorudzbeniceKriterijum();
        private BindingList<StavkaPorudzbenice> stavke = new BindingList<StavkaPorudzbenice>();
        public IzmeniPor()
        {
            InitializeComponent();
            btnSacuvajIzmenu.Enabled = false;
        }

        private void IzmeniPor_Load(object sender, EventArgs e)
        {
            dgvPorudzbine.DataSource = porudzbenice;
            dgvLajsne.DataSource = lajsne;
            dgvPorucene.DataSource = stavke;
            dtOd.Format = DateTimePickerFormat.Custom;
            dtOd.CustomFormat = "dd. MMMM yyyy.";
            dtDo.Format = DateTimePickerFormat.Custom;
            dtDo.CustomFormat = "dd. MMMM yyyy.";
            if (txtUkupno.Enabled == false && txtUkupno.ReadOnly == true)
            {
                txtUkupno.BackColor = Color.FromArgb(186, 226, 218);
            }
            SrediFormu();
            dgvPorucene.ClearSelection();
            btnSacuvajIzmenu.Enabled = false;
            dtDo.Value = DateTime.Today;
        }

        private void SrediFormu()
        {
            dgvPorucene.Columns["CenaMetra"].Visible = false;
            dgvPorucene.Columns["CenaLajsne"].Visible = false;
            dgvLajsne.Columns["CenaMetra"].Visible = false;
            dgvLajsne.Columns["UkupnaDuzina"].Visible = false;
        }

        private void dgvPorudzbine_SelectionChanged(object sender, EventArgs e)
        {
            stavke.Clear();
            try
            {
                btnSacuvajIzmenu.Enabled = true;
                if (dgvPorudzbine.SelectedRows.Count == 1)
                {
                    OdgovorServera odg = Komunikacija.Instance.UcitajPorudzbenicu(((Porudzbenica)dgvPorudzbine.SelectedRows[0].DataBoundItem).Id);
                    if(odg.Signal == Signal.Error)
                    {
                        FrmPoruka fr = new FrmPoruka(odg.Poruka);
                        fr.ShowDialog();
                        return;
                    }
                    p = odg.Objekat as Porudzbenica;
                    foreach (StavkaPorudzbenice s in p.Stavke)
                    {
                        stavke.Add(s);
                    }
                    dgvPorucene.DataSource = stavke;
                    dgvLajsne.DataSource = Komunikacija.Instance.VratiSveLajsne();
                    txtUkupno.Text = p.UkupnaCena.ToString();
                    dtOd.Value = p.DatumOd;
                    dtDo.Value = p.DatumDo;
                    //FrmPoruka fr1 = new FrmPoruka(odg.Poruka);
                    //fr1.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                FrmPoruka f = new FrmPoruka(ex.Message);
                f.ShowDialog();
                Environment.Exit(Environment.ExitCode);
            }
        }

        private void btnDodajLajsnu_Click(object sender, EventArgs e)
        {
            try
            {
                if (ProveraLajsne())
                {
                    StavkaPorudzbenice stavka = new StavkaPorudzbenice();
                    Lajsna l = Komunikacija.Instance.UcitajLajsnu(((Lajsna)dgvLajsne.SelectedRows[0].DataBoundItem).Id);
                    stavka.Lajsna = l;
                    stavka.DuzinaLajsne = double.Parse(txtDuzina.Text);
                    stavka.Porudzbenica = dgvPorudzbine.SelectedRows[0].DataBoundItem as Porudzbenica;
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
                                FrmPoruka f = new FrmPoruka($"Na stanju ima {l.UkupnaDuzina} metara lajsne {l.NazivLajsne}, ne mozete poruciti {stavka.DuzinaLajsne + s.DuzinaLajsne} metara!");
                                f.ShowDialog();
                                return;
                            }
                            txtDuzina.Clear();
                            s.DuzinaLajsne += stavka.DuzinaLajsne;
                            s.CenaLajsne = s.DuzinaLajsne * s.CenaMetra;
                            dgvPorucene.Refresh();
                            PostaviUkupnuSumu();
                            dgvLajsne.ClearSelection();
                            return;
                        }
                    }
                    txtDuzina.Clear();
                    stavke.Add(stavka);
                    dgvPorucene.Refresh();
                    PostaviUkupnuSumu();
                    dgvLajsne.ClearSelection();
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
            stavke.Clear();
            dgvLajsne.ClearSelection();
            dgvPorucene.ClearSelection();
            txtUkupno.Clear();
            dtDo.Value = DateTime.Today;
            dtOd.Value = DateTime.Today;
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

        private int VratiIdStavke()
        {
            return stavke.Count() + 1;
        }

        private bool ProveraLajsne()
        {
            bool provera = true;
            if (dgvLajsne.SelectedRows.Count != 1)
            {
                FrmPoruka f = new FrmPoruka("Morate odabrati lajsnu koju zelite da dodate!");
                f.ShowDialog();
                return false;
            }

            if (string.IsNullOrEmpty(txtDuzina.Text))
            {
                panel5.BackColor = Color.DarkRed;
                provera = false;
            }
            else
            {
                if (!double.TryParse(txtDuzina.Text, out double p1))
                {
                    panel5.BackColor = Color.DarkRed;
                    provera = false;
                }
                else
                {
                    panel5.BackColor = Color.FromArgb(160,130,106);
                }
            }
            return provera;
        }

        private void btnSacuvajIzmenu_Click(object sender, EventArgs e)
        {
            if (dgvPorudzbine.SelectedRows.Count == 1)
            {
                Porudzbenica nova = new Porudzbenica
                {
                    Id = p.Id,
                    DatumDo = dtDo.Value,
                    Stavke = stavke.ToList(),
                    UkupnaCena = int.Parse(txtUkupno.Text),
                    DatumOd = p.DatumOd,
                    Zaposleni = p.Zaposleni,
                    Kompanija = p.Kompanija
                };
                try
                {
                    if (!p.Equals(nova))
                    {
                        OdgovorServera odg = Komunikacija.Instance.IzmeniPorudzbenicu(nova);
                        if (odg.Signal == Signal.Ok)
                        {
                            FrmPoruka f = new FrmPoruka($"Uspesno ste izmenili porudzbenicu ciji je id: {nova.Id}");
                            f.ShowDialog();
                        }
                        else
                        {
                            FrmPoruka f = new FrmPoruka(odg.Poruka);
                            f.ShowDialog();
                        }
                    }
                    else
                    {
                        FrmPoruka f = new FrmPoruka($"Nije izmenili podatke izabrane porudzbenice. Pokusajte ponovo.");
                        f.ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    FrmPoruka f = new FrmPoruka(ex.Message);
                    f.ShowDialog();
                    Environment.Exit(Environment.ExitCode);
                }
            }
            else
            {
                FrmPoruka f = new FrmPoruka($"Odaberite porudzbenicu koju zelite da izmenite.");
                f.ShowDialog();
            }
            Ocisti();
            btnSacuvajIzmenu.Enabled = false;
        }
        
    }
}
