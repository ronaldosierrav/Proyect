using ConexionBD.ViewsModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ConexionBD.Views
{
    public class Lstpersona
    {
        public ObservableCollection<persona> lst2persona { get; set; }
        public Lstpersona()
        {
            lst2persona = new ObservableCollection<persona>();
            lst2persona.Add(new persona { Nombre = "", Apellido = "", Edad = 0 });
        }
    }

}
