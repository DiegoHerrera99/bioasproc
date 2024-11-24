using bioinsumos_asproc_backend.Models;

namespace bioinsumos_asproc_backend.Services
{
    public interface ICourseService
    {
        Task<List<Course>> GetCourses(); //GET
        Task<Course> GetCourseById(uint courseId); //GET BY ID

        Task<Course> CreateCourse(Course course, IFormFile file); //POST

        Task<Course> UpdateCourse(Course _course, Course course, IFormFile file = null); //PUT

        Task<bool> DeleteCourse(Course course); //DELETE

        string GetCourseCDN(string path);
    }
}