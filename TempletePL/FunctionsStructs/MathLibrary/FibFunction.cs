using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempletePL.Types;

namespace TempletePL.FunctionsStructs.MathLibrary
{
    public class FibFunction : Function
    {
        public string name { get; }

        public FibFunction(string name = "fib")
        {
            this.name = name;
        }

        private double fib(int n)
        {
            if (n <= 1) return n;
            else return fib(n - 1) + fib(n - 2);
        }

        public Value run(Value[] args)
        {
            if (args.Length != 1) throw new ArgumentException($"Неверное количество аргументов {name}");

            return new NumberValue(fib((int)args[0].GetDouble()));
        }

        public override string ToString()
        {
            return $"function [{name}] -> {{}}";
        }
    }
}
