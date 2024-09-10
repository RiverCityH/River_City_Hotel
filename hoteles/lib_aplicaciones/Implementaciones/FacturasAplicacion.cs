using lib_entidades_dominio;
using lib_repositorios.Implementaciones;
using System.Linq.Expressions;

namespace lib_aplicaciones.Implementaciones
{
    public class FacturasAplicacion
    {
        private FacturasRepositorio iRepositorio;

        public FacturasAplicacion(FacturasRepositorio iRepositorio)
        {
            this.iRepositorio = iRepositorio;
        }

        public void Configurar(string string_conexion)
        {
            this.iRepositorio.Configurar(string_conexion);
        }

        public List<Facturas> Listar()
        {
            return this.iRepositorio.Listar();
        }

        public List<Facturas> Buscar(Facturas entidad, string tipo)
        {
            Expression<Func<Facturas, bool>>? condiciones = null;
            switch (tipo.ToUpper())
            {
                case "NUMERO": condiciones = x => x.Numero == entidad.Numero; break;
                default: condiciones = x => x.Id != 0; break;
            }
            return this.iRepositorio.Buscar(condiciones);
        }

        public Facturas Guardar(Facturas entidad)
        {
            entidad.Id = 0;
            if (this.iRepositorio.Existe(
                    x => x.Numero == entidad.Numero))
            {
                throw new Exception("lbExisteFactura");
            }

            entidad = this.iRepositorio.Guardar(entidad);
            if (entidad.Id == 0)
            {
                throw new Exception("lbErrorBaseDeDatos");
            }
            return entidad;
        }

        public Facturas Modificar(Facturas entidad)
        {
            if (!this.iRepositorio.Existe(
                    x => x.Id == entidad.Id))
            {
                throw new Exception("lbNoExisteFactura");
            }
            if (this.iRepositorio.Existe(
                    x => x.Id != entidad.Id &&
                         x.Numero == entidad.Numero))
            {
                throw new Exception("lbExisteFactura");
            }

            entidad = this.iRepositorio.Modificar(entidad);
            return entidad;
        }

        public Facturas Borrar(Facturas entidad)
        {
            if (!this.iRepositorio.Existe(
                    x => x.Id == entidad.Id))
            {
                throw new Exception("lbNoExisteFactura");
            }
            entidad = this.iRepositorio.Borrar(entidad);
            return entidad;
        }
    }
}