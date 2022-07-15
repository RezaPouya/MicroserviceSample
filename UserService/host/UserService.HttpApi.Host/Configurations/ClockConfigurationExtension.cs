using System.IO;
using Volo.Abp.Timing;

namespace UserService.Configurations
{

    public static class ClockConfigurationExtension
    {
        /// <summary>
        /// Its read DateTimeKind node from setting and set AbpClockOptions
        /// the default value is DateTimeKind.Local
        /// Inject IClock service to services and use it instead of DateTime
        /// </summary>
        public static void ConfigureClock(this ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            context.Configure<AbpClockOptions>(opts =>
            {
                opts.Kind = GetDateTimeKindFromSetting(configuration["DateTimeKind:Current"]);
            });
        }

        private static DateTimeKind GetDateTimeKindFromSetting(string clockConfig)
        {
            if (clockConfig == null)
            {
                return DateTimeKind.Local;
            }

            DateTimeKind dateTimeKind = (DateTimeKind)Convert.ToInt32(clockConfig);

            if (Enum.IsDefined(typeof(DateTimeKind), dateTimeKind) == false)
            {
                throw new InvalidDataException("Clock Datetime kind is invalid");
            }

            return dateTimeKind;
        }
    }
}