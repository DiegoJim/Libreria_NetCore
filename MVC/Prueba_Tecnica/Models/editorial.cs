using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Prueba_Tecnica.Models
{
    public class editorial
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El Nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "El {0} debe ser al menos {2} y máximo {1} caracteres.", MinimumLength = 3)]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "La sede es obligatoria.")]
        [StringLength(50, ErrorMessage = "El {0} debe ser al menos {2} y máximo {1} caracteres.", MinimumLength = 3)]
        public string sede { get; set; }
        public List<Libro> Libros { get; set; }
    }
}
