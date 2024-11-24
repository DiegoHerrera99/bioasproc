using bioinsumos_asproc_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace bioinsumos_asproc_backend.Services
{
    public class CourseService : ICourseService
    {
        private readonly BioinsumosContext _dbcontext;
        private readonly IAWSImageService _AWSImageService;
        private readonly string _S3Bucket;

        public CourseService(BioinsumosContext dbcontext, IAWSImageService aWSImageService, IConfiguration config)
        {
            _dbcontext = dbcontext;
            _AWSImageService = aWSImageService;
            _S3Bucket = config.GetValue<string>("AWS:S3BucketCourseFolder");
        }

        public async Task<Course> CreateCourse(Course course, IFormFile file)
        {
            try
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    stream.Position = 0;
                    bool uploadToS3res = await _AWSImageService.UploadImageToS3Async(fileName, _S3Bucket, file.ContentType, stream);
                }

                course.ImgPath = fileName;
                _dbcontext.Courses.Add(course);
                await _dbcontext.SaveChangesAsync();
                return course;
            }
            catch (Exception)
            {
                return await Task.FromException<Course>(null);
            }
        }

        public async Task<bool> DeleteCourse(Course course)
        {
            try
            {
                if (course.ImgPath != "") await _AWSImageService.DeleteImageFromS3Async(course.ImgPath, _S3Bucket);
                course.Status = false;
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Course> GetCourseById(uint courseId)
        {
            try 
            {
                return await _dbcontext.Courses
                    .Where(c =>
                        c.CourseId == courseId &&
                        c.Status == true
                    )
                    .Include(c => 
                        c.Reviews.Where(r => r.Status == true)
                    )
                    .Include(c => 
                        c.Pdfs.Where(p => p.Status == true)
                    )
                    .Include(c => 
                        c.Videos.Where(v => v.Status == true)
                    )
                    .FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                return await Task.FromException<Course>(null);
            }
        }

        public async Task<List<Course>> GetCourses()
        {
            try
            {
                return await _dbcontext.Courses
                    .Where(c => c.Status == true)
                    .Include(c => 
                        c.Reviews.Where(r => r.Status == true)
                    )
                    .Include(c => 
                        c.Pdfs.Where(p => p.Status == true)
                    )
                    .Include(c => 
                        c.Videos.Where(v => v.Status == true)
                    )
                    .ToListAsync();
            }
            catch (Exception)
            {
                return await Task.FromException<List<Course>>(null);
            }
        }

        public async Task<Course> UpdateCourse(Course _course, Course course, IFormFile file = null)
        {
            try
            {
                if (file != null) 
                {
                    if (course.ImgPath == "") course.ImgPath = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    using (var stream = new MemoryStream())
                    {
                        await file.CopyToAsync(stream);
                        stream.Position = 0;
                        await _AWSImageService.UploadImageToS3Async(course.ImgPath, _S3Bucket, file.ContentType, stream);
                    }
                }
                if (_course.Name != null && _course.Name != course.Name) course.Name = _course.Name;
                if (_course.Description != null && _course.Description != course.Description) course.Description = _course.Description;
                await _dbcontext.SaveChangesAsync();
                return course;
            }
            catch (Exception)
            {
                return await Task.FromException<Course>(null);
            }
        }

        public string GetCourseCDN(string path)
        {
            return _AWSImageService.DownloadImageFromS3Async(path, _S3Bucket);
        }
    }
}