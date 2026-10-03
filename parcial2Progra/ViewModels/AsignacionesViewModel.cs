using System.Collections.ObjectModel;
using System.Windows.Input;
using parcial2Progra.Interfaces;
using parcial2Progra.Models;
using parcial2Progra.Service;

namespace parcial2Progra.ViewModels
{
    public class AsignacionesViewModel : BaseViewModel
    {
        private readonly AsignacionService _asignacionService;
        private readonly AlumnoService _alumnoService;
        private readonly CursoService _cursoService;
        private readonly INotificationManagerService _notificationService;

        string _comentarios = string.Empty;
        string _mensaje = string.Empty;
        Alumno _alumnoSeleccionado;
        Curso _cursoSeleccionado;

        public string Comentarios { get => _comentarios; set => SetProperty(ref _comentarios, value); }
        public string Mensaje { get => _mensaje; set => SetProperty(ref _mensaje, value); }

        public Alumno AlumnoSeleccionado
        {
            get => _alumnoSeleccionado;
            set => SetProperty(ref _alumnoSeleccionado, value);
        }

        public Curso CursoSeleccionado
        {
            get => _cursoSeleccionado;
            set => SetProperty(ref _cursoSeleccionado, value);
        }

        public ObservableCollection<Asignacion> Asignaciones { get; } = new();
        public ObservableCollection<Alumno> Alumnos { get; } = new();
        public ObservableCollection<Curso> Cursos { get; } = new();

        public ICommand GuardarCommand { get; }
        public ICommand CargarCommand { get; }

        public AsignacionesViewModel(
            AsignacionService asignacionService,
            AlumnoService alumnoService,
            CursoService cursoService,
            INotificationManagerService notificationService)
        {
            _asignacionService = asignacionService;
            _alumnoService = alumnoService;
            _cursoService = cursoService;
            _notificationService = notificationService;
            Title = "Asignaciones";

            GuardarCommand = new Command(async () => await GuardarAsignacion());
            CargarCommand = new Command(async () => await CargarDatos());
        }

        public async Task CargarDatos()
        {
            if (IsBusy) return;
            IsBusy = true;
            try
            {
                Alumnos.Clear();
                var listaAlumnos = await _alumnoService.GetAllAsync();
                foreach (var a in listaAlumnos) Alumnos.Add(a);

                Cursos.Clear();
                var listaCursos = await _cursoService.GetAllAsync();
                foreach (var c in listaCursos) Cursos.Add(c);

                Asignaciones.Clear();
                var listaAsig = await _asignacionService.GetAllAsync();
                foreach (var asig in listaAsig)
                {
                    asig.Alumno = listaAlumnos.FirstOrDefault(a => a.AlumnoId == asig.AlumnoId);
                    asig.Curso = listaCursos.FirstOrDefault(c => c.CursoId == asig.CursoId);
                    Asignaciones.Add(asig);
                }
            }
            finally { IsBusy = false; }
        }

        async Task GuardarAsignacion()
        {
            if (AlumnoSeleccionado == null || CursoSeleccionado == null)
            {
                Mensaje = "Debes seleccionar un alumno y un curso.";
                return;
            }

            IsBusy = true;
            try
            {
                var asignacion = new Asignacion
                {
                    AlumnoId = AlumnoSeleccionado.AlumnoId,
                    CursoId = CursoSeleccionado.CursoId,
                    Comentarios = Comentarios,
                    FechaAsignacion = DateTime.Now,
                    FechaMod = DateTime.Now,
                    Activo = true
                };

                await _asignacionService.CreateAsync(asignacion);
                Mensaje = $"Asignación guardada correctamente.";

                var fechaStr = asignacion.FechaAsignacion.ToString("dd/MM/yyyy HH:mm");
                var titulo = "Nueva Asignación Registrada";
                var cuerpo = $"Alumno: {AlumnoSeleccionado.Nombre} {AlumnoSeleccionado.Apellido}\n" +
                             $"Curso: {CursoSeleccionado.Nombre}\n" +
                             $"Fecha: {fechaStr}";

                _notificationService?.SendNotification(titulo, cuerpo);

                AlumnoSeleccionado = null;
                CursoSeleccionado = null;
                Comentarios = string.Empty;

                await CargarDatos();
            }
            finally { IsBusy = false; }
        }
    }
}
