using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myfirstWCF.Model
{
    /// <summary>
    /// @Description: TvShows class to store list of tv shows
    /// @Author: Harshu Kumar Gaonjur
    /// </summary>
    public class TvShows
    {
        public int[] TVID { get; set; }
        public string[] TvShowsName { get; set; }
        public int[] Ratings { get; set; }
    }
}