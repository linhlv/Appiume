﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appiume.Apm.Timing
{
    /// <summary>
    /// Implements <see cref="IClockProvider"/> to work with UTC times.
    /// </summary>
    public class UtcClockProvider : IClockProvider
    {
        public DateTime Now
        {
            get { return DateTime.UtcNow; }
        }

        public DateTimeKind Kind
        {
            get
            {
                return DateTimeKind.Utc;
            }
        }

        public DateTime Normalize(DateTime dateTime)
        {
            if (dateTime.Kind == DateTimeKind.Unspecified)
            {
                return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
            }

            if (dateTime.Kind == DateTimeKind.Local)
            {
                return dateTime.ToUniversalTime();
            }

            return dateTime;
        }

        public bool SupportsMultipleTimezone()
        {
            return true;
        }
    }
}