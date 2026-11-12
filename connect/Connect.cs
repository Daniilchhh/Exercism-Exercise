using System.Collections.Generic;
public enum ConnectWinner
{
    White,
    Black,
    None
}
public class Connect
{
    private readonly char[][] _board;
    public Connect(string[] input)
    {
        _board = new char[input.Length][];
        for (int r = 0; r < input.Length; r++)
            _board[r] = input[r].Replace(" ", "").ToCharArray();
    }
    public ConnectWinner Result()
    {
        if (HasPath('O', fromTop: true))
            return ConnectWinner.White;
        if (HasPath('X', fromTop: false))
            return ConnectWinner.Black;
        return ConnectWinner.None;
    }
    private bool HasPath(char player, bool fromTop)
    {
        int rows = _board.Length;
        var visited = new HashSet<(int, int)>();
        var queue = new Queue<(int, int)>();
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < _board[r].Length; c++)
            {
                bool isStart = fromTop ? r == 0 : c == 0;
                if (isStart && _board[r][c] == player)
                {
                    visited.Add((r, c));
                    queue.Enqueue((r, c));
                }
            }
        }
        while (queue.Count > 0)
        {
            var (r, c) = queue.Dequeue();
            bool isGoal = fromTop ? r == rows - 1 : c == _board[r].Length - 1;
            if (isGoal)
                return true;
            var neighbours = new (int, int)[]
            {
                (r, c - 1), (r, c + 1),
                (r - 1, c), (r - 1, c + 1),
                (r + 1, c - 1), (r + 1, c)
            };
            foreach (var (nr, nc) in neighbours)
            {
                if (nr < 0 || nr >= rows) continue;
                if (nc < 0 || nc >= _board[nr].Length) continue;
                if (_board[nr][nc] != player) continue;
                if (visited.Add((nr, nc)))
                    queue.Enqueue((nr, nc));
            }
        }
        return false;
    }
}
