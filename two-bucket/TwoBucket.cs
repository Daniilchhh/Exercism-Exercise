using System;
using System.Collections.Generic;
public enum Bucket
{
    One,
    Two
}
public class TwoBucketResult
{
    public int Moves { get; set; }
    public Bucket GoalBucket { get; set; }
    public int OtherBucket { get; set; }
}
public class TwoBucket
{
    private readonly int _capOne;
    private readonly int _capTwo;
    private readonly Bucket _startBucket;
    public TwoBucket(int bucketOne, int bucketTwo, Bucket startBucket)
    {
        _capOne = bucketOne;
        _capTwo = bucketTwo;
        _startBucket = startBucket;
    }
    public TwoBucketResult Measure(int goal)
    {
        var start = _startBucket == Bucket.One ? (_capOne, 0) : (0, _capTwo);
        var moves = new Dictionary<(int, int), int> { [start] = 1 };
        var queue = new Queue<(int, int)>();
        queue.Enqueue(start);
        while (queue.Count > 0)
        {
            var state = queue.Dequeue();
            int a = state.Item1;
            int b = state.Item2;
            int count = moves[state];
            if (a == goal)
                return new TwoBucketResult { Moves = count, GoalBucket = Bucket.One, OtherBucket = b };
            if (b == goal)
                return new TwoBucketResult { Moves = count, GoalBucket = Bucket.Two, OtherBucket = a };
            int pourOneToTwo = Math.Min(a, _capTwo - b);
            int pourTwoToOne = Math.Min(b, _capOne - a);
            var next = new (int, int)[]
            {
                (_capOne, b),
                (a, _capTwo),
                (0, b),
                (a, 0),
                (a - pourOneToTwo, b + pourOneToTwo),
                (a + pourTwoToOne, b - pourTwoToOne)
            };
            foreach (var n in next)
            {
                if (IsForbidden(n) || moves.ContainsKey(n))
                    continue;
                moves[n] = count + 1;
                queue.Enqueue(n);
            }
        }
        throw new ArgumentException("Goal cannot be reached");
    }
    private bool IsForbidden((int, int) state)
    {
        return _startBucket == Bucket.One
            ? state.Item1 == 0 && state.Item2 == _capTwo
            : state.Item2 == 0 && state.Item1 == _capOne;
    }
}
