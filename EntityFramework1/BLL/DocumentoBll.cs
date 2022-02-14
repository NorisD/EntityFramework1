using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFramework1.Model;

namespace EntityFramework1.BLL
{
    public class DocumentoBll
    {
        ///  1. Istanzo classe interazzione con il modelo (dichiarato in CustomWideWorkDImportersModel

        private WideWorldImportEntities _DbModelEntities;

        public partial class DEMODOC_DETT0 : DEMODOC_DETT
        {
           //  public DEMODOC_DETT DETTAGLIO { get; set; }
            public string MVCODCON { get; set; }
            public string MVTIPDOC { get; set; }
            public DateTime MVDATDOC { get; set; }
        }


            /// 2. Declaro costruttore

            public DocumentoBll(string @connectionString)
        {
            try
            {
                _DbModelEntities = new WideWorldImportEntities(@connectionString);
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
        }

        /// 3. Clase dettaglio documento
                //Elenco DOCU
        public List<DEMODOC_DETT> GetDocumento(DateTime dataini, DateTime datafin, DEMODOC_MAST modelloDoc)
        {
            List<DEMODOC_MAST> testatafiltro = _DbModelEntities.DEMODOC_MAST.Where(d => 
            d.MVDATDOC >= dataini && d.MVDATDOC <= datafin && d.MVTIPDOC.Equals(modelloDoc.MVTIPDOC)).ToList();

            List<DEMODOC_DETT> documento1 = new List<DEMODOC_DETT>();

            List<DEMODOC_DETT> tuttidocumenti = new List<DEMODOC_DETT>();

            // Elenco testata in rango di date
            foreach (var documento in testatafiltro)
            {
                documento1 = _DbModelEntities.DEMODOC_DETT.Where(
                p => p.MVSERIAL.Equals(documento.MVSERIAL) ).ToList();
                if( documento1 != null )
                {
                    tuttidocumenti.AddRange( documento1 );
                }
            }
           
            return tuttidocumenti;
        }

        /// 3. Clase dettaglio documento
        //JOIN Elenco DOCU
        public List<DEMODOC_DETT> GetDocumenti(DateTime dataini, DateTime datafin, DEMODOC_MAST modelloDoc)
        {
            List<DEMODOC_DETT> elencoDoc = (from dtt in _DbModelEntities.DEMODOC_DETT
                              join testa in _DbModelEntities.DEMODOC_MAST on dtt.MVSERIAL equals testa.MVSERIAL
                              where testa.MVDATDOC >= dataini && testa.MVDATDOC <= datafin && testa.MVTIPDOC.Equals(modelloDoc.MVTIPDOC)
                              select dtt).ToList();

            var listadoc0 = _DbModelEntities.DEMODOC_DETT    // your starting point - table in the "from" statement
                         .Join(_DbModelEntities.DEMODOC_MAST, // the source table of the inner join
                            dett => dett.MVSERIAL,        // Select the primary key (the first part of the "on" clause in an sql "join" statement)
                            mast => mast.MVSERIAL,   // Select the foreign key (the second part of the "on" clause)
                            (dett, mast) => new { Dett = dett, Mast = mast }) // selection
                         .Where(filto => filto.Mast.MVDATDOC >= dataini && filto.Mast.MVDATDOC <= datafin &&
                          filto.Mast.MVTIPDOC.Equals(modelloDoc.MVTIPDOC))
                     // ;
                     // .Select(lista => new { lista.Dett.MVSERIAL, lista.Dett.CPROWNUM, lista.Dett.MVCODICE, lista.Mast.MVDATDOC, lista.Mast.MVCODCON });
                     .Select(lista => new {
                         lista.Dett,
                         lista.Mast.MVDATDOC,lista.Mast.MVCODCON,lista.Mast.MVTIPDOC}).ToList();
            /*
 var dataset = entities.processlists
.Where(x => x.environmentID == environmentid && x.ProcessName == processname && x.RemoteIP == remoteip && x.CommandLine == commandlinepart)
.Select(x => new { x.ServerName, x.ProcessID, x.Username }).ToList(); 
            */
            // List<DEMODOC_DETT0> x = new List<DEMODOC_DETT0>();
            //    x = listadoc0;

            return elencoDoc;
        }
    }
}
