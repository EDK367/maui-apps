using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace articuloApp.model;

public class Articulo
{
    [Key]
    public int ArticuloId { get; set; }
    public int Codigo { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public bool Estado { get; set; }
    public DateTime FechaMod { get; set; }

    [ForeignKey("Fabricante")]
    public int FabricanteId { get; set; }
    public Fabricante? Fabricante { get; set; }
}
