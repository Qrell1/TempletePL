using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempletePL.Types
{
    public interface Value
    {
        double GetDouble();
        string GetString();
        bool GetBool();
    }
}
