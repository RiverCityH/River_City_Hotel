using lib_lenguajes;
using lib_utilidades;

namespace asp_hoteles.Nucleo
{
    public class EsconderID
    {
        public static string Encriptar(object data)
        {
            try
            {
                if (data == null)
                    return string.Empty;

                return EncryptHelper.Encriptar(data.ToString()!);
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex);
                return string.Empty;
            }
        }

        public static string Desencriptar(object data)
        {
            try
            {
                if (data == null)
                    return string.Empty;

                return EncryptHelper.Desencriptar(data.ToString()!);
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex);
                return string.Empty;
            }
        }
    }

    public class ConvertirBool
    {
        public static object Convert(object value)
        {
            try
            {
                if (value == null)
                    return RsMenu.lbInactivo;

                return value.ToString() == "False" ? RsMenu.lbInactivo : RsMenu.lbActivo;
            }
            catch
            {
                return RsMenu.lbInactivo;
            }
        }
    }
}
