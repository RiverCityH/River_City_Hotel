using lib_entidades_dominio;
using lib_repositorios.Implementaciones;
using System.Linq.Expressions;

namespace lib_aplicaciones.Implementaciones
{
    public class TiposAplicacion
    {
        private TiposRepositorio iRepositorio;

        public TiposAplicacion(TiposRepositorio iRepositorio)
        {
            this.iRepositorio = iRepositorio;
        }

        public void Configurar(string string_conexion)
        {
            this.iRepositorio.Configurar(string_conexion);
        }

        public List<Tipos> Listar()
        {
            return this.iRepositorio.Listar();
        }

        public List<Tipos> Buscar(Tipos entidad, string tipo)
        {
            Expression<Func<Tipos, bool>>? condiciones = null;
            switch (tipo.ToUpper())
            {
                case "NOMBRE": condiciones = x => x.Nombre == entidad.Nombre; break;
                default: condiciones = x => x.Id != 0; break;
            }
            return this.iRepositorio.Buscar(condiciones);
        }

        public Tipos Guardar(Tipos entidad)
        {
            entidad.Id = 0;
            if (this.iRepositorio.Existe(
                    x => x.Nombre == entidad.Nombre))
            {
                throw new Exception("lbExisteTipo");
            }

            entidad = this.iRepositorio.Guardar(entidad);
            if (entidad.Id == 0)
            {
                throw new Exception("lbErrorBaseDeDatos");
            }
            return entidad;
        }

        public Tipos Modificar(Tipos entidad)
        {
            if (!this.iRepositorio.Existe(
                    x => x.Id == entidad.Id))
            {
                throw new Exception("lbNoExisteTipo");
            }
            if (this.iRepositorio.Existe(
                    x => x.Id != entidad.Id &&
                         x.Nombre == entidad.Nombre))
            {
                throw new Exception("lbExisteTipo");
            }

            entidad = this.iRepositorio.Modificar(entidad);
            return entidad;
        }

        public Tipos Borrar(Tipos entidad)
        {
            if (!this.iRepositorio.Existe(
                    x => x.Id == entidad.Id))
            {
                throw new Exception("lbNoExisteTipo");
            }
            entidad = this.iRepositorio.Borrar(entidad);
            return entidad;
        }
    }
}