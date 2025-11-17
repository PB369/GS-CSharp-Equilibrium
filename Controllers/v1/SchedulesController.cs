using Equilibrium.Api.Data;
using Equilibrium.Api.DTOs;
using Equilibrium.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Equilibrium.Api.Controllers.v1
{
    /// <summary>
    /// Controlador responsável por gerenciar agendas (Schedules).
    /// Permite criar, listar, atualizar e remover registros de horários.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class SchedulesController : ControllerBase
    {
        private readonly AppDbContext _db;

        /// <summary>
        /// Construtor com injeção de dependência do contexto do banco de dados.
        /// </summary>
        public SchedulesController(AppDbContext db) => _db = db;

        /// <summary>
        /// Retorna todos os horários cadastrados, incluindo informações do usuário e workspace.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var schedules = await _db.Schedules
                .Include(s => s.User)        // inclui dados do usuário
                .Include(s => s.Workspace)   // inclui dados do local de trabalho
                .Select(s => new ScheduleDto
                {
                    Id = s.Id,
                    Date = s.Date,
                    Start = s.Start,
                    End = s.End,
                    Mode = s.Mode,
                    UserId = s.UserId,
                    WorkspaceId = s.WorkspaceId
                })
                .ToListAsync();

            return Ok(schedules);
        }

        /// <summary>
        /// Retorna um horário específico pelo ID.
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var s = await _db.Schedules.FindAsync(id);
            if (s == null) return NotFound();

            var dto = new ScheduleDto
            {
                Id = s.Id,
                Date = s.Date,
                Start = s.Start,
                End = s.End,
                Mode = s.Mode,
                UserId = s.UserId,
                WorkspaceId = s.WorkspaceId
            };

            return Ok(dto);
        }

        /// <summary>
        /// Cria um novo horário na agenda.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateScheduleDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // Verifica se o usuário associado existe
            var userExists = await _db.Users.AnyAsync(u => u.Id == dto.UserId);
            if (!userExists) return BadRequest(new { message = "UserId inválido." });

            var schedule = new Schedule
            {
                Date = dto.Date,
                Start = dto.Start,
                End = dto.End,
                Mode = dto.Mode,
                UserId = dto.UserId,
                WorkspaceId = dto.WorkspaceId
            };

            _db.Schedules.Add(schedule);
            await _db.SaveChangesAsync();

            var result = new ScheduleDto
            {
                Id = schedule.Id,
                Date = schedule.Date,
                Start = schedule.Start,
                End = schedule.End,
                Mode = schedule.Mode,
                UserId = schedule.UserId,
                WorkspaceId = schedule.WorkspaceId
            };

            // Retorna 201 Created com Location header apontando para o novo recurso
            return CreatedAtAction(nameof(Get), new { version = "1.0", id = schedule.Id }, result);
        }

        /// <summary>
        /// Atualiza um horário já existente.
        /// </summary>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateScheduleDto dto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var s = await _db.Schedules.FindAsync(id);
            if (s == null) return NotFound();

            s.Date = dto.Date;
            s.Start = dto.Start;
            s.End = dto.End;
            s.Mode = dto.Mode;
            s.WorkspaceId = dto.WorkspaceId;

            await _db.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Remove um horário pelo ID.
        /// </summary>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var s = await _db.Schedules.FindAsync(id);
            if (s == null) return NotFound();

            _db.Schedules.Remove(s);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
