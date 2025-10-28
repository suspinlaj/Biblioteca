using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca_SusanaPineros
{
    // Clase para compartir los datos entre pantallas
    public static class DatosLibros
    {
        public static List<Libro> biblioteca { get; set; } = new List<Libro>();

    }
}
