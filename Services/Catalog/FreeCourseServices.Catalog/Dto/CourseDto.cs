﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourseServices.Catalog.Dto
{
    public class CourseDto
    {

        public string Id { get; set; }

        public string Name { get; set; }


        public string Price { get; set; }

        public string Picture { get; set; }

        public DateTime CreatedTime { get; set; }

        public string UserId { get; set; }

        public FeatureDto Feature { get; set; }

        public string CategoryId { get; set; }

        public string Description { get; set; }

        public CategoryDto Category { get; set; }
    }
}
