using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domen;
using System.Configuration;

namespace Server
{
    public class Server
    {
        private Socket osluskujuciSoket;
        private List<Socket> soketi = new List<Socket>();
        private List<Obrada> obrade = new List<Obrada>();
        public List<Zaposleni> Zaposleni = new List<Zaposleni>();

        internal bool PokreniServer()
        {
            try
            {
                osluskujuciSoket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                string vrednostPorta = ConfigurationManager.AppSettings["port"];
                int port = int.Parse(vrednostPorta);
                osluskujuciSoket.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), port));
                osluskujuciSoket.Listen(2);
                return true;
            }
            catch (SocketException)
            {
                return false;
            }
        }

        internal void Osluskuj()
        {
            try
            {
                bool kraj = true;
                while (kraj)
                {
                    try
                    {
                        Socket klijentskiSoket = osluskujuciSoket.Accept();
                        Obrada obrada = new Obrada(klijentskiSoket, this);
                        obrade.Add(obrada);
                        Thread nitKlijenta = new Thread(obrada.ObradaZahteva);
                        nitKlijenta.IsBackground = true;
                        nitKlijenta.Start();
                    }
                    catch (Exception e)
                    {
                        Debug.Write(">>>>" + e.Message);
                        kraj = false;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Write(">>>>" + e.Message);
            }
        }

        internal void PrekiniServer()
        {
            foreach (Obrada o in obrade)
            {
                o.klijentskiSoket.Close();
            }
            obrade.Clear();
            osluskujuciSoket.Close();
        }
    }
}
