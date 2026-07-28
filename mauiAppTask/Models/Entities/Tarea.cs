using SQLite;

namespace mauiAppTask.Models.Entities
{
    public class Tarea
    {

        [PrimaryKey, AutoIncrement]
        public int TareaID { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public bool Realizada { get; set; }

        public bool Activa { get; set; }
    }
}
