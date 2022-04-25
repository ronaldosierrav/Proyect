using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ConexionBD.Views;
using ConexionBD.Data;
using System.IO;

namespace ConexionBD
{
    
    public partial class App : Application
    {
        static SQLiteH db;

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Views.View());
        }

         public static SQLiteH SQLiteDB
        {
            get
            {
                if (db==null)
                {
                    db = new SQLiteH(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Persona.db3"));
                }
                return db;
            }
        }
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
