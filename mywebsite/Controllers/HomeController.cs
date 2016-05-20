using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mywebsite.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            //Create Service Reference Object
            ServiceReference1.Service1Client DataService = new ServiceReference1.Service1Client();

            //Create TvShowsLists object using TvShows model
            ServiceReference1.TvShows TvShowsLists = new ServiceReference1.TvShows();
            
            //Add TvShow 
            TvShowsLists = DataService.DisplayTvShows();

            //Return to Index page
            return View(TvShowsLists);
        }

    }
}
