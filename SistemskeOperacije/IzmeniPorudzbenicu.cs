using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domen;

namespace SistemskeOperacije
{
    public class IzmeniPorudzbenicu : OpstaSistemskaOperacija
    {
        public Porudzbenica Porudzbenica;
        protected override void IzvrsiKonkretnuOperaciju(IDomenskiObjekat objekat)
        {
            if (broker.IzmeniObjekat(objekat))
            {
                List<StavkaPorudzbenice> stavke = ((Porudzbenica)objekat).Stavke;
                foreach (StavkaPorudzbenice s in stavke)
                {
                    StavkaPorudzbenice pom = broker.VratiObjekat(s) as StavkaPorudzbenice;
                    if (pom != null) broker.IzmeniObjekat(s);
                    else broker.SacuvajObjekat(s);
                }
                Porudzbenica = objekat as Porudzbenica;
            }
            else Porudzbenica = null;
        }

        protected override void Validacija(IDomenskiObjekat objekat)
        {
            if (!(objekat is Porudzbenica))
                throw new Exception("Objekat nije porudzbenica!");
        }
    }
}
