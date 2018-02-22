using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Findwise.SolutionManager.Core
{
    public static class Helpers
    {
        public static ICollection<KeyValuePair<TKey, TValue>> AsCollection<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            return dictionary;
        }
    }
}
