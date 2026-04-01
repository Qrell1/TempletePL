using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempletePL.Types
{
    public class VoidValue : Value
    {
        public VoidValue() { }

        public bool GetBool()
        {
            return false;
        }

        public double GetDouble()
        {
            return 0;
        }

        public string GetString()
        {
            return string.Empty;
        }
    }
}
