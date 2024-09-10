using lib_entidades_dominio;
using lib_repositorios.Implementaciones;
using System.Linq.Expressions;

namespace lib_aplicaciones.Implementaciones
{
    public class DetallesAplicacion
    {
        private DetallesRepositorio iRepositorio;

        public DetallesAplicacion(DetallesRepositorio iRepositorio)
        {
            this.iRepositorio = iRepositorio;
        }

        public void Configurar(string string_conexion)
        {
            this.iRepositorio.Configurar(string_conexion);
        }

        public List<Detalles> Listar()
        {
            return this.iRepositorio.Listar();
        }

        public List<Detalles> Buscar(Detalles entidad, string tipo)
        {
            Expression<Func<Detalles, bool>>? condiciones = null;
            switch (tipo.ToUpper())
            {
                case "FACTURA": condiciones = x => x.Factura == entidad.Factura; break;
                case "ID": condiciones = x => x.Id == entidad.Id; break;
                default: condiciones = x => x.Id != 0; break;
            }
            return this.iRepositorio.Buscar(condiciones);
        }

        public Detalles Guardar(Detalles entidad)
        {
            entidad.Id = 0;
            entidad._Factura = null;
            entidad._Producto = null;
            if (this.iRepositorio.Existe(
                    x => x.Factura == entidad.Factura && x.Producto == entidad.Producto))
            {
                throw new Exception("lbExisteDetalle");
            }

            entidad = this.iRepositorio.Guardar(entidad);
            if (entidad.Id == 0)
            {
                throw new Exception("lbErrorBaseDeDatos");
            }
            return entidad;
        }

        public Detalles Modificar(Detalles entidad)
        {
            entidad._Factura = null;
            entidad._Producto = null;
            if (!this.iRepositorio.Existe(
                    x => x.Id == entidad.Id))
            {
                throw new Exception("lbNoExisteDetalle");
            }
            if (this.iRepositorio.Existe(
                    x => x.Id != entidad.Id &&
                    x.Factura == entidad.Factura && 
                    x.Producto == entidad.Producto))
            {
                throw new Exception("lbExisteDetalle");
            }

            entidad = this.iRepositorio.Modificar(entidad);
            return entidad;
        }

        public Detalles Borrar(Detalles entidad)
        {
            entidad._Factura = null;
            entidad._Producto = null;
            if (!this.iRepositorio.Existe(
                    x => x.Id == entidad.Id))
            {
                throw new Exception("lbNoExisteDetalle");
            }
            entidad = this.iRepositorio.Borrar(entidad);
            return entidad;
        }
    }
}