using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CivitaiApiWrapper.Enums
{
    public enum Period
    {
        [Description("AllTime")]
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
