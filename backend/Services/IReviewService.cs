using bioinsumos_asproc_backend.Models;

namespace bioinsumos_asproc_backend.Services
{
    public interface IReviewService 
    {
        Task<List<Review>> GetReviews(); //GET
        Task<Review> GetReviewById(uint reviewId); //GET BY ID

        Task<ReviewDtoResponse> CreateReview(ReviewDtoPostRequest reviewDtoPostRequest); //POST

        Task<ReviewDtoResponse> UpdateReview(ReviewDtoPutRequest _reviewDtoPutRequest, Review review); //PUT

        Task<bool> DeleteReview(Review review); //DELETE
    }
}