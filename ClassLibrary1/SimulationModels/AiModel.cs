using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{

   
    public class Predizione
    {
        [ColumnName("Score")]
        public int Rate { get; set; }
        [ColumnName("Lista")]
        public List<SIRStat> Stats { get; set; } = new List<SIRStat>();
    }
    public class PredictValue
    {
        public Predizione Prediction { get; set; }
        public void Previsione(List<SIRStat> sir)
        {
            //Create MLContext
            MLContext mlContext = new MLContext();

            DataViewSchema predictionPipelineSchema;
            ITransformer predictionPipeline = mlContext.Model.Load("model.zip", out predictionPipelineSchema);
            PredictionEngine<List<SIRStat>, Predizione> predictionEngine = mlContext.Model.CreatePredictionEngine<List<SIRStat>, Predizione>(predictionPipeline);
            List<SIRStat> inputData = sir;
            // Get Prediction
            Prediction = predictionEngine.Predict(inputData);
        }
    }
}
