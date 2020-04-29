
using System;
using System.Collections.Generic;
using System.Text;

namespace Solutions
{
    /// <summary>
    /// Solution for drawing book question on hackerrank
    /// https://www.hackerrank.com/challenges/drawing-book/problem
    /// </summary>
    public static class DrawingBook
    {
        public static int PageCount(int n, int p)
        {
            return 2 * (p / 2) > n / 2 ? (n / 2) - (p / 2) : p / 2;
        }
    }
}
