using Dapper;
using FeeCourse.Shared.Dtos;
using FreeCourse.Services.Dsicount.Interfaces;
using FreeCourse.Services.Dsicount.Model;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.Services.Dsicount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IConfiguration _configuration;

        private readonly IDbConnection _dbConnection;

        public DiscountService(IConfiguration configuration)
        {
            _configuration = configuration;

            _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
        }

        public async Task<Response<List<Discount>>> GetAll()
        {
            var discount = await _dbConnection.QueryAsync<Model.Discount>("Select * from discount");
            return Response<List<Model.Discount>>.Success(discount.ToList(),200);
        }

        public async Task<Response<Discount>> GetById(int id)
        {
            var discount = (await _dbConnection.QueryAsync<Model.Discount>("Select * from discount where id = @Id", new { Id = id })).SingleOrDefault();
            if(discount == null)
            {
                return Response<Model.Discount>.Fail("Discount Not Found!",404);
            }

            return Response<Model.Discount>.Success(discount,200);
        }

        public async Task<Response<NoContent>> Save(Discount discount)
        {
            var  status = await _dbConnection.ExecuteAsync("INSERT INTO  discount (userid,rate,code) VALUES (@UserId,@Rate,@Code)",discount);
            if(status >0)
            {
                return Response<NoContent>.Success(204);
            }
            return Response<NoContent>.Fail("an error occured while adding process", 500);

        }

        public async Task<Response<NoContent>> Update(Discount discount)
        {
            var status = await _dbConnection.ExecuteAsync("update discount set userid=@UserId,code=@Code,rate=@Rate where id = @Id", new { Id = discount.Id, UserId = discount.UserId, Code = discount.Code, Rate = discount.Rate });
            if(status>0)
            {
                return Response<NoContent>.Success(204);
            }
            return Response<NoContent>.Fail("Discount Not Found",404);
        }

        public async Task<Response<NoContent>> Delete(int id)
        {
            var status = await _dbConnection.ExecuteAsync("delete from discount where id =@Id",new {Id=id });
            if (status > 0)
            {
                return Response<NoContent>.Success(204);
            }
            return Response<NoContent>.Fail("Discount Not Found", 404);
        }

        public async Task<Response<Discount>> GetByCodeAndUserId(string code, string userId)
        {
            var discounts = await _dbConnection.QueryAsync<Model.Discount>("select * from discount where userid =@UserId and code = @Code", new { UserId = userId,Code=code});
            var hasDiscount = discounts.FirstOrDefault();
            if (hasDiscount == null)
            {
                return Response<Model.Discount>.Fail("Discount Not Found",404);
            }
            return Response<Model.Discount>.Success(hasDiscount, 200);
        }
    }
}
