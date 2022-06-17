using FeeCourse.Shared.Dtos;
using FreeCourseServices.Catalog.Dto;
using FreeCourseServices.Catalog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourseServices.Catalog.Interfaces
{
    public interface ICourseService
    {
        Task<Response<List<CourseDto>>> GetAllAsync();

        Task<Response<CourseDto>> GetByIdAsync(string Id);

        Task<Response<List<CourseDto>>> GetByUserIdAsync(string userId);

        Task<Response<NoContent>> UpdateAsync(CourseUpdateDto courseUpdateDto);

        Task<Response<NoContent>> DeleteAsync(string Id);

        Task<Response<CourseDto>> CreateAsync(CourseCreateDto category);


    }
}
