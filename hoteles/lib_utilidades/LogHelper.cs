using System.Runtime.CompilerServices;

namespace lib_utilidades
{
    public class LogHelper
    {
        public static ILogHelper? ILogHelper = null;

        public static void Log(Exception exception, bool subError = false, [CallerMemberName] string caller = "", [CallerFilePath] string file = "")
        {
            if (ILogHelper == null)
                return;

            ILogHelper.Log(exception, subError, caller, file);
        }
    }

    public interface ILogHelper
    {
        void Log(Exception exception, bool subError = false, [CallerMemberName] string caller = "", [CallerFilePath] string file = "");
    }
}
