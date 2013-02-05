using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using MvcApplication1.GamesServiceReference;

namespace MvcApplication1.Models
{
    /*
     * GamesModel class is a  wrapper for web services
     * and defines some helper methods/security etc. on top of
     * standard web services methods
     */
    public class GamesModel
    {
        private XboxVotingServiceSoapClient client;
        private string apiKey = "60763ba65b673567fed3343af7fcfd9b";
       
        public GamesModel()
        {
            client = new XboxVotingServiceSoapClient();
        }

        public bool CheckKey()
        {
            return client.CheckKey(apiKey);
        }

       /*
        * GetGamesWeWant() returns the games that are added to the system
        * but not yet owned
        * Any user can view these Games
        * @parameters: none
        * Returns an ArrayList of XboxGame
        */
        public ArrayList GetGamesWeWant()
        {
            XboxGame[] games = client.GetGames(apiKey);
            ArrayList gamesWeWant = new ArrayList(); 
            for (int i=0;i<games.Length;i++)
            {
                if (games[i].Status.Equals("wantit"))
                {
                    gamesWeWant.Add(games[i]); 
                }
            }
            return gamesWeWant;
        }

        /*
         * GetGamesWeWant() returns the games are already owned
         * Any user can view these Games
         * @parameters: none
         * Returns an ArrayList of XboxGame
         */

        public ArrayList GetGamesWeHave()
        {
            XboxGame[] games = client.GetGames(apiKey);
            ArrayList gamesWeHave = new ArrayList();
            for (int i = 0; i < games.Length; i++)
            {
                if (games[i].Status.Equals("gotit"))
                {
                    gamesWeHave.Add(games[i]);
                }
            }
            return gamesWeHave;
        }

        [Authorize]
        public bool VoteGame(int gameId)
        {
            //TODO: if user's cookie says 'not voted'
            return client.AddVote(gameId, apiKey);
        }


       [Authorize(Roles = "Administrators")]
        public bool OwnGame(int gameId)
        {
            return client.SetGotIt(gameId, apiKey);
        }

       [Authorize]
        public bool AddGame(String title)
        {
            //duplicate check
            XboxGame[] games = client.GetGames(apiKey);
            for (int i = 0; i < games.Length; i++)
            {
                if (games[i].Title.Equals(title))
                {
                    return false;
                }
            }
            //add the game to db
            return client.AddGame(title, apiKey);
        }
    }
}