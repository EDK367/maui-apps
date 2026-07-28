using SQLite;

namespace mauiAppTask.Models.Entities
{
    public class Tarea
    {

        [PrimaryKey, AutoIncrement]
        public int TareaID { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public bool Realizada { get; set; }

        public bool Activa { get; set; }
    }
}
