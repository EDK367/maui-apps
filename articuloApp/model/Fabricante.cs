using System.ComponentModel.DataAnnotations;

namespace articuloApp.model;

public class Fabricante
{
    [Key]
    public int FabricanteId { get; set; }
    public int Codigo { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public bool Estado { get; set; }
    public DateTime FechaMod { get; set; }
    public ICollection<Articulo> Articulos { get; set; } = new List<Articulo>();
}
