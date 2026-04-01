using TempletePL;
using TempletePL.Expressions;
using TempletePL.Statements;
using TempletePL.Structs;

namespace Program
{
    class Program
    {
        public static Lexer lexer = new Lexer();
        public static Parser parser = new Parser();
        public static void Main(string[] args)
        {
#if (DEBUG)
            Console.WriteLine("---<=== TPL ===>---");
#endif

            string code = string.Empty;

            if (args.Length > 0)
            {
                if (args[0] != "console")
                    code = File.ReadAllText(args[0]);
            }
            else
            {
                code = File.ReadAllText("codes/test1.tpl");
            }

            //Console.WriteLine(code);
#if (DEBUG)
            Console.WriteLine("Lexing...");
#endif
            List<Token> tokens = lexer.lex(code);

#if (DEBUG)
            foreach (Token token in tokens)
            {
                Console.WriteLine(token.toString());
            }
            Console.WriteLine("Parsing..");
#endif
            List<Statement> ast = parser.parse(tokens);
#if (DEBUG)
            foreach (Statement st in ast)
                Console.WriteLine(st.ToString());

            Console.WriteLine("\n\n--<Program Running>--");
#endif
            foreach (Statement st in ast)
                st.run();
#if (DEBUG)
            Console.WriteLine("\n--<Program End>--");
            Console.WriteLine("--<Code: 0");
#endif
            if (args.Length > 0 && args[0] != "console") goto end;
            Console.WriteLine("\n--<Console Mode>--");
            while (true)
            {
                Variables.VariablesMap.Clear();
                Variables.ConstantCreate();
                ast.Clear();
                code = string.Empty;
                while (true)
                {
                    Console.Write("<< ");
                    string temp = Console.ReadLine();
                    if (temp == "-run") break;
                    if (temp == "-clear")
                    {
                        Console.Clear();
                        continue;
                    }
                    if (temp == "-exit")
                    {
                        goto end;
                    }
                    if (temp.Contains("-save"))
                    {
                        string path = temp.Split(' ')[1];
                        File.Create(path).Close();
                        File.WriteAllText(path, code);
                        continue;
                    }
                    code = code + " " + temp;
                }
                
                lexer = new Lexer();
                parser = new Parser();
                tokens = lexer.lex(code);
                ast = parser.parse(tokens);
                Console.WriteLine("\n\n--<Program Running>--");
                foreach (Statement st in ast)
                    st.run();
                Console.WriteLine("\n--<Program End>--");
            }
            end:;
            //Console.ReadKey();
        }
    }
}