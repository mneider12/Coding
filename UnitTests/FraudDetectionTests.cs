using Solutions;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace UnitTests
{
    /// <summary>
    /// Tests for Fraudulent Activity Notification challenge on Hacker Rank
    /// https://www.hackerrank.com/challenges/fraudulent-activity-notifications/problem?h_l=interview&playlist_slugs%5B%5D=interview-preparation-kit&playlist_slugs%5B%5D=sorting
    /// </summary>
    public class FraudDetectionTests
    {
        [Theory]
        [MemberData(nameof(Data))]
        public void FraudDetectionTest(int d, int[] expenditure, int expected)
        {
            int result = FraudDetection.ActivityNotifications(expenditure, d);

            Assert.Equal(expected, result);
        }

        public static IEnumerable<object[]> Data = new List<object[]>
        {
            new object[] { 5, new int[] { 2, 3, 4, 2, 3, 6, 8, 4, 5 }, 2 },
            new object[] { 4, new int[] { 1, 2, 3, 4, 4 }, 0 },
            new object[] { 3, new int[] { 10, 20, 30, 40, 50 }, 1 },
            new object[] { 1, new int[] { 1, 2, 3, 4, 5 }, 1 },
            LoadData("FraudDetectionLargeTest1.txt"),
            LoadData("FraudDetectionLargeTest2.txt"),
        };

        private static object[] LoadData(string filePath)
        {
            using StreamReader file = new StreamReader(filePath) ;
            string[] header = file.ReadLine().Split(' ');
            string[] data = file.ReadLine().Split(' ');

            int d = Convert.ToInt32(header[1]);
            int expected = Convert.ToInt32(header[2]);
            int[] expenditure = Array.ConvertAll(data, expenditureTemp => Convert.ToInt32(expenditureTemp));

            return new object[] { d, expenditure, expected };
        }
    }
}
