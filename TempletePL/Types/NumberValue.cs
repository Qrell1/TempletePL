using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempletePL.Types
{
    public class NumberValue : Value
    {
        private double value;
        public NumberValue(double value)
        {
            this.value = value;
        }

        public double GetDouble()
        {
            return value;
        }

        public string GetString()
        {
            return ((int)value).ToString();
        }

        public bool GetBool()
        {
            if (value == 0)
                return false;
            return true;
        }

        public override string ToString()
        {
            return value.ToString();
        }
    }
}
