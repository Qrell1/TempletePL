using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TempletePL.Structs;

namespace TempletePL
{
    internal class Lexer
    {
        private string code;
        private int pos;
        private List<Token> tokens;

        public Lexer() { }

        public List<Token> lex (string _code)
        {
            pos = 0;
            code = _code;
            tokens = new List<Token>();
            while (nextToken()) { }

            return tokens;
        }
        private bool nextToken()
        {
            if (pos >= code.Length)
                return false;
            if (skipSpace())
                return true;


            foreach (var tokenType in TokenTypeList.TypesRegexs)
            {
                Match regx = Regex.Match(code.Substring(pos), "^" + tokenType.Value);
                if (regx.Success && !string.IsNullOrEmpty(regx.Value))
                {
                    Token token;

                    if (tokenType.Key == TokenType.STRING)
                    {
                        string value = regx.Value;
                        value = value.Substring(1, value.Length - 2);
                        value = value.Replace("\\\"", "\"").Replace("\\\\", "\\");
                        pos += regx.Length;
                        tokens.Add(new Token(value, tokenType.Key));
                        return true;
                    }

                    pos += regx.Length;
                    tokens.Add(new Token(regx.Value, tokenType.Key));
                    return true;
                }
            }

            throw new Exception($"На позиции {pos} синтаксическая ошибка. Символ: '{code[pos]}'");
        }

        /* Пропуск пробельных символов и комментариев */
        private bool skipSpace()
        {
            bool skip = false;

            while (true)
            {
                Match whitespace = Regex.Match(code.Substring(pos), @"^\s+");
                if (whitespace.Success)
                {
                    pos += whitespace.Value.Length;
                    skip = true;
                }
                if (code.Substring(pos).StartsWith("/*"))
                {
                    Match comment = Regex.Match(code.Substring(pos), @"^/\*[\s\S]*?\*/");
                    if (comment.Success)
                    {
                        pos += comment.Value.Length;
                        skip = true;
                        continue;
                    }
                }

                break;
            }

            return skip;
        }
    }
}
