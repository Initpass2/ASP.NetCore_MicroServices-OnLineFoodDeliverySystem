using Newtonsoft.Json;
using OMF.ReviewManagement.Domain;
using OMF.ReviewManagement.Services;
using System;
using System.Threading.Tasks;

namespace OMF.ReviewManagement.Moderator
{
    public class ReviewValidation : IReviewValidation
    {
        private readonly IReviewModeration _reviewModeration;
        private readonly IReviewServices _service;
        public ReviewValidation(IReviewModeration reviewModeration, IReviewServices reviewServices)
        {
            _reviewModeration = reviewModeration;
            _service = reviewServices;
        }

        public async Task<bool> ValidateReview(Review reviewModel, string operationType)
        {
            try
            {
                // calling azure congnative service
                var response = await _reviewModeration.TextModerationReviewAsync(reviewModel.ReviewText);
                // Deserialize api response
                var apiresponse = JsonConvert.DeserializeObject<CongnativeApiResponseModel>(response);

                reviewModel.ModeratorTrackingId = apiresponse.TrackingId;

                // suvankar : 9/5/2019
                // Here I am updating review test with Autocorrectedtext 
                // Based on business requirement we can update original reviewtext value 

                reviewModel.ReviewText = apiresponse.AutoCorrectedText;

                //Validating response msg
                if (!apiresponse.Classification.ReviewRecommended)
                {
                    reviewModel.IsManualReviewRequired = false;
                    await PerformOperation(reviewModel, operationType);
                }
                else
                {
                    // Suvankar : 8/22/2019
                    // based on cognative api's response , customer's review needs to be manually reviewd.
                    // We can integrate human review flow here 
                    // In that case, we have to create a team/sub team into review tools dashboard 
                    // Create a new job /workflow/
                    // Use apis to get the human rewiew result 
                    // I am not going into that part in details for this assignment
                    // so, I am saving this data into database for futher human review 

                    reviewModel.IsManualReviewRequired = true;

                    await PerformOperation(reviewModel, operationType);
                }

                return apiresponse.Classification.ReviewRecommended == false ? true : false;
            }
            catch 
            {
                throw;
            }
        }

        private async Task PerformOperation(Review review, string operationType)
        {
            switch (operationType.ToLower())
            {
                case "add":
                    await _service.AddReviewAsync(review);
                    break;
                case "update":
                    await _service.UpdateReviewAsync(review);
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
