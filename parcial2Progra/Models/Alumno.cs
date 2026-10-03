using SQLite;

namespace parcial2Progra.Models
{
    public class Alumno
    {
        [PrimaryKey, AutoIncrement]
        public int AlumnoId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Edad { get; set; }
        public string Genero { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaMod { get; set; } = DateTime.Now;
        public bool Activo { get; set; } = true;
        
    }
}







