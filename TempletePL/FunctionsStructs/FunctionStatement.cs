using TempletePL.FunctionsStructs;
using TempletePL.Statements;
using TempletePL.Structs;
using TempletePL.Types;

namespace TempletePL.FunctionsStucts
{
    public class FunctionStatement : Function
    {
        public List<VariableDeclarationStatement> arguments { get; }
        public List<Statement> statements { get; }
        public string name { get; }

        public FunctionStatement(List<VariableDeclarationStatement> arguments, List<Statement> statements, string name)
        {
            this.arguments = arguments;
            this.statements = statements;
            this.name = name;
        }


        public Value run(Value[] args)
        {
            Variables.OpenSpace();
            if (args.Length != arguments.Count) throw new Exception("Количество аргументов вызова функции не равны! " + name);
            for (int i = 0; i < args.Length; i++)
                Variables.VariablesMap.Peek().Add(arguments[i].GetVariable(), args[i]);

            foreach (var statement in statements)
                try
                {
                    statement.run();
                }
                catch (Exception e)
                {
                    if (e.Message == "R")
                    {
                        Value value = Functions.ReturnExpression.run();
                        Variables.CloseSpace();
                        return value;
                    }
                }

            Variables.CloseSpace();
            return new VoidValue();
        }

        public override string ToString()
        {
            return $"function ({arguments.ToString()}) -> {{{statements.ToString()}}}";
        }
    }
}
