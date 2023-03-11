using Microsoft.AspNetCore.Mvc;
using Prueba_Tecnica.Data;
using Prueba_Tecnica.Models;
using System.Collections.Generic;
using System.Linq;

namespace Prueba_Tecnica.Controllers
{
    public class AutoresController : Controller
    {

        //Instancia Para la conexión con la báse de datos
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Constructor del método
        /// </summary>
        /// <param name="context"></param>
        public AutoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Métodon encargado traer la información inicial
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            //Buscamos los autores
            IEnumerable<autores> ListAutores = _context.Autores;
            return View(ListAutores);
        }

        /// <summary>
        /// Método encargado de cargar la vista de creación de autores
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Método encargado de 
        /// </summary>
        /// <param name="autores"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(autores autores)
        {
            //Validamos si el modelo pasó la validación
            if (ModelState.IsValid)
            {
                //Agregamos el modelo a la entidad
                _context.Autores.Add(autores);
                //Guardamos los cambios
                _context.SaveChanges();
                //Guardamos en temporales el mensaje de alerta
                TempData["mensaje"] = "El autor se ha guardado de manera correcta.";
                //Redireccionamos a la ventana principal de las autores
                return RedirectToAction("Index");
            }

            return View();
        }

        /// <summary>
        /// Método encargado de cargar la vista de edición de autor
        /// </summary>
        /// <returns></returns>
        public IActionResult Edit(int? Id)
        {
            //Realizamos la validación de que el Id no venga nulo o vacio
            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            //Obtenemos la data
            var autor = _context.Autores.Where(x => x.Id == Id).FirstOrDefault();

            if (autor == null)
            {
                return NotFound();
            }

            return View(autor);
        }

        /// <summary>
        /// Método encargado de 
        /// </summary>
        /// <param name="autores"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Edit(autores autores)
        {
            //Validamos si el modelo pasó la validación
            if (ModelState.IsValid)
            {
                //Agregamos el modelo a la entidad
                _context.Autores.Update(autores);
                //Guardamos los cambios
                _context.SaveChanges();
                //Guardamos en temporales el mensaje de alerta
                TempData["mensaje"] = "El autor se ha actualizado de manera correcta.";
                //Redireccionamos a la ventana principal de las Autores
                return RedirectToAction("Index");
            }

            return View();
        }

        /// <summary>
        /// Método encargado de eliminar el autor
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IActionResult Delete(int? Id)
        {
            //Realizamos la validación de que el Id no venga nulo o vacio
            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            //Obtenemos la data
            var autores = _context.Autores.Where(x => x.Id == Id).FirstOrDefault();
            //eliminamos el libro autores y relación 
            _context.Autores.Remove(autores);
            //Guardamos los cambios
            _context.SaveChanges();
            //Cargamos la alerta
            TempData["mensaje"] = "El autor se ha eliminado de manera correcta.";
            //Redirigir a la pagina principal de autores
            return RedirectToAction("Index");
        }
    }
}
