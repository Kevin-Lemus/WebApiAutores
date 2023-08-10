using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.DTOs;
using WebApiAutores.Entidades;

namespace WebApiAutores.Controllers
{
    [ApiController]
    [Route("api/libro")]
    public class LibroController : Controller
    {
        private readonly AplicationContext _context;
        private readonly IMapper mapper;

        public LibroController(AplicationContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<List<LibroDTO>> Get()
        {
            var libros = await _context.Libros
                                       .Include(libroDb => libroDb.Comentarios)
                                       .Include(libroDb => libroDb.LibrosAutores)
                                       .ThenInclude(libroAutorDb => libroAutorDb.Autor)
                                       .ToListAsync();

            foreach (var libro in libros)
            {
                libro.LibrosAutores = libro.LibrosAutores.OrderBy(x => x.Orden).ToList();
            }

            return mapper.Map<List<LibroDTO>>(libros);
        }

        [HttpGet("{id:int}", Name = "ObtenerLibro")] // Ruta: api/libro/id
        public async Task<ActionResult<FullLibroDTO>> Get(int id)
        {
            var exist = await _context.Libros.AnyAsync(x => x.Id == id);
            if (!exist) return NotFound();
            var libro = await _context.Libros
                                    .Include(libroDb => libroDb.Comentarios)
                                    .Include(libroDb => libroDb.LibrosAutores)
                                    .ThenInclude(libroAutorDb => libroAutorDb.Autor)
                                    .FirstOrDefaultAsync(x => x.Id == id);

            libro.LibrosAutores = libro.LibrosAutores.OrderBy(x => x.Orden).ToList();

            return mapper.Map<FullLibroDTO>(libro);
        }

        [HttpPost]
        public async Task<ActionResult> Post(LibroCreacionDTO libroDTO)
        {
            var autoresIds = await _context.Autores.
                Where(autor => libroDTO.AutoresIds.Contains(autor.Id)).Select(x => x.Id).ToListAsync();
            if (libroDTO.AutoresIds.Count() != autoresIds.Count())
                return BadRequest("Uno o varios de los IDs de autores no fue encontrado");

            var libro = mapper.Map<Libro>(libroDTO);

            if(libro.LibrosAutores != null)
            {
                for (int i = 0; i < libro.LibrosAutores.Count(); i++)
                {
                    libro.LibrosAutores[i].Orden = i;
                }
            }

            _context.Libros.Add(libro);
            await _context.SaveChangesAsync();

            var libroRespuesta = mapper.Map<LibroDTO>(libro);

            return CreatedAtRoute("ObtenerLibro", new {id = libro.Id}, libroRespuesta);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(LibroCreacionDTO libroDTO, int id)
        {
            var libroDB = await _context.Libros
                                        .Include(x => x.LibrosAutores)
                                        .FirstOrDefaultAsync(x => x.Id == id);

            if (libroDB == null) return NotFound();
            

            libroDB = mapper.Map(libroDTO, libroDB);

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult> Patch(int id, JsonPatchDocument<LibroPatchDTO> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest("Complete los campos de la petición");
            }

            var libroDB = await _context.Libros
                                        .Include(x => x.LibrosAutores)
                                        .FirstOrDefaultAsync(libro => libro.Id == id);

            if (libroDB == null) return NotFound("Libro no encontrado");

            var libroDTO = mapper.Map<LibroPatchDTO>(libroDB);

            patchDocument.ApplyTo(libroDTO, ModelState);

            var esValido = TryValidateModel(libroDTO);

            if (!esValido) return BadRequest(ModelState);

            mapper.Map(libroDTO, libroDB);

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
