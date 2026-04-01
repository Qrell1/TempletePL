using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempletePL.Types;

namespace TempletePL.FunctionsStructs
{
    public class KeyFunction : Function
    {
        public string name { get; }

        public KeyFunction(string name = "readkey")
        {
            this.name = name;
        }


        public Value run(Value[] args)
        {
            return new StringValue(Console.ReadKey().Key.ToString());
        }

        public override string ToString()
        {
            return $"function [{name}] -> {{}}";
        }
    }
}
