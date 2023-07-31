using AutoMapper;
using WebApiAutores.DTOs;
using WebApiAutores.Entidades;

namespace WebApiAutores.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //metódo POST
            CreateMap<AutorCreacionDTO, Autor>();
            //metódo GET
            CreateMap<Autor, AutorDTO>()
                .ForMember(
                    autorDTO => autorDTO.Libros, 
                    opc => opc.MapFrom(MapAutorAutorDTO)
                );
            //metódo POST
            CreateMap<LibroCreacionDTO, Libro>()
                .ForMember(libro => libro.LibrosAutores, opc => opc.MapFrom(MapLibroAutor));
            //metódo GET
            CreateMap<Libro, LibroDTO>()
                .ForMember(libroDTO => libroDTO.Autores, opciones => opciones.MapFrom(MapLibroLibroDTO));
            //metódo POST
            CreateMap<ComentarioCreacionDTO, Comentario>();
            //metódo GET
            CreateMap<Comentario, ComentarioDTO>();
        }

        private List<LibroDTO> MapAutorAutorDTO(Autor autor, AutorDTO autorDTO)
        {
            var resultado = new List<LibroDTO>();

            if(autor.LibrosAutores == null) return resultado;

            foreach (var libroAutor in autor.LibrosAutores)
            {
                resultado.Add(
                        new LibroDTO()
                        {
                            Id = libroAutor.LibroId,
                            Titulo = libroAutor.Libro.Titulo
                        }
                    );
            }

            return resultado;
        }

        private List<AutorDTO> MapLibroLibroDTO(Libro libro, LibroDTO libroDTO)
        {
            var resultado = new List<AutorDTO>();

            if(libro.LibrosAutores == null) return resultado;

            foreach (var libroAutor in libro.LibrosAutores)
            {
                resultado.Add(
                              new AutorDTO() 
                              { 
                                  Id = libroAutor.AutorId, 
                                  Nombre = libroAutor.Autor.Nombre
                              }
                             );
            }

            return resultado;
        }

        private List<LibroAutor> MapLibroAutor(LibroCreacionDTO dTO, Libro libro)
        {
            var listaLibroAutor = new List<LibroAutor>();

            if(dTO.AutoresIds == null) return listaLibroAutor;

            foreach (var autorId in dTO.AutoresIds)
            {
                listaLibroAutor.Add(new LibroAutor(){AutorId = autorId});
            }
            return listaLibroAutor;
        }
    }
}
