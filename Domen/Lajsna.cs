using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domen
{
    public enum Boja
    {
        srebrna,
        zlatna,
        plava,
        zelena,
        zuta,
        siva,
        bela,
        smedja,
        crna,
        bez,
        crvena
    }
    [Serializable]
    public class Lajsna : IDomenskiObjekat
    {
        [Browsable(false)]
        public int Id { get; set; }
        [DisplayName("Naziv")]
        public string NazivLajsne { get; set; }
        [DisplayName("Boja")]
        public Boja Boja { get; set; }
        public double CenaMetra { get; set; }
        public double UkupnaDuzina { get; set; }

        [Browsable(false)]
        public string Tabela => "Lajsna";
        [Browsable(false)]

        public int ID => 0;

        [Browsable(false)]
        public string VrednostiZaInsert => $" '{NazivLajsne}',{CenaMetra},{UkupnaDuzina},'{Boja.ToString()}'";

        [Browsable(false)]
        public string UslovZaPretragu => $" where Id = {Id}";

        [Browsable(false)]
        public string NaziviKolona => " Id,NazivLajsne,CenaMetra,UkupnaDuzina,Boja";

        [Browsable(false)]
        public string PostaviVrednostiKolona => $" NazivLajsne='{NazivLajsne}',CenaMetra={CenaMetra},UkupnaDuzina={UkupnaDuzina}, Boja='{Boja.ToString()}'";

        [Browsable(false)]
        public string Identifikator => "Id";

        public List<IDomenskiObjekat> VratiListu(SqlDataReader reader)
        {
            List<IDomenskiObjekat> lajsne = new List<IDomenskiObjekat>();
            while (reader.Read())
            {
                Lajsna l = new Lajsna
                {
                    Id = (int)reader[0],
                    NazivLajsne = reader[1].ToString(),
                    CenaMetra = (double)reader[2],
                    UkupnaDuzina = (double)reader[3],
                    Boja = (Boja)Enum.Parse(typeof(Boja), reader[4].ToString())
                };
                lajsne.Add(l);
            }
            return lajsne;
        }

        public IDomenskiObjekat VratiObjekat(SqlDataReader reader)
        {
            try
            {
                Lajsna lajsna = new Lajsna();
                while (reader.Read())
                {
                    lajsna.Id = (int)reader[0];
                    lajsna.NazivLajsne = reader[1].ToString();
                    lajsna.CenaMetra = (double)reader[2];
                    lajsna.UkupnaDuzina = (double)reader[3];
                    lajsna.Boja = (Boja)Enum.Parse(typeof(Boja), reader[4].ToString());
                    break;
                }
                if (string.IsNullOrEmpty(lajsna.NazivLajsne)) throw new Exception();
                return lajsna;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
