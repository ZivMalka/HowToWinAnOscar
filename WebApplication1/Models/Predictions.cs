using System.Data;
using System;
using Accord.Math;
using Accord.MachineLearning.DecisionTrees;
using Accord.MachineLearning.DecisionTrees.Learning;
using System.Collections.Generic;
using MongoDB.Driver;
using System.Linq;
using System.ComponentModel;
using Accord.Statistics.Models.Regression;
using Accord.MachineLearning.VectorMachines.Learning;


namespace WebApplication1.Models
{
    public class Predictions
    {
        string[] inputColumns =
         {
        "BAFTA", "GoldenGlobe", "Guild", "running_time", "box_office", "imdb_score", "rt_audience_score", "rt_critic_score", "stars_count", "writers_count", "produced_USA", "R", "PG", "PG13", "G", "q1_release", "q2_release", "q3_release", "q4_release"
         };

        public string [] inputColumns1()
        {
                 return inputColumns;
        }



        public DataTable ToDataTable<Movie>(IList<Movie> data)
        {
            PropertyDescriptorCollection properties =
            TypeDescriptor.GetProperties(typeof(Movie));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (Movie item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        public int[] DecisionTreePrediction(double[][] inputs, int[] outputs, double[][] currentP)
        {

            // Create a teaching algorithm:
            var teacher = new C45Learning()
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

            };

            // and induce a decision tree from the data:
            DecisionTree tree = teacher.Learn(inputs, outputs);

            int[] prediction = tree.Decide(currentP);

            return prediction;
        }

        public double[] LogisticRegressionPrediction(double[][] inputs, int[] outputs, double[][] currentP)
        {
            // Create the L1-regularization learning algorithm
            var teacher1 = new ProbabilisticCoordinateDescent()
            {
                Tolerance = 1e-10,
                Complexity = 1e+10, // learn a hard-margin model

            };

            // Learn the L1-regularized machine
            var svm = teacher1.Learn(inputs, outputs);
            // Convert the svm to logistic regression
            var regression = (LogisticRegression)svm;

            double[] scores = regression.Probability(currentP);

            return scores;
        }

        public double CaculateProability(double[][] inputs, int[] outputs, double[][] currentP)
        {
            double[] scores = LogisticRegressionPrediction(inputs, outputs, currentP);
            double max = scores.Max();
     
            return max;
        }


        public int[] RandomForest(double[][] inputs, int[] outputs, double[][] currentP)
        {
            // Create the forest learning algorithm
            var teacher2 = new RandomForestLearning()
            {
                NumberOfTrees = 10, // use 10 trees in the forest
            };
            // Finally, learn a random forest from data
            var forest = teacher2.Learn(inputs, outputs);
            // We can estimate class labels using

            int[] predicted = forest.Decide(currentP);

            return predicted;
        }


        public int[] ChecBestPictureWinner(int[] PredictionsOfDecisionTree, double[] scores)
        {
                if ((PredictionsOfDecisionTree.Sum() == 1))
                {
                    return PredictionsOfDecisionTree;
                }

                double max = 0;

            if ((PredictionsOfDecisionTree.Sum() > 1))
                {
                  for (int i = 0; i < PredictionsOfDecisionTree.Length; i++)
                    {
                        if (PredictionsOfDecisionTree[i] == 1)
                        {
                            double x = scores[i];
                            if (x > max)
                            {
                                max = x;
                            }
                        }
                    }
                }

                else
                {
                    max = scores.Max();
                }


                for (int i = 0; i < PredictionsOfDecisionTree.Length; i++)
                {
                    PredictionsOfDecisionTree[i] = 0;
                    if ((scores[i]) == max)
                    {
                        PredictionsOfDecisionTree[i] = 1;
                    }
                }

                return PredictionsOfDecisionTree;
            }
        


        public string Program(double[][] inputs, int[] outputs, double[][] currentP, DataTable currentYear)
        {
            int [] PredictionsOfDecisionTree = DecisionTreePrediction(inputs,  outputs, currentP);
            double[] scores = LogisticRegressionPrediction(inputs, outputs, currentP);
            int[] Predict = ChecBestPictureWinner(PredictionsOfDecisionTree, scores);

            string winner = null;
            for (int i = 0; i < Predict.Length; i++)
            {
                if (Predict[i] == 1)
                {
                    winner = currentYear.Rows[i][3].ToString();
                }
            }
            return winner;
        }


    }
}


    


