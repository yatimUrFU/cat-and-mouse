using System;
using System.Windows.Forms;
using cat_and_mouse.Domain;

namespace cat_and_mouse
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TypeOfGameForm());

            // while (true)
            // {
            //     Application.Frame();
            // }
        }
    }
}