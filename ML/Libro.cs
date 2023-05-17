﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Libro
    {
        public int IdLibro { get; set; }
        public string Titulo { get; set; }
        public int AnioPublicacion { get; set; }
        public string Editorial { get; set; }
        public string FechaPublicacion { get; set; }
        public ML.Autor Autor { get; set; }

        public List<object> Libros { get; set; }
    }
}
