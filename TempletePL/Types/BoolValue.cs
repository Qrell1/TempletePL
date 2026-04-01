using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempletePL.Types
{
    public class BoolValue : Value
    {
        private bool value;

        public BoolValue (bool value)
        {
            this.value = value;
        }

        public double GetDouble()
        {
            if (value) return 1;
            return 0;
        }

        public string GetString()
        {
            if (value) return "true";
            return "false";
        }
        public bool GetBool()
        {
            return value;
        }
    }
}
