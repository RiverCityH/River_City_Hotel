using lib_entidades_dominio;
using lib_repositorios.Implementaciones;
using System.Linq.Expressions;

namespace lib_aplicaciones.Implementaciones
{
    public class PaisesAplicacion
    {
        private PaisesRepositorio iRepositorio;

        public PaisesAplicacion(PaisesRepositorio iRepositorio)
        {
            this.iRepositorio = iRepositorio;
        }

        public void Configurar(string string_conexion)
        {
            this.iRepositorio.Configurar(string_conexion);
        }

        public List<Paises> Listar()
        {
            return this.iRepositorio.Listar();
        }

        public List<Paises> Buscar(Paises entidad, string tipo)
        {
            Expression<Func<Paises, bool>>? condiciones = null;
            switch (tipo.ToUpper())
            {
                case "NOMBRE": condiciones = x => x.Nombre == entidad.Nombre; break;
                default: condiciones = x => x.Id != 0; break;
            }
            return this.iRepositorio.Buscar(condiciones);
        }

        public Paises Guardar(Paises entidad)
        {
            entidad.Id = 0;
            if (this.iRepositorio.Existe(
                    x => x.Nombre == entidad.Nombre))
            {
                throw new Exception("lbExistePais");
            }

            entidad = this.iRepositorio.Guardar(entidad);
            if (entidad.Id == 0)
            {
                throw new Exception("lbErrorBaseDeDatos");
            }
            return entidad;
        }

        public Paises Modificar(Paises entidad)
        {
            if (!this.iRepositorio.Existe(
                    x => x.Id == entidad.Id))
            {
                throw new Exception("lbNoExistePais");
            }
            if (this.iRepositorio.Existe(
                    x => x.Id != entidad.Id &&
                         x.Nombre == entidad.Nombre))
            {
                throw new Exception("lbExistePais");
            }

            entidad = this.iRepositorio.Modificar(entidad);
            return entidad;
        }

        public Paises Borrar(Paises entidad)
        {
            if (!this.iRepositorio.Existe(
                    x => x.Id == entidad.Id))
            {
                throw new Exception("lbNoExistePais");
            }
            entidad = this.iRepositorio.Borrar(entidad);
            return entidad;
        }
    }
}