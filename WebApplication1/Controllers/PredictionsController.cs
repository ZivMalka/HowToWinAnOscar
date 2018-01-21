using Accord.Math;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using WebApplication1.DB;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class PredictionsController : Controller
    {
        string[] inputColumns =
        {
        "BAFTA", "GoldenGlobe", "Guild", "running_time", "box_office", "imdb_score", "rt_audience_score", "rt_critic_score", "stars_count", "writers_count", "produced_USA", "R", "PG", "PG13", "G", "q1_release", "q2_release", "q3_release", "q4_release"
        };

        // GET: Predictions
        public ActionResult Index()
        {
            var data = DBManager.GetOscarCollection().AsQueryable<Attributes>().ToList();
            return View(data);
        }

        public ActionResult ChooseYear()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CheckWinner(string txtFirst)
        {
            try
            {
                int year = Convert.ToInt32(txtFirst);

                {
                    Predictions p = new Predictions();
                    //find wanted year
                    var Year = DBManager.GetOscarCollection().AsQueryable<Attributes>().Where(x => x.Year < year).ToList<Attributes>();
                   // var current = DBManager.GetOscarCollection().AsQueryable<Attributes>().Where(x => x.Year == year).ToList<Attributes>();

                    //convert mongoCollection into datatable
                    DataTable convert = p.ToDataTable(Year);

                    //prepring input and output to the classifier
                    double[][] inputs = convert.ToJagged<double>(inputColumns);
                    int[] outputs = convert.ToArray<int>("Oscar");

                    //clear datatable, and prepring data for the current year 
                    convert.Clear();
                    Year.Clear();
                    Year = DBManager.GetOscarCollection().AsQueryable<Attributes>().Where(x => x.Year == year).ToList<Attributes>();

                    convert = p.ToDataTable(Year);
                    double[][] currentP = convert.ToJagged<double>(inputColumns);

              
                    string winner = p.Program(inputs, outputs, currentP, convert);

                   ViewBag.yearResult = winner.ToString();
                   ViewBag.curr = Year.ToList();

                    return View("ChooseYear");
                }
            }

            catch
            {
                return View("yearwasnotfount");
            }
        }

        public ActionResult ProabilityOfLogisticRegression()
        {
            return View();
        }

        [HttpPost]
        public ActionResult BestProabilityOfLogisticRegression(Attributes movie)
        {
            List<Attributes> Movie = new List<Attributes>
            {
                movie
            };

            Predictions p = new Predictions();

            var data = DBManager.GetOscarCollection().AsQueryable<Attributes>().ToList();
            DataTable convert = p.ToDataTable(data);
            double[][] inputs = convert.ToJagged<double>(inputColumns);
            int[] outputs = convert.ToArray<int>("Oscar");

            convert.Clear();
            convert = p.ToDataTable(Movie);
            double [][] Query = convert.ToJagged<double>(inputColumns);
            double winner = p.CaculateProability(inputs, outputs, Query);


            ViewBag.proability = winner.ToString();

            return View("ProabilityOfLogisticRegression");

        }


    }
}