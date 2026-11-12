using System.Collections.Generic;
using System.Linq;
public static class Dominoes
{
    public static bool CanChain(IEnumerable<(int, int)> dominoes)
    {
        var list = dominoes.ToList();
        if (list.Count == 0)
            return true;
        var first = list[0];
        list.RemoveAt(0);
        return Search(first.Item1, first.Item2, list);
    }
    private static bool Search(int start, int end, List<(int, int)> remaining)
    {
        if (remaining.Count == 0)
            return start == end;
        for (int i = 0; i < remaining.Count; i++)
        {
            var d = remaining[i];
            int next;
            if (d.Item1 == end)
                next = d.Item2;
            else if (d.Item2 == end)
                next = d.Item1;
            else
                continue;
            var rest = new List<(int, int)>(remaining);
            rest.RemoveAt(i);
            if (Search(start, next, rest))
                return true;
        }
        return false;
    }
}
