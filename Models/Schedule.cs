using System.ComponentModel.DataAnnotations;

namespace Equilibrium.Api.Models
{
    /// <summary>
    /// Representa um agendamento de turno, incluindo data, horário,
    /// modo de trabalho e vínculos com usuário e workspace.
    /// </summary>
    public class Schedule
    {
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }   // dia do turno

        [Required]
        public TimeSpan Start { get; set; }

        [Required]
        public TimeSpan End { get; set; }

        /// <summary>
        /// Define o modo de trabalho (remote, presential, hybrid).
        /// </summary>
        [Required, MaxLength(20)]
        public string Mode { get; set; }

        /// <summary>
        /// Foreign key do usuário associado ao agendamento.
        /// </summary>
        public int UserId { get; set; }

        public User User { get; set; }

        /// <summary>
        /// Foreign key opcional do workspace associado.
        /// </summary>
        public int? WorkspaceId { get; set; }

        public Workspace Workspace { get; set; }
    }
}
