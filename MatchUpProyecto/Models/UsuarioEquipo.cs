using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatchUpProyecto.Models
{
    [Table("UsuarioEquipo")]
    [PrimaryKey(nameof(IdUsuario), nameof(IdEquipo))]
    public class UsuarioEquipo
    {
        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }

        [ForeignKey("Equipo")]
        public int IdEquipo { get; set; }

    }
}
