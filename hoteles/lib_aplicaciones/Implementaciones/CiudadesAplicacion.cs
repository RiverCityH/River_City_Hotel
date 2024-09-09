using lib_entidades_dominio;
using lib_repositorios.Implementaciones;
using System.Linq.Expressions;

namespace lib_aplicaciones.Implementaciones
{
    public class CiudadesAplicacion
    {
        private CiudadesRepositorio iRepositorio;

        public CiudadesAplicacion(CiudadesRepositorio iRepositorio)
        {
            this.iRepositorio = iRepositorio;
        }

        public void Configurar(string string_conexion)
        {
            this.iRepositorio.Configurar(string_conexion);
        }

        public List<Ciudades> Listar()
        {
            return this.iRepositorio.Listar();
        }

        public List<Ciudades> Buscar(Ciudades entidad, string tipo)
        {
            Expression<Func<Ciudades, bool>>? condiciones = null;
            switch (tipo.ToUpper())
            {
                case "NOMBRE": condiciones = x => x.Nombre == entidad.Nombre; break;
                default: condiciones = x => x.Id != 0; break;
            }
            return this.iRepositorio.Buscar(condiciones);
        }

        public Ciudades Guardar(Ciudades entidad)
        {
            entidad.Id = 0;
            if (this.iRepositorio.Existe(
                    x => x.Nombre == entidad.Nombre))
            {
                throw new Exception("lbExisteCiudad");
            }

            entidad = this.iRepositorio.Guardar(entidad);
            if (entidad.Id == 0)
            {
                throw new Exception("lbErrorBaseDeDatos");
            }
            return entidad;
        }

        public Ciudades Modificar(Ciudades entidad)
        {
            if (!this.iRepositorio.Existe(
                    x => x.Id == entidad.Id))
            {
                throw new Exception("lbNoExisteCiudad");
            }
            if (this.iRepositorio.Existe(
                    x => x.Id != entidad.Id &&
                         x.Nombre == entidad.Nombre))
            {
                throw new Exception("lbExisteCiudad");
            }

            entidad = this.iRepositorio.Modificar(entidad);
            return entidad;
        }

        public Ciudades Borrar(Ciudades entidad)
        {
            if (!this.iRepositorio.Existe(
                    x => x.Id == entidad.Id))
            {
                throw new Exception("lbNoExisteCiudad");
            }
            entidad = this.iRepositorio.Borrar(entidad);
            return entidad;
        }
    }
}