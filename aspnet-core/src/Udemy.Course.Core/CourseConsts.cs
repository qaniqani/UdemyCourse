using Udemy.Course.Debugging;

namespace Udemy.Course
{
    public class CourseConsts
    {
        public const string LocalizationSourceName = "Course";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "fd134cd8540b4f68b06efea45ddf70e4";
    }
}
