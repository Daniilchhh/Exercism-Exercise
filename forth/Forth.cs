using System;
using System.Collections.Generic;
using System.Linq;
public static class Forth
{
    private static readonly HashSet<string> Builtins =
        new HashSet<string> { "+", "-", "*", "/", "dup", "drop", "swap", "over" };
    public static string Evaluate(string[] instructions)
    {
        var stack = new List<int>();
        var words = new Dictionary<string, List<string>>();
        foreach (var line in instructions)
        {
            var tokens = line.ToLowerInvariant()
                             .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                             .ToList();
            if (tokens.Count == 0)
                continue;
            if (tokens[0] == ":")
            {
                Define(tokens, words);
            }
            else
            {
                foreach (var token in tokens)
                    Run(token, stack, words);
            }
        }
        return string.Join(" ", stack);
    }
    private static void Define(List<string> tokens, Dictionary<string, List<string>> words)
    {
        if (tokens.Count < 3 || tokens[tokens.Count - 1] != ";")
            throw new InvalidOperationException("Invalid definition");
        string name = tokens[1];
        if (IsNumber(name))
            throw new InvalidOperationException("Cannot redefine a number");
        var body = new List<string>();
        for (int i = 2; i < tokens.Count - 1; i++)
        {
            string t = tokens[i];
            if (words.TryGetValue(t, out var expansion))
                body.AddRange(expansion);
            else if (IsNumber(t) || Builtins.Contains(t))
                body.Add(t);
            else
                throw new InvalidOperationException("Unknown word: " + t);
        }
        words[name] = body;
    }
    private static void Run(string token, List<int> stack, Dictionary<string, List<string>> words)
    {
        if (words.TryGetValue(token, out var expansion))
        {
            foreach (var t in expansion)
                Exec(t, stack);
        }
        else
        {
            Exec(token, stack);
        }
    }
    private static void Exec(string token, List<int> stack)
    {
        if (int.TryParse(token, out int number))
        {
            stack.Add(number);
            return;
        }
        switch (token)
        {
            case "+":
            {
                int b = Pop(stack), a = Pop(stack);
                stack.Add(a + b);
                break;
            }
            case "-":
            {
                int b = Pop(stack), a = Pop(stack);
                stack.Add(a - b);
                break;
            }
            case "*":
            {
                int b = Pop(stack), a = Pop(stack);
                stack.Add(a * b);
                break;
            }
            case "/":
{
    int b = Pop(stack), a = Pop(stack);
    if (b == 0)
        throw new DivideByZeroException("Division by zero");
    stack.Add(a / b);
    break;
}
            case "dup":
            {
                int a = Pop(stack);
                stack.Add(a);
                stack.Add(a);
                break;
            }
            case "drop":
                Pop(stack);
                break;
            case "swap":
            {
                int b = Pop(stack), a = Pop(stack);
                stack.Add(b);
                stack.Add(a);
                break;
            }
            case "over":
            {
                int b = Pop(stack), a = Pop(stack);
                stack.Add(a);
                stack.Add(b);
                stack.Add(a);
                break;
            }
            default:
                throw new InvalidOperationException("Unknown word: " + token);
        }
    }
    private static int Pop(List<int> stack)
    {
        if (stack.Count == 0)
            throw new InvalidOperationException("Stack is empty");
        int value = stack[stack.Count - 1];
        stack.RemoveAt(stack.Count - 1);
        return value;
    }
    private static bool IsNumber(string s) => int.TryParse(s, out _);
}
