using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatchUpProyecto.Models
{
    [Table("PachangaPartido")]
    [PrimaryKey(nameof(IdPachanga), nameof(IdPartido))]
    public class PachangaPartido
    {
        [ForeignKey("IdPachanga")]
        public int IdPachanga { get; set; }

        [ForeignKey("IdPartido")]
        public int IdPartido { get; set; }
        [Column("Estado")]
        public string Estado { get; set;}

    }
}
