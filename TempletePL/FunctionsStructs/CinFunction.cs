using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempletePL.Statements;
using TempletePL.Structs;
using TempletePL.Types;

namespace TempletePL.FunctionsStructs
{
    public class CinFunction : Function
    {
        public string name { get; }

        public CinFunction(string name = "cin")
        {
            this.name = name;
        }


        public Value run(Value[] args)
        {
            string input = Console.ReadLine();
            try { return new NumberValue(Convert.ToDouble(input)); }
            catch { }
            return new StringValue(input);
        }

        public override string ToString()
        {
            return $"function [{name}] -> {{}}";
        }
    }
}
