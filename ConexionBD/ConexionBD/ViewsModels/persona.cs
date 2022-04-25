using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace ConexionBD.ViewsModels
{
    public class persona
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Edad  { get; set; }
    }
}
