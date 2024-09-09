using lib_utilidades;

namespace asp_hoteles.Nucleo
{
    public static class SessionExtensions
    {
        public static void SetObject(this ISession session, string key, object value)
        {
            if (value == null)
                return;
            value = JsonHelper.ConvertToString(value, true);
            if (value == null)
                return;
            session.SetString(key, value.ToString()!);
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            if (value == null)
                return default(T)!;
            return JsonHelper.ConvertToObject<T>(value);
        }
    }
}
