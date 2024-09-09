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
                //Id = 0,
                //Nombre = "Prueba tipo",
                //Tabla = "Test",
                //Accion = 0
            };
        }
    }
}