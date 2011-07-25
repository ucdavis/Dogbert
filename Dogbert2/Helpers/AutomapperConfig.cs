using AutoMapper;
using Dogbert2.Core.Domain;
using Dogbert2.Models;

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
                .ForMember(x => x.Id, x => x.Ignore())
                .ForMember(x => x.Begin, x => x.Ignore())
                .ForMember(x => x.End, x => x.Ignore())
                .ForMember(x => x.DateAdded, x => x.Ignore())
                .ForMember(x => x.LastUpdate, x => x.Ignore())
                .ForMember(x => x.ProjectWorkgroups, x => x.Ignore())
                .ForMember(x => x.ProjectTerms, x => x.Ignore())
                .ForMember(x => x.ProjectSections, x => x.Ignore())
                .ForMember(x => x.Files, x => x.Ignore())
                .ForMember(x => x.RequirementCategories, x => x.Ignore())
                .ForMember(x => x.Requirements, x => x.Ignore())
                .ForMember(x => x.UseCases, x => x.Ignore())
                .ForMember(x => x.ProjectManager, x => x.Ignore())
                .ForMember(x => x.LeadProgrammer, x => x.Ignore())
                ;

            CreateMap<Workgroup, Workgroup>()
                .ForMember(x => x.Id, x => x.Ignore())
                .ForMember(x => x.IsActive, x => x.Ignore())
                .ForMember(x => x.WorkgroupWorkers, x => x.Ignore())
                .ForMember(x => x.ProjectWorkgroups, x => x.Ignore());

            CreateMap<ProjectTerm, ProjectTerm>()
                .ForMember(x => x.Id, x => x.Ignore())
                .ForMember(x => x.Project, x => x.Ignore());

            CreateMap<SectionType, SectionType>();

            CreateMap<ProjectSection, ProjectSection>()
                .ForMember(x => x.Id, x => x.Ignore())
                .ForMember(x => x.DateCreated, x => x.Ignore())
                .ForMember(x => x.Project, x=> x.Ignore());

            CreateMap<FilePostModel, File>()
                .ForMember(x => x.Title, x => x.MapFrom(y => y.Title))
                .ForMember(x => x.Caption, x => x.MapFrom(y => y.Caption))
                .ForMember(x => x.Append, x => x.MapFrom(y => y.Append))
                .ForMember(x => x.IsImage, x => x.MapFrom(y => y.IsImage))
                .ForMember(x => x.FileName, x => x.MapFrom(y => y.File.FileName))
                .ForMember(x => x.ContentType, x => x.MapFrom(y => y.File.ContentType))
                .ForMember(x => x.Contents, x=> x.MapFrom(y => y.FileContents));

            CreateMap<File, File>()
                .ForMember(x => x.Id, x => x.Ignore())
                .ForMember(x => x.DateCreated, x => x.Ignore())
                .ForMember(x => x.IsImage, x => x.Ignore())
                .ForMember(x => x.ContentType, x => x.Ignore())
                .ForMember(x => x.Contents, x => x.Ignore())
                .ForMember(x => x.FileName, x => x.Ignore())
                .ForMember(x => x.Project, x => x.Ignore());

            CreateMap<Requirement, Requirement>()
                .ForMember(x => x.Id, x => x.Ignore())
                .ForMember(x => x.RequirementId, x => x.Ignore())
                .ForMember(x => x.Project, x => x.Ignore())
                .ForMember(x => x.DateAdded, x => x.Ignore());
        }
    }
}