using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using Appiume.Apm.Extensions;

namespace Appiume.Apm.Localization
{
    internal static class GlobalizationHelper
    {
        public static bool IsValidCultureCode(string cultureCode)
        {
            if (cultureCode.IsNullOrWhiteSpace())
            {
                return false;
            }

            try
            {
                CultureInfo.GetCultureInfo(cultureCode);
                return true;
            }
            catch (CultureNotFoundException)
            {
                return false;
            }
        }
    }
}