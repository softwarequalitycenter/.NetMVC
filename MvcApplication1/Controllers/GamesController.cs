using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication1.Models;
using MvcApplication1.GamesServiceReference;

namespace MvcApplication1.Controllers
{
    public class GamesController : Controller
    { 
        /* 
         * TODO: DESIGN IMPROVEMENT
         * I belive the provided Web Services is fine for a small number of users but the architecture is not scalable.
         * It is not a good idea to call the web service methods to retrieve the Games data, say every second, for each user in a large website.
         * One better alternative would be to use a local cache where a single method continously retrieves the data ,
         * There is a little chance that the user may not see the change if it has not been updated in the local cache.
         * 
         * Another better alternative: If you need the most current data for all users, the web service has to be modified to include a method
         * that notifies where there is a change. That way only the changed values need to be fetched/reloaded.
         * This would be a similar pattern that is used by gmail to update the mails.
         * 
         * The two alternatives can be used together.
         * 
        */

        GamesModel gm = new GamesModel();
        XboxGame xBox = new XboxGame();
        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("GameList", gm);
            }
            return View(gm);
        }

        /*
         * VoteGame method checks users cookies before voting
         * If the user has voted already for a particular game id,
         * then they won't be able to vote again
         * 
         * */
        [Authorize]
        public ActionResult VoteGame(int? id)
        {
            if (id.HasValue)
            {
                if (Request.Cookies.Get("GameTitles") == null)
                {
                    System.Diagnostics.Debug.WriteLine("cookies doesn't exist");
                    Response.Cookies.Add(new HttpCookie("GameTitles", Convert.ToString((int)id)));
                    if (!gm.VoteGame((int)id))
                    {
                        ViewData["Message"] = "Your vote was not successful. This could  be because you already voted, or the system is not accepting the vote at this time";
                    }
                    else
                    {
                        ViewData["Message"] = "Your vote was successfully added.";
                    }
                    return RedirectToAction("Index", "Games");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("cookies  exist");
                    HttpCookie cookie = Request.Cookies.Get("GameTitles");
                    string existingCookieValue = cookie.Value;
                    //System.Diagnostics.Debug.WriteLine(existingCookieValue);
                    string[] values = existingCookieValue.Split(',');
                    foreach (string word in values)
                    {
                        if (Convert.ToString((int)id).Equals(word.Trim()))
                        {
                            ViewData["Message"] = "Your vote was not successful. This could  be because you already voted, or the system is not accepting the vote at this time";
                            return RedirectToAction("Index", "Games");
                        }
                    }
                    if (!gm.VoteGame((int)id))
                    {
                        ViewData["Message"] = "Your vote was not successful. This could  be because you already voted, or the system is not accepting the vote at this time";
                    }
                    else
                    {
                        cookie.Value += "," + Convert.ToString((int)id);
                        Response.Cookies.Add(cookie);
                        ViewData["Message"] = "Your vote was successfully added.";
                    }
                }
            }
            return RedirectToAction("Index", "Games");
        }

     
        [Authorize(Roles="Administrators")]
        public ActionResult OwnGame(int? id)
        {
            if (id.HasValue)
            {
                if (!gm.OwnGame((int) id))
                {
                    ViewData["Message"] = "Your attempt was not successful. This could be because you don't have sufficient privileges or that the system is not accepting Owning games at this time";
                }
                else
                {
                    ViewData["Message"] = "Game successfully owned";
                }
            }
            return RedirectToAction("Index", "Games");
        }

   
        [Authorize]
        public ActionResult AddGame(String title)
        {
            if (!String.IsNullOrEmpty(title))
            {
                if (!gm.AddGame(title))
                {
                    ViewData["Message"] = "The title was not added. This may be because the title already exists in the DB";
                }
                else
                {
                    ViewData["Message"] = "The title was added successful";
                }
            }
            return View("AddGame");
        }

    }
}
