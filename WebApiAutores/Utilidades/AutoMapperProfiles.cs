using AutoMapper;
using WebApiAutores.DTOs;
using WebApiAutores.Entidades;

namespace WebApiAutores.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AutorCreacionDTO, Autor>();
            CreateMap<Autor, AutorDTO>();
            CreateMap<LibroCreacionDTO, Libro>()
                .ForMember(libro => libro.LibrosAutores, opc => opc.MapFrom(MapLibroAutor));
            CreateMap<Libro, LibroDTO>();
            CreateMap<ComentarioCreacionDTO, Comentario>();
            CreateMap<Comentario, ComentarioDTO>();
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
