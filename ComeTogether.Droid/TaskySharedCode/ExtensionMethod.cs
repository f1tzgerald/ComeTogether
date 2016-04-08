using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ComeTogether.Droid
{
    public static class ExtensionMethod
    {
        public static bool Contains(this string source, string toCheck, StringComparison comparisonType)
        {
            return (source.IndexOf(toCheck, comparisonType) >= 0);
        }
    }
}
