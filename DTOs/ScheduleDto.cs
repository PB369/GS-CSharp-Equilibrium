namespace Equilibrium.Api.DTOs
{
    public class ScheduleDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public string Mode { get; set; }
        public int UserId { get; set; }
        public int? WorkspaceId { get; set; }
    }
}
