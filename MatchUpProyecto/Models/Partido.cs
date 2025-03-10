using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatchUpProyecto.Models
{
    [Table("Partido")]
    public class Partido
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Fecha")]
        public DateTime Fecha { get; set; }
        [ForeignKey("Equipo")]
        [Column("EquipoLocal")]
        public int EquipoLocal { get; set; }
        [ForeignKey("Equipo")]
        [Column("EquipoVisitante")]
        public int? EquipoVisitante { get; set; }
        [Column("Resultado")]
        public string Resultado { get; set; }
        [Column("UbiLatitud")]
        public string UbiLatitud { get; set; }
        [Column("UbiLongitud")]
        public string UbiLongitud { get; set; }
        [Column("UbiProvincia")]
        public string UbiProvincia { get; set; }
        [Column("Tiempo")]
        public int Tiempo { get; set; }

    }
}
