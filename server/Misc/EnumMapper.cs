using System;
using System.Collections.Generic;
using System.Linq;

namespace QuestApp.Misc
{
    public static class EnumMapper
    {
        public static int GetMappedId<T>(List<T> collection)
        {
            var result = 0;
            var marker = 1;
            foreach (T value in Enum.GetValues(typeof(T)))
            {
                if (collection.Contains(value))
                {
                    result |= marker;
                }
                marker *= 2;
            }
            return result;
        }

        public static List<T> GetCollectionFromMappedId<T>(int mappedId)
        {
            List<T> collection = new List<T>();
            var enums = Enum.GetValues(typeof(T)).Cast<T>().ToList();
            var marker = 1;
            for (int i = 1; i <= enums.Count; i++)
            {
                if (Convert.ToBoolean(mappedId & marker))
                {
                    collection.Add(enums[i - 1]);
                }
                marker *= 2;
            }

            return collection;
        }
    }
}
