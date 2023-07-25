using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.DTOs;
using WebApiAutores.Entidades;

namespace WebApiAutores.Controllers
{
    [ApiController]
    [Route("api/libro/{LibroId}/comentario")]
    public class ComentarioController : Controller
    {
        private readonly AplicationContext context;
        private readonly IMapper mapper;

        public ComentarioController(AplicationContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ComentarioDTO>>> Get(int LibroId)
        {
            var existeLibro = await context.Libros.AnyAsync(libroDB => libroDB.Id == LibroId);
            if (!existeLibro) return NotFound();
            var comentarios = await context.Comentarios.Where(com => com.LibroId == LibroId)
                .ToListAsync();
            return mapper.Map<List<ComentarioDTO>>(comentarios);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromRoute] int LibroId, 
           [FromBody] ComentarioCreacionDTO comentarioCreacionDTO)
        {
            var existeLibro = await context.Libros.AnyAsync(libroDB => libroDB.Id == LibroId);
            if (!existeLibro) return NotFound();
            var comentario = mapper.Map<Comentario>(comentarioCreacionDTO);
            comentario.LibroId = LibroId;
            context.Comentarios.Add(comentario);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
