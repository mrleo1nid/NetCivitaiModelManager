using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CivitaiApiWrapper.Enums
{
    public enum Sort
    {
        [Description("Highest Rated")]
        HighestRated,
        [Description("Most Downloaded")]
        MostDownloaded,
        [Description("Newest")]
        Newest,
        [Description("Most Liked")]
        MostLiked,
        [Description("Most Discussed")]
        MostDiscussed
    }
}
