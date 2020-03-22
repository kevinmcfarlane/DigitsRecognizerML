using System;
using System.Collections.Generic;
using System.Linq;
using DigitsRecognizerMLML.Model;
using Microsoft.ML;

namespace DigitsRecognizerML
{
    class Program
    {
        // Training data 
        private const string DATA_FILEPATH = @"C:\Users\Kevin\source\repos\DigitsRecognizerML\DigitsRecognizerML\Data\trainingsample.csv";
        
        // Validation data
        private const string VALIDATION_FILEPATH = @"C:\Users\Kevin\source\repos\DigitsRecognizerML\DigitsRecognizerML\Data\validationsample.csv";

        static void Main(string[] args)
        {
            //SinglePrediction();
            MultiplePredictions();
        }

        private static void SinglePrediction()
        {
            // Create single instance of sample data from first line of dataset for model input
            //ModelInput sampleData = CreateSingleDataSample(DATA_FILEPATH);
            ModelInput sampleData = CreateSingleDataSample(VALIDATION_FILEPATH);

            // Make a single prediction on the sample data and print results
            var predictionResult = ConsumeModel.Predict(sampleData);

            Console.WriteLine("Using model to make single prediction -- Comparing actual Label with predicted Label from sample data...\n\n");
            Console.WriteLine($"\n\nActual Label: {sampleData.Label} \nPredicted Label value {predictionResult.Prediction} \nPredicted Label scores: [{String.Join(",", predictionResult.Score)}]\n\n");
            Console.WriteLine("=============== End of process, hit any key to finish ===============");
            Console.ReadKey();
        }

        private static void MultiplePredictions()
        {
            var samples = CreateDataSamples(VALIDATION_FILEPATH);

            Console.WriteLine("Using model to make multiple predictions -- Comparing actual Label with predicted Label from sample data...\n");

            foreach (var sample in samples)
            {
                var prediction = ConsumeModel.Predict(sample);
                Console.WriteLine($"Actual Label: {sample.Label} Predicted Label: {prediction.Prediction}");
            }

            // TODO: Calculate percent accuracy.

            Console.WriteLine("=============== End of process, hit any key to finish ===============");
            Console.ReadKey();
        }

        #region Create Samples

        private static ModelInput CreateSingleDataSample(string dataFilePath)
        {
            // Create MLContext
            MLContext mlContext = new MLContext();

            // Load dataset
            IDataView dataView = mlContext.Data.LoadFromTextFile<ModelInput>(
                                            path: dataFilePath,
                                            hasHeader: true,
                                            separatorChar: ',',
                                            allowQuoting: true,
                                            allowSparse: false);

            // Use first line of dataset as model input
            // You can replace this with new test data (hardcoded or from end-user application)
            ModelInput sampleForPrediction = mlContext.Data.CreateEnumerable<ModelInput>(dataView, false)
                .First();
            return sampleForPrediction;
        }

        private static IEnumerable<ModelInput> CreateDataSamples(string dataFilePath)
        {
            MLContext mlContext = new MLContext();

            IDataView dataView = mlContext.Data.LoadFromTextFile<ModelInput>(
                                            path: dataFilePath,
                                            hasHeader: true,
                                            separatorChar: ',',
                                            allowQuoting: true,
                                            allowSparse: false);
            IEnumerable<ModelInput> samplesForPrediction = mlContext.Data.CreateEnumerable<ModelInput>(dataView, false);

            return samplesForPrediction;
        }
        #endregion
    }
}
