using System;

namespace Dogbert.Core.Abstractions
{
    public static class SystemTime
    {
        public static Func<DateTime> Now = () => DateTime.Now;

        public static void Reset()
        {
            Now = () => DateTime.Now;
        }
    }
}