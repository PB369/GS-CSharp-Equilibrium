using System.ComponentModel.DataAnnotations;

namespace Equilibrium.Api.DTOs
{
    public class CreateScheduleDto
    {
        [Required] public DateTime Date { get; set; }
        [Required] public TimeSpan Start { get; set; }
        [Required] public TimeSpan End { get; set; }
        [Required] public string Mode { get; set; }
        [Required] public int UserId { get; set; }
        public int? WorkspaceId { get; set; }
    }
}
