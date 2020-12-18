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
    public class MenadzerProdaje : Zaposleni,IDomenskiObjekat
    {
        public string KontaktTelefon { get; set; }
        public new string Tabela => "MenadzerProdaje";

        public new string VrednostiZaInsert => $" {KontaktTelefon}";

        public new string UslovZaPretragu => $" where Id = {Id}";

        public new string NaziviKolona => " KontaktTelefon";

        public new string PostaviVrednostiKolona => $" KontaktTelefon = {KontaktTelefon}";
        [Browsable(false)]

        public int ID => 0;

        public new List<IDomenskiObjekat> VratiListu(SqlDataReader reader)
        {
            throw new NotImplementedException();
        }

        public new IDomenskiObjekat VratiObjekat(SqlDataReader reader)
        {
            try
            {
                MenadzerProdaje men = new MenadzerProdaje();
                while (reader.Read())
                {
                    men = new MenadzerProdaje()
                    {
                        Id = (int)reader["Id"],
                        KontaktTelefon = reader["KontaktTelefon"].ToString()
                    };
                    break;
                }
                if (string.IsNullOrEmpty(men.KontaktTelefon)) throw new Exception();
                return men;
            }
            catch (Exception)
            {
                return null;
            }
        }        
    }
}
