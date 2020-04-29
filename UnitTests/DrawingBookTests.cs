using Solutions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SolutionTests
{
    public class DrawingBookTests
    {
        [Theory]
        [MemberData(nameof(Data))]
        public void DrawingBookTest(int pages, int page, int flips)
        {
            Assert.Equal(flips, DrawingBook.PageCount(pages, page));
        }

        public static IEnumerable<object[]> Data = new List<object[]>()
        {
            new object[] { 6, 2, 1 },
            new object[] { 5, 4, 0 },
        };

    }
}
