using SQLite;

namespace parcial2Progra.Models
{
    public class Curso
    {
        [PrimaryKey, AutoIncrement]
        public int CursoId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaMod { get; set; } = DateTime.Now;
        public bool Activo { get; set; } = true;
        
    }
}
