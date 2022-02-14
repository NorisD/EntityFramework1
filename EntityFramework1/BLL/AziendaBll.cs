using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFramework1.Model;

namespace EntityFramework1.BLL
{
    public class AziendaBll
    {
        private WideWorldImportEntities _DbModelEntities;

        public  AziendaBll(string @connectionString){
            try
            {
                _DbModelEntities = new WideWorldImportEntities(@connectionString);
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
        }
        //Elenco Aziende
        public List<AZIENDA> GetAziende()
        {
            List<AZIENDA> aziendes = new List<AZIENDA>();
            aziendes = _DbModelEntities.AZIENDA.ToList();
            return aziendes;
        }
        public AZIENDA GetUnAzienda( string AziendaID)
        {
            AZIENDA azienda = new AZIENDA();
            azienda = _DbModelEntities.AZIENDA.FirstOrDefault(p => p.AZCODAZI.Equals(AziendaID));
            return azienda;
        }

        public AZIENDA UpdateAzienda( string AziendaID, AZIENDA mAzienda)
        {
            if (AziendaID != null)
            {
                AZIENDA aziendaUpd = _DbModelEntities.AZIENDA.FirstOrDefault(p => p.AZCODAZI.Equals(AziendaID));
                if (aziendaUpd != null)
                {
                    aziendaUpd.AZINDAZI = mAzienda.AZINDAZI;
                }
                else { 
                    return null;
                }
                _DbModelEntities.SaveChanges();
                return aziendaUpd;
            }
            return null;
        }
    }
}
