using Domen;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrokerBaze
{
    public class Broker
    {
        private SqlConnection connection;
        private SqlTransaction transaction;
        public Broker()
        {
            connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ISInjac;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        public void OpenConnection()
        {
            connection.Open();
        }
        public void CloseConnection()
        {
            connection.Close();
        }
        public void BeginTransaction()
        {
            transaction = connection.BeginTransaction();
        }
        public void Commit()
        {
            transaction.Commit();
        }
        public void Rollback()
        {
            transaction.Rollback();
        }
        public List<IDomenskiObjekat> VratiSveObjekte(IDomenskiObjekat objekat)
        {
            SqlCommand commmand = new SqlCommand("", connection, transaction);
            commmand.CommandText = $"select * from {objekat.Tabela}";
            SqlDataReader reader = commmand.ExecuteReader();
            List<IDomenskiObjekat> rez = objekat.VratiListu(reader);
            reader.Close();
            return rez;
        }

        public IDomenskiObjekat VratiObjekat(IDomenskiObjekat objekat)
        {
            SqlCommand commmand = new SqlCommand("", connection, transaction);
            commmand.CommandText = $"select * from {objekat.Tabela} {objekat.UslovZaPretragu}";
            SqlDataReader reader = commmand.ExecuteReader();
            IDomenskiObjekat rez = objekat.VratiObjekat(reader);
            reader.Close();
            return rez;
        }
        public bool IzmeniObjekat(IDomenskiObjekat objekat)
        {
            SqlCommand commmand = new SqlCommand("", connection, transaction);
            commmand.CommandText = $"update {objekat.Tabela} set {objekat.PostaviVrednostiKolona} {objekat.UslovZaPretragu}";
            if (commmand.ExecuteNonQuery() != 0) return true;
            return false;
        }

        public int ObrisiObjekat(IDomenskiObjekat objekat)
        {
            SqlCommand commmand = new SqlCommand("", connection, transaction);
            commmand.CommandText = $"delete from {objekat.Tabela} {objekat.UslovZaPretragu}";
            return commmand.ExecuteNonQuery();
        }
        public int SacuvajObjekat(IDomenskiObjekat objekat)
        {
            SqlCommand commmand = new SqlCommand("", connection, transaction);
            commmand.CommandText = $"insert into {objekat.Tabela} output inserted.Id values ({objekat.VrednostiZaInsert})";
            int id = (int)commmand.ExecuteScalar();
            return id;
        }
        public List<IDomenskiObjekat> VratiObjekteKriterijum(string kriterijum, IDomenskiObjekat objekat)
        {
            SqlCommand commmand = new SqlCommand("", connection, transaction);
            commmand.CommandText = $"select * from {objekat.Tabela} {kriterijum}";
            SqlDataReader reader = commmand.ExecuteReader();
            List<IDomenskiObjekat> rez = objekat.VratiListu(reader);
            reader.Close();
            return rez;
        }
    }
}
