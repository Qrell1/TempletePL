using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempletePL.Types
{
    public class StringValue : Value
    {
        private string value;

        public StringValue (string value)
        {
            this.value = value;
        }

        public double GetDouble()
        {
            try
            {
                return Convert.ToDouble(value);
            } catch (FormatException e)
            { return 0; }
        }

        public string GetString()
        {
            return value;
        }

        public bool GetBool()
        {
            if (value.ToUpper().Contains("TRUE"))
                return true;
            return false;
        }

        public override string ToString()
        {
            return value;
        }
    }
}
