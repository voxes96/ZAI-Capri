using System;
using System.Threading.Tasks;
using Capri.Web.ViewModels.Course;

namespace Capri.Services.Courses
{
    public interface ICourseUpdater
    {
        Task<IServiceResult<CourseViewModel>> Update(int id, CourseRegistration newData);
    }
}