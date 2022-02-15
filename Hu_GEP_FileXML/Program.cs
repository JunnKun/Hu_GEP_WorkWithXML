using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Xml.Schema;

namespace Hu_GEP_FileXML
{
    public class Legame
    {
        public int codice;
        public string spec;
        public string legami = ".\\LEGAMI.txt";

        public List<Legame> get_listLegami()
        {
            List<Legame> Legami = new List<Legame>();
            using (StreamReader sr = new StreamReader(legami))
            {
                while (sr.Peek() >= 0)
                {
                    string str;
                    string[] strArray;
                    string[] codeArray;
                    str = sr.ReadLine();

                    strArray = str.Split(';');
                    Legame currentLink = new Legame();
                    codeArray = strArray[0].Split(',');
                    currentLink.codice = Convert.ToInt32(codeArray[0]);
                    currentLink.spec = strArray[1];
                    // Console.WriteLine(currentLink.codice + " " + currentLink.spec);
                    Legami.Add(currentLink);
                }
            }
            return Legami;
        }
    }
    public class TipoRetta
    {
        public string spec;
        public string servizio;
        string tipi_rete = ".\\TIPI_RETTE.txt";

        public List<TipoRetta> get_listRette()
        {
            List<TipoRetta> TipoRette = new List<TipoRetta>();
            using (StreamReader sr = new StreamReader(tipi_rete))
            {
                while (sr.Peek() >= 0)
                {
                    string str;
                    string[] strArray;
                    string[] codeArray;
                    str = sr.ReadLine();

                    strArray = str.Split(';');
                    TipoRetta currentRette = new TipoRetta();
                    currentRette.spec = strArray[0];
                    currentRette.servizio = strArray[1];
                    // Console.WriteLine(currentRette.spec + " " + currentRette.servizio);
                    TipoRette.Add(currentRette);
                }
            }
            return TipoRette;
        }
    }
    public class Utente
    {
        public int codice;
        public string cognome;
        public string nome;
        public string data_nascita;
        public char sesso;
        public string scuola;
        public int classe;
        public char sezione;
        public string cogn_geni;
        public string nom_geni;
        string utenti = ".\\UTENTI.txt";

        public List<Utente> get_listUtenti()
        {
            List<Utente> Utenti = new List<Utente>();
            using (StreamReader sr = new StreamReader(utenti))
            {
                while (sr.Peek() >= 0)
                {
                    string str;
                    string[] strArray;
                    string[] codeArray;
                    str = sr.ReadLine();

                    strArray = str.Split(';');
                    Utente currentUtente = new Utente();
                    codeArray = strArray[0].Split(',');
                    currentUtente.codice = Convert.ToInt32(codeArray[0]);
                    currentUtente.cognome = strArray[1];
                    currentUtente.nome = strArray[2];
                    currentUtente.data_nascita = strArray[3];
                    currentUtente.sesso = Convert.ToChar(strArray[4]);
                    currentUtente.scuola = strArray[5];
                    currentUtente.classe = Convert.ToInt32(strArray[6]);
                    currentUtente.sezione = Convert.ToChar(strArray[7]);
                    currentUtente.cogn_geni = strArray[8];
                    currentUtente.nom_geni = strArray[9];
                    Utenti.Add(currentUtente);
                }
            }
            return Utenti;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlAttribute attribute;
            string file_result = ".\\utenti_rette.xml";

            Legame legame = new Legame();
            TipoRetta tiporette = new TipoRetta();
            Utente utente = new Utente();

            List<Legame> Legami = new List<Legame>();
            List<TipoRetta> TipoRette = new List<TipoRetta>();
            List<Utente> Utenti = new List<Utente>();

            Legami = legame.get_listLegami();
            TipoRette = tiporette.get_listRette();
            Utenti = utente.get_listUtenti();

            //
            //  CREO FILE
            //

            XmlNode dichiarazione = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            xmlDoc.RemoveAll();
            xmlDoc.AppendChild(dichiarazione);

            XmlNode nodo_root = xmlDoc.CreateElement("utenti_rette");
            xmlDoc.AppendChild(nodo_root);
            xmlDoc.Save(file_result);

            for (int i = 0; i < Utenti.Count; i++)
            {
                XmlNode utenteNode = xmlDoc.CreateElement("utente");
                attribute = xmlDoc.CreateAttribute("Altro_attributo");
                attribute.Value = "Sono un altro valore: " + Convert.ToString(i + 1);
                utenteNode.Attributes.Append(attribute);
                nodo_root.AppendChild(utenteNode);

                //
                //  AGGIUNTA UTENTI
                //

                XmlNode codiceNode = xmlDoc.CreateElement("codice");
                attribute.Value = Utenti[i].codice.ToString();
                codiceNode.Attributes.Append(attribute);
                nodo_root.AppendChild(utenteNode);
            }
            xmlDoc.Save(file_result);
        }
    }
}
