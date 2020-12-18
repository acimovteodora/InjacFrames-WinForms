using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domen
{
    public interface IDomenskiObjekat
    {
        string Tabela { get; }
        string VrednostiZaInsert { get; }
        string UslovZaPretragu { get; }
        string NaziviKolona { get; }
        string PostaviVrednostiKolona { get; }
        string Identifikator { get; }
        int ID { get; }
        List<IDomenskiObjekat> VratiListu(SqlDataReader reader);
        IDomenskiObjekat VratiObjekat(SqlDataReader reader);
    }
}
