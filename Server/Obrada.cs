using Domen;
using Kontroler;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Obrada
    {
        public Socket klijentskiSoket;
        private Server server;
        public NetworkStream Tok;
        private BinaryFormatter formatter = new BinaryFormatter();

        public Obrada(Socket klijentskiSoket, Server server)
        {
            this.klijentskiSoket = klijentskiSoket;
            this.server = server;
            Tok = new NetworkStream(klijentskiSoket);
        }

        internal void ObradaZahteva()
        {
            bool kraj = false;
            while (!kraj)
            {
                try
                {
                    ZahtevKlijenta zahtev = (ZahtevKlijenta)formatter.Deserialize(Tok);
                    OdgovorServera odg = IzvrsiZahtev(zahtev);
                    formatter.Serialize(Tok, odg);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($">>> {ex.Message}");
                    kraj = true;
                }
            }
        }

        private OdgovorServera IzvrsiZahtev(ZahtevKlijenta zahtev)
        {
            OdgovorServera odgovor = new OdgovorServera();
            try
            {
                switch (zahtev.Operacija)
                {
                    case Operacija.PrijavaAdministratora:
                        odgovor.Objekat = Kontroler.Kontroler.Instance.PrijavaAdministratora((zahtev.Objekat as Zaposleni).Id);
                        odgovor.Poruka = "Sistem je uspesno pronasao zaposlenog na poziciji administratora baze podataka!";
                        break;
                    case Operacija.PrijavaMenadzera:
                        odgovor.Objekat = Kontroler.Kontroler.Instance.PrijavaMenadzera((zahtev.Objekat as Zaposleni).Id);
                        odgovor.Poruka = "Sistem je uspesno pronasao zaposlenog na poziciji menadzera prodaje!";
                        break;
                    case Operacija.UcitajListuLajsni:
                        odgovor.Objekat = Kontroler.Kontroler.Instance.VratiSveLajsne();
                        odgovor.Poruka = "Sistem je uspesno pronasao lajsne!";
                        break;
                    case Operacija.UcitajLisuKompanija:
                        odgovor.Objekat = Kontroler.Kontroler.Instance.VratiSveKompanije();
                        odgovor.Poruka = "Sistem je uspesno pronasao kompanije!";
                        break;
                    case Operacija.UcitajListuPorudzbenica:
                        odgovor.Objekat = Kontroler.Kontroler.Instance.VratiSvePorudzbenice("");
                        odgovor.Poruka = "Sistem je uspesno pronasao porudzbenice!";
                        break;
                    case Operacija.PretraziKompanije:
                        odgovor.Objekat = Kontroler.Kontroler.Instance.VratiKompanijeKriterijum(zahtev.Objekat.ToString());
                        if (((List<Kompanija>)odgovor.Objekat).Count() == 0) throw new Exception("Nisu pronadjene kompanije po unetom kriterijumu!");
                        odgovor.Poruka = "Sistem je uspesno pretrazio kompanije po kriterijumu!";
                        break;
                    case Operacija.PretraziLajsne:
                        odgovor.Objekat = Kontroler.Kontroler.Instance.VratiLajsnuKriterijum(zahtev.Objekat.ToString());
                        if (((List<Lajsna>)odgovor.Objekat).Count() == 0) throw new Exception("Nisu pronadjene lajsne po unetom kriterijumu!");
                        odgovor.Poruka = "Sistem je uspesno pronasao lajsne po unetom kriterijumu!";
                        break;
                    case Operacija.PretraziPorudzbenice:
                        odgovor.Objekat = Kontroler.Kontroler.Instance.VratiSvePorudzbenice(zahtev.Objekat.ToString());
                        odgovor.Poruka = "Sistem je uspesno pretrazio porudzbenicu po unetom kriterijumu!";
                        break;
                    case Operacija.UcitajKompaniju:
                        odgovor.Objekat = Kontroler.Kontroler.Instance.UcitajKompaniju(int.Parse(zahtev.Objekat.ToString()));
                        odgovor.Poruka = "Sistem je uspesno ucitao kompaniju!";
                        break;
                    case Operacija.ObrisiPorudzbenicu:
                        odgovor.Objekat = Kontroler.Kontroler.Instance.ObrisiPorudzbenicu(zahtev.Objekat as Porudzbenica);
                        odgovor.Poruka = "Sistem je uspesno obrisao porudzbenicu!";
                        break;
                    case Operacija.UcitajLajsnu:
                        odgovor.Objekat = Kontroler.Kontroler.Instance.UcitajLajsnu(int.Parse(zahtev.Objekat.ToString()));
                        odgovor.Poruka = "Sistem je uspesno ucitao lajsnu!";
                        break;
                    case Operacija.ZapamtiKompaniju:
                        odgovor.Objekat = Kontroler.Kontroler.Instance.ZapamtiKompaniju(zahtev.Objekat as Kompanija);
                        odgovor.Poruka = "Sistem je uspesno sacuvao kompaniju!";
                        break;
                    case Operacija.ZapamtiLajsnu:
                        odgovor.Objekat = Kontroler.Kontroler.Instance.ZapamtiLajsnu(zahtev.Objekat as Lajsna);
                        odgovor.Poruka = $"Uspesno je sacuvana lajsna {((Lajsna)zahtev.Objekat).NazivLajsne}!";
                        break;
                    case Operacija.ZapamtiPorudzbenicu:
                        odgovor.Objekat = Kontroler.Kontroler.Instance.ZapamtiPorudzbenicu(zahtev.Objekat as Porudzbenica);
                        odgovor.Poruka = "Sistem je uspesno sacuvao porudzbenicu!";
                        break;
                    case Operacija.UcitajPorudzbenicu:
                        odgovor.Objekat = Kontroler.Kontroler.Instance.UcitajPorudzbenicu(int.Parse(zahtev.Objekat.ToString()));
                        odgovor.Poruka = "Sistem je uspesno pronasao porudzbenicu!";
                        break;
                    case Operacija.IzmeniPorudzbenicu:
                        odgovor.Objekat = Kontroler.Kontroler.Instance.IzmeniPorudzbenicu(zahtev.Objekat as Porudzbenica);
                        odgovor.Poruka = "Sistem je uspesno izmenio porudzbenicu!";
                        break;
                    case Operacija.IzmeniKompaniju:
                        odgovor.Objekat = Kontroler.Kontroler.Instance.IzmeniKompaniju(zahtev.Objekat as Kompanija);
                        odgovor.Poruka = "Sistem je uspesno izmenio kompaniju!";
                        odgovor.Signal = Signal.Ok;
                        break;
                    case Operacija.Odjava:
                        {
                            server.Zaposleni.Remove(new Zaposleni { Id = ((Zaposleni)zahtev.Objekat).Id });
                            foreach (Zaposleni zap in server.Zaposleni)
                            {
                                Debug.WriteLine(zap.KorisnickoIme + " ");
                            }
                            odgovor.Poruka = "Uspesno ste se izlogovali!";
                            break;
                        }
                    case Operacija.PrijavaZaposlenog:
                        Zaposleni k = (Zaposleni)zahtev.Objekat;
                        Zaposleni pronadjiZaposlenog = Kontroler.Kontroler.Instance.PrijavaZaposlenog(k.KorisnickoIme, k.Lozinka);
                        if (pronadjiZaposlenog == null)
                        {
                            odgovor.Poruka = $"Korisnik pod korisnickim imenom '{k.KorisnickoIme}' ne postoji!";
                            odgovor.Objekat = null;
                            odgovor.Signal = Signal.Error;
                            break;
                        }
                        if (server.Zaposleni.Contains(pronadjiZaposlenog))
                        {
                            odgovor.Poruka = $"Korisnik pod korisnickim imenom '{pronadjiZaposlenog.KorisnickoIme}' je vec ulogovan!";
                            odgovor.Objekat = null;
                            odgovor.Signal = Signal.Error;
                            break;
                        }
                        else
                        {
                            odgovor.Poruka = "Sistem je uspesno pronasao zaposlenog!";
                            odgovor.Objekat = pronadjiZaposlenog;
                            server.Zaposleni.Add(pronadjiZaposlenog);
                        }
                        break;
                }
            }
            catch (Exception e)
            {
                odgovor.Signal = Signal.Error;
                odgovor.Poruka = e.Message;
            }
            return odgovor;
        }
    }
}
