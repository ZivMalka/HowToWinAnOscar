using Accord.MachineLearning;
using Accord.MachineLearning.DecisionTrees;
using Accord.MachineLearning.DecisionTrees.Learning;
using Accord.Math;
using Accord.Math.Optimization.Losses;
using Accord.Statistics.Analysis;
using Accord.Statistics.Kernels;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WebApplication1.DB;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Models
{
    public class Analysis
    {
        //anlysis of decition tree algorithem

        string[] inputColumns =
         {
        "BAFTA", "GoldenGlobe", "Guild", "running_time", "box_office", "imdb_score", "rt_audience_score", "rt_critic_score", "stars_count", "writers_count", "produced_USA", "R", "PG", "PG13", "G", "q1_release", "q2_release", "q3_release", "q4_release"
         };

        public static double crossValidation(double[][] input, int[] output)
        {

            var cv = CrossValidation.Create(

        k: 10, // We will be using 10-fold cross validation

        learner: (p) => new C45Learning() // here we create the learning algorithm
        {
                new DecisionVariable("BAFTA", 2),
                new DecisionVariable("GoldenGlobe", 2),
                new DecisionVariable("Guild", 2),
                new DecisionVariable("running_time", DecisionVariableKind.Continuous),
                new DecisionVariable("box_office", DecisionVariableKind.Continuous),
                new DecisionVariable("imdb_score", DecisionVariableKind.Continuous),
                new DecisionVariable("rt_audience_score", DecisionVariableKind.Continuous),
                new DecisionVariable("rt_critic_score", DecisionVariableKind.Continuous),
                new DecisionVariable("stars_count", DecisionVariableKind.Continuous),
                new DecisionVariable("writers_count",    DecisionVariableKind.Continuous),
                new DecisionVariable("produced_USA",  2),
                new DecisionVariable("R",  2),
                new DecisionVariable("PG",  2),
                new DecisionVariable("PG13",  2),
                new DecisionVariable("G",  2),
                new DecisionVariable("q1_release",  2),
                new DecisionVariable("q2_release",  2),
                new DecisionVariable("q3_release",  2),
                new DecisionVariable("q4_release",  2),
        },

        // Now we have to specify how the tree performance should be measured:
        loss: (actual, expected, p) => new ZeroOneLoss(expected).Loss(actual),

        // This function can be used to perform any special
        // operations before the actual learning is done, but
        // here we will just leave it as simple as it can be:
        fit: (teacher, x, y, w) => teacher.Learn(x, y, w),

        // Finally, we have to pass the input and output data
        // that will be used in cross-validation. 
        x: input, y: output
    );

            // After the cross-validation object has been created,
            // we can call its .Learn method with the input and 
            // output data that will be partitioned into the folds:
            var result = cv.Learn(input, output);

            double trainingError = result.Training.Mean; // should be 0.017771153143274855
            double validationError = result.Validation.Mean; // should be 0.0755952380952381

            // If desired, compute an aggregate confusion matrix for the validation sets:
            GeneralConfusionMatrix gcm = result.ToConfusionMatrix(input, output);

            double accuracy = gcm.Accuracy; // result should be 0.92442882249560632

            return accuracy;
        }

        public void AccuracyTest()
        {
            double acc = 0;
            int year = 1981;
            double TruePositives = 0;
            double TrueNegatives = 0;
            double FalseNegatives = 0;
            double FalsePositives = 0;
            int count = 0;
            Predictions p = new Predictions();

            while (year < 2017)
            {
                //find wanted year
                var past = DBManager.GetOscarCollection().AsQueryable<Attributes>().Where(x => x.Year < year).ToList<Attributes>();
                var current = DBManager.GetOscarCollection().AsQueryable<Attributes>().Where(x => x.Year == year).ToList<Attributes>();

                //convert mongoCollection into datatable
                DataTable preYear = p.ToDataTable(past);
                DataTable currentYear = p.ToDataTable(current);
                
                double[][] inputs = preYear.ToJagged<double>(inputColumns);
                int[] outputs = preYear.ToArray<int>("Oscar");

                double[][] currentP = currentYear.ToJagged<double>(inputColumns);
                int[] Trueoutputs = currentYear.ToArray<int>("Oscar");


                // The 3 Models
                int[] predictedRandomForest = p.RandomForest(inputs, outputs, currentP);
                double[] scores = p.LogisticRegressionPrediction(inputs, outputs, currentP);
                int[] PredictionsOfDecisionTree = p.DecisionTreePrediction(inputs, outputs, currentP);


                int[] predicted21 = p.ChecBestPictureWinner(PredictionsOfDecisionTree, scores);

                // Create a confusion matrix to show the machine's performance
                var m1 = new ConfusionMatrix(predicted: predicted21, expected: Trueoutputs);

                acc = acc + m1.Accuracy;
                TruePositives = TruePositives + m1.TruePositives;
                TrueNegatives = TrueNegatives + m1.TrueNegatives;
                FalseNegatives = FalseNegatives + m1.FalseNegatives;
                FalsePositives = FalsePositives + m1.FalsePositives;

                year = year + 1;

                count++;


            }

            Console.WriteLine("the accuracy of RandomForest: " + acc / count);
            Console.WriteLine("true Positives: " + TruePositives);
            Console.WriteLine("TrueNegatives:" + TrueNegatives);
            Console.WriteLine("FalseNegatives" + FalseNegatives);
            Console.WriteLine("FalsePositives" + FalsePositives);

        }

        public void Query()
        {
            var data1 = DBManager.GetOscarCollection().AsQueryable<Attributes>().ToList();
            var WonOscar = data1.Where(z => z.Oscar == 1).ToList<Attributes>();

            float RatioOFR = (float)(data1.Where(z => z.R == 1 && z.Oscar == 1).Count()) / (float)(data1.Where(z => z.R == 1).Count());
            float RatioOFPG13 = (float)data1.Where(z => z.PG13 == 1 && z.Oscar == 1).Count() / (float)(data1.Where(z => z.PG13 == 1).Count());
            float RatioOFPG = (float)data1.Where(z => z.PG == 1 && z.Oscar == 1).Count() / (float)(data1.Where(z => z.PG == 1).Count());
            float RatioOFG = (float)data1.Where(z => z.G == 1 && z.Oscar == 1).Count() / (float)(data1.Where(z => z.G == 1).Count());

            int WonAll4 = data1.Where(z => z.Guild == 1 && z.Oscar == 1 && z.GoldenGlobe == 1 && z.BAFTA == 1).Count();
            int WonOnlyOscar = data1.Where(z => z.Guild == 0 && z.Oscar == 1 && z.GoldenGlobe == 0 && z.BAFTA == 0).Count();
            double MaxBoxOffice = data1.Where(z => z.Oscar == 1).Max(z => z.Box_office);

            double RunninTime1 = data1.Average(z => z.Running_time);

            foreach (var item in WonOscar)
            {
                if (MaxBoxOffice == item.Box_office)
                {
                    Console.WriteLine(item.Name);
                }
            }
        

        }



    }
}