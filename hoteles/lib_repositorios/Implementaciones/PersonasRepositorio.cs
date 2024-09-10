using lib_entidades_dominio;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace lib_repositorios.Implementaciones
{
    public class PersonasRepositorio
    {
        private Conexion? conexion;

        public PersonasRepositorio(Conexion conexion)
        {
            this.conexion = conexion;
        }

        public void Configurar(string string_conexion)
        {
            this.conexion!.StringConnection = string_conexion;
        }

        public List<Personas> Listar()
        {
            return conexion!.ObtenerSet<Personas>()
                .Include(x => x._TipoDocumento)
                .Include(x => x._Genero)
                .Include(x => x._Ciudad)
                .ToList();
        }

        public List<Personas> Buscar(Expression<Func<Personas, bool>> condiciones)
        {
            return conexion!.ObtenerSet<Personas>()
                .Where(condiciones)
                .Include(x => x._TipoDocumento)
                .Include(x => x._Genero)
                .Include(x => x._Ciudad)
                .ToList();
        }

        public Personas Guardar(Personas entidad)
        {
            conexion!.Guardar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }

        public Personas Modificar(Personas entidad)
        {
            conexion!.Modificar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }

        public Personas Borrar(Personas entidad)
        {
            conexion!.Borrar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }

        public bool Existe(Expression<Func<Personas, bool>> condiciones)
        {
            return conexion!.Existe(condiciones);
        }
    }
}
