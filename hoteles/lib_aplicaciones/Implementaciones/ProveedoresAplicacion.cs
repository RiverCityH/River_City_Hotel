using lib_entidades_dominio;
using lib_repositorios.Implementaciones;
using System.Linq.Expressions;

namespace lib_aplicaciones.Implementaciones
{
    public class ProveedoresAplicacion
    {
        private ProveedoresRepositorio iRepositorio;

        public ProveedoresAplicacion(ProveedoresRepositorio iRepositorio)
        {
            this.iRepositorio = iRepositorio;
        }

        public void Configurar(string string_conexion)
        {
            this.iRepositorio.Configurar(string_conexion);
        }

        public List<Proveedores> Listar()
        {
            return this.iRepositorio.Listar();
        }

        public List<Proveedores> Buscar(Proveedores entidad, string tipo)
        {
            Expression<Func<Proveedores, bool>>? condiciones = null;
            switch (tipo.ToUpper())
            {
                case "DOCUMENTO": condiciones = x => x.Documento == entidad.Documento; break;
                case "NOMBRE": condiciones = x => x.Nombre == entidad.Nombre; break;
                default: condiciones = x => x.Id != 0; break;
            }
            return this.iRepositorio.Buscar(condiciones);
        }

        public Proveedores Guardar(Proveedores entidad)
        {
            entidad.Id = 0;
            entidad._TipoDocumento = null;
            entidad._Ciudad = null;
            if (this.iRepositorio.Existe(
                    x => x.Documento == entidad.Documento))
            {
                throw new Exception("lbExisteProveedor");
            }

            entidad = this.iRepositorio.Guardar(entidad);
            if (entidad.Id == 0)
            {
                throw new Exception("lbErrorBaseDeDatos");
            }
            return entidad;
        }

        public Proveedores Modificar(Proveedores entidad)
        {
            entidad._TipoDocumento = null;
            entidad._Ciudad = null;
            if (!this.iRepositorio.Existe(
                    x => x.Id == entidad.Id))
            {
                throw new Exception("lbNoExisteProveedor");
            }
            if (this.iRepositorio.Existe(
                    x => x.Id != entidad.Id &&
                         x.Nombre == entidad.Nombre))
            {
                throw new Exception("lbExisteProveedor");
            }

            entidad = this.iRepositorio.Modificar(entidad);
            return entidad;
        }

        public Proveedores Borrar(Proveedores entidad)
        {
            entidad._TipoDocumento = null;
            entidad._Ciudad = null;
            if (!this.iRepositorio.Existe(
                    x => x.Id == entidad.Id))
            {
                throw new Exception("lbNoExisteProveedor");
            }
            entidad = this.iRepositorio.Borrar(entidad);
            return entidad;
        }
    }
}