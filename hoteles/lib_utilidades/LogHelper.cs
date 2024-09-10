using System.Runtime.CompilerServices;

namespace lib_utilidades
{
    public class LogHelper
    {
        public static void Log(Exception exception,
            IDictionary<string, object> ViewData)
        {
            if (ViewData == null)
                return;
            var mensaje = exception.Message.ToString();
            if (mensaje.Length >= 200)
                mensaje = mensaje.Substring(0, 200);
            ViewData!["Mensaje"] = mensaje;
        }
    }
}