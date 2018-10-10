using System;
using System.Collections.Generic;
using System.Text;

namespace DatasturctAndAlgo.Algo.递归和动态规划
{
    /// <summary>
    /// 数字字符转换为字母组合
    /// A~Z 对应 1~26
    /// 1111 可能是 AAAA LAA ALA AAL LL 5种可能
    /// </summary>
    public static class 数字字符转换为字母组合
    {
        private static Dictionary<int, string> _charDic = new Dictionary<int, string>();
        public static Dictionary<int, string> charDic
        {
            get
            {
                int i = 0;
                while (i < 26)
                {
                    _charDic[i] = ((char)('A' + i)).ToString();
                    i++;
                }
                return _charDic;
            }

        }

        public static int GetArrayCount(string s)
        {
            if (s.Length == 0)
                return 0;
            if (s[0] == '0')
                return 0;
            if (s.Length == 1)
                return 1;


            int[] dp = new int[10000];
            dp[0] = 1;


            for (int i = 1; i < s.Length; i++)
            {
                if (s[i] == '0')
                {
                    if (s[i - 1] == '0' || s[i - 1] > '2')
                    {
                        return 0;
                    }

                    if (s[i - 1] <= '2')
                    {
                        dp[i] = dp[i - 2];
                    }
                }
                else
                {
                    if (s[i - 1] != 0)
                    {
                        if (s[i - 1] == '1' || (s[i - 1] == '2' && s[i] < '7'))
                            dp[i] = (i == 1 ? 2 : dp[i - 1] + dp[i - 2]);
                        else
                            dp[i] = dp[i - 1];
                    }
                    else
                    {
                        dp[i] = dp[i - 1];
                    }
                }
            }
            return dp[s.Length - 1];


        }

    }

}

}
}
