using FeeCourse.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.Services.Dsicount.Interfaces
{
    public interface IDiscountService
    {
        Task<Response<List<Model.Discount>>> GetAll();

        Task<Response<Model.Discount>> GetById(int Id);

        Task<Response<NoContent>> Save(Model.Discount discount);

        Task<Response<NoContent>> Update(Model.Discount discount);

        Task<Response<NoContent>> Delete(int id);

        Task<Response<Model.Discount>> GetByCodeAndUserId(string code ,string userId);

    }
}
