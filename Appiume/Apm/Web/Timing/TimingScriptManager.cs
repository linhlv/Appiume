using System;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Appiume.Apm.Configuration;
using Appiume.Apm.Dependency;
using Appiume.Apm.Extensions;
using Appiume.Apm.Timing;
using Appiume.Apm.Timing.Timezone;

namespace Appiume.Apm.Web.Timing
{
    /// <summary>
    /// This class is used to build timing script.
    /// </summary>
    public class TimingScriptManager : ITimingScriptManager, ITransientDependency
    {
        private readonly ISettingManager _settingManager;

        public TimingScriptManager(ISettingManager settingManager)
        {
            _settingManager = settingManager;
        }

        public async Task<string> GetScriptAsync()
        {
            var script = new StringBuilder();

            script.AppendLine("(function(){");

            script.AppendLine("    apm.clock.provider = apm.timing." + Clock.Provider.GetType().Name.ToCamelCase() + " || apm.timing.localClockProvider;");
            script.AppendLine("    apm.clock.provider.supportsMultipleTimezone = " + Clock.SupportsMultipleTimezone().ToString().ToLower(CultureInfo.InvariantCulture) + ";");

            if (Clock.SupportsMultipleTimezone())
            {
                script.AppendLine("    apm.timing.timeZoneInfo = " + await GetUsersTimezoneScriptsAsync());
            }

            script.Append("})();");

            return script.ToString();
        }

        private async Task<string> GetUsersTimezoneScriptsAsync()
        {
            var timezoneId = await _settingManager.GetSettingValueAsync(TimingSettingNames.TimeZone);
            var timezone = TimeZoneInfo.FindSystemTimeZoneById(timezoneId);

            return " {" +
                   "        windows: {" +
                   "            timeZoneId: '" + timezoneId + "'," +
                   "            baseUtcOffsetInMilliseconds: '" + timezone.BaseUtcOffset.TotalMilliseconds + "'," +
                   "            currentUtcOffsetInMilliseconds: '" + timezone.GetUtcOffset(Clock.Now).TotalMilliseconds + "'," +
                   "            isDaylightSavingTimeNow: '" + timezone.IsDaylightSavingTime(Clock.Now) + "'" +
                   "        }," +
                   "        iana: {" +
                   "            timeZoneId:'" + TimezoneHelper.WindowsToIana(timezoneId) + "'" +
                   "        }," +
                   "    }";
        }
    }
}