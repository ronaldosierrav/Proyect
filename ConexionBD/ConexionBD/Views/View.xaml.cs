using ConexionBD.ViewsModels;
using ConexionBD.Views;
using ConexionBD.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ConexionBD.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class View : ContentPage
    {
        public View()
        {
            InitializeComponent();
            llenarDat();
        }

        private async void btnRegistrar_Clicked(object sender, EventArgs e)
        {
            if (datos())
            {
                persona Persona = new persona
                {
                    Nombre = txtNombre.Text,
                    Apellido = txtApellido.Text,
                    Edad = int.Parse(txtEdad.Text),
                };
                await App.SQLiteDB.SavePersonaAsync(Persona);
                txtNombre.Text = "";
                txtApellido.Text = "";
                txtEdad.Text = "";
                await DisplayAlert("Alerta", "Se han guardado los datos correctamente", "Aceptar");
                llenarDat();
            }

                
            else
            {
                await DisplayAlert("Error", "Ingresar todos Los Datos", "ok");
            }

            
            }

        public async void llenarDat()
        {
           
            var personaList = await App.SQLiteDB.GetpersonasAsync();
            if (personaList != null)
            {
                Listpersona.ItemsSource = personaList;
            }
        }
         public bool datos()
             {
            bool resp;
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                resp = false;
            }
            else if (string.IsNullOrEmpty(txtApellido.Text))
            {
                resp = false;
            }
            else if (string.IsNullOrEmpty(txtEdad.Text))
            {
                resp = false;
            }
            else
            {
                resp = true;
            }
            return resp;
        }

       
        private async void btnActualizar_Clicked_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdpersona.Text))
            {
                persona Persona = new persona()
                {
                    Id = Convert.ToInt32(txtIdpersona.Text),
                    Nombre = txtNombre.Text,
                    Apellido = txtApellido.Text,
                    Edad = Convert.ToInt32(txtEdad.Text),
                };
                await App.SQLiteDB.SavePersonaAsync(Persona);
                await DisplayAlert("Registro", "Se Actualizo de forma correcta", "Ok");

                txtIdpersona.Text = "";
                txtNombre.Text = "";
                 txtApellido.Text = "";
                txtEdad.Text = "";
                txtIdpersona.IsVisible = false;
                btnRegistrar.IsVisible = true;
                btnActualizar.IsVisible = false;
                btnEliminar.IsVisible = false;
                llenarDat();
            }

        }

        private async void Lstpersona_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (persona)e.SelectedItem;
            btnRegistrar.IsVisible = false;
            txtIdpersona.IsVisible = true;
            btnActualizar.IsVisible = true;
            btnEliminar.IsVisible = true;
            if (!string.IsNullOrEmpty(obj.Id.ToString()))
            {
                var persona = await App.SQLiteDB.GetPersonaByIdAsync(obj.Id);
                if (persona != null)
                {
                    txtIdpersona.Text = persona.Id.ToString();
                    txtNombre.Text = persona.Nombre;
                    txtApellido.Text = persona.Apellido;
                    txtEdad.Text = persona.Edad.ToString();


                }
            }
        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            var persona = await App.SQLiteDB.GetPersonaByIdAsync(Convert.ToInt32(txtIdpersona.Text));
            if (persona != null)
            {
                await App.SQLiteDB.DeletePersonaAsync(persona);
                await DisplayAlert("Alumno", "Se elimino persona correctamente", "ok");
                txtIdpersona.Text = "";
                txtNombre.Text = "";
                txtApellido.Text = "";
                txtEdad.Text = "";
                llenarDat();
                btnRegistrar.IsVisible = true;
                btnActualizar.IsVisible = false;
                btnEliminar.IsVisible = false;
                txtIdpersona.IsVisible = false;
            }

        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            Listpersona.ItemsSource = await App.SQLiteDB.GetpersonasAsync();
        }

        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var lstpersona = await App.SQLiteDB.GetpersonasAsync();
            Listpersona.ItemsSource = lstpersona.Where(x => x.Nombre.Contains(e.NewTextValue));
        }

        private async void btnLista_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Lista());
        }

        private async void BuscarUno(object sender, EventArgs e)
        {

            persona personafound = await App.SQLiteDB.GetPersonaByIdAsync(Convert.ToInt32(txtIdpersona.Text));
            if (personafound != null)
            {
                txtNombre.Text = personafound.Nombre;
                txtApellido.Text = personafound.Apellido;
                txtEdad.Text = Convert.ToString(personafound.Edad);
            }
            else
            {
                await DisplayAlert("Error", "Persona encontrada", "OK!");
                txtNombre.Text = "";
                txtApellido.Text = "";
                txtEdad.Text = "";
            }
            
        }
    }

}
    
