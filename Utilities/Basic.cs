using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Utilities
{
    public static class Basic
    {
        public static string ListToString<T>(this List<T> list, string separator)
        {
            string tmp = "";

            foreach (T x in list)
            {
                tmp += separator + x.ToString();
            }

            if (tmp.Length != 0)
            {
                tmp = tmp.Remove(0, 1);
            }

            return tmp;
        }

        public static bool AreEqual<T>(this List<T> list, List<T> list2) where T : IEquatable<T>
        {
            if (list.Count != list2.Count)
            {
                return false;
            }

            for (int i = 0; i < list.Count; i++)
            {
                if (!(list[i].Equals(list2[i])))
                {
                    return false;
                }
            }

            return true;
        }

        public static T DeepCopy<T>(T other)
        {
            using (MemoryStream ms = new())
            {
                BinaryFormatter formatter = new();
                formatter.Serialize(ms, other);
                ms.Position = 0;
                return (T)formatter.Deserialize(ms);
            }
        }

        public static string CutStringForParse(this string s)
        {
            return s.Remove(0, 2).Substring(0, s.Length - 2 - 1);
        }
    }
}
