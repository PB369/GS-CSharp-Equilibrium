using Microsoft.AspNetCore.Mvc;
using Equilibrium.Api.DTOs;
using Equilibrium.Api.Services;

namespace Equilibrium.Api.Controllers.v1
{
    /// <summary>
    /// Controlador responsável por gerenciar operações relacionadas a usuários.
    /// Inclui endpoints para criação, atualização, listagem, busca e remoção.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _svc;
        private readonly ILogger<UsersController> _logger;

        /// <summary>
        /// Construtor que injeta o serviço de usuários e o logger do controlador.
        /// </summary>
        public UsersController(IUserService svc, ILogger<UsersController> logger)
        {
            _svc = svc;
            _logger = logger;
        }

        /// <summary>
        /// Obtém todos os usuários cadastrados.
        /// </summary>
        // GET: api/v1/users
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _svc.GetAllAsync();
            return Ok(users); // 200
        }

        /// <summary>
        /// Obtém um usuário específico pelo ID.
        /// </summary>
        // GET: api/v1/users/{id}
        [HttpGet("{id:int}", Name = "GetUserById")]
        public async Task<IActionResult> Get(int id)
        {
            var u = await _svc.GetByIdAsync(id);
            if (u == null) return NotFound(); // 404
            return Ok(u); // 200
        }

        /// <summary>
        /// Cria um novo usuário no sistema.
        /// </summary>
        // POST: api/v1/users
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState); // 400

            var created = await _svc.CreateAsync(dto);

            // 201 Created com header Location apontando para o recurso recém-criado
            return CreatedAtRoute("GetUserById", new { version = "1.0", id = created.Id }, created);
        }

        /// <summary>
        /// Atualiza os dados de um usuário existente.
        /// </summary>
        // PUT: api/v1/users/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateUserDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState); // 400

            var ok = await _svc.UpdateAsync(id, dto);
            if (!ok) return NotFound(); // 404

            return NoContent(); // 204
        }

        /// <summary>
        /// Remove um usuário do sistema.
        /// </summary>
        // DELETE: api/v1/users/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _svc.DeleteAsync(id);
            if (!ok) return NotFound(); // 404

            return NoContent(); // 204
        }
    }
}
