using TempletePL.Expressions;
using TempletePL.FunctionsStructs;
using TempletePL.FunctionsStucts;
using TempletePL.Statements;
using TempletePL.Types;

namespace TempletePL.Structs
{
    public static class Functions
    {
        public static Expression ReturnExpression;
        public static Dictionary<string, Function> FunctionsMap = new Dictionary<string, Function>();
        public static List<string> SystemFunction = new List<string>();

        static Functions ()
        {
            SystemFunction.Add("cin");
            SystemFunction.Add("clear");
            SystemFunction.Add("readkey");
        }

        public static Function GetFunction(string key)
        {
            if (!FunctionsMap.ContainsKey(key)) throw new Exception($"Функции {key} не существует!");
            return FunctionsMap[key];
        }

        public static void CreateFunction(string key, Function function)
        {
            if (FunctionsMap.ContainsKey(key)) throw new Exception($"Функция {key} уже существует!");
            FunctionsMap.Add(key, function);
        }

        public static Value RunFunction(string key, Value[] values)
        {
            if (!FunctionsMap.ContainsKey(key)) throw new Exception($"Функции {key} не существует!");
            return FunctionsMap[key].run(values);
        }

        public static Function GetNewFunction(string key)
        {
            switch (key)
            {
                case "cin": return new CinFunction();
                case "clear": return new ClearFucntion();
                case "readkey": return new KeyFunction();
            }
            return new FunctionStatement(
                new List<VariableDeclarationStatement>(((FunctionStatement)FunctionsMap[key]).arguments),
                new List<Statement>(((FunctionStatement)FunctionsMap[key]).statements),
                ((FunctionStatement)FunctionsMap[key]).name);
        }
    }
}
