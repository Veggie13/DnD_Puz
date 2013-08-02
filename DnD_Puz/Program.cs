using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DnD_Puz
{
    class Program
    {
        class V
        {
            public int R, C, D, D2;
            public V(int r, int c, int d, int d2)
            {
                R = r; C = c; D = d; D2 = d2;
            }
            public override bool Equals(object obj)
            {
                V o = obj as V;
                return R == o.R && C == o.C;
            }
        }
        static void Maker(int[,] grid, int rr, int cc, int dd, int inc)
        {
            switch (dd)
            {
                default:
                    return;
                case 0:
                    {
                        int r = rr, c = cc;
                        for (; r > 2 && c < 5 - Math.Abs(r - 2); r--, c++)
                            grid[r, c] += inc;
                        for (; r >= 0 && c < 5 - Math.Abs(r - 2); r--)
                            grid[r, c] += inc;
                        break;
                    }
                case 1:
                    for (int c = cc; c < 5 - Math.Abs(rr - 2); c++)
                        grid[rr, c] += inc;
                    break;
                case 2:
                    {
                        int r = rr, c = cc;
                        for (; r < 2 && c < 5 - Math.Abs(r - 2); r++, c++)
                            grid[r, c] += inc;
                        for (; r < 5 && c < 5 - Math.Abs(r - 2); r++)
                            grid[r, c] += inc;
                        break;
                    }
                case 3:
                    {
                        int r = rr, c = cc;
                        for (; r < 2 && c >= 0; r++)
                            grid[r, c] += inc;
                        for (; r < 5 && c >= 0; r++, c--)
                            grid[r, c] += inc;
                        break;
                    }
                case 4:
                    for (int c = cc; c >= 0; c--)
                        grid[rr, c] += inc;
                    break;
                case 5:
                    {
                        int r = rr, c = cc;
                        for (; r > 2 && c >= 0; r--)
                            grid[r, c] += inc;
                        for (; r >= 0 && c >= 0; r--, c--)
                            grid[r, c] += inc;
                        break;
                    }
            }
        }
        static void Main(string[] args)
        {
            List<V> pts = new List<V>();
            Random rnd = new Random();
            for (int n = 0; n < 6; n++)
            {
                int rr = (int)(rnd.NextDouble() * 5);
                int cc = (int)(rnd.NextDouble() * (5 - Math.Abs(rr - 2)));
                int dd;
                while (true)
                {
                    dd = (int)(rnd.NextDouble() * 6);
                    if (cc == 0 && rr <= 2 && dd == 5)
                        continue;
                    if (cc == 0 && rr >= 2 && dd == 3)
                        continue;
                    if (cc == 0 && dd == 4)
                        continue;
                    if (cc == (4 - Math.Abs(rr - 2)))
                    {
                        if (dd == 1)
                            continue;
                        if (rr <= 2 && dd == 0)
                            continue;
                        if (rr >= 2 && dd == 2)
                            continue;
                    }
                    if (rr == 0 && (dd == 0 || dd == 5))
                        continue;
                    if (rr == 4 && (dd == 2 || dd == 3))
                        continue;
                    break;
                }
                int dd2;
                do
                {
                    dd2 = (int)(rnd.NextDouble() * 6);
                } while (dd2 == dd);
                var nxt = new V(rr, cc, dd, dd2);
                if (pts.Contains(nxt))
                {
                    n--;
                    continue;
                }
                pts.Add(nxt);
                Console.WriteLine("{0}, {1}, {2}, {3}", rr, cc, dd, dd2);
            }

            int[,] grid = new int[5, 5], grid2 = new int[5, 5];
            foreach (V v in pts)
            {
                Maker(grid, v.R, v.C, v.D, -1);
                Maker(grid, v.R, v.C, v.D2, 1);
            }

            string[] tr = { " ", "A", "B", "C", "D", "E", "F" };
            for (int r = 0; r < 5; r++)
            {
                for (int c = 0; c < 5 - Math.Abs(r - 2); c++)
                {
                    Console.Write((grid[r, c] < 0 ? tr[-grid[r, c]] : grid[r, c].ToString()) + " ");
                }
                Console.WriteLine("");
            }
            Console.ReadLine();
        }
    }
}
