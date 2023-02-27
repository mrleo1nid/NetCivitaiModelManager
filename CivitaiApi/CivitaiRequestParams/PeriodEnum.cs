

using System.ComponentModel;

namespace CivitaiApi.CivitaiRequestParams
{
    public enum PeriodEnum
    {
        [Description("All Time")]
        AllTime,
        [Description("Year")]
        Year,
        [Description("Month")]
        Month,
        [Description("Week")]
        Week,
        [Description("Day")]
        Day
    }
}
