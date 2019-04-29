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
        public static int Precedence(char chr)
        {
            if (chr == '^')                         //Power have highest precedence
                return 3;
            else if (chr == '*' || chr == '/')
                return 2;
            else if (chr == '+' || chr == '-')
                return 1;
            else
                return -1;                          
        }


        //This function converts the infix expression into postfix expression
        public static string Infix_To_Postfix()
        {
            char chr;                               //chr holds the i'th character of string/expression
            int brace_conut = 0;                    //This counts the occurance of '(' & ')', if it is even the expression is 'valid' else 'invalid'
            string postfix = "", expression;
            Stack<char> stack = new Stack<char>();  //Initialize the stack

            Console.Write("Enter an mathematical expression : ");
            expression = Console.ReadLine();        //Get the mathematical expression from user
            expression = "(" + expression + ")";    //here we enclose the expression braces

            for (int i = 0; i < expression.Length; i++)         //This loop run till length of string/expression
            {
                chr = expression[i];                            //i'th character of string is assigned to 'chr '

                if (char.IsLetterOrDigit(chr))                  //If 'chr' is letter or digit put it into stack
                    postfix += chr;

                else if (chr == '(')                            //Push if chr is '(', we are pushing this cause if in future chr= ')' appears 
                {
                    stack.Push(chr);                                //We should know were to stop Poping from stack
                    brace_conut++;
                }
                else if (chr == ')')                            //If chr = ')' we Pop the values from stack until '(' this appears
                {
                    while (stack.Count > 0 && stack.Peek() != '(')
                        postfix += stack.Pop();                 //Here we add operators to the postfix expression
                    if (stack.Count > 0)
                        stack.Pop();                            //This extra pop is for '(', cause we don't want '(' in postfix expression
                    brace_conut++;
                }
                else
                {
                    if (Precedence(chr) > Precedence(stack.Peek())) //If Precedence of chr is greater than the precendence of last added Operator then push
                        stack.Push(chr);
                    else                                            //If Precedence of chr is less than the precendence of last added Operator then 
                    {                                               //pop untill precedence of chr is greater than stack top operator/element
                        while (Precedence(chr) <= Precedence(stack.Peek()))
                            postfix += stack.Pop();                 //Here we add operators to the postfix expression
                        stack.Push(chr);
                    }
                }
            }
            if (brace_conut % 2 != 0)
            {
                postfix = "";
                Console.WriteLine("\n\nInvalid Expression!!!\n\n");
            }
            return postfix;                                         //Return Postfix expression
        }

        //This function finds the value of postfix expression
        public static double Postfix_To_Value(string s)
        {                                                           //Here the logic is, we put the numbers in stack and when encounter with any operator 
            char chr;                                                //Pop stack two times do respective Operation and push the result back into stack
            double result = 0, first_Operand, second_Operand=0;       
            Stack<string> stack = new Stack<string>();              
            for (int i = 0; i < s.Length; i++)                      
            {                                                       
                chr = s[i];                                         //Here chr contains the i'th character of string
                if (char.IsLetterOrDigit(chr))                      //if chr is letter of digit we put this into stack
                {
                    stack.Push(chr.ToString());
                }
                else
                {
                    first_Operand = double.Parse(stack.Pop());      //first_Operand contains the first Poped 
                    if(stack.Count>0)
                        second_Operand = double.Parse(stack.Pop());     //second_Operand contains the second Poped 

                    if (chr == '+')                            //If Operator is + do addition
                    {
                        result = second_Operand + first_Operand;
                    }
                                                                   
                    else if (chr == '-')                           //If Operator is - do substraction
                    {                                              
                        result = second_Operand - first_Operand;   
                    }                                              
                                                                   
                    else if (chr == '*')                           //If Operator is * do multiplication
                    {                                              
                        result = second_Operand * first_Operand;   
                    }                                              
                                                                   
                    else if (chr == '/')                           //If Operator is / do division
                    {                                              
                        result = second_Operand / first_Operand;   
                    }                                              
                                                                   
                    else if (chr == '^')                          
                    {                                              
                        result = Math.Pow(second_Operand, first_Operand);
                    }

                    stack.Push(result.ToString());                 //Put the result back in stack
                }
            }
            return result;                                         //Return result
        }

        static void Main(string[] args)
        {
            string expression;
            double result;
            while (true)
            {
                expression = Infix_To_Postfix();
                result = Postfix_To_Value(expression);
                Console.WriteLine("Postfix : " + expression);
                Console.WriteLine("Value   : " + result);
                Console.ReadLine();
            }
            
        }
    }
}
