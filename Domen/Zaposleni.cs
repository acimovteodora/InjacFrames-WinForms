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
    public class Zaposleni : IDomenskiObjekat
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }

        public string Tabela => "Zaposleni";

        public string VrednostiZaInsert => $" {Id}, '{Ime}','{Prezime}','{KorisnickoIme}','{Lozinka}'";

        public string UslovZaPretragu => $" where KorisnickoIme = '{KorisnickoIme}' and Lozinka = '{Lozinka}'";

        public string NaziviKolona => " Id Ime, Prezime, KorisnickoIme, Lozinka";

        public string PostaviVrednostiKolona => $" Id = {Id}, Ime = '{Ime}', Prezime = '{Prezime}', KorisnickoIme = '{KorisnickoIme}', Lozinka = '{Lozinka}'";

        public string Identifikator => "Id";

        public override string ToString()
        {
            return Ime + " " + Prezime;
        }
        [Browsable(false)]

        public int ID => 0;
        public List<IDomenskiObjekat> VratiListu(SqlDataReader reader)
        {
            throw new NotImplementedException();
        }

        public IDomenskiObjekat VratiObjekat(SqlDataReader reader)
        {
            try
            {
                Zaposleni zap = new Zaposleni();
                while (reader.Read())
                {
                    zap = new Zaposleni()
                    {
                        Id = (int)reader["Id"],
                        Ime = reader["Ime"].ToString(),
                        Prezime = reader["Prezime"].ToString(),
                        KorisnickoIme = reader["KorisnickoIme"].ToString(),
                        Lozinka = reader["Lozinka"].ToString()
                    };
                    break;
                }
                if (string.IsNullOrEmpty(zap.Ime)) throw new Exception();
                return zap;
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
            Zaposleni z = obj as Zaposleni;
            if (Id == z.Id)
                return true;
            return false;
        }
    }
}
