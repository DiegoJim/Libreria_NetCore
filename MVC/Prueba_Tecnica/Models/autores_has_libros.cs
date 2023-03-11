using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prueba_Tecnica.Models
{
    public class autores_has_libros
    {
        public int autoresId { get; set; }
        public int LibroId { get; set; }
        public autores autores { get; set; }
        public Libro Libro { get; set; }
    }
}
