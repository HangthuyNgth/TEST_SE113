using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group13_Lab3
{
    public class Main
    {
        public static int CheckDateInMonth(int Month, int Year)
        {
            int[] Month31Days = { 1, 3, 5, 7, 8, 12 };
            int[] Month30Days = { 4, 6, 9, 11 };

            if (Month31Days.Contains(Month)) return 31;
            if (Month30Days.Contains(Month)) return 30;
            if (Month == 2)
            {
                if (Year % 400 == 0) return 29;
                else if (Year % 100 == 0) return 28;
                else if (Year % 4 == 0) return 29;
                return 28;
            }
            return 0;
        }

        public static bool CheckDate(int Day, int Month, int Year)
        {
            if (Month >= 1 && Month <= 12)
            {
                if (Day >= 1 && Day <= CheckDateInMonth(Month, Year))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
