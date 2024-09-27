using lib_entidades_dominio;
using lib_repositorios.Implementaciones;
using System.Linq.Expressions;

namespace lib_aplicaciones.Implementaciones
{
    public class PersonasAplicacion
    {
        private PersonasRepositorio iRepositorio;

        public PersonasAplicacion(PersonasRepositorio iRepositorio)
        {
            this.iRepositorio = iRepositorio;
        }

        public void Configurar(string string_conexion)
        {
            this.iRepositorio.Configurar(string_conexion);
        }

        public List<Personas> Listar()
        {
            return this.iRepositorio.Listar();
        }

        public List<Personas> Buscar(Personas entidad, string tipo)
        {
            Expression<Func<Personas, bool>>? condiciones = null;
            switch (tipo.ToUpper())
            {
                case "DOCUMENTO": condiciones = x => x.Documento == entidad.Documento; break;
                case "NOMBRE": condiciones = x => x.Nombre == entidad.Nombre; break;
                case "LOGIN": condiciones = x => x.Email == entidad.Email && x.Contraseña == entidad.Contraseña && x.Activo && x.Confirmar; break;
                default: condiciones = x => x.Id != 0; break;
            }
            return this.iRepositorio.Buscar(condiciones);
        }

        public Personas Guardar(Personas entidad)
        {
            entidad.Id = 0;
            entidad._TipoDocumento = null;
            entidad._Genero = null;
            entidad._Ciudad = null;
            if (this.iRepositorio.Existe(
                    x => x.Documento == entidad.Documento))
            {
                throw new Exception("lbExistePersona");
            }

            entidad = this.iRepositorio.Guardar(entidad);
            if (entidad.Id == 0)
            {
                throw new Exception("lbErrorBaseDeDatos");
            }
            return entidad;
        }

        public Personas Modificar(Personas entidad)
        {
            entidad._TipoDocumento = null;
            entidad._Genero = null;
            entidad._Ciudad = null;
            if (!this.iRepositorio.Existe(
                    x => x.Id == entidad.Id))
            {
                throw new Exception("lbNoExistePersona");
            }
            if (this.iRepositorio.Existe(
                    x => x.Id != entidad.Id &&
                         x.Nombre == entidad.Nombre))
            {
                throw new Exception("lbExistePersona");
            }

            entidad = this.iRepositorio.Modificar(entidad);
            return entidad;
        }

        public Personas Borrar(Personas entidad)
        {
            entidad._TipoDocumento = null;
            entidad._Genero = null;
            entidad._Ciudad = null;
            if (!this.iRepositorio.Existe(
                    x => x.Id == entidad.Id))
            {
                throw new Exception("lbNoExistePersona");
            }
            entidad = this.iRepositorio.Borrar(entidad);
            return entidad;
        }

        public Personas VerificarToken(string email, string token)
        {
            var persona = this.iRepositorio.Buscar(x => x.Email == email && x.Token == token).FirstOrDefault();

            if (persona == null)
            {
                throw new Exception("Token inválido o usuario no encontrado.");
            }

            persona.Confirmar = true;
            return this.iRepositorio.Modificar(persona);
        }



    }
}