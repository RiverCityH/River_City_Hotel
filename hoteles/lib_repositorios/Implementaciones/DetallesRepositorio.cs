using lib_entidades_dominio;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace lib_repositorios.Implementaciones
{
    public class DetallesRepositorio
    {
        private Conexion? conexion;

        public DetallesRepositorio(Conexion conexion)
        {
            this.conexion = conexion;
        }

        public void Configurar(string string_conexion)
        {
            this.conexion!.StringConnection = string_conexion;
        }

        public List<Detalles> Listar()
        {
            return conexion!.ObtenerSet<Detalles>()
                .Include(x => x._Producto)
                .ToList();
        }

        public List<Detalles> Buscar(Expression<Func<Detalles, bool>> condiciones)
        {
            return conexion!.ObtenerSet<Detalles>()
                .Where(condiciones)
                .Include(x => x._Producto)
                .ToList();
        }

        public Detalles Guardar(Detalles entidad)
        {
            conexion!.Guardar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }

        public Detalles Modificar(Detalles entidad)
        {
            conexion!.Modificar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }

        public Detalles Borrar(Detalles entidad)
        {
            conexion!.Borrar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }

        public bool Existe(Expression<Func<Detalles, bool>> condiciones)
        {
            return conexion!.Existe(condiciones);
        }
    }
}
