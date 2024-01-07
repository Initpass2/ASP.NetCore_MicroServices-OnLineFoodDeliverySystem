using Newtonsoft.Json;

namespace OMF.ReviewManagement.Moderator
{
    public  class CongnativeApiResponseModel
    {
        [JsonProperty("OriginalText")]
        public string OriginalText { get; set; }

        [JsonProperty("NormalizedText")]
        public string NormalizedText { get; set; }

        [JsonProperty("AutoCorrectedText")]
        public string AutoCorrectedText { get; set; }

        [JsonProperty("Misrepresentation")]
        public object Misrepresentation { get; set; }

        [JsonProperty("Classification")]
        public Classification Classification { get; set; }

        [JsonProperty("Language")]
        public string Language { get; set; }

        [JsonProperty("Terms")]
        public object Terms { get; set; }

        [JsonProperty("Status")]
        public Status Status { get; set; }

        [JsonProperty("TrackingId")]
        public string TrackingId { get; set; }
    }

    public  class Classification
    {
        [JsonProperty("ReviewRecommended")]
        public bool ReviewRecommended { get; set; }

        [JsonProperty("Category1")]
        public Category Category1 { get; set; }

        [JsonProperty("Category2")]
        public Category Category2 { get; set; }

        [JsonProperty("Category3")]
        public Category Category3 { get; set; }
    }

    public  class Category
    {
        [JsonProperty("Score")]
        public double Score { get; set; }
    }

    public  class Status
    {
        [JsonProperty("Code")]
        public long Code { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("Exception")]
        public object Exception { get; set; }
    }
}
