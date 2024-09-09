using Newtonsoft.Json;

namespace mst_prueba_unitarias.Nucleo
{
    public class ConfiguracionHelper
    {
        public static string ObtenerValor(string clave)
        {
            string path = @"E:\RiverCityH\River_City_Hotel\hoteles\secrets.json";
            StreamReader jsonStream = File.OpenText(path);
            var json = jsonStream.ReadToEnd();
            Dictionary<string, object> datos = JsonConvert.DeserializeObject<Dictionary<string, object>>(json)!;
            if (!datos.ContainsKey(clave))
                return string.Empty;
            return datos[clave].ToString()!;
        }
    }
}