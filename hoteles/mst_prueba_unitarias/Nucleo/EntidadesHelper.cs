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
    }
}