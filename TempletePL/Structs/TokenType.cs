using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempletePL.Structs
{
    public enum TokenType
    {
        OPER,
        SUFIX,
        SEM,
        LPAR,
        RPAR,
        LFIG,
        RFIG,
        LB,
        RB,
        END,
        THEN,
        VARDECL,
        VAR,
        STRING,
        NUMBER,
        FLOAT,
        BOOL,
        OUT,
        PS,
        TS,

        DO,
        WHILE,
        FOR,
        REPT,

        BREAK,
        CONTINUE,
        RETURN,

        IF,
        ELSE,
        ELIF,

        FUNCTION,
    }

    public static class TokenTypeList
    {
        public static readonly Dictionary<TokenType, string> TypesRegexs = new Dictionary<TokenType, string>()
        {
            {TokenType.SUFIX, "(\\+\\+|\\-\\-)"},
            {TokenType.OPER, "(@|\\+=|==|!=|<=|>=|<|>|&&|\\|\\||-=|\\*=|\\/=|%=|&=|\\|=|\\^=|<<=|>>=|->|[+\\-\\*/%=|!:~])"},
            {TokenType.FLOAT, @"([0-9]+\.[0-9]*f?|\.[0-9]+f?|[0-9]+f)"},
            {TokenType.NUMBER, "[0-9]+"},
            {TokenType.FUNCTION, @"\bdef\b"},
            {TokenType.RETURN, @"\breturn\b"},
            {TokenType.CONTINUE, @"\bcontinue\b"},
            {TokenType.BREAK, @"\bbreak\b"},
            {TokenType.DO, @"\bdo\b"},
            {TokenType.WHILE, @"\bwhile\b"},
            {TokenType.FOR, @"\bfor\b"},
            {TokenType.REPT, @"\brept\b"},
            {TokenType.THEN, @"\bthen\b"},
            {TokenType.END, @"\bend\b"},
            {TokenType.BOOL, @"(\btrue\b|\bfalse\b)"},
            {TokenType.IF, @"\bif\b"},
            {TokenType.ELSE, @"\belse\b"},
            {TokenType.ELIF, @"\belif\b"},
            {TokenType.OUT, @"\bout\b"},
            {TokenType.VARDECL, @"(\bvar\b|\blet\b)"},
            {TokenType.VAR, @"\b[а-яА-Яa-zA-Z_][а-яА-Яa-zA-Z0-9_]*\b"},
            {TokenType.STRING, @"""[^""]*"""},
            {TokenType.LPAR, "\\("},
            {TokenType.RPAR, "\\)"},
            {TokenType.LFIG, "\\{"},
            {TokenType.RFIG, "\\}"},
            {TokenType.LB, "\\["},
            {TokenType.RB, "\\]"},
            {TokenType.PS, "\\,"},
            {TokenType.TS, "\\."}
        };
    }
}
