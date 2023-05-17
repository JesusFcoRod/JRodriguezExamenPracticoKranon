using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    public class LibroController : Controller
    {
        [HttpGet]
        [Route("api/Libro/GetAll")]
        public ActionResult GetAll()
        {
            ML.Libro libro = new ML.Libro();
            libro.Autor = new ML.Autor();

            List<object> Libros = BL.Libro.GetAll(libro);

            if (Libros.Count > 0)
            {
                return Ok(Libros);
            }
            else
            {
                return NotFound(Libros);
            }
        }

        [HttpPost]
        [Route("api/Libro/Add")]

        public ActionResult Add([FromBody]ML.Libro libro)
        {
            int RowAffected = BL.Libro.Add(libro);

            if (RowAffected > 0)
            {
                return Ok(RowAffected);
            }
            else
            {
                return NotFound(RowAffected);
            }
        }

        [HttpPost]
        [Route("api/Libro/Update/")]
        public ActionResult Update([FromBody]ML.Libro libro)
        {
            int RowAffected = BL.Libro.Update(libro);

            if (RowAffected > 0)
            {
                return Ok(RowAffected);
            }
            else
            {
                return NotFound(RowAffected);   
            }
        }

        [HttpGet]
        [Route("api/Libro/GetById/{IdLibro}")]
        public ActionResult GetById(int IdLibro)
        {
            object Libro = BL.Libro.GetById(IdLibro);

            if (Libro != null)
            {
                return Ok(Libro);
            }
            else
            {
                return NotFound(Libro);
            }
        }
        [HttpPost]
        [Route("api/Libro/Delete/{Editorial}")]

        public ActionResult Delete(string Editorial )
        {
            int RowAffected = BL.Libro.DeleteByEditorial(Editorial);

            if (RowAffected > 0)
            {
                return Ok(RowAffected);
            }
            else
            {
                return NotFound(RowAffected);
            }
        }
    }
}
