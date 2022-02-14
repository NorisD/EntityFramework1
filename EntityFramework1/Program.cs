using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using EntityFramework1.Model;
using EntityFramework1.BLL;

/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using EntityFramework1.Model;
using EntityFramework1.BLL;
using System.Windows.Forms;
*/


namespace EntityFramework1
{
    public class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Brogliaccio());
        }
        
    }
}
