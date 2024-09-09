using lib_entidades_dominio;
using System.Linq.Expressions;

namespace lib_repositorios.Implementaciones
{
    public class PaisesRepositorio
    {
        private Conexion? conexion;

        public PaisesRepositorio(Conexion conexion)
        {
            this.conexion = conexion;
        }

        public void Configurar(string string_conexion)
        {
            this.conexion!.StringConnection = string_conexion;
        }

        public List<Paises> Listar()
        {
            return conexion!.Listar<Paises>();
        }

        public List<Paises> Buscar(Expression<Func<Paises, bool>> condiciones)
        {
            return conexion!.Buscar(condiciones);
        }

        public Paises Guardar(Paises entidad)
        {
            conexion!.Guardar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }

        public Paises Modificar(Paises entidad)
        {
            conexion!.Modificar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }

        public Paises Borrar(Paises entidad)
        {
            conexion!.Borrar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }

        public bool Existe(Expression<Func<Paises, bool>> condiciones)
        {
            return conexion!.Existe(condiciones);
        }
    }
}
