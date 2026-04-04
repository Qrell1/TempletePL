using TempletePL.Expressions;
using TempletePL.FunctionsStucts;
using TempletePL.Statements;
using TempletePL.Structs;
using TempletePL.Types;

namespace TempletePL
{
    internal class Parser
    {
        private List<Token> tokens;
        private int pos;

        private bool cmp;

        public Parser() { }

        private void except(TokenType type)
        {
            if (pos < tokens.Count)
            {
                Token token = tokens[pos];
                if (token.type != type) throw new Exception($"На позиции: {pos} ожидался токен: {type.ToString()}");
            }
        }
        private void except(TokenType type, string value)
        {
            if (pos < tokens.Count)
            {
                Token token = tokens[pos];
                if (token.type != type && token.value != value) throw new Exception($"На позиции: {pos} ожидался токен: {type.ToString()}");
            }
        }

        private Token take(int offset)
        {
            offset += pos;
            if (offset < tokens.Count) return tokens[offset];
            return tokens[0];
        }
        private Token take()
        {
            if (pos < tokens.Count)
            {
                pos++;
                return tokens[pos-1];
            }
            return tokens[0];
        }

        private bool peek(TokenType type)
        {
            if (pos < tokens.Count)
            {
                Token token = tokens[pos];
                if (token.type == type) return true;
            }
            return false;
        }
        private void skip()
        {
            pos++;
        }

        public List<Statement> parse (List<Token> tokens)
        {
            this.tokens = tokens;

            List<Statement> result = new List<Statement> ();

            while (true)
            {
                Statement statement = ParseStatement();
                if (statement != null)
                {
                    result.Add(statement);
                    continue;
                }
                break;
            }

            return result;
        }

        private Statement ParseStatement()
        {
            if (peek(TokenType.FUNCTION))
            {
                ParseFunction();
            }
            if (peek(TokenType.LB))
            {
                return ParseArrayDeclarationStatement();
            }
            if (peek(TokenType.VAR) && tokens[pos + 1].type == TokenType.LB)
            {
                return ParseArrayAssignIndexStatement();
            }
            if (peek(TokenType.VAR) && tokens[pos + 1].type == TokenType.LPAR)
            {
                return ParseCallStatement();
            }
            if (peek(TokenType.VAR) && tokens[pos + 1].type == TokenType.SUFIX)
            {
                return ParseSufixStatement();
            }
            if (peek(TokenType.OUT))
            {
                return ParseOutStatemnt();
            }
            if (peek(TokenType.VARDECL))
            {
                return ParseVariableDeclarationStatement();
            }
            if (peek(TokenType.VAR))
            {
                return ParseAssignStatement();
            }
            if (peek(TokenType.IF))
            {
                return ParseIfStatement();
            }
            if (peek(TokenType.WHILE))
            {
                return ParseWhileStatement();
            }
            if (peek(TokenType.DO))
            {
                return ParseDoWhileStatement();
            }
            if (peek(TokenType.FOR))
            {
                return ParseForStatement();
            }
            if (peek(TokenType.BREAK))
            {
                return ParseBreakStatement();
            }
            if (peek(TokenType.CONTINUE))
            {
                return ParseContinueStatement();
            }
            if (peek(TokenType.RETURN))
            {
                return ParseReturnStatement();
            }
            return null;
        }

        private Statement ParseCallStatement()
        {
            string name = take().value;
            List<Expression> arguments = ParseFormulaSignature();
            return new CallStatement(arguments.ToArray(), name);
        }
        private void ParseFunction()
        {
            skip(); except(TokenType.VAR);
            string name = take().value;
            List<VariableDeclarationStatement> arguments = ParseVariableDeclarationSignature();
            List<Statement> statements = ParseBody();

            Functions.CreateFunction(name, new FunctionStatement(arguments, statements, name));
        }
        private Statement ParseReturnStatement()
        {
            skip();
            return new ReturnStatement(parseFormula());
        }
        private Statement ParseContinueStatement()
        {
            skip();
            return new ContinueStatement();
        }
        private Statement ParseBreakStatement()
        {
            skip();
            return new BreakStatement();
        }
        private Statement ParseSufixStatement()
        {
            return new SufixStatement(take().value, take());
        }
        private Statement ParseDoWhileStatement()
        {
            skip();

            List<Statement> statements = ParseBody();
            except(TokenType.WHILE); skip();
            cmp = true;
            Expression conditionExpression = parseCmp(); cmp = false;
            
            return new DoWhileStatement(conditionExpression, statements);
        }
        private Statement ParseForStatement()
        {
            skip();

            VariableDeclarationStatement variableDeclarationStatement = (VariableDeclarationStatement)ParseVariableDeclarationStatement();
            if (peek(TokenType.PS)) skip(); cmp = true;
            Expression condition = parseCmp(); cmp = false; if (peek(TokenType.PS)) skip();
            Expression operation = parseFormula();

            return new ForStatement(variableDeclarationStatement, condition, operation, ParseBody());
        }
        private Statement ParseWhileStatement()
        {
            skip();

            cmp = true;
            Expression conditionExpression = parseCmp(); cmp = false;
            List<Statement> statements = ParseBody();
            return new WhileStatement(conditionExpression, statements);
        }

