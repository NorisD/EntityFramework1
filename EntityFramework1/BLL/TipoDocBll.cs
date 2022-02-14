using EntityFramework1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework1.BLL
{
    public class TipoDocBll
    {
        private WideWorldImportEntities _DbModelEntities;

        public TipoDocBll(string @connectionString)
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
        //Elenco Tipo Documento
        public List<DEMOTIP_DOCU> GetTipoDocu()
        {
            List<DEMOTIP_DOCU> tipodocs = new List<DEMOTIP_DOCU>();
            tipodocs = _DbModelEntities.DEMOTIP_DOCU.ToList();
            return tipodocs;
        }
      }

}
