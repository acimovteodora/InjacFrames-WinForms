using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Domen;
using System.Diagnostics;

namespace Forms
{
    public class Komunikacija
    {
        private Socket klijentskiSocket;
        private NetworkStream stream;
        private BinaryFormatter formatter = new BinaryFormatter();
        private static Komunikacija _instance;
        public static Komunikacija Instance
        {
            get
            {
                if (_instance == null) _instance = new Komunikacija();
                return _instance;
            }
        }

        public Komunikacija()
        {

        }
        private void PosaljiPoruku( ZahtevKlijenta zahtev)
        {
            try
            {
                formatter.Serialize(stream, zahtev);
            }
            catch (Exception)
            {
                throw new Exception("Server je prekinuo sa radom! Otvoren prozor ce se iskljuciti. Pokusajte sa prijavom kasnije.");
            }
        }
        private OdgovorServera PrihvatiPorukuServera()
        {
            try
            {
                return (OdgovorServera)formatter.Deserialize(stream);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /* FORMA LOGIN */
        public bool PoveziSe()
        {
            try
            {
                if (klijentskiSocket == null || !klijentskiSocket.Connected)
                {
                    klijentskiSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    klijentskiSocket.Connect("localhost", 9091);
                    stream = new NetworkStream(klijentskiSocket);
                }
                return true;
            }
            catch (SocketException)
            {
                throw new Exception("Server nije pokrenut! Pokusajte kasnije.");
            }
        }

        internal OdgovorServera PrijaviZaposlenog(string user, string pass)
        {
            try
            {
                Zaposleni z = new Zaposleni { KorisnickoIme = user, Lozinka = pass };
                ZahtevKlijenta zahtev = new ZahtevKlijenta { Operacija = Operacija.PrijavaZaposlenog, Objekat = z };
                PosaljiPoruku(zahtev);
                OdgovorServera odg = PrihvatiPorukuServera();
                return odg;
            }
            catch (Exception e)
            {
                throw new Exception("Server je prekinuo sa radom! Otvoren prozor ce se iskljuciti. Pokusajte sa prijavom kasnije.");
            }
        }

        internal MenadzerProdaje PrijavaMenadzera(Zaposleni z)
        {
            ZahtevKlijenta zahtev = new ZahtevKlijenta { Operacija = Operacija.PrijavaMenadzera, Objekat = z };
            PosaljiPoruku(zahtev);
            OdgovorServera odg = PrihvatiPorukuServera();
            return odg.Objekat as MenadzerProdaje;
        }

        internal AdministratorBazePodataka PrijaviAdministratora(Zaposleni z)
        {
            ZahtevKlijenta zahtev = new ZahtevKlijenta { Operacija = Operacija.PrijavaAdministratora, Objekat = z };
            PosaljiPoruku(zahtev);
            OdgovorServera odg = PrihvatiPorukuServera();
            return odg.Objekat as AdministratorBazePodataka;
        }
        
        /* FORMA LAJSNA */
        internal Lajsna UcitajLajsnu(int idLajsne)
        {
            ZahtevKlijenta zahtev = new ZahtevKlijenta { Operacija = Operacija.UcitajLajsnu, Objekat = idLajsne };
            PosaljiPoruku(zahtev);
            return (Lajsna)PrihvatiPorukuServera().Objekat;
        }
        internal OdgovorServera ZapamtiLajsnu(Lajsna lajsna)
        {
            ZahtevKlijenta zahtev = new ZahtevKlijenta { Operacija = Operacija.ZapamtiLajsnu, Objekat = lajsna };
            PosaljiPoruku(zahtev);
            return PrihvatiPorukuServera();
        }

        internal List<Lajsna> VratiSveLajsne()
        {
            try
            {
                ZahtevKlijenta zahtev = new ZahtevKlijenta { Operacija = Operacija.UcitajListuLajsni };
                PosaljiPoruku(zahtev);
                return (List<Lajsna>)PrihvatiPorukuServera().Objekat;
            }
            catch (SocketException)
            {
                throw new Exception("Server je prekinuo sa radom! Otvoren prozor ce se iskljuciti. Pokusajte sa prijavom kasnije.");
            }
        }

        internal void OdjaviMe()
        {
            ZahtevKlijenta zahtev = new ZahtevKlijenta { Operacija = Operacija.Odjava, Objekat = Sesija.Instance.Zaposleni };
            PosaljiPoruku(zahtev);
        }

        /* FORMA KOMPANIJA */
        internal OdgovorServera UcitajKompaniju(int idKompanije)
        {
            ZahtevKlijenta zahtev = new ZahtevKlijenta { Operacija = Operacija.UcitajKompaniju, Objekat = idKompanije };
            PosaljiPoruku(zahtev);
            OdgovorServera odg = PrihvatiPorukuServera();
            return odg;
        }
        internal List<Kompanija> VratiSveKompanije()
        {
            ZahtevKlijenta zahtev = new ZahtevKlijenta { Operacija = Operacija.UcitajLisuKompanija };
            PosaljiPoruku(zahtev);
            return (List<Kompanija>)PrihvatiPorukuServera().Objekat;
        }
        internal OdgovorServera ZapamtiKompaniju(Kompanija k)
        {
            ZahtevKlijenta zahtev = new ZahtevKlijenta { Operacija = Operacija.ZapamtiKompaniju, Objekat = k };
            PosaljiPoruku(zahtev);
            return PrihvatiPorukuServera();
        }

        internal OdgovorServera VratiKompanijeKriterijum(string text)
        {
            ZahtevKlijenta zahtev = new ZahtevKlijenta { Operacija = Operacija.PretraziKompanije, Objekat = text };
            PosaljiPoruku(zahtev);
            return PrihvatiPorukuServera();
        }
        internal OdgovorServera IzmeniKompaniju(Kompanija k)
        {
            ZahtevKlijenta zahtev = new ZahtevKlijenta { Operacija = Operacija.IzmeniKompaniju, Objekat = k };
            PosaljiPoruku(zahtev);
            return PrihvatiPorukuServera();
        }

        /* FORMA KREIRAJ PORUDZBENICU*/

        internal Porudzbenica ZapamtiPorudzbenicu(Porudzbenica p)
        {
            ZahtevKlijenta zahtev = new ZahtevKlijenta { Operacija = Operacija.ZapamtiPorudzbenicu, Objekat = p };
            PosaljiPoruku(zahtev);
            return (Porudzbenica)PrihvatiPorukuServera().Objekat;
        }

        internal OdgovorServera VratiLajsneKriterijum(string text)
        {
            ZahtevKlijenta zahtev = new ZahtevKlijenta { Operacija = Operacija.PretraziLajsne, Objekat = text };
            PosaljiPoruku(zahtev);
            return PrihvatiPorukuServera();
        }

        /* FORMA OBRISI PORUDZBENICU*/

        internal List<Porudzbenica> VratiSvePorudzbenice()
        {
            ZahtevKlijenta zahtev = new ZahtevKlijenta { Operacija = Operacija.UcitajListuPorudzbenica };
            PosaljiPoruku(zahtev);
            return (List<Porudzbenica>)PrihvatiPorukuServera().Objekat;
        }

        internal OdgovorServera ObrisiPorudzbenicu(Porudzbenica p)
        {
            ZahtevKlijenta zahtev = new ZahtevKlijenta { Operacija = Operacija.ObrisiPorudzbenicu, Objekat = p };
            PosaljiPoruku(zahtev);
            return PrihvatiPorukuServera();
        }
        internal OdgovorServera UcitajPorudzbenicu(int idPorudzbenice)
        {
            ZahtevKlijenta zahtev = new ZahtevKlijenta { Operacija = Operacija.UcitajPorudzbenicu, Objekat = idPorudzbenice };
            PosaljiPoruku(zahtev);
            return PrihvatiPorukuServera();
        }

        internal OdgovorServera IzmeniPorudzbenicu(Porudzbenica nova)
        {
            ZahtevKlijenta zahtev = new ZahtevKlijenta { Operacija = Operacija.IzmeniPorudzbenicu, Objekat = nova };
            PosaljiPoruku(zahtev);
            return PrihvatiPorukuServera();
        }

        /* FORMA IZMENI PORUDZBENICU */

        internal List<Porudzbenica> VratiSvePorudzbeniceKriterijum()
        {
            string kriterijum = DateTime.Today.Year + "-" + DateTime.Today.Month + "-" + DateTime.Today.Day  ;
            ZahtevKlijenta zahtev = new ZahtevKlijenta { Operacija = Operacija.PretraziPorudzbenice, Objekat = $" where DatumDo > '{kriterijum}'" };
            PosaljiPoruku(zahtev);
            return (List<Porudzbenica>)PrihvatiPorukuServera().Objekat;
        }

    }
}
