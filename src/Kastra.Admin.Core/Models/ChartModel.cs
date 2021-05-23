using System.Collections.Generic;

namespace Kastra.Admin.Core.Models
{
    public class ChartModel
    {
        public List<string> Labels { get; set; }
        public List<DataSet> Datasets { get; set; }
    }

    public class DataSet
    {
        public string Label { get; set; }
        public string FillColor { get; set; } = "rgb(25, 151, 198,0.5)";
        public string StrokeColor { get; set; } = "rgb(25, 151, 198,0.8)";
        public string HighlightFill { get; set; } = "rgb(25, 151, 198,0.75)";
        public string HighlightStroke { get; set; } = "rgb(25, 151, 198,1)";

        public List<int> Data { get; set; } = new List<int>();
    }
}
