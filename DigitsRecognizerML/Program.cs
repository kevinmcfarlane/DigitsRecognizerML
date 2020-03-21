using System;
using System.Linq;
using DigitsRecognizerMLML.Model;
using Microsoft.ML;

namespace DigitsRecognizerML
{
    class Program
    {
        //Dataset to use for predictions 
        private const string DATA_FILEPATH = @"C:\Users\Kevin\source\repos\DigitsRecognizerML\DigitsRecognizerML\Data\trainingsample.csv";
        private const string VALIDATION_FILEPATH = @"C:\Users\Kevin\source\repos\DigitsRecognizerML\DigitsRecognizerML\Data\validationsample.csv";

        static void Main(string[] args)
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

        // Change this code to create your own sample data
        #region CreateSingleDataSample
        // Method to load single row of dataset to try a single prediction
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
                .Skip(2)
                .First();
            return sampleForPrediction;
        }
        #endregion
    }
}
