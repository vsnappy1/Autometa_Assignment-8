using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToCS_Assignment_8
{
    /***************************************************************************************************************************************
     * Objective:
     *          This program converts the given arithmetic expression into post-fix notation and evaluate it.
     **************************************************************************************************************************************/
    class Program
    {

        //This function specify the precendence of operators
        public static int Precedence(string chr)
        {
            if (chr == "^")                         //Power have highest precedence
                return 3;
            else if (chr == "*" || chr == "/")
                return 2;
            else if (chr == "+" || chr == "-")
                return 1;
            else
                return -1;                          
        }

        //This prints the array of string
        public static string print_array(string[] array)
        {
            string postfix = "";
            for (int i = 0; i < array.Length; i++)
            {
                postfix+= array[i];
            }
            return postfix;
        }

        //This counts the number of operators in an expression
        public static int operation_count(string str)
        {
            int count=0;
            char chr;
            for (int i = 0; i < str.Length; i++)
            {
                chr = str[i];
                if (chr == '+' || chr == '-' || chr == '*' || chr == '/' || chr == '^')
                    count++;
            }
            return count;
        }

        //This function converts the infix expression into postfix expression
        public static string Infix_To_Postfix()
        {
            char chr;                                   //chr holds the i'th character of string/expression
            int i, j;
            string postfix = "", expression, temp;
            Stack<string> stack = new Stack<string>();  //Initialize the stack
            string[] postfix_array;

            Console.Write("Enter an mathematical expression : ");
            expression = Console.ReadLine();        //Get the mathematical expression from user
            expression = "(" + expression + ")";    //here we enclose the expression braces

            postfix_array = new string[2*operation_count(expression)+1+1];   //because if an expression have 'n' operators than it has max n+1 terms, and in total 2n+1 values to store in array

            string term = "";
            for (i = 0 ,j = 0; i < expression.Length; i++)         //This loop run till length of string/expression
            {
                chr = expression[i];                            //i'th character of string is assigned to 'chr '

                if (char.IsLetterOrDigit(chr))                  //If 'chr' is letter or digit put it into stack
                {
                    while (char.IsLetterOrDigit(expression[i]))
                    {
                        term += expression[i];
                        i++;
                    }
                    j++;
                    postfix += term;
                    postfix_array[j] = term;
                    term = "";
                    i--;
                }
                else if (chr == '(')                            //Push if chr is '(', we are pushing this cause if in future chr= ')' appears 
                {
                    stack.Push(chr.ToString());                 //We should know were to stop Poping from stack
                }
                else if (chr == ')')                            //If chr = ')' we Pop the values from stack until '(' this appears
                {
                    while (stack.Count > 0 && stack.Peek() != "(")
                    {
                        j++;
                        temp = stack.Pop();                 //Here we add operators to the postfix expression
                        postfix += temp;
                        postfix_array[j] = temp;
                    }
                        if (stack.Count > 0)
                        stack.Pop();                            //This extra pop is for '(', cause we don't want '(' in postfix expression
                }
                else
                {
                    if (Precedence(chr.ToString()) > Precedence(stack.Peek())) //If Precedence of chr is greater than the precendence of last added Operator then push
                    {
                        stack.Push(chr.ToString());
                    }
                    else                                            //If Precedence of chr is less than the precendence of last added Operator then 
                    {                                               //pop untill precedence of chr is greater than stack top operator/element
                        while (Precedence(chr.ToString()) <= Precedence(stack.Peek()))
                        {
                            j++;
                            temp = stack.Pop();                 
                            postfix += temp;                        //Here we add operators to the postfix expression
                            postfix_array[j] = temp;
                        }
                            stack.Push(chr.ToString());
                    }
                }
            }
            Console.WriteLine("Postfix: " + print_array(postfix_array));
            Console.WriteLine("Postfix Value : " + Postfix_To_Value(postfix_array) +" \n");

            return postfix;                                         //Return Postfix expression
        }

        //This evaluate the value of post fix expression
        public static double Postfix_To_Value(string [] postfix)
       {                                                           //Here the logic is, we put the numbers in stack and when encounter with any operator... 
           string operation;                                        //Pop stack two times do respective Operation and push the result back into stack
           double result = 0, first_Operand, second_Operand=0;       
           Stack<double> stack = new Stack<double>();

            for (int i = 1; i <postfix.Length; i++)
            {
                operation = postfix[i];
                if (operation == "+" || operation == "-" || operation == "*" || operation == "/" || operation == "^") 
                {
                    first_Operand = stack.Pop();
                    second_Operand = stack.Pop();

                    if (operation == "+")                                 //If Operator is + do addition
                        result = second_Operand + first_Operand;

                    else if (operation == "-")                            //If Operator is - do substraction
                        result = second_Operand - first_Operand;

                    else if (operation == "*")                            //If Operator is * do multiplication
                        result = second_Operand * first_Operand;

                    else if (operation == "/")                            //If Operator is / do division
                        result = second_Operand / first_Operand;

                    else if (operation == "^")
                        result = Math.Pow(second_Operand , first_Operand);

                    stack.Push(result);                                   //Here we put the result back into stack

                }
                else
                {
                    stack.Push(double.Parse(postfix[i]));                 //This pushes the numbers in stack
                }

            }

           return result;                                         //Return result
       }

        static void Main(string[] args)
        {
            while (true)
            {
              Infix_To_Postfix();
            }
            
        }
    }
}
