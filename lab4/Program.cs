using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    static class StatisticOperation
    {
        public static uint Sum(Set set)
        {
            uint len = 0;

            foreach (string item in set.Data)
            {
                len += (uint)item.Length;
            }

            return len;
        }

        public static int MinMaxDiff(Set set)
        {
            Console.WriteLine($"{set.GetMax()} {set.GetMin()}");
            return set.GetMax() - set.GetMin();
        }

        public static uint Count(Set set)
        {
            return set.GetNum();
        }

        public static string AddDot(this string str)
        {
            return str + '.';
        }

        public static void RemoveNull(this Set set)
        {
            set.Data.Remove("");
        }
    }

    class Set
    {
        class Owner
        {
            public readonly string mName;
            public readonly string mCreatorName;
            public readonly uint mId;

            public Owner(string name, string creator_name, uint id)
            {
                mName = name;
                mCreatorName = creator_name;
                mId = id;
            }
        }

        private Owner mOwner = new Owner("name", "cName", 1);
        private DateTime mDt = new DateTime();

        private List<string> mData;
        public List<string> Data { get; set; }

        public Set()
        {
            mData = new List<string>();
            Data = mData;
        }

        public Set(string[] items)
        {
            mData = new List<string>(items);
            Data = mData;
        }

        public uint GetPower()
        {
            uint i = 0;
            var dict = new Dictionary<string, int>();
            foreach (string item in mData)
            {
                if (!dict.ContainsKey(item))
                {
                    dict[item] = 0;
                }
                dict[item]++;
                if (dict[item] == 1) { i++; };

            }
            return i;
        }

        public uint GetNum()
        {
            return (uint)mData.Count;
        }

        public int GetMax()
        {
            int max = -1;
            foreach (string item in mData)
            {
                int len = item.Length;
                if (max < len)
                {
                    max = len;
                }
            }
            return max;
        }

        public int GetMin()
        {
            int min = 99;
            foreach (string item in mData)
            {
                int len = item.Length;
                if (min > len)
                {
                    min = len;
                }
            }
            return min;
        }

        public string this[int i]
        {
            get
            {
                return mData[i];
            }
            set
            {
                mData[i] = value;
            }
        }

        public static Set operator -(Set a, string b)
        {
            Set new_set = (Set)a.MemberwiseClone();

            new_set.mData.Remove(b);

            return new_set;
        }

        public static Set operator *(Set a, Set b)
        {
            Set new_set = (Set)a.MemberwiseClone();

            foreach(string item in new_set.mData)
            {
                if (!b.mData.Contains(item))
                {
                    new_set.mData.Remove(item);
                }
            }

            return new_set;
        }

        public static bool operator <(Set a, Set b)
        {
            return a.GetPower() < b.GetPower();
        }

        public static bool operator >(Set a, Set b)
        {
            foreach(string item in a.mData)
            {
                if (!b.mData.Contains(item))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool operator &(Set a, Set b)
        {
            return a.mData[0] == b.mData[0];
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            foreach (string item in mData)
            {
                str.Append($"{item}\t");
            }
            return str.ToString();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var Set1 = new Set(new string[] { "1", "2", "a" });
            var Set2 = new Set(new string[] { "1", "2", "3" });
            var Set3 = new Set(new string[] { "a", "aa", "aaa" });

            Console.WriteLine($"-: { Set1 - "a"}");
            Console.WriteLine($"*: { Set1 * Set2}");
            Console.WriteLine($"<: { Set1 < Set2}");
            Console.WriteLine($">: { Set1 > Set2}");
            Console.WriteLine($"&: { Set1 & Set2}");

            Console.WriteLine($"Count: {StatisticOperation.Count(Set3)}");
            Console.WriteLine($"MinMaxDiff: {StatisticOperation.MinMaxDiff(Set3)}");
            Console.WriteLine($"Sum: {StatisticOperation.Sum(Set3)}");

            string shock = "AddDot";
            Console.WriteLine($"AddDot: {shock.AddDot()}");

            var SetNull = new Set(new string[] { "", "", "a" });
            SetNull.RemoveNull();
            Console.WriteLine(SetNull);
        }
    }
}
