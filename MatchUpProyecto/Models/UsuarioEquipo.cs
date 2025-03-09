using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatchUpProyecto.Models
{
    [Table("UsuarioEquipo")]
    public class UsuarioEquipo
    {
        [Key]
        [Column("IdUsuario")]
        public int IdUsuario { get; set; }
        [Column("IdEquipo")]
        public int IdEquipo { get; set; }

    }
}
