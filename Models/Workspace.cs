using System.ComponentModel.DataAnnotations;

namespace Equilibrium.Api.Models
{
    /// <summary>
    /// Representa um espaço de trabalho usado pelo usuário para organizar
    /// atividades, ambientes ou contextos dentro da plataforma Equilibrium.
    /// Pode ser usado, por exemplo, para separar ambientes de trabalho remoto,
    /// presencial ou áreas temáticas da vida pessoal e profissional.
    /// </summary>
    public class Workspace
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
