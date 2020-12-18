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
    public class Porudzbenica : IDomenskiObjekat
    {
        [Browsable(false)]
        public int Id { get; set; }
        [DisplayName("Datum od")]
        public DateTime DatumOd { get; set; }
        [DisplayName("Datum do")]
        public DateTime DatumDo { get; set; }
        [DisplayName("Suma")]
        public double UkupnaCena { get; set; }
        [Browsable(false)]
        public Zaposleni Zaposleni { get; set; }
        [Browsable(false)]
        public Kompanija Kompanija { get; set; }
        public List<StavkaPorudzbenice> Stavke { get; set; }
        [DisplayName("Kompanija")]
        public string KompanijaString => Kompanija.NazivKompanije;
        [Browsable(false)]
        public string Tabela => "Porudzbenica";
        [Browsable(false)]

        public int ID => 0;

        [Browsable(false)]
        public string VrednostiZaInsert => $" '{DatumOd}','{DatumDo}',{UkupnaCena},{Zaposleni.Id},{Kompanija.Id}";

        [Browsable(false)]
        public string UslovZaPretragu => $" where Id = {Id}";

        [Browsable(false)]
        public string NaziviKolona => "Id,DatumOd,DatumDo,UkupnaCena,IdZaposlenog,IdKompanije";

        [Browsable(false)]
        public string PostaviVrednostiKolona => $" DatumOd='{DatumOd}',DatumDo='{DatumDo}',UkupnaCena={UkupnaCena},IdZaposlenog={Zaposleni.Id},IdKompanije={Kompanija.Id}";

        [Browsable(false)]
        public string Identifikator => "Id";

        public List<IDomenskiObjekat> VratiListu(SqlDataReader reader)
        {
            List<IDomenskiObjekat> porudzbenice = new List<IDomenskiObjekat>();
            while (reader.Read())
            {
                Porudzbenica porudzb = new Porudzbenica();
                porudzb.Id = (int)reader[0];
                porudzb.DatumOd = (DateTime)reader[1];
                porudzb.DatumDo = (DateTime)reader[2];
                porudzb.UkupnaCena = (double)reader[3];
                porudzb.Zaposleni = new Zaposleni { Id = (int)reader[4] };
                porudzb.Kompanija = new Kompanija {Id = (int)reader[5] };
                porudzbenice.Add(porudzb);
            }
            return porudzbenice;
        }

        public IDomenskiObjekat VratiObjekat(SqlDataReader reader)
        {
            try
            {
                Porudzbenica porudzb = new Porudzbenica();
                while (reader.Read())
                {
                    porudzb.Id = (int)reader[0];
                    porudzb.DatumOd = (DateTime)reader[1];
                    porudzb.DatumDo = (DateTime)reader[2];
                    porudzb.UkupnaCena = (double)reader[3];
                    porudzb.Zaposleni = new Zaposleni { Id = (int)reader[4] };
                    porudzb.Kompanija = new Kompanija { Id = (int)reader[5] };
                    break;
                }
                if (porudzb.Id <= 0) throw new Exception();
                return porudzb;
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
            Porudzbenica p = (Porudzbenica)obj;
            foreach (StavkaPorudzbenice s in Stavke)
            {
                if (!p.Stavke.Contains(s)) return false;
                foreach (StavkaPorudzbenice ps in p.Stavke)
                {
                    if (s.Equals(ps))
                    {
                        if (s.DuzinaLajsne != ps.DuzinaLajsne) return false;
                    }
                }
            }
            foreach (StavkaPorudzbenice s in p.Stavke)
            {
                if (!Stavke.Contains(s)) return false;
            }
            if(DatumDo != p.DatumDo) return false;
            return true;
        }
    }
}
