using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domen
{
    [Serializable]
    public class Kompanija : IDomenskiObjekat
    {
        [Browsable(false)]
        public int Id { get; set; }
        [DisplayName("Naziv kompanije")]
        public string NazivKompanije { get; set; }
        public string Drzava { get; set; }
        public string Grad { get; set; }
        public string Adresa { get; set; }
        public string Vlasnik { get; set; }
        public string Kontakt { get; set; }
        public string Email { get; set; }

        [Browsable(false)]
        public string Tabela => "Kompanija";
        [Browsable(false)]

        public int ID => 0;

        [Browsable(false)]
        public string VrednostiZaInsert => $" '{NazivKompanije}','{Drzava}','{Grad}','{Adresa}','{Vlasnik}','{Kontakt}','{Email}'";

        [Browsable(false)]
        public string UslovZaPretragu => $" where Id = {Id}";

        [Browsable(false)]
        public string NaziviKolona => " Id,NazivKompanije,Drzava,Grad,Adresa,Vlasnik,Kontakt,Email";

        [Browsable(false)]
        public string PostaviVrednostiKolona => $"NazivKompanije='{NazivKompanije}',Drzava='{Drzava}',Grad='{Grad}',Adresa='{Adresa}',Vlasnik='{Vlasnik}',KontaktTelefon='{Kontakt}',Email='{Email}'";

        [Browsable(false)]
        public string Identifikator => "Id";

        public List<IDomenskiObjekat> VratiListu(SqlDataReader reader)
        {
            List<IDomenskiObjekat> kompanije = new List<IDomenskiObjekat>();
            while (reader.Read())
            {
                Kompanija komp = new Kompanija();
                komp.Id = (int)reader[0];
                komp.NazivKompanije = reader[1].ToString();
                komp.Drzava = reader[2].ToString();
                komp.Grad = reader[3].ToString();
                komp.Adresa = reader[4].ToString();
                komp.Vlasnik = reader[5].ToString();
                komp.Kontakt = reader[6].ToString();
                komp.Email = reader[7].ToString();
                kompanije.Add(komp);
            }
            return kompanije;
        }

        public IDomenskiObjekat VratiObjekat(SqlDataReader reader)
        {
            try
            {
                Kompanija komp = new Kompanija();
                while (reader.Read())
                {
                    komp.Id = (int)reader[0];
                    komp.NazivKompanije = reader[1].ToString();
                    komp.Drzava = reader[2].ToString();
                    komp.Grad = reader[3].ToString();
                    komp.Adresa = reader[4].ToString();
                    komp.Vlasnik = reader[5].ToString();
                    komp.Kontakt = reader[6].ToString();
                    komp.Email = reader[7].ToString();
                    break;
                }
                if (string.IsNullOrEmpty(komp.NazivKompanije)) throw new Exception();
                return komp;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != GetType()) return false;
            Kompanija k = (Kompanija)obj;
            if((k.NazivKompanije == NazivKompanije) && (Adresa == k.Adresa) && (k.Kontakt == Kontakt) && (k.Email == Email))
                return true;
            return false;
        }

        public override string ToString()
        {
            return UslovZaPretragu;
        }

        public bool Izmenjena(Kompanija nova)
        {
            if (NazivKompanije != nova.NazivKompanije) return true;
            if (Drzava != nova.Drzava) return true;
            if (Grad != nova.Grad) return true;
            if (Adresa != nova.Adresa) return true;
            if (Vlasnik != nova.Vlasnik) return true;
            if (Kontakt != nova.Kontakt) return true;
            if (Email != nova.Email) return true;
            return false;
        }
    }
}
