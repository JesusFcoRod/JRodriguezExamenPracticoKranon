namespace ML
{
    public class Autor
    {
        public int IdAutor { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }

        public string NombreCompleto { get; set; }

        public List<object> Autores { get; set; }
    }
}