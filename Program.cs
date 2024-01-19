using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infix_conversion
{
    class Program
    {
        public static void PrintMenu()
        {
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("|                  Main menu                |");
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("1: Infix to Postfix Conversion");
            Console.WriteLine("2: Infix to Prefix Conversion");
            Console.WriteLine("3: Postfix to Infix Conversion");
            Console.WriteLine("4: Prefix to Infix Conversion");
            Console.WriteLine("5: Postfix to prefix Conversion");
            Console.WriteLine("6: Prefix to postfix Conversion");
            Console.WriteLine("7:Test");
            Console.WriteLine("8:Quit");
            Console.WriteLine("--------------------------------------------");
        }
        static void Main(string[] args)
        {
            try
            {
                while (true)
                {
                    PrintMenu();
                    string selection = Console.ReadLine();
                    int selectionNum = Convert.ToInt16(selection);
                    if (selectionNum >= 1 && selectionNum <= 7)
                    {
                        switch (selectionNum)
                        {
                            case 1:
                                Console.WriteLine("Enter Infix Expression");
                                string inf = Console.ReadLine();
                                Console.WriteLine("Postfix:" + Infix_To_Postfix(inf));
                                Console.WriteLine("--------------------------------------------");
                                break;
                            case 2:
                                Console.WriteLine("Enter Infix Expression");
                                string inf1 = Console.ReadLine();
                                Console.WriteLine("Prefix:" + Infix_To_Prefix(inf1));
                                Console.WriteLine("Prefix:" + Infix_To_Prefix(inf1));
                                Console.WriteLine("--------------------------------------------");
                                break;
                            case 3:
                                Console.WriteLine("Enter postfix Expression");
                                string psf = Console.ReadLine();
                                Console.WriteLine("Infix:" + Postfix_To_Infix(psf));
                                Console.WriteLine("--------------------------------------------");
                                break;
                            case 4:
                                Console.WriteLine("Enter pretfix Expression");
                                string prf = Console.ReadLine();
                                Console.WriteLine("Infix:" + Prefix_To_Infix(prf));
                                Console.WriteLine("--------------------------------------------");
                                break;
                            case 5:
                                Console.WriteLine("Enter postfix Expression");
                                string psf2 = Console.ReadLine();
                                string a = Postfix_To_Infix(psf2);
                                Console.WriteLine("Prefix:" + Infix_To_Prefix(a));
                                Console.WriteLine("--------------------------------------------");
                                break;
                            case 6:
                                Console.WriteLine("Enter pretfix Expression");
                                string prf2 = Console.ReadLine();
                                string b = Prefix_To_Infix(prf2);
                                Console.WriteLine(Infix_To_Postfix(b));
                                Console.WriteLine("--------------------------------------------");
                                break;
                            case 7:
                                string infix = "a*(b+c)*d";
                                string postfix = "abc+*d*";
                                Console.WriteLine(Infix_To_Postfix(infix));
                                Console.WriteLine(Infix_To_Prefix(infix));
                                Console.WriteLine(Postfix_To_Infix(postfix));
                                string pr = Postfix_To_Infix(postfix);
                                Console.WriteLine(Infix_To_Prefix(pr));
                                break;


                        }
                    }
                    else if (selectionNum == 8)
                        break;
                    else
                    {
                        Console.WriteLine("--------------------------------------------");
                        Console.WriteLine("|      Invalis selection, try again :(      |");
                        Console.WriteLine("--------------------------------------------");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();


            }


        }
        static int prio(char c)
        {
            switch (c)
            {
                case '+':
                case '-':
                    return 1;
                case '*':
                case '/':
                    return 2;
                case '^':
                    return 3;
            }
            return -1;
        }
        //Cheks if character is operator or not
        static bool isOperator(char x)
        {
            switch (x)
            {
                case '+':
                case '-':
                case '*':
                case '/':
                case '^':
                case '%':
                    return true;
            }
            return false;
        }
        //Infix to Postfix conversion
        public static string Infix_To_Postfix(string exp)
        {
            string postfix = "";
            Stack s = new Stack();
            for (int i = 0; i < exp.Length; ++i)
            {
                char c = exp[i];

                if (!isOperator(c))
                    postfix += c;

                else if (c == '(')
                    s.Push(Convert.ToString(c));

                else if (c == ')')
                {
                    while (s.top > 0 && s.Peek() != '(')
                        postfix += s.Pop();

                    if (s.top > 0 && s.Peek() != '(')
                        return "Invalid Expression";
                    else
                        s.Pop();
                }

                else
                {
                    while (s.top > 0 && prio(c) <= prio(s.Peek()))
                        postfix += s.Pop();
                    s.Push(Convert.ToString(c));
                }
            }

            // Pop all the operators from the stack
            while (s.top >= 0)
            {
                postfix += s.Pop();
            }
            return postfix;
        }
        //Infix to Prefix conversion
        public static string Infix_To_Prefix(string exp)
        {
            CStack operators = new CStack();

            Stack operands = new Stack();

            for (int i = 0; i < exp.Length; i++)
            {
                char c = exp[i];

                if (c == '(')
                {
                    operators.Push(c);
                }

                else if (c == ')')
                {
                    while (operators.NodeNumber != 0 && operators.Peek() != '(')
                    {

                        string opr1 = operands.Pop();

                        string opr2 = operands.Pop();

                        char opt = operators.Pop();

                        string tmp = opt + opr2 + opr1;
                        operands.Push(tmp);
                    }

                    operators.Pop();
                }

                else if (!isOperator(c))
                {
                    operands.Push(c + "");
                }

                else
                {
                    while (operators.NodeNumber != 0 && prio(c) <= prio(operators.Peek()))
                    {

                        string opr1 = operands.Pop();

                        string opr2 = operands.Pop();

                        char opt = operators.Pop();

                        string tmp = opt + opr2 + opr1;
                        operands.Push(tmp);
                    }

                    operators.Push(c);
                }
            }

            while (operators.NodeNumber != 0)
            {
                string opr1 = operands.Pop();

                string opr2 = operands.Pop();

                char opt = operators.Pop();

                string tmp = opt + opr2 + opr1;
                operands.Push(tmp);
            }

            return operands.peek();
        }
        public static string Postfix_To_Infix(string exp)
        {
            Stack s = new Stack();
            for (int i = 0; i < exp.Length; i++)
            {
                char c = exp[i];
                if (!isOperator(c))
                {
                    s.Push(c + "");
                }
                else
                {
                    string opr1 = s.Pop();
                    string opr2 = s.Pop();
                    string temp = "(" + opr2 + c + opr1 + ")";
                    s.Push(temp);
                }
            }
            return s.Pop();
        }
        public static string Prefix_To_Infix(string exp)
        {
            int l = exp.Length;
            Stack s = new Stack();
            for (int i = l - 1; i >= 0; i--)
            {
                char c = exp[i];
                if (isOperator(c))
                {
                    string opr1 = s.Pop();
                    string opr2 = s.Pop();
                    string temp = "(" + opr1 + c + opr2 + ")";
                    s.Push(temp);
                }
                else
                    s.Push(c + "");
            }
            return s.Pop();
        }
    }
    internal class Stack
    {
        static int Max = 1000;
        public int top;
        string[] stack = new string[Max];

        bool IsEmpty()
        {
            return (top < 0);
        }
        public Stack()
        {
            top = -1;
        }
        public void Push(string data)
        {
            if (top >= Max)
                Console.WriteLine("Stack is Full");
            else
                stack[++top] = data;
        }
        public string Pop()
        {
            if (top < 0)
            {
                Console.WriteLine("Stack is Emptys");
                return "0";
            }
            else
            {
                string value = stack[top--];
                return value;
            }
        }
        public void PrintStack()
        {
            if (top < 0)
            {
                Console.WriteLine("Stack Underflow");
                return;
            }
            else
            {
                Console.WriteLine("Items in the Stack are :");
                for (int i = top; i >= 0; i--)
                {
                    Console.WriteLine(stack[i]);
                }
            }
        }
        public char Peek()
        {
            if (top < 0)
            {
                Console.WriteLine("Stack is Empty");
                return '0';
            }
            else
                return Convert.ToChar(stack[top]);
        }
        public string peek()
        {
            if (top < 0)
            {
                Console.WriteLine("Stack Underflow");
                return "0";
            }
            else
                return stack[top];
        }
    }
    internal class CStack
    {
        static int Max = 1000;
        public int top;
        public int NodeNumber;
        char[] stack = new char[Max];

        bool IsEmpty()
        {
            return (top < 0);
        }
        public CStack()
        {
            top = -1;
        }
        public void Push(char data)
        {
            if (top >= Max)
                Console.WriteLine("Stack is Full");
            else
                stack[++top] = data;
            NodeNumber++;
        }
        public char Pop()
        {
            if (top < 0)
            {
                Console.WriteLine("Stack is Emptys");
                return '0';
            }
            else
            {
                char value = stack[top--];
                NodeNumber--;
                return value;
            }
        }
        public void PrintStack()
        {
            if (top < 0)
            {
                Console.WriteLine("Stack Underflow");
                return;
            }
            else
            {
                Console.WriteLine("Items in the Stack are :");
                for (int i = top; i >= 0; i--)
                {
                    Console.WriteLine(stack[i]);
                }
            }
        }
        public char Peek()
        {
            if (top < 0)
            {
                Console.WriteLine("Stack Underflow");
                return '0';
            }
            else
                return Convert.ToChar(stack[top]);
        }
    }
}
