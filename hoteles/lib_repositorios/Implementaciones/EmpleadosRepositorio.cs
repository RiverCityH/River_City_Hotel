using lib_entidades_dominio;
using System.Linq.Expressions;

namespace lib_repositorios.Implementaciones
{
    public class EmpleadosRepositorio
    {
        private Conexion? conexion;

        public EmpleadosRepositorio(Conexion conexion)
        {
            this.conexion = conexion;
        }

        public void Configurar(string string_conexion)
        {
            this.conexion!.StringConnection = string_conexion;
        }

        public List<Empleados> Listar()
        {
            return conexion!.Listar<Empleados>();
        }

        public List<Empleados> Buscar(Expression<Func<Empleados, bool>> condiciones)
        {
            return conexion!.Buscar(condiciones);
        }

        public Empleados Guardar(Empleados entidad)
        {
            conexion!.Guardar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }

        public Empleados Modificar(Empleados entidad)
        {
            conexion!.Modificar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }

        public Empleados Borrar(Empleados entidad)
        {
            conexion!.Borrar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }

        public bool Existe(Expression<Func<Empleados, bool>> condiciones)
        {
            return conexion!.Existe(condiciones);
        }
    }
}
