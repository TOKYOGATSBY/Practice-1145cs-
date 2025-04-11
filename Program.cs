using System;
using System.Collections.Generic;

class Program
{
    static int n, m;
    static List<string> maze = new List<string>();
    static int[,] dist;
    static readonly int[] dx = { 1, -1, 0, 0 };
    static readonly int[] dy = { 0, 0, 1, -1 };

    static bool InBounds(int x, int y)
    {
        return x >= 0 && x < m && y >= 0 && y < n;
    }

    static Tuple<int, Tuple<int, int>> Bfs(int sx, int sy)
    {
        dist = new int[m, n];
        for (int i = 0; i < m; ++i)
            for (int j = 0; j < n; ++j)
                dist[i, j] = -1;

        Queue<Tuple<int, int>> q = new Queue<Tuple<int, int>>();
        q.Enqueue(Tuple.Create(sx, sy));
        dist[sx, sy] = 0;

        var res = Tuple.Create(0, Tuple.Create(sx, sy));

        while (q.Count > 0)
        {
            var (x, y) = q.Dequeue();
            for (int dir = 0; dir < 4; ++dir)
            {
                int nx = x + dx[dir];
                int ny = y + dy[dir];
                if (InBounds(nx, ny) && maze[nx][ny] == '.' && dist[nx, ny] == -1)
                {
                    dist[nx, ny] = dist[x, y] + 1;
                    q.Enqueue(Tuple.Create(nx, ny));
                    if (dist[nx, ny] > res.Item1)
                    {
                        res = Tuple.Create(dist[nx, ny], Tuple.Create(nx, ny));
                    }
                }
            }
        }

        return res;
    }

    static void Main()
    {
        var tokens = Console.ReadLine().Split();
        n = int.Parse(tokens[0]);
        m = int.Parse(tokens[1]);

        for (int i = 0; i < m; ++i)
        {
            maze.Add(Console.ReadLine());
        }

        int sx = -1, sy = -1;
        for (int i = 0; i < m && sx == -1; ++i)
        {
            for (int j = 0; j < n && sy == -1; ++j)
            {
                if (maze[i][j] == '.')
                {
                    sx = i;
                    sy = j;
                }
            }
        }

        var first = Bfs(sx, sy);
        var result = Bfs(first.Item2.Item1, first.Item2.Item2);

        Console.WriteLine(result.Item1);
    }
}
