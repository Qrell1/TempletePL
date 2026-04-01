using TempletePL.Types;

namespace TempletePL.FunctionsStructs
{
    public interface Function
    {
        public Value run(Value[] args);
    }
}
