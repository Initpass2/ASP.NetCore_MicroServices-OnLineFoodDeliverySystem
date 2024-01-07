using OMF.ReviewManagement.Domain;
using System.Threading.Tasks;

namespace OMF.ReviewManagement.Moderator
{
    public interface IReviewValidation
    {
        Task<bool> ValidateReview(Review reviewModel, string operationType);
    }
}
