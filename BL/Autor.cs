using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Autor
    {
        public static List<object> GetAll()
        {
            var Autores = new List<object>();
            try
            {
                using (DL.JrodriguezExamenPracticoKranonContext contex = new DL.JrodriguezExamenPracticoKranonContext())
                {
                    var query = contex.Autors.FromSqlRaw("[AutorGetAlll]").ToList();

                    if (query.Count > 0)
                    {
                        foreach (var item in query)
                        {
                            ML.Autor autor = new ML.Autor();
                            autor.IdAutor = item.IdAutor;
                            autor.NombreCompleto = item.Nombre + " " + item.ApellidoPaterno + " " + item.ApellidoMaterno;
                            Autores.Add(autor);
                        }
                    }
                }
                

            }catch(Exception ex)
            {

            }
            return Autores;
        }
    }
}
