using System;
using System.Collections.Generic;

namespace ColorToolPlusInternals
{
    public static class Enums
    {
        public static IEnumerable<T> GetValues<T>() where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum) throw new ArgumentException("T must be an enumerated type");

            foreach (var item in Enum.GetValues(typeof(T)))
            {
                yield return (T)item;
            }
        }
    }
}
