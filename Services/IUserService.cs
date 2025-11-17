using Equilibrium.Api.DTOs;

namespace Equilibrium.Api.Services
{
    /// <summary>
    /// Define o contrato para operações relacionadas ao gerenciamento de usuários.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Cria um novo usuário a partir de um DTO contendo os dados necessários.
        /// </summary>
        Task<UserDto> CreateAsync(CreateUserDto dto);

        /// <summary>
        /// Busca um usuário pelo ID.
        /// </summary>
        Task<UserDto?> GetByIdAsync(int id);

        /// <summary>
        /// Retorna todos os usuários cadastrados.
        /// </summary>
        Task<IEnumerable<UserDto>> GetAllAsync();

        /// <summary>
        /// Atualiza um usuário existente.
        /// </summary>
        Task<bool> UpdateAsync(int id, CreateUserDto dto);

        /// <summary>
        /// Remove um usuário pelo ID.
        /// </summary>
        Task<bool> DeleteAsync(int id);
    }
}