        private Statement ParseIfStatement()
        {
            skip();

            cmp = true;
            Expression conditionExpression = parseCmp(); cmp = false;
            List<Statement> statements = ParseBody();

            if (!peek(TokenType.ELSE) && !peek(TokenType.ELIF)) return new IfStatement(conditionExpression, statements, null);
            return new IfStatement(conditionExpression, statements, ParseElseStatement());
        }
        private ElseStatement ParseElseStatement()
        {
            if (peek(TokenType.ELSE))
            {
                skip();
                List<Statement> statements = ParseBody();
                if (!peek(TokenType.ELSE) && !peek(TokenType.ELIF)) return new ElseStatement(null, statements, null);
                return new ElseStatement(null, statements, ParseElseStatement());
            }
            if (peek(TokenType.ELIF))
            {
                skip(); cmp = true;
                Expression conditionExpression = parseCmp(); cmp = false;
                List<Statement> statements = ParseBody();
                if (!peek(TokenType.ELSE) && !peek(TokenType.ELIF)) return new ElseStatement(conditionExpression, statements, null);
                return new ElseStatement(conditionExpression, statements, ParseElseStatement());
            }
            throw new Exception($"На позиции: {pos} ожидался токен: ELSE, ELIF");
        }
        private Statement ParseOutStatemnt()
        {
            skip();
            Expression expr = parseFormula();
            return new OutStatement(expr);
        }
        private Statement ParseVariableDeclarationStatement()
        {
            skip();
            except(TokenType.VAR);
            Token variableToken = take();
            if (!peek(TokenType.OPER)) return new VariableDeclarationStatement(variableToken.value, new VoidExpression());
            except(TokenType.OPER, "="); skip();
            Expression expr = parseFormula();

            return new VariableDeclarationStatement(variableToken.value, expr);
        }
        private Statement ParseArrayDeclarationStatement()
        {
            skip();
            Expression expr = parseFormula();
            except(TokenType.RB); skip(); except(TokenType.VAR);
            return new ArrayDeclarationStatement(take().value, expr);
        }
        private Statement ParseArrayAssignIndexStatement()
        {
            Token variable = take(); skip();
            Expression expr = parseFormula();
            except(TokenType.RB); skip(); except(TokenType.OPER, "="); skip();
            return new ArrayAssignStatement(variable.value, expr, parseFormula());
        }
        private Statement ParseAssignStatement()
        {
            Token variableToken = take();
            if (!peek(TokenType.OPER)) return new VariableDeclarationStatement(variableToken.value, new VoidExpression());
            except(TokenType.OPER, "="); skip();
            Expression expr = parseFormula();

            return new AssignStatement(variableToken.value, expr);
        }

        private List<Statement> ParseBody()
        {
            if (!peek(TokenType.LFIG) && peek(TokenType.END)) return new List<Statement> { };
            if (peek(TokenType.LFIG)) skip();
            List<Statement> statements = new List<Statement>();

            while (true)
            {
                statements.Add(ParseStatement());
                if (peek(TokenType.RFIG) || peek(TokenType.END) || peek(TokenType.THEN))
                    break;
            }

            skip();
            return statements;
        }
        private List<Expression> ParseFormulaSignature()
        {
            skip();
            if (peek(TokenType.RPAR) || peek(TokenType.THEN))
            {
                skip();
                return new List<Expression> { };
            }

            List<Expression> expressions = new List<Expression>();
            do
            {
                expressions.Add(parseFormula());
            } while (peek(TokenType.PS) && take().type == TokenType.PS);

            if (peek(TokenType.RPAR) || peek(TokenType.END) || peek(TokenType.THEN)) skip();
            return expressions;
        }
        private List<VariableDeclarationStatement> ParseVariableDeclarationSignature()
        {
            if (peek(TokenType.LPAR)) skip();
            if (peek(TokenType.RPAR) || peek(TokenType.THEN)) return new List<VariableDeclarationStatement> { };

            List<VariableDeclarationStatement> expressions = new List<VariableDeclarationStatement>();
            do
            {
                expressions.Add((VariableDeclarationStatement)ParseVariableDeclarationStatement());
            } while (peek(TokenType.PS) && take().type == TokenType.PS);

            if (peek(TokenType.RPAR) || peek(TokenType.END) || peek(TokenType.THEN)) skip();
            return expressions;
        }

