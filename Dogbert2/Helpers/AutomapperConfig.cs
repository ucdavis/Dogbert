using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace Dogbert2.Helpers
{
    public class AutomapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg => cfg.AddProfile<ViewModelProfile>());
        }
    }


    public class ViewModelProfile : Profile
    {
        protected override void Configure()
        {
        }
    }
}