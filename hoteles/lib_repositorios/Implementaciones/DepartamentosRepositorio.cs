using lib_entidades_dominio;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace lib_repositorios.Implementaciones
{
    public class DepartamentosRepositorio
    {
        private Conexion? conexion;

        public DepartamentosRepositorio(Conexion conexion)
        {
            this.conexion = conexion;
        }

        public void Configurar(string string_conexion)
        {
            this.conexion!.StringConnection = string_conexion;
        }

        public List<Departamentos> Listar()
        {
            return conexion!.ObtenerSet<Departamentos>()
                .Include(x => x._Pais)
                .ToList();
        }

        public List<Departamentos> Buscar(Expression<Func<Departamentos, bool>> condiciones)
        {
            return conexion!.ObtenerSet<Departamentos>()
                .Where(condiciones)
                .Include(x => x._Pais)
                .ToList();
        }

        public Departamentos Guardar(Departamentos entidad)
        {
            conexion!.Guardar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }

        public Departamentos Modificar(Departamentos entidad)
        {
            conexion!.Modificar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }

        public Departamentos Borrar(Departamentos entidad)
        {
            conexion!.Borrar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }

        public bool Existe(Expression<Func<Departamentos, bool>> condiciones)
        {
            return conexion!.Existe(condiciones);
        }
    }
}