        private Expression parsePar()
        {
            if (peek(TokenType.LPAR))
            {
                skip();
                Expression exp = (cmp) ? parseCmp() : parseFormula();

                if (!peek(TokenType.RPAR))
                {
                    throw new Exception($"Ожидалась закрывающая скобка на позиции {pos}");
                }
                skip();
                return exp;
            }
            else
            {
                return parseVariableOrNumberOrFunction();
            }
        }
        private Expression parseVariableOrNumberOrFunction()
        {
            Token token = take();

            if (token.type == TokenType.VAR && peek(TokenType.LB))
            {
                skip();
                ArrayExpression expr = new ArrayExpression(token.value, parseFormula()); except(TokenType.RB);
                skip();
                return expr;
            }
            if (token.type == TokenType.VAR && peek(TokenType.LPAR)) return new CallExpression(ParseFormulaSignature().ToArray(), token.value);
            if (token.type == TokenType.SUFIX && peek(TokenType.VAR)) return new SufixExpression(take().value, token, false);
            if (token.type == TokenType.VAR && peek(TokenType.SUFIX)) return new SufixExpression(token.value, take(), true);
            if (token.type == TokenType.VAR) return new VariableExpression(token.value);
            if (token.type == TokenType.OPER && (token.value == "+" || token.value == "-")) return new UnaryExpression(parseFormula(), token);
            if (token.type == TokenType.NUMBER) return new NumberExpression(Convert.ToDouble(token.value));
            if (token.type == TokenType.STRING) return new StringExpression(token.value.Replace("\\n", "\n").Replace("\\t", "\t"));
            //if (token.type.type == "CHAR") return new CommonNode("CHAR", token);
            if (token.type == TokenType.BOOL) return new BoolExpression((token.value == "true") ? true : false);
            if (token.type == TokenType.FLOAT) return new NumberExpression(Convert.ToDouble(token.value.Replace(".",",").Replace("f","")));

            throw new Exception($"Ошибка в парсинге формулы из-за Токена: " + token.toString());
        }
        
        public Expression parseCmp(Expression leftOper = null)
        {
            Expression buffer;
            Expression left;
            if (leftOper == null)
                left = parseTerm();
            else
                left = leftOper;
            Token operatpor = null;
            if (peek(TokenType.OPER) && (new string[] { "&&", "||" }.Contains(tokens[pos].value)))
                operatpor = take();
            while (operatpor != null)
            {
                Expression right = parseTerm();

                buffer = left;
                left = new ConditionExpression(buffer, right, operatpor);

                if (peek(TokenType.OPER) && (new string[] { "&&", "||" }.Contains(tokens[pos].value)))
                    operatpor = take();
                else
                    operatpor = null;
            }

            return left;
        }
        
        public Expression parseTerm()
        {
            Expression buffer;
            Expression left = parseFormula(); // token 1
            Token operatpor = null;         // token 2
            while (peek(TokenType.OPER) && (new string[] { "==", "!=", "<=", ">=", "<", ">" }.Contains(tokens[pos].value)))
            {
                operatpor = take();
                Expression right = parseFormula();

                buffer = left;
                left = new ConditionExpression(buffer, right, operatpor);
            }

            return left;
        }
        private Expression parseFormula()
        {
            Expression buffer;
            Expression left = parseTerm3(); // token 1
            Token operatpor = null;         // token 2
            while (peek(TokenType.OPER) && (new string[] { "+", "-" }.Contains(tokens[pos].value)))
            {
                operatpor = take();
                Expression right = parseTerm3();

                buffer = left;
                left = new BinaryExpression(buffer, right, operatpor);
            }

            return left;
        }
        private Expression parseTerm3()
        {
            Expression buffer;
            Expression left = parsePar(); // token 1
            Token operatpor = null;       // token 2
  
            while (peek(TokenType.OPER) && (new string[] { "*", "/", "%" }.Contains(tokens[pos].value)))
            {
                operatpor = take();
                Expression right = parsePar();

                buffer = left;
                left = new BinaryExpression(buffer, right, operatpor);
            }

            return left;
        }
    }
}
