using BrokerBaze;
using Domen;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemskeOperacije
{
    public abstract class OpstaSistemskaOperacija
    {
        protected Broker broker = new Broker();
        public void IzvrsiSO(IDomenskiObjekat objekat)
        {
            try
            {
                broker.OpenConnection();
                broker.BeginTransaction();
                Validacija(objekat);
                IzvrsiKonkretnuOperaciju(objekat);
                broker.Commit();
            }
            catch (Exception ex)
            {
                Debug.Write(">>>" + ex.Message);
                broker.Rollback();
                throw;
            }
            finally
            {
                broker.CloseConnection();
            }
        }

        protected abstract void Validacija(IDomenskiObjekat objekat);

        protected abstract void IzvrsiKonkretnuOperaciju(IDomenskiObjekat objekat);
    }
}
