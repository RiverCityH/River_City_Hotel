using lib_entidades_dominio;
using System.Linq.Expressions;

namespace lib_repositorios.Repositorios
{
    public class TiposRepositorio
    {
        private Conexion? conexion;

        public TiposRepositorio(Conexion conexion)
        {
            this.conexion = conexion;
        }

        public void Configurar(string string_conexion)
        {
            this.conexion!.StringConnection = string_conexion;
        }

        public List<Tipos> Listar()
        {
            return conexion!.Listar<Tipos>();
        }

        public List<Tipos> Buscar(Expression<Func<Tipos, bool>> condiciones)
        {
            return conexion!.Buscar(condiciones);
        }

        public Tipos Guardar(Tipos entidad)
        {
            conexion!.Guardar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }

        public Tipos Modificar(Tipos entidad)
        {
            conexion!.Modificar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }

        public Tipos Borrar(Tipos entidad)
        {
            conexion!.Borrar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }

        public bool Existe(Expression<Func<Tipos, bool>> condiciones)
        {
            return conexion!.Existe(condiciones);
        }
    }
}
