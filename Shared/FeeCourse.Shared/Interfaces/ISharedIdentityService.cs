using System;
using System.Collections.Generic;
using System.Text;

namespace FeeCourse.Shared.Interfaces
{
    public interface ISharedIdentityService
    {
        public string GetUserId { get; }
    }
}
