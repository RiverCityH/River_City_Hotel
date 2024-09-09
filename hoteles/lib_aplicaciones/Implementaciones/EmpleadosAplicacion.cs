using lib_entidades_dominio;
using lib_repositorios.Implementaciones;
using System.Linq.Expressions;

namespace lib_aplicaciones.Implementaciones
{
    public class EmpleadosAplicacion
    {
        private EmpleadosRepositorio iRepositorio;

        public EmpleadosAplicacion(EmpleadosRepositorio iRepositorio)
        {
            this.iRepositorio = iRepositorio;
        }

        public void Configurar(string string_conexion)
        {
            this.iRepositorio.Configurar(string_conexion);
        }

        public List<Empleados> Listar()
        {
            return this.iRepositorio.Listar();
        }

        public List<Empleados> Buscar(Empleados entidad, string tipo)
        {
            Expression<Func<Empleados, bool>>? condiciones = null;
            switch (tipo.ToUpper())
            {
                default: condiciones = x => x.Id != 0; break;
            }
            return this.iRepositorio.Buscar(condiciones);
        }

        public Empleados Guardar(Empleados entidad)
        {
            entidad.Id = 0;
            if (this.iRepositorio.Existe(
                    x => x.Persona == entidad.Persona))
            {
                throw new Exception("lbExisteEmpleado");
            }

            entidad = this.iRepositorio.Guardar(entidad);
            if (entidad.Id == 0)
            {
                throw new Exception("lbErrorBaseDeDatos");
            }
            return entidad;
        }

        public Empleados Modificar(Empleados entidad)
        {
            if (!this.iRepositorio.Existe(
                    x => x.Id == entidad.Id))
            {
                throw new Exception("lbNoExisteEmpleado");
            }
            if (this.iRepositorio.Existe(
                    x => x.Id != entidad.Id &&
                         x.Persona == entidad.Persona))
            {
                throw new Exception("lbExisteEmpleado");
            }

            entidad = this.iRepositorio.Modificar(entidad);
            return entidad;
        }

        public Empleados Borrar(Empleados entidad)
        {
            if (!this.iRepositorio.Existe(
                    x => x.Id == entidad.Id))
            {
                throw new Exception("lbNoExisteEmpleado");
            }
            entidad = this.iRepositorio.Borrar(entidad);
            return entidad;
        }
    }
}