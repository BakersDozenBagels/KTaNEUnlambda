using System;
using UnlambdaLib;
using System.Linq;

public class ULInterface
{
    public static void Main()
    {
        Console.WriteLine("Max iterations?");
        int iters = int.Parse(Console.ReadLine());
        Console.WriteLine("Number of k and s?");
        int count = int.Parse(Console.ReadLine());
        string code = Generate(count, false);
        Console.WriteLine(code);
        Console.ReadKey();
        Console.WriteLine(Check(code, iters));
        Console.ReadKey();
    }

    #region Public Methods
    /// <summary>
    /// Runs a given program and returns its output.
    /// </summary>
    /// <param name="program">Program to run</param>
    /// <param name="iters">How many applies to allow</param>
    /// <returns></returns>
    public static string Check(string program, int iters)
    {
        return Interpreter.Interpret(program, iters).ToString();
    }

    /// <summary>
    /// Generates random code of a given length.
    /// </summary>
    /// <param name="symbols">The amount of code to generate, excluding `</param>
    /// <param name="prepend">True if this code goes before other code</param>
    /// <returns>Returns a string of code</returns>
    public static string Generate(int count, bool prepend)
    {
        if (count < 2 && !prepend || count < 1 && prepend) throw new ArgumentException("Count must be at least 2. (1 in prepend mode)");
        int slots = 2;
        CodeTree tree = new CodeTree("`");
        while (slots < count)
        {
            tree.FillRandom(new CodeTree("`"));
            slots++;
        }
        if (prepend)
        {
            tree.FillLast(new Leaf(""));
            slots--;
        }
        for (; slots > 0; slots--)
        {
            tree.FillLast(new Random().Next() % 2 == 0 ? new Leaf("k") : new Leaf("s"));
        }
        return tree.ToString();
    }
    #endregion

    #region Private Classes
    private class CodeTree
    {
        private string N { set; get; }
        private CodeTree child1;
        private CodeTree child2;

        public CodeTree(string name)
        {
            N = name;
        }

        public override string ToString()
        {
            return N + child1.ToString() + child2.ToString();
        }

        public virtual bool FillRandom(CodeTree toAdd)
        {
            if (new Random().Next() % 2 == 0)
            {
                if (child1 != null)
                {
                    if (child1.FillRandom(toAdd))
                    {
                        return true;
                    }
                    else
                    {
                        if (child2.FillRandom(toAdd))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    child1 = toAdd;
                    return true;
                }
            }
            else
            {
                if (child2 != null)
                {
                    if (child2.FillRandom(toAdd))
                    {
                        return true;
                    }
                    else
                    {
                        if (child1.FillRandom(toAdd))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    child2 = toAdd;
                    return true;
                }
            }
        }

        public virtual bool FillLast(CodeTree toAdd)
        {
            if (child2 == null)
            {
                child2 = toAdd;
                return true;
            }
            else if (child2.FillLast(toAdd))
            {
                return true;
            }
            else if (child1 == null)
            {
                child1 = toAdd;
                return true;
            }
            else if (child1.FillLast(toAdd))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    private class Leaf : CodeTree
    {
        private string L { get; set; }

        public Leaf(string name) : base(name)
        {
            L = name;
        }

        public override string ToString()
        {
            return L;
        }

        public override bool FillRandom(CodeTree toAdd)
        {
            return false;
        }

        public override bool FillLast(CodeTree toAdd)
        {
            return false;
        }
    }
    #endregion

}
