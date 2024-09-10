using lib_entidades_dominio;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace lib_repositorios.Implementaciones
{
    public class ProductosRepositorio
    {
        private Conexion? conexion;

        public ProductosRepositorio(Conexion conexion)
        {
            this.conexion = conexion;
        }

        public void Configurar(string string_conexion)
        {
            this.conexion!.StringConnection = string_conexion;
        }

        public List<Productos> Listar()
        {
            return conexion!.ObtenerSet<Productos>()
                .Include(x => x._Categoria)
                .Include(x => x._Proveedor)
                .ToList();
        }

        public List<Productos> Buscar(Expression<Func<Productos, bool>> condiciones)
        {
            return conexion!.ObtenerSet<Productos>()
                .Where(condiciones)
                .Include(x => x._Categoria)
                .Include(x => x._Proveedor)
                .ToList();
        }

        public Productos Guardar(Productos entidad)
        {
            conexion!.Guardar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }

        public Productos Modificar(Productos entidad)
        {
            conexion!.Modificar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }

        public Productos Borrar(Productos entidad)
        {
            conexion!.Borrar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }

        public bool Existe(Expression<Func<Productos, bool>> condiciones)
        {
            return conexion!.Existe(condiciones);
        }
    }
}
