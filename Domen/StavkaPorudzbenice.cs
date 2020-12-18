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
    public class StavkaPorudzbenice : IDomenskiObjekat
    {
        [Browsable(false)]
        public int Id { get; set; }
        [Browsable(false)]
        public Porudzbenica Porudzbenica { get; set; }
        [Browsable(false)]
        public Lajsna Lajsna { get; set; }
        [DisplayName("Lajsna")]
        public String NazivLajsne
        {
            get
            {
                return Lajsna.NazivLajsne;
            }
        }
        [DisplayName("Dužina")]
        public double DuzinaLajsne { get; set; }
        public double CenaMetra { get; set; }
        [DisplayName("Cena")]
        public double CenaLajsne { get; set; }
        public void PostaviCenuMetra(double cena) => CenaMetra = cena;
        [Browsable(false)]
        public string Tabela => "StavkaPorudzbenice";
        [Browsable(false)]

        public int ID => 0;

        [Browsable(false)]
        public string VrednostiZaInsert => $" {Id},{Porudzbenica.Id},{Lajsna.Id},{DuzinaLajsne},{CenaMetra},{CenaLajsne}";

        [Browsable(false)]
        public string UslovZaPretragu => $" where IdPorudzbenice = {Porudzbenica.Id} and IdLajsne = {Lajsna.Id}";

        [Browsable(false)]
        public string NaziviKolona => "Id,IdPorudzbenice,IdLajsne,DuzinaLajsne,CenaMetra,CenaLajsne";

        [Browsable(false)]
        public string PostaviVrednostiKolona => $"IdPorudzbenice = {Porudzbenica.Id},IdLajsne = {Lajsna.Id},DuzinaLajsne = {DuzinaLajsne},CenaMetra = {CenaMetra},CenaLajsne = {CenaLajsne}";

        [Browsable(false)]
        public string Identifikator => "Id";

        public List<IDomenskiObjekat> VratiListu(SqlDataReader reader)
        {
            List<IDomenskiObjekat> stavke = new List<IDomenskiObjekat>();
            while (reader.Read())
            {
                StavkaPorudzbenice stavka = new StavkaPorudzbenice();
                stavka.Id = (int)reader[0];
                stavka.Porudzbenica = new Porudzbenica { Id = (int)reader[1] };
                stavka.Lajsna = new Lajsna { Id = (int)reader[2] };
                stavka.DuzinaLajsne = (double)reader[3];
                stavka.CenaMetra = (double)reader[4];
                stavka.CenaLajsne = (double)reader[5];
                stavke.Add(stavka);
            }
            return stavke;
        }

        public IDomenskiObjekat VratiObjekat(SqlDataReader reader)
        {
            try
            {
                StavkaPorudzbenice stavka = new StavkaPorudzbenice();
                while (reader.Read())
                {
                    stavka.Id = (int)reader[0];
                    stavka.Porudzbenica = new Porudzbenica { Id = (int)reader[1] };
                    stavka.Lajsna = new Lajsna { Id = (int)reader[2] };
                    stavka.DuzinaLajsne = (double)reader[3];
                    stavka.CenaMetra = (double)reader[4];
                    stavka.CenaLajsne = (double)reader[5];
                    break;
                }
                if (stavka.Id == 0) throw new Exception();
                return stavka;
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
            StavkaPorudzbenice sp = (StavkaPorudzbenice)obj;
            if (sp.Lajsna.Id == Lajsna.Id) return true;
            return false;
        }
    }
}
