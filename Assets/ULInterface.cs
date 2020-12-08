using UnityEngine;
using UnlambdaLib;

public class ULInterface : MonoBehaviour {
    /// <summary>
    /// Checks if a given program produces a given output.
    /// </summary>
    /// <param name="program">Program to run</param>
    /// <param name="output">String to check</param>
    /// <param name="iters">How many applies to allow</param>
    /// <returns></returns>
    public bool Check(string program, string output, int iters)
    {
        string result = Interpreter.Interpret(program, iters).ToString();
        return result == output;
    }
}
