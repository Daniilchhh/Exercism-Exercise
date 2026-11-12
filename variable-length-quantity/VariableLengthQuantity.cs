using System;
using System.Collections.Generic;
public static class VariableLengthQuantity
{
    public static uint[] Encode(uint[] numbers)
    {
        var result = new List<uint>();
        foreach (var number in numbers)
        {
            var chunk = new Stack<uint>();
            var n = number;
            chunk.Push(n & 0x7F);
            n >>= 7;
            while (n > 0)
            {
                chunk.Push((n & 0x7F) | 0x80);
                n >>= 7;
            }
            result.AddRange(chunk);
        }
        return result.ToArray();
    }
    public static uint[] Decode(uint[] bytes)
    {
        var result = new List<uint>();
        uint current = 0;
        bool incomplete = false;
        foreach (var b in bytes)
        {
            current = (current << 7) | (b & 0x7F);
            if ((b & 0x80) != 0)
            {
                incomplete = true;
            }
            else
            {
                result.Add(current);
                current = 0;
                incomplete = false;
            }
        }
        if (incomplete)
                        throw new InvalidOperationException("Incomplete byte sequence");
        return result.ToArray();
    }
}
