using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatchUpProyecto.Models
{
    [Table("Equipo")]
    public class Equipo
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Nombre")]
        public string Nombre { get; set; }
        [Column("Color")]
        public string Color { get; set; }
        [ForeignKey("Deporte")]
        [Column("Deporte")]
        public int Deporte { get; set; }
        [Column("Emblema")]
        public string Emblema { get; set; }
        [ForeignKey("Usuario")]
        [Column("IdAdmin")]
        public int IdAdmin { get; set; }
    }
}
