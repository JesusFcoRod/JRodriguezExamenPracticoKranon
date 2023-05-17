using Azure.Core;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Libro
    {
        public static int Add(ML.Libro libro)
        {
            int RawAffected = 0;

            try
            {
                using (DL.JrodriguezExamenPracticoKranonContext contex = new DL.JrodriguezExamenPracticoKranonContext())
                {
                    var query = contex.Database.ExecuteSqlRaw($"[AddLibro] '{libro.Titulo}',{libro.AnioPublicacion},'{libro.Editorial}','{libro.FechaPublicacion}',{libro.Autor.IdAutor}");

                    if (query != null)
                    {
                        RawAffected = query;
                    }
                    else
                    {
                        RawAffected = 0;
                    }
                }

            }
            catch (Exception ex)
            {

            }
            return RawAffected;

        }

        public static int Update(ML.Libro libro)
        {
            int RowAffected = 0;
            try
            {
                using (DL.JrodriguezExamenPracticoKranonContext contex = new DL.JrodriguezExamenPracticoKranonContext())
                {
                    var query = contex.Database.ExecuteSqlRaw($" [UpdateLibro] {libro.IdLibro},'{libro.Titulo}',{libro.AnioPublicacion},'{libro.Editorial}','{libro.FechaPublicacion}',{libro.Autor.IdAutor}");
                    if (query > 0)
                    {
                        RowAffected = query;
                    }
                    else
                    {
                        RowAffected = 0;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return RowAffected;
        }
        public static int DeleteByEditorial(string Editorial)
        {
            int RowAffected = 0;

            try
            {
                using (DL.JrodriguezExamenPracticoKranonContext contex = new DL.JrodriguezExamenPracticoKranonContext())
                {
                    var query = contex.Database.ExecuteSqlRaw($"[LibroDelete] '{Editorial}'");

                    if (query > 0)
                    {
                        RowAffected = query;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return RowAffected;
        }
        public static List<object> GetAll(ML.Libro libro)
        {
            var Libros = new List<object>();
            try
            {

                using (DL.JrodriguezExamenPracticoKranonContext contex = new DL.JrodriguezExamenPracticoKranonContext())
                {
                    var query = contex.Libros.FromSqlRaw($"[LibroGetAll] '{libro.Titulo}','{libro.Autor.Nombre}','{libro.Editorial}'").ToList();

                    if (query.Count > 0)
                    {
                        foreach (var item in query)
                        {
                            libro = new ML.Libro();
                            libro.IdLibro = item.IdLibro;
                            libro.Titulo = item.Titulo;
                            libro.AnioPublicacion = item.AnioPublicacion.Value;
                            libro.Editorial = item.Editorial;
                            libro.FechaPublicacion = item.FechaPublicacion.Value.ToString("yyyy/MM/dd");

                            libro.Autor = new ML.Autor();
                            libro.Autor.IdAutor = item.IdAutor.Value;
                            libro.Autor.NombreCompleto = item.Nombre + " " + item.ApellidoPaterno + " " + item.ApellidoMaterno;

                            Libros.Add(libro);

                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }
            return Libros;
        }

        public static object GetById(int idLibro)
        {
            object Libro = null;

            try
            {
                using (DL.JrodriguezExamenPracticoKranonContext contex = new DL.JrodriguezExamenPracticoKranonContext())
                {
                    var query = contex.Libros.FromSqlRaw($"[LibroGetById] {idLibro}").AsEnumerable().FirstOrDefault();

                    if (query != null)
                    {
                        ML.Libro libro = new ML.Libro();
                        libro.IdLibro = query.IdLibro;
                        libro.Titulo = query.Titulo;
                        libro.AnioPublicacion = query.AnioPublicacion.Value;
                        libro.Editorial = query.Editorial;
                        libro.FechaPublicacion = query.FechaPublicacion.Value.ToString("yyyy/dd/MM");

                        libro.Autor = new ML.Autor();
                        libro.Autor.NombreCompleto = query.Nombre + " " + query.ApellidoPaterno + " " + query.ApellidoMaterno;

                        Libro = libro;

                    }
                }

            }
            catch (Exception ex)
            {

            }
            return Libro;
        }

        public static List<object> GetByFecha(ML.Libro libro)
        {
            var Libros = new List<object>();
            try
            {

                using (DL.JrodriguezExamenPracticoKranonContext contex = new DL.JrodriguezExamenPracticoKranonContext())
                {
                    var query = contex.Libros.FromSqlRaw($"[LibroGetByFecha] '{libro.FechaPublicacion}'").ToList();

                    if (query.Count > 0)
                    {
                        foreach (var item in query)
                        {
                            libro = new ML.Libro();
                            libro.IdLibro = item.IdLibro;
                            libro.Titulo = item.Titulo;
                            libro.AnioPublicacion = item.AnioPublicacion.Value;
                            libro.Editorial = item.Editorial;
                            libro.FechaPublicacion = item.FechaPublicacion.Value.ToString("yyyy/MM/dd");

                            libro.Autor = new ML.Autor();
                            libro.Autor.IdAutor = item.IdAutor.Value;
                            libro.Autor.NombreCompleto = item.Nombre + " " + item.ApellidoPaterno + " " + item.ApellidoMaterno;

                            Libros.Add(libro);

                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }
            return Libros;
        }
    }
}