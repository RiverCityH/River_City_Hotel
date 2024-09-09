using lib_entidades_dominio;

namespace mst_prueba_unitarias.Nucleo
{
    public class EntidadesHelper
    {
        public static Paises ObtenerPaises()
        {
            return new Paises()
            {
                Id = 0,
                Nombre = "Prueba pais",
            };
        }

        public static Departamentos ObtenerDepartamentos()
        {
            return new Departamentos()
            {
                Id = 0,
                Nombre = "Prueba departamento",
                Pais = 1,
            };
        }

        public static Ciudades ObtenerCiudades()
        {
            return new Ciudades()
            {
                Id = 0,
                Nombre = "Prueba ciudad",
                Departamento = 1,
            };
        }

        public static Tipos ObtenerTipos()
        {
            return new Tipos()
            {
                Id = 0,
                Nombre = "Prueba tipo",
                Tabla = "Test",
                Accion = 0
            };
        }

        public static Personas ObtenerPersonas()
        {
            return new Personas()
            {
                Id = 0,
                TipoDocumento = 2,
                Documento = "25461321321",
                Nombre = "Prueba personas",
                FechaNacimiento = DateTime.Now,
                Celular = "3692581478",
                Genero = 4,
                Direccion = "Cra 52 # 75 - 15",
                Email = "test@email.com",
                Contraseña = "HJGv32687ghjYUTug",
                Confirmar = false,
                Restablecer = false,
                Token = null,
                Ciudad = 1,
                Activo = false
            };
        }

        public static Empleados ObtenerEmpleados()
        {
            return new Empleados()
            {
                Id = 0,
                Persona = 1,
                Cargo = 6,
                ARL = 9,
                Pension = 11,
                EPS = 10,
                TipoSangre = 14,
                EstadoCivil= 16
            };
        }
    }
}