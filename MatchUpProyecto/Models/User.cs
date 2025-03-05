using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatchUpProyecto.Models
{
    [Table("Usuario")]
    public class User
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Nombre")]
        public string Nombre { get; set; }
        [Column("Email")]
        public string Email { get; set; }
        [Column("Imagen")]
        public string Imagen { get; set; }
        [Column("Pass")]
        public string Pass { get; set; }
        [Column("Rol")]
        public string Rol { get; set; }
    }
}
