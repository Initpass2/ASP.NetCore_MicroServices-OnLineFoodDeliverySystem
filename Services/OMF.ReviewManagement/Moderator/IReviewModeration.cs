using System.Threading.Tasks;

namespace OMF.ReviewManagement.Moderator
{
    public interface IReviewModeration
    {
        Task<string> TextModerationReviewAsync(string reviewText);
        // suvankar : 
        // There r diffrent types of Moderation service avaible
        // Video/Image 
        
    }
}
