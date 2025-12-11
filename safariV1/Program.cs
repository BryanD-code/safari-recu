using Safari.Vista;
using System;
using System.Windows.Forms;

namespace Safari
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmSafari());
        }
    }
}
