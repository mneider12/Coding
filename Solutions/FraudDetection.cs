using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Text;

namespace Solutions
{
    /// <summary>
    /// Solution for Fraudulent Activity Notification challenge on Hacker Rank
    /// https://www.hackerrank.com/challenges/fraudulent-activity-notifications/problem?h_l=interview&playlist_slugs%5B%5D=interview-preparation-kit&playlist_slugs%5B%5D=sorting
    /// </summary>
    public class FraudDetection
    {
        private const int MAX_EXPENDITURE = 200;

        /// <summary>
        /// given a list of expenditures, and number of trailing days, 
        /// alert whenever a day's expenditures exceed 2x the trailing days median expenditures
        /// </summary>
        /// <remarks>
        /// assumes that expenditure[i] is >= 0 and <= 200
        /// length of expenditre (n) is >= 1 and <= 2 * 10 ^ 5
        /// 1 <= d <= n
        /// </remarks>
        /// <param name="expenditure">list of daily expenditures</param>
        /// <param name="d">trailing days to calculate the median</param>
        /// <returns>number of alerts</returns>
        public static int ActivityNotifications(int[] expenditure, int d)
        {
            Queue<int> recent = new Queue<int>();
            int[] cumulative = new int[MAX_EXPENDITURE + 1];
            int leftCount = (d + 1) / 2;
            int rightCount = (d + 2) / 2;
            int alerts = 0;

            foreach (int amount in expenditure)
            {
                if (recent.Count == d)
                {
                    int testLeft = 0, testRight = 0;
                    bool leftFound = false, rightFound = false;
                    for (int i = 0; !rightFound; i++)
                    {
                        if (!leftFound && cumulative[i] >= leftCount)
                        {
                            leftFound = true;
                            testLeft = i;
                        }
                        if (cumulative[i] >= rightCount)
                        {
                            rightFound = true;
                            testRight = i;
                        }
                    }

                    if (amount >= testLeft + testRight)
                    {
                        alerts++;
                    }
                }

                recent.Enqueue(amount);
                for (int i = amount; i <= MAX_EXPENDITURE; i++)
                {
                    cumulative[i]++;
                }

                if (recent.Count > d)
                {
                    int dequeued = recent.Dequeue();
                    for (int i = dequeued; i <= MAX_EXPENDITURE; i++)
                    {
                        cumulative[i]--;
                    }
                }
            }

            return alerts;
        }
    }
}
