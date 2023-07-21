using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.DTOs;
using WebApiAutores.Entidades;
using WebApiAutores.Utilidades;

namespace WebApiAutores.Controllers
{
    [ApiController]
    [Route("api/autor")]
    public class AutorController : Controller
    {
        private readonly AplicationContext _context;
        private readonly IMapper _mapper;

        public AutorController(AplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Autor>>> Get()
        {
            return await _context.Autores.Include(x => x.Libros).ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Post(AutorCreacionDTO autorDTO)
        {
            var autor = _mapper.Map<Autor>(autorDTO);
            _context.Autores.Add(autor);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")] // api/autor/id
        public async Task<IActionResult> Put(Autor autor, int id)
        {
            if (autor.Id != id) 
                return BadRequest("El id enviado no coincide con el autor");

            var exist = await _context.Autores.AnyAsync(x => x.Id == id);

            if(!exist) return NotFound();

            _context.Autores.Update(autor);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")] // api/autor/id
        public async Task<IActionResult> Delete(int id)
        {
            var exist = await _context.Autores.AnyAsync(x => x.Id == id);

            if (!exist) return NotFound();

            var autorEliminado = new Autor(){ Id = id };

            _context.Autores.Remove(autorEliminado);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
