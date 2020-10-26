using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    class Set
    {
        private List<string> mData;

        public Set()
        {
            mData = new List<string>();
        }

        public Set(string[] items)
        {
            mData = new List<string>(items);
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

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            foreach (string item in mData)
            {
                str.Append($"\t{item}");
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

            Console.WriteLine(Set1 - "a");
            Console.WriteLine(Set1 * Set2);
            Console.WriteLine(Set1 < Set2);
            Console.WriteLine(Set1 > Set2);
        }
    }
}
