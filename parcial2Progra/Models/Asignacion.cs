using SQLite;

namespace parcial2Progra.Models
{
    public class Asignacion
    {
        [PrimaryKey, AutoIncrement]
        public int AsignacionId { get; set; }
        public int AlumnoId { get; set; }
        public int CursoId { get; set; }
        public string Comentarios { get; set; } = string.Empty;
        public DateTime FechaAsignacion { get; set; } = DateTime.Now;
        public DateTime FechaMod { get; set; } = DateTime.Now;
        public bool Activo { get; set; } = true;

        [Ignore]
        public Alumno Alumno { get; set; }
        [Ignore]
        public Curso Curso { get; set; }
    }
}
