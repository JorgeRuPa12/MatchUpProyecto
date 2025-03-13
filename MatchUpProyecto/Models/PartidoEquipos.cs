namespace MatchUpProyecto.Models
{
    public class PartidoEquipos
    {
        public Partido Match { get; set; }
        public Pachanga Pacha { get; set; }
        public Equipo Local { get; set; }
        public Equipo Visitante { get; set; }
    }
}
