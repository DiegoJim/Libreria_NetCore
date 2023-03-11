using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Prueba_Tecnica.Models
{
    public class libro_list
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "La editorial es obligatoria.")]
        [Display(Name = "Editorial")]
        public int EditorialId { get; set; }

        [Required(ErrorMessage = "El título es obligatorio.")]
        [StringLength(50, ErrorMessage = "El {0} debe ser al menos {2} y máximo {1} caracteres.", MinimumLength = 3)]
        [Display(Name = "Título")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "La sinopsis es obligatoria.")]
        [StringLength(50, ErrorMessage = "El {0} debe ser al menos {2} y máximo {1} caracteres.", MinimumLength = 3)]
        [Display(Name = "Descripción")]
        public string Sinopsis { get; set; }

        [Required(ErrorMessage = "El número de páginas es obligatorio.")]
        public string n_paginas { get; set; }

        [Required(ErrorMessage = "El autor es obligatorio.")]
        [Display(Name = "Autor")]
        public int autor { get; set; }
        public autores_has_libros Autores_Has_Libros { get; set; }
        public List<autores> autores { get; set; }
        public List<editorial> editoriales { get; set; }
        public editorial Editorial { get; set; }
    }
}
