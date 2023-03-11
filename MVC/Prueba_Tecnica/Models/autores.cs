using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Prueba_Tecnica.Models
{
    public class autores
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El Nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "El {0} debe ser al menos {2} y máximo {1} caracteres.", MinimumLength = 3)]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El Apellido es obligatorio.")]
        [StringLength(50, ErrorMessage = "El {0} debe ser al menos {2} y máximo {1} caracteres.", MinimumLength = 3)]
        public string Apellido { get; set; }

        public List<autores_has_libros> Autores_Has_Libros { get; set; }
    }
}
