using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trustid
{
    public static class Globals
    {
        public static String jwt_access = "";
        public static String jwt_refresh = "";
        public static String user_name = "";
        public static String surname = "";
        public static Int64 verification_retry_attempts = 0;
        public static String exam_id = "";
        public static String exam_name = "";
        public static String exam_status = "";
        public static String exam_scheduled_date = "";
        public static bool is_exam_started = false;
        public static String role = "";
        public static dynamic enrolled_students;
        public static bool is_monitoring_active = false;
        public static int attack;
        public static int macroat = 0;

        public static System.Windows.Media.Color ConvertStringToColor(String hex)
        {
            //remove the # at the front
            hex = hex.Replace("#", "");

            byte a = 255;
            byte r = 255;
            byte g = 255;
            byte b = 255;

            int start = 0;

            //handle ARGB strings (8 characters long)
            if (hex.Length == 8)
            {
                a = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
                start = 2;
            }

            //convert RGB characters to bytes
            r = byte.Parse(hex.Substring(start, 2), System.Globalization.NumberStyles.HexNumber);
            g = byte.Parse(hex.Substring(start + 2, 2), System.Globalization.NumberStyles.HexNumber);
            b = byte.Parse(hex.Substring(start + 4, 2), System.Globalization.NumberStyles.HexNumber);

            return System.Windows.Media.Color.FromArgb(a, r, g, b);
        }
    }
}
