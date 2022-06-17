using AutoMapper;
using FeeCourse.Shared.Dtos;
using FreeCourseServices.Catalog.Dto;
using FreeCourseServices.Catalog.Interfaces;
using FreeCourseServices.Catalog.Models;
using FreeCourseServices.Catalog.Settings;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourseServices.Catalog.Services
{
    public class CourseService: ICourseService
    {
        private readonly IMongoCollection<Course> _courseCollection;
        private readonly IMongoCollection<Category> _categoryCollection;

        private readonly IMapper _mapper;

        public CourseService( IMapper mapper, IDatabaseSettings databaseSettings)
        {
            // _categoryCollection = categoryCollection;
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _courseCollection = database.GetCollection<Course>(databaseSettings.CourseCollectionName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);


            _mapper = mapper;
        }

        public async Task<Response<List<CourseDto>>> GetAllAsync()
        {
            var courses = await _courseCollection.Find(course => true).ToListAsync();
            if (courses.Any()) 
            {
              foreach(var course in courses) 
                {
                    course.Category = await _categoryCollection.Find<Category>(x => x.Id == course.CategoryId).FirstAsync();
                }
            }
            else 
            {
                courses = new List<Course>();
            }
            return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses),200);
        }

        public async Task<Response<CourseDto>> GetByIdAsync(string Id)
        {
            var course = await _courseCollection.Find(x=>x.Id==Id).FirstOrDefaultAsync();
            if (course==null)
            {
                return Response<CourseDto>.Fail("Course Not Found",404);
            }
            course.Category = await _categoryCollection.Find<Category>(x=>x.Id==course.CategoryId).FirstAsync();
            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(course), 200);
        }

        public async Task<Response<List<CourseDto>>> GetByUserIdAsync(string userId)
        {
            var courses = await _courseCollection.Find(x => x.UserId == userId).ToListAsync();
            if (courses.Any())
            {
                foreach (var course in courses)
                {
                    course.Category = await _categoryCollection.Find<Category>(x => x.Id == course.CategoryId).FirstAsync();
                }
            }
            else
            {
                courses = new List<Course>();
            }
            return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
        }

        public async Task<Response<NoContent>> UpdateAsync( CourseUpdateDto courseUpdateDto) 
        {
            var updateCourse = _mapper.Map<Course>(courseUpdateDto);

            var result = await _courseCollection.FindOneAndReplaceAsync(x=>x.Id== courseUpdateDto.Id,updateCourse);

            if(result== null) 
            {
                return Response<NoContent>.Fail("Course Not Found", 404);
            }

            return Response<NoContent>.Success(204); 
        }

        public async Task<Response<NoContent>> DeleteAsync(string Id) 
        {
            var result = await _courseCollection.DeleteOneAsync(x => x.Id == Id);
            if(result.DeletedCount>0)
            {
                return Response<NoContent>.Success(204);
            }
            else
            {
                return Response<NoContent>.Fail("Course Not Found",404);
            }


        }

        public async Task<Response<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto)
        {
            var course = _mapper.Map<Course>(courseCreateDto);

            await _courseCollection.InsertOneAsync(course);

            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(course), 200);
        }
    }
}
