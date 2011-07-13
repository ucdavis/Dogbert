using System.Collections;

namespace Dogbert2.Helpers
{
    static class StringExt
    {
        public static IEnumerable IndexOfAll(this string haystack, string needle)
        {
            int pos,offset = 0;
            while ((pos = haystack.IndexOf(needle))>=0)
            {
            haystack = haystack.Substring(pos+needle.Length);
            offset += pos;
            yield return offset;
            }
        }
    }
}