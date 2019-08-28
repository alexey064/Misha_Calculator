using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics;
using System.Diagnostics;

namespace Misha_Calculator.Additional
{
    /// <summary>
    /// stores methods for math expressions calculation
    /// </summary>
    class Counter
    {
        /// <summary>
        /// output postfixed polsky expression
        /// </summary>
        private List<string> _expression;
        /// <summary>
        /// priorities for the functions and arithmetical operations
        /// </summary>
        private Dictionary<string, short> _priorities;
        /// <summary>
        /// input infixed math expression
        /// </summary>
        private string _math;
        /// <summary>
        /// error flag indicates a wrong input
        /// </summary>
        public double _error = 0.123456789;
        /// <summary>
        /// calculation result
        /// </summary>
        private double _result = 0.0;

        #region checking functions

        /// <summary>
        /// checks if symbol is dot
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        private bool IsDot(char symbol)
        {
            return symbol == '.';
        }
        /// <summary>
        /// checkes if symbol is operation
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        private bool IsOperation(char symbol)
        {
            return (symbol == '(' || symbol == ')' || symbol == '+' ||
                symbol == '-' || symbol == '*' || symbol == '/' || symbol == '^');
        }
        /// <summary>
        /// checkes if symbol is pi
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        private bool IsPi(string symbol)
        {
            return symbol == "pi";
        }
        /// <summary>
        /// checkes if symbol is coma
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        private bool IsComa(char symbol)
        {
            return symbol == ',';
        }

        #endregion
        
        /// <summary>
        /// constructor with creation parameters of the class
        /// </summary>
        public Counter()
        {
            _priorities = new Dictionary<string, short>
            {
                { "(", 1 },
                { "+", 2 },
                { "-", 2 },
                { "*", 3 },
                { "/", 3 },
                { "^", 4 },
                { ")", 6 },
                { "exp", 5 },
                { "ln", 5 },
                { "lg", 5 },
                { "log", 5 },
                { "sin", 5 },
                { "cos", 5 },
                { "tg", 5 },
                { "arcsin", 5 },
                { "arccos", 5 },
                { "arctg", 5 },
                { "sh", 5 },
                { "ch", 5 },
                { "th", 5 },
                { "sqrt", 5 }
            };
            _expression = new List<string>();
        }

        /// <summary>
        /// returns a mathematical expression
        /// </summary>
        /// <returns></returns>
        public string MathExpression()
        {
            return _math;
        }

        #region solution functions

        /// <summary>
        /// convertes a math expression to polsky expression
        /// </summary>
        /// <param name="expr"></param>
        private void Conversion(string expr)
        {
            StringBuilder digits = new StringBuilder(30), 
                function = new StringBuilder(10);
            _math = expr;
            Stack<string> buffer = new Stack<string>();
            foreach (var element in _math)
            {
                if (char.IsDigit(element) || IsDot(element))
                {
                    if (function.Length > 0)
                    {
                        var func = function.ToString();
                        if (IsPi(func)) _expression.Add(func);
                        else if (_priorities[func] == 5) buffer.Push(func);
                        function.Clear();
                    }
                    digits.Append(element);
                }
                else if (char.IsLetter(element))
                {
                    if (digits.Length > 0)
                    {
                        var dig = digits.ToString();
                        _expression.Add(dig);
                        digits.Clear();
                    }
                    function.Append(element);
                }
                else if (IsOperation(element))
                {
                    var oper = new string(element, 1);
                    if (function.Length > 0)
                    {
                        var func = function.ToString();
                        if (IsPi(func)) _expression.Add(func);
                        else if (_priorities[func] == 5) buffer.Push(func);
                        function.Clear();
                    }
                    else if (digits.Length > 0)
                    {
                        var dig = digits.ToString();
                        _expression.Add(dig);
                        digits.Clear();
                    }
                    if (buffer.IsEmpty()) buffer.Push(oper);
                    else
                    {
                        if (_priorities[oper] == 6)
                        {
                            while (!buffer.IsEmpty() && _priorities[buffer.First()] > 1)
                                _expression.Add(buffer.Pop());
                            buffer.Pop();
                        }
                        else if (_priorities[oper] == 1) buffer.Push(oper);
                        else if (_priorities[oper] <= _priorities[buffer.First()])
                        {
                            while (!buffer.IsEmpty() &&
                                _priorities[oper] <= _priorities[buffer.First()])
                                _expression.Add(buffer.Pop());
                            buffer.Push(oper);
                        }
                        else buffer.Push(oper);
                    }
                }
                else if (IsComa(element))
                {
                    if (function.Length > 0)
                    {
                        var func = function.ToString();
                        if (IsPi(func)) _expression.Add(func);
                        else if (_priorities[func] == 5) buffer.Push(func);
                        function.Clear();
                    }
                    else if (digits.Length > 0)
                    {
                        var dig = digits.ToString();
                        _expression.Add(dig);
                        digits.Clear();
                    }
                }
            }
            if (digits.Length > 0) _expression.Add(digits.ToString());
            if (function.Length > 0) _expression.Add(function.ToString());
            while (!buffer.IsEmpty()) _expression.Add(buffer.Pop());
            foreach (var el in _expression)
            {
                Debug.Write(el + "|");
            }
        }

