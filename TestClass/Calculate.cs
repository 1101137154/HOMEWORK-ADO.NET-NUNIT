using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestClass
{
    public class Calculate
    {
        public int GetBirthYear(int Age, int format)
        {
            int WestYear = (DateTime.Now.Year - Age);

            if (format == 0)//Chinese
                return (WestYear - 1911);
            else if (format == 1)//West
                return WestYear;

            return 0;
        }
    }
}
