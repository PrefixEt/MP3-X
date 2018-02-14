using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MP3_X.O;

namespace MP3_X
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm view = new MainForm();
            FileMod Module = new FileMod();
            MainPresenter presenter = new MainPresenter(view, Module);
            Application.Run(view);

         }
    }
}
