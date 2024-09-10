using lib_entidades_dominio;
using lib_repositorios.Implementaciones;
using System.Linq.Expressions;

namespace lib_aplicaciones.Implementaciones
{
    public class ProductosAplicacion
    {
        private ProductosRepositorio iRepositorio;

        public ProductosAplicacion(ProductosRepositorio iRepositorio)
        {
            this.iRepositorio = iRepositorio;
        }

        public void Configurar(string string_conexion)
        {
            this.iRepositorio.Configurar(string_conexion);
        }

        public List<Productos> Listar()
        {
            return this.iRepositorio.Listar();
        }

        public List<Productos> Buscar(Productos entidad, string tipo)
        {
            Expression<Func<Productos, bool>>? condiciones = null;
            switch (tipo.ToUpper())
            {
                case "CODIGO": condiciones = x => x.Codigo == entidad.Codigo; break;
                default: condiciones = x => x.Id != 0; break;
            }
            return this.iRepositorio.Buscar(condiciones);
        }

        public Productos Guardar(Productos entidad)
        {
            entidad.Id = 0;
            if (this.iRepositorio.Existe(
                    x => x.Nombre == entidad.Nombre))
            {
                throw new Exception("lbExisteProducto");
            }

            entidad = this.iRepositorio.Guardar(entidad);
            if (entidad.Id == 0)
            {
                throw new Exception("lbErrorBaseDeDatos");
            }
            return entidad;
        }

        public Productos Modificar(Productos entidad)
        {
            if (!this.iRepositorio.Existe(
                    x => x.Id == entidad.Id))
            {
                throw new Exception("lbNoExisteProducto");
            }
            if (this.iRepositorio.Existe(
                    x => x.Id != entidad.Id &&
                         x.Nombre == entidad.Nombre))
            {
                throw new Exception("lbExisteProducto");
            }

            entidad = this.iRepositorio.Modificar(entidad);
            return entidad;
        }

        public Productos Borrar(Productos entidad)
        {
            if (!this.iRepositorio.Existe(
                    x => x.Id == entidad.Id))
            {
                throw new Exception("lbNoExisteProducto");
            }
            entidad = this.iRepositorio.Borrar(entidad);
            return entidad;
        }
    }
}