using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempletePL.Types;

namespace TempletePL.FunctionsStructs.MathLibrary
{
    public class SqrtFunction : Function
    {
        public string name { get; }

        public SqrtFunction(string name = "sqrt")
        {
            this.name = name;
        }


        public Value run(Value[] args)
        {
            if (args.Length != 1) throw new ArgumentException($"Неверное количество аргументов {name}");

            return new NumberValue(Math.Sqrt(args[0].GetDouble()));
        }

        public override string ToString()
        {
            return $"function [{name}] -> {{}}";
        }
    }
}
