using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Для завершения работы введите exit");
            string expression;
            do
            {
                Console.WriteLine("Введите выражение(пример: \"2 + 3 * 5\"): ");
                expression = Console.ReadLine();
                try
                {
                    Console.WriteLine("{0} = {1}", expression, Calc(expression));
                }
                catch
                {
                    Console.WriteLine("Ошибка рассчёта");
                }
            }
            while (expression != "exit");
        }

       
        static double Calc(string mathExpression)
        {
            var parts = mathExpression.Split(' '); 
            var operands = new List<double>();
            var operations = new List<string>();
            for (var i = 0; i < parts.Length; i += 2)
            {
                operands.Add(Convert.ToDouble(parts[i]));
                if (i + 1 < parts.Length)
                {
                    operations.Add(parts[i + 1]);
                }
            }
            Calculate(operands, operations, "*", (a, b) => a * b, "/", (a, b) => a / b);
            Calculate(operands, operations, "+", (a, b) => a + b, "-", (a, b) => a - b);

            return operands[0]; 
        }

   
        static void Calculate(List<double> operands, List<string> operations,
            string currentOperation1, Func<double, double, double> function1,
            string currentOperation2, Func<double, double, double> function2)
        {
            while (true)
            {
                var i1 = operations.IndexOf(currentOperation1);
                var i2 = operations.IndexOf(currentOperation2);
                int index; 
                if (i1 >= 0 && i2 >= 0)
                {
                    index = Math.Min(i1, i2);
                }
                else
                {
                    index = Math.Max(i1, i2);
                }

                if (index < 0)
                {
                    break;
                }

                if (index == i1)
                {
                    operands[index] = function1(operands[index], operands[index + 1]);
                }
                else
                {
                    operands[index] = function2(operands[index], operands[index + 1]);
                }
                operations.RemoveAt(index);
                operands.RemoveAt(index + 1);
            }
        }
    }
}