        /// <summary>
        /// calculates a result of the expression
        /// </summary>
        private void Calculation()
        { 
            Stack<double> stack = new Stack<double>();
            foreach (var element in _expression)
            {
                if (char.IsDigit(element.First())) stack.Push(Convert.ToDouble(element));
                else try
                    {
                        double a, b;
                        switch (element)
                        {
                            case "pi": stack.Push(Math.PI); break;
                            case "-": a = stack.Pop(); b = stack.Pop(); stack.Push(b - a); break;
                            case "+": a = stack.Pop(); b = stack.Pop(); stack.Push(b + a); break;
                            case "*": a = stack.Pop(); b = stack.Pop(); stack.Push(b * a); break;
                            case "/": a = stack.Pop(); b = stack.Pop(); stack.Push(b / a); break;
                            case "^": a = stack.Pop(); b = stack.Pop(); stack.Push(Math.Pow(b, a)); break;
                            case "!": a = stack.Pop(); stack.Push(SpecialFunctions.Gamma(a)); break;
                            case "sin": a = stack.Pop(); stack.Push(Math.Sin(a)); break;
                            case "cos": a = stack.Pop(); stack.Push(Math.Cos(a)); break;
                            case "tg": a = stack.Pop(); stack.Push(Math.Tan(a)); break;
                            case "arcsin": a = stack.Pop(); stack.Push(Math.Asin(a)); break;
                            case "arccos": a = stack.Pop(); stack.Push(Math.Acos(a)); break;
                            case "arctg": a = stack.Pop(); stack.Push(Math.Atan(a)); break;
                            case "sh": a = stack.Pop(); stack.Push(Math.Sinh(a)); break;
                            case "ch": a = stack.Pop(); stack.Push(Math.Cosh(a)); break;
                            case "th": a = stack.Pop(); stack.Push(Math.Tanh(a)); break;
                            case "ln": a = stack.Pop(); stack.Push(Math.Log(a)); break;
                            case "lg": a = stack.Pop(); stack.Push(Math.Log10(a)); break;
                            case "log": a = stack.Pop(); b = stack.Pop(); stack.Push(Math.Log(a)/Math.Log(b)); break;
                            case "exp": a = stack.Pop(); stack.Push(Math.Exp(a)); break;
                            case "sqrt": a = stack.Pop(); stack.Push(Math.Sqrt(a)); break;
                        }
                    }
                    catch (Exception ) { _result = _error; return; }
            }
            if (!stack.IsEmpty()) _result = stack.Pop();
            else _result = _error;
            stack.Clear();
        }

        /// <summary>
        /// calculates a result
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        public double Result(string expr)
        {
            Conversion(expr);
            Calculation();
            return _result;
        }


        #endregion
    }
}
