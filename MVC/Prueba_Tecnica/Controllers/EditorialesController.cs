using Microsoft.AspNetCore.Mvc;
using Prueba_Tecnica.Data;
using Prueba_Tecnica.Models;
using System.Collections.Generic;
using System.Linq;

namespace Prueba_Tecnica.Controllers
{
    public class EditorialesController : Controller
    {
        //Instancia Para la conexión con la báse de datos
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Constructor del método
        /// </summary>
        /// <param name="context"></param>
        public EditorialesController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Método encargado de traer la información de editoriales con su vista
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            //Buscamos al libro al que eliminaremos en autores
            IEnumerable<editorial> ListEditoriales = _context.Editorial;

            return View(ListEditoriales);
        }

        /// <summary>
        /// Método encargado de cargar la vista de creación de editorial
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Método encargado de 
        /// </summary>
        /// <param name="editorial"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(editorial editorial)
        {
            //Validamos si el modelo pasó la validación
            if(ModelState.IsValid)
            {
                //Agregamos el modelo a la entidad
                _context.Editorial.Add(editorial);
                //Guardamos los cambios
                _context.SaveChanges();
                //Guardamos en temporales el mensaje de alerta
                TempData["mensaje"] = "La editorial se ha guardado de manera correcta.";
                //Redireccionamos a la ventana principal de las editoriales
                return RedirectToAction("Index");
            }

            return View();
        }

        /// <summary>
        /// Método encargado de cargar la vista de edición de editorial
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
            var editorial = _context.Editorial.Where(x => x.Id == Id).FirstOrDefault();

            if(editorial == null)
            {
                return NotFound();
            }

            return View(editorial);
        }

        /// <summary>
        /// Método encargado de 
        /// </summary>
        /// <param name="editorial"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Edit(editorial editorial)
        {
            //Validamos si el modelo pasó la validación
            if (ModelState.IsValid)
            {
                //Agregamos el modelo a la entidad
                _context.Editorial.Update(editorial);
                //Guardamos los cambios
                _context.SaveChanges();
                //Guardamos en temporales el mensaje de alerta
                TempData["mensaje"] = "La editorial se ha actualizado de manera correcta.";
                //Redireccionamos a la ventana principal de las editoriales
                return RedirectToAction("Index");
            }

            return View();
        }

        /// <summary>
        /// Método encargado de eliminar la editorial
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
            var editorial = _context.Editorial.Where(x => x.Id == Id).FirstOrDefault();
            //eliminamos el libro autores y relación 
            _context.Editorial.Remove(editorial);
            //Guardamos los cambios
            _context.SaveChanges();
            //Cargamos la alerta
            TempData["mensaje"] = "La editorial se ha eliminado de manera correcta.";
            //Redirigir a la pagina principal de editoriales
            return RedirectToAction("Index");
        }

    }
}
