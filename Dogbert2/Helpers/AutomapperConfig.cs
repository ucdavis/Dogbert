using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Dogbert2.Core.Domain;

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
            CreateMap<Project, Project>()
                .ForMember(x => x.Id, x=> x.Ignore())
                .ForMember(x => x.Begin, x=> x.Ignore())
                .ForMember(x => x.End, x => x.Ignore())
                .ForMember(x => x.DateAdded, x=> x.Ignore())
                .ForMember(x => x.LastUpdate, x=> x.Ignore())
                .ForMember(x => x.ProjectWorkgroups, x=> x.Ignore());
        }
    }
}