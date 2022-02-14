using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using EntityFramework1.Model;
using EntityFramework1.BLL;
using System.Data.SqlClient;

namespace EntityFramework1
{
    public partial class Brogliaccio : Form
    {
        public Brogliaccio()
        {
            InitializeComponent();

            string encConnection = ConfigurationManager.ConnectionStrings["WideWorldImportEntities"].ConnectionString;

            TipoDocBll tipodocubll = new TipoDocBll(encConnection);

            comboBox1.DropDownWidth = 80;


            comboBox1.DataSource = tipodocubll.GetTipoDocu().Select(d => d.TDTIPDOC).ToList();
        }

        private void dataini_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Brogliaccio_Load(object sender, EventArgs e)
        {

        }

        private void Search_Click(object sender, EventArgs e)
        {
            try
            {
                string encConnection = ConfigurationManager.ConnectionStrings["WideWorldImportEntities"].ConnectionString;

                DocumentoBll documentoBll = new DocumentoBll(encConnection);

                DateTime dataIni = this.date1.Value; // new DateTime(2015, 01, 01);
                DateTime dataFin = this.date2.Value;//new DateTime(2015, 05, 01);

                string tipoDoc = comboBox1.Text;

                DEMODOC_MAST modelloDoc = new DEMODOC_MAST();
                modelloDoc.MVTIPDOC = tipoDoc;

                List<DEMODOC_DETT> outDoc_Dett = documentoBll.GetDocumento(dataIni, dataFin, modelloDoc);
                
                // List<DEMODOC_DETT> outDoc_Dett = documentoBll.GetDocumenti(dataIni, dataFin, modelloDoc);

                //AZIENDA azienda = aziendabll.GetUnAzienda(idAzienda);

                if (outDoc_Dett != null)
                {
                    var xlista = outDoc_Dett.GroupBy(m => m.MVSERIAL).Select(x => new
                                {
                                     Seriale = x.Key, Righe = x.Count() 
                                }).ToList();
                    //dataGridView1.DataSource = outDoc_Dett.GroupBy(m => m.MVSERIAL).Select(x => new { serial = x.Key, count = x.Count() });

                    dataGridView1.DataSource = xlista;
                    /*
                    var conta = (outDoc_Dett.GroupBy(p => p.MVSERIAL)).Count();



                    Console.WriteLine("Brogliacio Documenti {0} {1}\n===============================", tipoDoc, conta);
                    var i = 1;
                    foreach (var outDoc in outDoc_Dett)
                    {
                        Console.WriteLine("{1}: {0}", outDoc.MVSERIAL, i++);
                    } */
                }
                else
                {
                    //Console.WriteLine("Non ci sono documenti ");
                    MessageBox("Non ci sono documenti");
                }

               // Console.ReadKey();
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                throw;
            }
                    
        }

        private void MessageBox(string v)
        {
            throw new NotImplementedException();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
