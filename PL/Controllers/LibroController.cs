using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class LibroController : Controller
    {
        //Inyeccion de dependencias-- patron de diseño
        private readonly IConfiguration _configuration;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public LibroController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        public ActionResult GetAll()
        {
            ML.Libro libro = new ML.Libro();
            var Libros = new List<object>();

            try
            {
                using (var client = new HttpClient())
                {
                    string urlApi = _configuration["urlApi"];
                    client.BaseAddress = new Uri(urlApi);

                    var responseTask = client.GetAsync("Libro/GetAll");
                    responseTask.Wait();

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<List<Object>>();
                        readTask.Wait();

                        foreach (var resultItem in readTask.Result)
                        {
                            ML.Libro resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Libro>(resultItem.ToString());
                            Libros.Add(resultItemList);
                        }
                    }
                    libro.Libros = Libros;
                }
            }
            catch (Exception ex)
            {

            }
            return View(libro);
        }

        [HttpPost]
        public ActionResult GetAll(ML.Libro libro)
        {
            if (libro.FechaPublicacion == null)
            {
                var Libros = BL.Libro.GetAll(libro);
                libro.Libros = Libros;
                return View(libro);
            }
            else
            {
                var Libros = BL.Libro.GetByFecha(libro);
                libro.Libros = Libros;
                return View(libro);
            }
        }

        [HttpGet]
        public ActionResult Form(int? idLibro)
        {
            var Autores = BL.Autor.GetAll();
            ML.Libro libro = new ML.Libro();
            libro.Autor = new ML.Autor();


            if (Autores.Count > 0)
            {
                libro.Autor.Autores = Autores;
            }

            if (idLibro != null)
            {
                libro.IdLibro = idLibro.Value;

                try
                {
                    using (var client = new HttpClient())
                    {
                        string urlApi = _configuration["urlApi"];
                        client.BaseAddress = new Uri(urlApi);

                        var responseTask = client.GetAsync("Libro/GetById/" + idLibro);
                        responseTask.Wait();

                        var resultServicio = responseTask.Result;

                        if (resultServicio.IsSuccessStatusCode)
                        {
                            var readTask = resultServicio.Content.ReadAsAsync<object>();
                            readTask.Wait();

                            string LibroCast = readTask.Result.ToString();

                            ML.Libro resultItem = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Libro>(LibroCast);
                            libro = resultItem;

                        }
                    }

                }
                catch (Exception ex)
                {

                }

                if (libro != null)
                {
                    libro = (ML.Libro)libro;
                    libro.Autor.Autores = Autores;

                    return View(libro);
                }
                else
                {
                    ViewBag.Message = "Ocurrio algo al consultar la informacion del usuario";
                    return View("Modal");
                }


            }
            else
            {
                return View(libro);
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Libro libro)
        {
            int RawAffected = 0;

            try
            {
                using (var client = new HttpClient())
                {
                    if (libro.IdLibro == 0)
                    {
                        client.BaseAddress = new Uri(_configuration["urlApi"]);

                        var postTask = client.PostAsJsonAsync<ML.Libro>("Libro/Add", libro);
                        postTask.Wait();

                        var result = postTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            ViewBag.Message = "Libro agregado correctamente";
                            return PartialView("Modal");
                        }
                        else
                        {
                            ViewBag.Message = "Ocurrio un problema al agregar el libro a la Base de Datos";
                            return PartialView("Modal");
                        }
                    }
                    else
                    {
                        client.BaseAddress = new Uri(_configuration["urlApi"]);

                        var postTask = client.PostAsJsonAsync<ML.Libro>("Libro/Update", libro);
                        postTask.Wait();

                        var result = postTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            ViewBag.Mensaje = "Se ha actualizado el registro";
                            return PartialView("Modal");
                        }
                        else
                        {
                            ViewBag.Mensaje = "No se ha registrado el usuario";
                            return PartialView("Modal");
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return View();
        }

        [HttpGet]
        public ActionResult Delete(string Editorial)
        {
            ML.Libro libro = new ML.Libro();
            libro.Editorial = Editorial;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["UrlApi"]);

                var postTask = client.GetAsync("Libro/Delete/" + Editorial);
                postTask.Wait();

                var result = postTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Mensaje = "Se ha eliminado el libro";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Mensaje = "No se ha eliminado el libro";
                    return PartialView("Modal");
                }
            }
        }
    }
}
