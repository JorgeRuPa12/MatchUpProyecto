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
        [Column("Ganador")]
        public int Ganador { get; set; }
        [Column("Deporte")]
        public string Deporte { get; set; }
        [Column("UbiLatitud")]
        public float UbiLatitud { get; set; }
        [Column("UbiLongitud")]
        public float UbiLongitud { get; set; }
        [Column("UbiProvincia")]
        public string UbiProvincia { get; set; }
        [Column("Inscripcion")]
        public decimal Inscripcion { get; set; }
        [Column("Acceso")]
        public string Acceso { get; set; }
    }
}
