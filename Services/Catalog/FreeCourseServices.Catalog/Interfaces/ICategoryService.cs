using FeeCourse.Shared.Dtos;
using FreeCourseServices.Catalog.Dto;
using FreeCourseServices.Catalog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourseServices.Catalog.Interfaces
{
    public interface ICategoryService
    {

        Task<Response<List<CategoryDto>>> GetAllAsync();

        Task<Response<CategoryDto>> CreateAsync(Category category);

        Task<Response<CategoryDto>> GetByIdAsync(string id);
      
    }
}
