using Microsoft.AspNetCore.Mvc;
using Prueba_Tecnica.Data;
using Prueba_Tecnica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prueba_Tecnica.Controllers
{
    public class LibrosController : Controller
    {

        //Instancia Para la conexión con la báse de datos
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Constructor del método
        /// </summary>
        /// <param name="context"></param>
        public LibrosController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Método encargado de iniciar la vista con los datos de los libros
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            try
            {
                //Obtenemos el modelo que vamos a retornar a la vista
                var result = from Libro in _context.Libro
                             join Editorial
                             in _context.Editorial
                             on Libro.EditorialId
                             equals Editorial.Id
                             select new Libro
                             {
                                 Id = Libro.Id,
                                 Editorial = Editorial,
                                 EditorialId = Libro.EditorialId,
                                 Titulo = Libro.Titulo,
                                 Sinopsis = Libro.Sinopsis,
                                 n_paginas = Libro.n_paginas
                             };

                //Retornamos la vista
                return View(result);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        /// <summary>
        /// Método encargado de las acciones de retornar la vista
        /// </summary>
        /// <param name="add_libro"></param>
        /// <returns></returns>
        public IActionResult Create()
        {
            try
            {

                //Traemos listas de autores y editoriales para la inserción
                libro_list listas = new libro_list();
                listas.editoriales = _context.Editorial.ToList();
                listas.autores = _context.Autores.ToList();

                //Retornamos estas listas
                return View(listas);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Método encargado de las acciones de retornar la vista
        /// </summary>
        /// <param name="add_libro"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(libro_list add_libro)
        {

            //Creamos el objeto a enviar
            Libro libro = new Libro()
            {

                EditorialId = add_libro.EditorialId,
                Titulo = add_libro.Titulo,
                Sinopsis = add_libro.Sinopsis,
                n_paginas = add_libro.n_paginas
            };


            //Realizamos nuestra validación del módelo
            if (ModelState.IsValid)
            {
                //agregamos el libro al contexto
                _context.Libro.Add(libro);
                //Guardamos los cambios
                _context.SaveChanges();

                //Obtenemos el identity recien insertado
                int id = libro.Id;

                //Validamos si la variable es mayor a 0
                if (id > 0)
                {

                    //Creamos el modelo a enviar para guardar el autor del libro
                    autores_has_libros data = new autores_has_libros()
                    {
                        autoresId = add_libro.autor,
                        LibroId = id
                    };

                    //agregamos el autor al contexto
                    _context.Autores_Has_Libros.Add(data);
                    //Guardamos los cambios
                    _context.SaveChanges();
                }

                TempData["mensaje"] = "El libro se ha guardado de manera correcta.";
                return RedirectToAction("Index");
            }

            //Traemos listas de autores y editoriales para la inserción
            libro_list listas = new libro_list();
            listas.editoriales = _context.Editorial.ToList();
            listas.autores = _context.Autores.ToList();

            //Retornamos la vista
            return View(listas);

        }

        /// <summary>
        /// Método encargado traer listas con la vista de editar
        /// </summary>
        /// <returns></returns>
        public IActionResult Edit(int? Id)
        {
            //Realizamos la validación de que el Id no venga nulo o vacio
            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            //Validamos el autor del libro
            int autor = _context.Autores_Has_Libros.Where(x => x.LibroId == Id).Select(x => x.autoresId).FirstOrDefault();

            //creamos la variable que retornaremos
            var result = _context.Libro.Where(x => x.Id == Id).Select(x => new libro_list()
            {

                Titulo = x.Titulo,
                EditorialId = x.EditorialId,
                Sinopsis = x.Sinopsis,
                n_paginas = x.n_paginas,
                autor = autor

            }).FirstOrDefault();

            //Validamos que el libro si exista
            if (result == null)
            {
                return NotFound();
            }

            //Traemos listas de autores y editoriales para la inserción
            result.editoriales = _context.Editorial.ToList();
            result.autores = _context.Autores.ToList();

            //Retornamos estas listas
            return View(result);
        }

        /// <summary>
        /// Método encargado de realizar la edición en báse de datos
        /// </summary>
        /// <param name="add_libro"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Edit(libro_list add_libro)
        {


            //Creamos el objeto a enviar
            Libro libro = new Libro()
            {
                Id = add_libro.Id,
                EditorialId = add_libro.EditorialId,
                Titulo = add_libro.Titulo,
                Sinopsis = add_libro.Sinopsis,
                n_paginas = add_libro.n_paginas
            };


            //Realizamos nuestra validación del módelo
            if (ModelState.IsValid)
            {
                //agregamos el libro al contexto
                _context.Libro.Update(libro);
                //Guardamos los cambios
                _context.SaveChanges();

                //Obtenemos el identity recien insertado
                int id = libro.Id;

                //Validamos si la variable es mayor a 0
                if (id > 0)
                {

                    //Buscamos al libro al que le haremos el cambio de autor
                    var query = _context.Autores_Has_Libros.Where(x => x.LibroId == id).Select(x => x).FirstOrDefault();
                    //eliminamos el libro
                    _context.Autores_Has_Libros.Remove(query);
                    //Guardamos los cambios
                    _context.SaveChanges();

                    //Creamos el modelo a enviar para guardar el autor del libro
                    autores_has_libros data = new autores_has_libros()
                    {
                        autoresId = add_libro.autor,
                        LibroId = id
                    };

                    //agregamos el autor al contexto
                    _context.Autores_Has_Libros.Add(data);
                    //Guardamos los cambios
                    _context.SaveChanges();
                }
                //Cargamos la alerta
                TempData["mensaje"] = "El libro se ha editado de manera correcta.";
                return RedirectToAction("Index");
            }

            //Traemos listas de autores y editoriales para la inserción
            libro_list listas = new libro_list();
            listas.editoriales = _context.Editorial.ToList();
            listas.autores = _context.Autores.ToList();

            //Retornamos la vista
            return View(listas);

        }

        /// <summary>
        /// Metodo encargado eliminar la eliminación del libro
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IActionResult Delete(int? Id)
        {
            //Buscamos al libro al que eliminaremos en autores
            var query = _context.Autores_Has_Libros.Where(x => x.LibroId == Id).Select(x => x).FirstOrDefault();
            //eliminamos el libro autores y relación 
            _context.Autores_Has_Libros.Remove(query);
            //Guardamos los cambios
            _context.SaveChanges();


            //Buscamos al libro al que eliminaremos en autores
            var libro = _context.Libro.Where(x => x.Id == Id).Select(x => x).FirstOrDefault();
            //eliminamos el libro autores y relación 
            _context.Libro.Remove(libro);
            //Guardamos los cambios
            _context.SaveChanges();

            //Cargamos la alerta
            TempData["mensaje"] = "El libro se ha eliminado de manera correcta.";
            return RedirectToAction("Index");
        }

    }
}
