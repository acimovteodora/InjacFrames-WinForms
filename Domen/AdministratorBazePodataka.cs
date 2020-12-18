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
    public class AdministratorBazePodataka : Zaposleni,IDomenskiObjekat
    {
        public string Email { get; set; }
        [Browsable(false)]

        public int ID => 0;

        public new string Tabela => "AdministratorBazaPodataka";

        public new string VrednostiZaInsert => $" {Email}";

        public new string UslovZaPretragu => $" where Id = {Id}";

        public new string NaziviKolona => " Email";

        public new string PostaviVrednostiKolona => $" Email = {Email}";

        public new List<IDomenskiObjekat> VratiListu(SqlDataReader reader)
        {
            throw new NotImplementedException();
        }

        public new IDomenskiObjekat VratiObjekat(SqlDataReader reader)
        {
            try
            {
                AdministratorBazePodataka abp = new AdministratorBazePodataka();
                while (reader.Read())
                {
                    abp = new AdministratorBazePodataka()
                    {
                        Id = (int)reader["Id"],
                        Email = reader["Email"].ToString()
                    };
                    break;
                }
                if (string.IsNullOrEmpty(abp.Email)) throw new Exception();
                return abp;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
