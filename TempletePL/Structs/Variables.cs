using TempletePL.Types;

namespace TempletePL.Structs
{
    public static class Variables
    { 
        public static Stack<Dictionary<string, Value>> VariablesMap = new Stack<Dictionary<string, Value>>();
        public static Dictionary<string, Value> ConstantsMap = new Dictionary<string, Value>();

        static Variables ()
        {
            ConstantsMap.Add("PI", new NumberValue(Math.PI));
            ConstantsMap.Add("pi", new NumberValue(Math.PI));
            ConstantsMap.Add("E", new NumberValue(Math.E));
            ConstantsMap.Add("e", new NumberValue(Math.E));
            VariablesMap.Push(new Dictionary<string, Value>());
            foreach (var key in ConstantsMap) VariablesMap.Peek().Add(key.Key, key.Value);
        }

        public static void ConstantCreate ()
        {
            VariablesMap.Clear();
            VariablesMap.Push(new Dictionary<string, Value>());
            foreach (var key in ConstantsMap) VariablesMap.Peek().Add(key.Key, key.Value);
        }

        public static Value GetValue (string variable)
        {
            if (!VariablesMap.Peek().ContainsKey(variable)) throw new Exception($"Переменной: {variable} несуществует!");

            return VariablesMap.Peek()[variable];
        }

        public static void AddVariable(string variable, Value value)
        {
            if (!VariablesMap.Peek().ContainsKey(variable)) VariablesMap.Peek().Add(variable, value);
            VariablesMap.Peek()[variable] = value;
        }


        public static bool IsVariable (string variable)
        {
            return VariablesMap.Peek().ContainsKey(variable);
        }

        public static void OpenSpace()
        {
            VariablesMap.Push(new Dictionary<string, Value>());
        }
        public static void CloseSpace()
        {
            VariablesMap.Pop();
        }
    }
}
