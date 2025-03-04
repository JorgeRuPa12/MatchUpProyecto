using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatchUpProyecto.Models
{
    [Table("Pachanga")]
    public class Pachanga
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Nombre")]
        public string Nombre { get; set; }
        [Column("Ganador")]
        public int Ganador { get; set; }
        [Column("Deporte")]
        public string Deporte { get; set; }
        [Column("UbiLatitud")]
        public string UbiLatitud { get; set; }
        [Column("UbiLongitud")]
        public string UbiLongitud { get; set; }
        [Column("UbiProvincia")]
        public string UbiProvincia { get; set; }
        [Column("Inscripcion")]
        public decimal Inscripcion { get; set; }
        [Column("Estado")]
        public string Estado { get; set; }
        [Column("Acceso")]
        public string Acceso { get; set; }
        [Column("Fecha")]
        public DateTime Fecha { get; set; }
    }
}
