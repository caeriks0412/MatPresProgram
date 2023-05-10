using MVPMatpres.Models;
using MVPMatpres.Views;
using MVPMatpres.Presenter;
using MVPMatpres.Repositories;
using System.Diagnostics;

namespace MVPMatpres
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            string server = "";
            string connection = "";

            IMaterialView materialView = new MaterialView();
            IMaterialRepository materialRepository = new MaterialRepository(connection);
            MaterialPresenter presenter = new MaterialPresenter(materialView, materialRepository);

            Application.Run((Form) materialView);
        }
    }
}