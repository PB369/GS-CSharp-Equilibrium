using Equilibrium.Api.Data;
using Equilibrium.Api.DTOs;
using Equilibrium.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Equilibrium.Api.Services
{
    /// <summary>
    /// Serviço responsável por manipular dados de usuários.
    /// Implementa operações de CRUD utilizando o Entity Framework Core.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly AppDbContext _db;

        /// <summary>
        /// Injeta a instância do banco de dados (DbContext).
        /// </summary>
        public UserService(AppDbContext db) => _db = db;


        /// <summary>
        /// Cria um novo usuário no banco de dados.
        /// </summary>
        public async Task<UserDto> CreateAsync(CreateUserDto dto)
        {
            // Converte o DTO para o modelo de domínio (User)
            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PreferredMode = dto.PreferredMode
            };

            // Adiciona ao contexto (não salva ainda)
            _db.Users.Add(user);

            // Salva no banco
            await _db.SaveChangesAsync();

            // Retorna DTO para exposição externa (API)
            return new UserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                PreferredMode = user.PreferredMode
            };
        }

        /// <summary>
        /// Remove um usuário pelo ID.
        /// </summary>
        public async Task<bool> DeleteAsync(int id)
        {
            // Busca o usuário por ID
            var u = await _db.Users.FindAsync(id);

            // Se não existir, retorna false
            if (u == null) return false;

            // Remove do contexto e salva
            _db.Users.Remove(u);
            await _db.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Retorna todos os usuários cadastrados.
        /// </summary>
        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            // Projeção direta para DTO (melhor performance)
            return await _db.Users
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    FullName = u.FullName,
                    Email = u.Email,
                    PreferredMode = u.PreferredMode
                })
                .ToListAsync();
        }

        /// <summary>
        /// Retorna um usuário específico pelo ID.
        /// </summary>
        public async Task<UserDto?> GetByIdAsync(int id)
        {
            // Busca pelo ID
            var u = await _db.Users.FindAsync(id);

            // Se não existir, retorna null
            if (u == null) return null;

            // Converte para DTO
            return new UserDto
            {
                Id = u.Id,
                FullName = u.FullName,
                Email = u.Email,
                PreferredMode = u.PreferredMode
            };
        }


        /// <summary>
        /// Atualiza os dados de um usuário existente.
        /// </summary>
        public async Task<bool> UpdateAsync(int id, CreateUserDto dto)
        {
            // Busca o usuário existente
            var u = await _db.Users.FindAsync(id);

            // Retorna false caso não exista
            if (u == null) return false;

            // Atualiza propriedades
            u.FullName = dto.FullName;
            u.Email = dto.Email;
            u.PreferredMode = dto.PreferredMode;

            // Salva mudanças
            await _db.SaveChangesAsync();

            return true;
        }
    }
}
