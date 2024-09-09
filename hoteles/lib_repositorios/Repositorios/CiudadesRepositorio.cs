using lib_entidades_dominio;
using System.Linq.Expressions;

namespace lib_repositorios.Repositorios
{
    public class CiudadesRepositorio
    {
        private Conexion? conexion;

        public CiudadesRepositorio(Conexion conexion)
        {
            this.conexion = conexion;
        }

        public void Configurar(string string_conexion)
        {
            this.conexion!.StringConnection = string_conexion;
        }

        public List<Ciudades> Listar()
        {
            return conexion!.Listar<Ciudades>();
        }

        public List<Ciudades> Buscar(Expression<Func<Ciudades, bool>> condiciones)
        {
            return conexion!.Buscar(condiciones);
        }

        public Ciudades Guardar(Ciudades entidad)
        {
            conexion!.Guardar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }

        public Ciudades Modificar(Ciudades entidad)
        {
            conexion!.Modificar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }

        public Ciudades Borrar(Ciudades entidad)
        {
            conexion!.Borrar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }

        public bool Existe(Expression<Func<Ciudades, bool>> condiciones)
        {
            return conexion!.Existe(condiciones);
        }
    }
}
