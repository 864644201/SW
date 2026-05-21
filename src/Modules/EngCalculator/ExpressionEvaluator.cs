using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace EngCalculator
{
    /// <summary>
    /// 简单数学表达式求值器
    /// 支持: +, -, *, /, ^, sin, cos, tan, asin, acos, atan, sqrt, cbrt, log, ln, abs, pi, e
    /// </summary>
    public static class ExpressionEvaluator
    {
        private static readonly Dictionary<string, double> Constants = new Dictionary<string, double>
        {
            { "pi", Math.PI },
            { "e", Math.E },
        };

        public static double Evaluate(string expression, Dictionary<string, double> variables = null)
        {
            if (string.IsNullOrWhiteSpace(expression))
                throw new ArgumentException("表达式为空");

            // 预处理
            expression = expression.Trim();
            expression = expression.Replace("×", "*").Replace("÷", "/").Replace("（", "(").Replace("）", ")");
            expression = expression.Replace("^", "**");
            expression = expression.Replace("π", "pi");

            // 替换变量
            if (variables != null)
            {
                foreach (var kvp in variables)
                {
                    expression = Regex.Replace(expression,
                        @"\b" + Regex.Escape(kvp.Key) + @"\b",
                        kvp.Value.ToString(System.Globalization.CultureInfo.InvariantCulture),
                        RegexOptions.IgnoreCase);
                }
            }

            // 词法分析
            var tokens = Tokenize(expression);
            // 语法分析与求值
            int pos = 0;
            double result = ParseExpression(tokens, ref pos);
            if (pos < tokens.Count)
                throw new Exception("表达式语法错误: 多余的符号 '" + tokens[pos].Value + "'");
            return result;
        }

        private class Token
        {
            public string Type;
            public string Value;
            public double NumValue;
        }

        private static List<Token> Tokenize(string expr)
        {
            var tokens = new List<Token>();
            int i = 0;
            while (i < expr.Length)
            {
                char c = expr[i];
                if (char.IsWhiteSpace(c)) { i++; continue; }

                // 数字
                if (char.IsDigit(c) || c == '.')
                {
                    int start = i;
                    while (i < expr.Length && (char.IsDigit(expr[i]) || expr[i] == '.')) i++;
                    string numStr = expr.Substring(start, i - start);
                    var token = new Token { Type = "NUM", Value = numStr };
                    token.NumValue = double.Parse(numStr, System.Globalization.CultureInfo.InvariantCulture);
                    tokens.Add(token);
                    continue;
                }

                // 标识符 (函数名/变量名/常量)
                if (char.IsLetter(c))
                {
                    int start = i;
                    while (i < expr.Length && (char.IsLetterOrDigit(expr[i]) || expr[i] == '_')) i++;
                    string name = expr.Substring(start, i - start).ToLower();
                    if (Constants.ContainsKey(name))
                    {
                        tokens.Add(new Token { Type = "NUM", Value = name, NumValue = Constants[name] });
                    }
                    else if (name == "sin" || name == "cos" || name == "tan" ||
                             name == "asin" || name == "acos" || name == "atan" ||
                             name == "sqrt" || name == "cbrt" || name == "log" ||
                             name == "ln" || name == "abs" || name == "exp")
                    {
                        tokens.Add(new Token { Type = "FUNC", Value = name });
                    }
                    else
                    {
                        tokens.Add(new Token { Type = "VAR", Value = name });
                    }
                    continue;
                }

                // 运算符
                if (c == '+' || c == '-' || c == '*' || c == '/' || c == '(' || c == ')')
                {
                    // 处理 ** 幂运算
                    if (c == '*' && i + 1 < expr.Length && expr[i + 1] == '*')
                    {
                        tokens.Add(new Token { Type = "OP", Value = "**" });
                        i += 2;
                        continue;
                    }
                    tokens.Add(new Token { Type = c == '(' || c == ')' ? "PAREN" : "OP", Value = c.ToString() });
                    i++;
                    continue;
                }

                // 逗号 (函数参数分隔)
                if (c == ',')
                {
                    tokens.Add(new Token { Type = "COMMA", Value = "," });
                    i++;
                    continue;
                }

                throw new Exception("未知字符: " + c);
            }
            return tokens;
        }

        // 表达式 = 加减项
        private static double ParseExpression(List<Token> tokens, ref int pos)
        {
            double result = ParseTerm(tokens, ref pos);
            while (pos < tokens.Count && tokens[pos].Type == "OP" &&
                   (tokens[pos].Value == "+" || tokens[pos].Value == "-"))
            {
                string op = tokens[pos].Value;
                pos++;
                double right = ParseTerm(tokens, ref pos);
                if (op == "+") result += right;
                else result -= right;
            }
            return result;
        }

        // 项 = 乘除因子
        private static double ParseTerm(List<Token> tokens, ref int pos)
        {
            double result = ParseFactor(tokens, ref pos);
            while (pos < tokens.Count && tokens[pos].Type == "OP" &&
                   (tokens[pos].Value == "*" || tokens[pos].Value == "/"))
            {
                string op = tokens[pos].Value;
                pos++;
                double right = ParseFactor(tokens, ref pos);
                if (op == "*") result *= right;
                else result /= right;
            }
            return result;
        }

        // 因子 = 幂运算
        private static double ParseFactor(List<Token> tokens, ref int pos)
        {
            double result = ParseUnary(tokens, ref pos);
            if (pos < tokens.Count && tokens[pos].Type == "OP" && tokens[pos].Value == "**")
            {
                pos++;
                double exponent = ParseUnary(tokens, ref pos);
                result = Math.Pow(result, exponent);
            }
            return result;
        }

        // 一元运算
        private static double ParseUnary(List<Token> tokens, ref int pos)
        {
            if (pos < tokens.Count && tokens[pos].Type == "OP" && tokens[pos].Value == "-")
            {
                pos++;
                return -ParsePrimary(tokens, ref pos);
            }
            if (pos < tokens.Count && tokens[pos].Type == "OP" && tokens[pos].Value == "+")
            {
                pos++;
            }
            return ParsePrimary(tokens, ref pos);
        }

        // 基本元素
        private static double ParsePrimary(List<Token> tokens, ref int pos)
        {
            if (pos >= tokens.Count)
                throw new Exception("表达式不完整");

            var token = tokens[pos];

            // 数字
            if (token.Type == "NUM")
            {
                pos++;
                return token.NumValue;
            }

            // 变量
            if (token.Type == "VAR")
            {
                pos++;
                throw new Exception("未知变量: " + token.Value);
            }

            // 函数调用
            if (token.Type == "FUNC")
            {
                string funcName = token.Value;
                pos++;
                if (pos >= tokens.Count || tokens[pos].Value != "(")
                    throw new Exception("函数 " + funcName + " 缺少括号");
                pos++; // 跳过 (

                var args = new List<double>();
                args.Add(ParseExpression(tokens, ref pos));
                while (pos < tokens.Count && tokens[pos].Type == "COMMA")
                {
                    pos++; // 跳过逗号
                    args.Add(ParseExpression(tokens, ref pos));
                }

                if (pos >= tokens.Count || tokens[pos].Value != ")")
                    throw new Exception("函数 " + funcName + " 缺少右括号");
                pos++; // 跳过 )

                return ApplyFunction(funcName, args);
            }

            // 括号
            if (token.Type == "PAREN" && token.Value == "(")
            {
                pos++; // 跳过 (
                double result = ParseExpression(tokens, ref pos);
                if (pos >= tokens.Count || tokens[pos].Value != ")")
                    throw new Exception("缺少右括号");
                pos++; // 跳过 )
                return result;
            }

            throw new Exception("意外的符号: " + token.Value);
        }

        private static double ApplyFunction(string name, List<double> args)
        {
            switch (name)
            {
                case "sin": return Math.Sin(args[0] * Math.PI / 180.0);
                case "cos": return Math.Cos(args[0] * Math.PI / 180.0);
                case "tan": return Math.Tan(args[0] * Math.PI / 180.0);
                case "asin": return Math.Asin(args[0]) * 180.0 / Math.PI;
                case "acos": return Math.Acos(args[0]) * 180.0 / Math.PI;
                case "atan": return Math.Atan(args[0]) * 180.0 / Math.PI;
                case "sqrt": return Math.Sqrt(args[0]);
                case "cbrt": return Math.Pow(args[0], 1.0 / 3.0);
                case "log": return Math.Log10(args[0]);
                case "ln": return Math.Log(args[0]);
                case "abs": return Math.Abs(args[0]);
                case "exp": return Math.Exp(args[0]);
                default: throw new Exception("未知函数: " + name);
            }
        }
    }
}
