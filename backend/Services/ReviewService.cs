using bioinsumos_asproc_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace bioinsumos_asproc_backend.Services
{
    public class ReviewService : IReviewService
    {
        private readonly BioinsumosContext _dbcontext;

        public ReviewService(BioinsumosContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<ReviewDtoResponse> CreateReview(ReviewDtoPostRequest reviewDtoPostRequest)
        {
            ReviewDtoResponse res = new()
            {
                Result = true,
                Message = "",
                Review1 = null
            };

            try
            {
                Review review = new()
                {
                    CourseId = reviewDtoPostRequest.CourseId,
                    Review1 = reviewDtoPostRequest.Review,
                    Score = reviewDtoPostRequest.Score,
                    Name = reviewDtoPostRequest.Name,
                    Email = reviewDtoPostRequest.Email,
                    Phone = reviewDtoPostRequest.Phone
                };

                _dbcontext.Reviews.Add(review);
                await _dbcontext.SaveChangesAsync();

                res.Message = "Ok";
                res.Review1 = review;
                return res;
            }
            catch (Exception ex)
            {
                res.Result = false;
                res.Message = ex.Message;
                return res;
            }
        }

        public async Task<bool> DeleteReview(Review review)
        {
            try
            {
                review.Status = false;
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Review> GetReviewById(uint reviewId)
        {
            try 
            {
                return await _dbcontext.Reviews.Where(c =>
                    c.ReviewId == reviewId &&
                    c.Status == true
                ).FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                return await Task.FromException<Review>(null);
            }
        }

        public async Task<List<Review>> GetReviews()
        {
            try
            {
                return await _dbcontext.Reviews.Where(c => c.Status == true).ToListAsync();
            }
            catch (Exception)
            {
                return await Task.FromException<List<Review>>(null);
            }
        }

        public async Task<ReviewDtoResponse> UpdateReview(ReviewDtoPutRequest reviewDtoPutRequest, Review review)
        {
            ReviewDtoResponse res = new()
            {
                Result = true,
                Message = "",
                Review1 = null
            };

            try
            {
                review.Response = reviewDtoPutRequest.Response;
                await _dbcontext.SaveChangesAsync();
                res.Message = "Review answered succesfull.";
                res.Review1 = review;
                return res;
            }
            catch (Exception ex)
            {
                res.Result = false;
                res.Message = ex.Message;
                return res;
            }
        }
    }
}