using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appiume.Apm.Timing
{
    public class UnspecifiedClockProvider : IClockProvider
    {
        public DateTime Now
        {
            get
            {
                return DateTime.Now;
            }
        }

        public DateTimeKind Kind
        {
            get
            {
                return DateTimeKind.Unspecified;
            }
        }

        public DateTime Normalize(DateTime dateTime)
        {
            return dateTime;
        }

        public bool SupportsMultipleTimezone()
        {
            return false;
        }
    }
}