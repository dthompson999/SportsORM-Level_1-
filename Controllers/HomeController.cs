using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsORM.Models;
using Microsoft.EntityFrameworkCore;

namespace SportsORM.Controllers
{
    public class HomeController : Controller
    {

        private static Context context;

        public HomeController(Context DBContext)
        {
            context = DBContext;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            ViewBag.BaseballLeagues = context.Leagues
                .Where(l => l.Sport.Contains("Baseball"));
            return View();
        }

        [HttpGet("level_1")]
        public IActionResult Level1()
        {
            ViewBag.WomenLeagues = context.Leagues
                .Where(wl => wl.Name.Contains("Women"));
            ViewBag.HockeyLeagues = context.Leagues
                .Where(hl => hl.Sport.Contains("Hockey"));
            ViewBag.NotFootball = context.Leagues
                .Where(nf => nf.Sport != "Football");
            ViewBag.Conferences = context.Leagues
                .Where(cl => cl.Name.Contains("Conference"));
            ViewBag.Atlantic = context.Leagues
                .Where(ar => ar.Name.Contains("Atlantic"));
            ViewBag.Dallas = context.Teams
                .Where(dt => dt.Location.Contains("Dallas")); 
            ViewBag.Raptors = context.Teams
                .Where(rt => rt.TeamName.Contains("Raptors"));
            ViewBag.City = context.Teams
                .Where(ct => ct.Location.Contains("City"));
            ViewBag.TNames = context.Teams
                .Where(tn => tn.TeamName[0] == 'T').ToList();  
            ViewBag.ABCLocation = context.Teams
                .OrderBy(abcLoc => abcLoc.Location); 
            ViewBag.ABCReverse = context.Teams
                .OrderByDescending(abcRev => abcRev.Location); 
            ViewBag.LNCooper = context.Players
                .Where(lnCooper => lnCooper.LastName.Contains("Cooper"));
            ViewBag.FNJoshua = context.Players
                .Where(fnJoshua => fnJoshua.FirstName.Contains("Joshua"));
            ViewBag.CoopNoJosh = context.Players
                .Where(cnj => cnj.LastName.Contains("Cooper") && cnj.FirstName != "Joshua");
            ViewBag.AlexWyatt = context.Players
                .Where(anw => anw.FirstName.Contains("Alexander") || anw.FirstName.Contains("Wyatt"));
            return View();
        }

        [HttpGet("level_2")]
        public IActionResult Level2()
        {
            ViewBag.AtlSoccer = context.Teams
                .Include(atl => atl.CurrLeague)
                .Where(atl => atl.CurrLeague.Name.Contains("Atlantic Soccer"));
            ViewBag.PengPlayers = context.Teams
                .Include(t => t.CurrentPlayers)
                .Where(t => t.Location == "Boston");
            ViewBag.ColBaseball = context.Teams
                .Where(t => t.CurrLeague.Name.Contains("Collegiate"));
            ViewBag.ColBaseball = context.Teams
                .Where(t => t.CurrLeague.Name.Contains("Amateur Football"));
            ViewBag.AllFootball = context.Teams
                .Where(t => t.CurrLeague.Sport == "Football");
            ViewBag.SophiaTeams = context.Players
                .Include(p => p.CurrentTeam)
                .Where(p => p.FirstName == "Sophia")
                .ToList();
            ViewBag.Flores = context.Players
                .Where(p => p.LastName == "Flores" && p.CurrentTeam.TeamName != "Raptors");
            ViewBag.MTC = context.Teams
                .Include(t => t.CurrentPlayers)
                .Where(t => t.TeamName == "Tiger-Cats")
                .ToList();
            ViewBag.TwelveAngryTeams = context.Teams
                .Include(t => t.AllPlayers)
                .Include(t => t.CurrLeague)
                .OrderByDescending(t => t.AllPlayers.Count)
                .Where(t => t.AllPlayers.Count > 11)
                .ToList();
            return View();
        }

        [HttpGet("level_3")]
        public IActionResult Level3()
        {
            return View();
        }

    }
}