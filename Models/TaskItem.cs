using System.ComponentModel.DataAnnotations;

namespace Equilibrium.Api.Models
{
    /// <summary>
    /// Representa uma tarefa individual associada a um agendamento dentro
    /// do sistema Equilibrium. Cada tarefa pode conter título, descrição,
    /// status de conclusão e um vínculo opcional com um Schedule.
    /// </summary>
    public class TaskItem
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsCompleted { get; set; }

        /// <summary>
        /// Chave estrangeira opcional que referencia o agendamento ao qual
        /// esta tarefa pertence. O vínculo não é obrigatório.
        /// </summary>
        public int? ScheduleId { get; set; }

        public Schedule Schedule { get; set; }
    }
}
