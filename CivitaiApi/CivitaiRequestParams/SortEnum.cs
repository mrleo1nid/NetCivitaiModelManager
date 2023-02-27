

using System.ComponentModel;

namespace CivitaiApi.CivitaiRequestParams
{
    public enum SortEnum
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
