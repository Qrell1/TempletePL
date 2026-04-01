using TempletePL.Types;

namespace TempletePL.FunctionsStructs
{
    public class ClearFucntion : Function
    {
        public string name { get; }

        public ClearFucntion(string name = "clear")
        {
            this.name = name;
        }


        public Value run(Value[] args)
        {
            Console.Clear();
            return new VoidValue();
        }

        public override string ToString()
        {
            return $"function [{name}] -> {{}}";
        }
    }
}
