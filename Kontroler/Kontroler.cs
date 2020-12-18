using BrokerBaze;
using Domen;
using SistemskeOperacije;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kontroler
{
    public class Kontroler
    {
        private Broker broker = new Broker();
        private static Kontroler instance;
        public static Kontroler Instance
        {
            get
            {
                if (instance == null)
                    instance = new Kontroler();
                return instance;
            }
        }

        public Kontroler()
        {

        }

        public List<Kompanija> VratiSveKompanije()
        {
            OpstaSistemskaOperacija oso = new UcitajListuKompanija();
            oso.IzvrsiSO(new Kompanija());
            return ((UcitajListuKompanija)oso).Kompanije;
        }

        public List<Lajsna> VratiSveLajsne()
        {
            OpstaSistemskaOperacija oso = new UcitajListuLajsni();
            oso.IzvrsiSO(new Lajsna());
            return ((UcitajListuLajsni)oso).Lajsne;
        }

        public List<Porudzbenica> VratiSvePorudzbenice(string krit)
        {
            OpstaSistemskaOperacija oso = new UcitajListuPorudzbenica(krit);
            oso.IzvrsiSO(new Porudzbenica());
            return ((UcitajListuPorudzbenica)oso).Porudzbenice;
        }
        public Kompanija UcitajKompaniju(int idKompanije)
        {
            Kompanija komp = new Kompanija { Id = idKompanije };
            OpstaSistemskaOperacija oso = new UcitajKompaniju();
            oso.IzvrsiSO(komp);
            return ((UcitajKompaniju)oso).Kompanija;
        }
        public Zaposleni PrijavaZaposlenog(string user,string pass)
        {
            Zaposleni z = new Zaposleni { KorisnickoIme = user, Lozinka = pass };
            OpstaSistemskaOperacija oso = new PrijavaZaposlenog();
            oso.IzvrsiSO(z);
            return ((PrijavaZaposlenog)oso).Zaposleni;
        }

        public Porudzbenica ObrisiPorudzbenicu(Porudzbenica p)
        {
            OpstaSistemskaOperacija oso = new ObrisiPorudzbenicu();
            oso.IzvrsiSO(p);
            return ((ObrisiPorudzbenicu)oso).Porudzbenica;
        }

        public AdministratorBazePodataka PrijavaAdministratora(int id)
        {
            AdministratorBazePodataka a = new AdministratorBazePodataka { Id = id };
            OpstaSistemskaOperacija oso = new PrijavaZaposlenog();
            oso.IzvrsiSO(a);
            return ((PrijavaZaposlenog)oso).Administrator;
        }
        public MenadzerProdaje PrijavaMenadzera(int id)
        {
            MenadzerProdaje m = new MenadzerProdaje { Id = id };
            OpstaSistemskaOperacija oso = new PrijavaZaposlenog();
            oso.IzvrsiSO(m);
            return ((PrijavaZaposlenog)oso).Menadzer;
        }

        public Lajsna ZapamtiLajsnu(Lajsna l)
        {
            OpstaSistemskaOperacija oso = new ZapamtiLajsnu();
            oso.IzvrsiSO(l);
            return ((ZapamtiLajsnu)oso).Lajsna;
        }

        public Kompanija ZapamtiKompaniju(Kompanija k)
        {
            OpstaSistemskaOperacija oso = new ZapamtiKompaniju();
            oso.IzvrsiSO(k);
            return ((ZapamtiKompaniju)oso).Kompanija;
        }

        public List<Kompanija> VratiKompanijeKriterijum(string pretraga)
        {
            string kriterijum = $" where NazivKompanije like '%{pretraga}%' or Vlasnik like '%{pretraga}%' or Grad like '%{pretraga}%'";
            OpstaSistemskaOperacija oso = new PretraziKompanije(kriterijum);
            oso.IzvrsiSO(new Kompanija());
            return ((PretraziKompanije)oso).Kompanije;
        }

        public Porudzbenica ZapamtiPorudzbenicu(Porudzbenica p)
        {
            OpstaSistemskaOperacija oso = new ZapamtiPorudzbenicu();
            oso.IzvrsiSO(p);
            return ((ZapamtiPorudzbenicu)oso).Porudzbenica;
        }
        
        public List<Lajsna> VratiLajsnuKriterijum(string pretraga)
        {
            string kriterijum = $" where NazivLajsne like '%{pretraga}%'";
            OpstaSistemskaOperacija oso = new PretraziLajsne(kriterijum);
            oso.IzvrsiSO(new Lajsna());
            return ((PretraziLajsne)oso).Lajsne;
        }

        public Lajsna UcitajLajsnu(int idLajsne)
        {
            Lajsna l = new Lajsna { Id = idLajsne };
            OpstaSistemskaOperacija oso = new UcitajLajsnu();
            oso.IzvrsiSO(l);
            return ((UcitajLajsnu)oso).Lajsna;
        }

        public Porudzbenica UcitajPorudzbenicu(int idPorudzbenice)
        {
            Porudzbenica p = new Porudzbenica { Id = idPorudzbenice };
            OpstaSistemskaOperacija oso = new UcitajPorudzbenicu();
            oso.IzvrsiSO(p);
            return ((UcitajPorudzbenicu)oso).Porudzbenica;
        }

        public Porudzbenica IzmeniPorudzbenicu(Porudzbenica p)
        {
            OpstaSistemskaOperacija oso = new IzmeniPorudzbenicu();
            oso.IzvrsiSO(p);
            return ((IzmeniPorudzbenicu)oso).Porudzbenica;
        }

        public Kompanija IzmeniKompaniju(Kompanija kompanija)
        {
            OpstaSistemskaOperacija oso = new IzmeniKompaniju();
            oso.IzvrsiSO(kompanija);
            return ((IzmeniKompaniju)oso).Kompanija;
        }
    }
}
