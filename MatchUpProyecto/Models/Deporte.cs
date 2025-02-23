using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatchUpProyecto.Models
{
    [Table("Deporte")]
    public class Deporte
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Nombre")]
        public string Nombre { get; set; }
        [Column("MaxJugadoresEquipo")]
        public int MaxJugadores { get; set; }
        [Column("MinJugadoresEquipo")]
        public int MinJugadores { get; set; }
        [Column("Imagen")]
        public string Imagen { get; set; }
    }
}
