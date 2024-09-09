using lib_entidades_dominio;
using lib_repositorios.Implementaciones;
using System.Linq.Expressions;

namespace lib_aplicaciones.Implementaciones
{
    public class DepartamentosAplicacion
    {
        private DepartamentosRepositorio iRepositorio;

        public DepartamentosAplicacion(DepartamentosRepositorio iRepositorio)
        {
            this.iRepositorio = iRepositorio;
        }

        public void Configurar(string string_conexion)
        {
            this.iRepositorio.Configurar(string_conexion);
        }

        public List<Departamentos> Listar()
        {
            return this.iRepositorio.Listar();
        }

        public List<Departamentos> Buscar(Departamentos entidad, string tipo)
        {
            Expression<Func<Departamentos, bool>>? condiciones = null;
            switch (tipo.ToUpper())
            {
                case "NOMBRE": condiciones = x => x.Nombre == entidad.Nombre; break;
                default: condiciones = x => x.Id != 0; break;
            }
            return this.iRepositorio.Buscar(condiciones);
        }

        public Departamentos Guardar(Departamentos entidad)
        {
            entidad.Id = 0;
            if (this.iRepositorio.Existe(
                    x => x.Nombre == entidad.Nombre))
            {
                throw new Exception("lbExisteDepartamento");
            }

            entidad = this.iRepositorio.Guardar(entidad);
            if (entidad.Id == 0)
            {
                throw new Exception("lbErrorBaseDeDatos");
            }
            return entidad;
        }

        public Departamentos Modificar(Departamentos entidad)
        {
            if (!this.iRepositorio.Existe(
                    x => x.Id == entidad.Id))
            {
                throw new Exception("lbNoExisteDepartamento");
            }
            if (this.iRepositorio.Existe(
                    x => x.Id != entidad.Id &&
                         x.Nombre == entidad.Nombre))
            {
                throw new Exception("lbExisteDepartamento");
            }

            entidad = this.iRepositorio.Modificar(entidad);
            return entidad;
        }

        public Departamentos Borrar(Departamentos entidad)
        {
            if (!this.iRepositorio.Existe(
                    x => x.Id == entidad.Id))
            {
                throw new Exception("lbNoExisteDepartamento");
            }
            entidad = this.iRepositorio.Borrar(entidad);
            return entidad;
        }
    }
}