using mauiAppTask.Models.Entities;
using mauiAppTask.Service;

namespace mauiAppTask.Service
{
    public static class TaskService1
    {
        private static List<Tarea> _tareas = new();
        private static int nextInt = 1;


        public static List<Tarea> GetAll() => _tareas;

        public static void CreateTask(string nombre, string descripcion)
        {
            _tareas.Add(
                new Tarea
                {
                    TareaID = nextInt++,
                    Nombre = nombre,
                    Descripcion = descripcion,
                    Realizada = false
                }
            );
        }

        public static void UpdateTask(Tarea tarea)
        {
        }

        public static void DeleteTask(int id)
        {
            var tarea = _tareas.FirstOrDefault(t => t.TareaID == id);
            if (tarea != null) _tareas.Remove(tarea);
        }

        public static void ChangeStatus(int id)
        {
            var tarea = _tareas.FirstOrDefault(t => t.TareaID == id);
            if (tarea != null) tarea.Realizada = !tarea.Realizada;
        }

    }
}
