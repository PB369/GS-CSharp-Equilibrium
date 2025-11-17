using System.ComponentModel.DataAnnotations;

namespace Equilibrium.Api.Models
{
    /// <summary>
    /// Representa um usuário do sistema Equilibrium. Contém informações
    /// pessoais básicas, preferências de trabalho e a relação com seus
    /// agendamentos. Essa entidade é persistida no banco de dados.
    /// </summary>
    public class User
    {
        public int Id { get; set; }

        [Required, MaxLength(120)]
        public string FullName { get; set; }

        [Required, MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(20)]
        public string PreferredMode { get; set; }

        public ICollection<Schedule> Schedules { get; set; }
    }
}
