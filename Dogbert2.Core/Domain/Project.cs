using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Dogbert2.Core.Resources;
using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;
using System.Linq;

namespace Dogbert2.Core.Domain
{
    public class Project : DomainObject
    {
        public Project()
        {
            SetDefaults();
        }

        private void SetDefaults()
        {
            Hide = false;

            DateAdded = DateTime.Now;
            LastUpdate = DateTime.Now;

            ProjectWorkgroups = new List<ProjectWorkgroup>();
            ProjectTerms = new List<ProjectTerm>();
            ProjectSections = new List<ProjectSection>();
            Files = new List<File>();
            RequirementCategories = new List<RequirementCategory>();
            Requirements = new List<Requirement>();
            UseCases = new List<UseCase>();
        }

        [Required]
        [StringLength(50)]
        public virtual string Name { get; set; }
        [Required]
        [Display(Name="Project Type")]
        public virtual ProjectType ProjectType { get; set; }
        public virtual PriorityType Priority { get; set; }
        [Range(0, 10)]
        [DataType(DataTypes.Range)]
        public virtual int? Complexity { get; set; }
        public virtual DateTime? Deadline { get; set; }
        [Display(Name="Projected Begin")]
        public virtual DateTime? ProjectedBegin { get; set; }
        public virtual DateTime? Begin { get; set; }
        [Display(Name = "Projected End")]
        public virtual DateTime? ProjectedEnd { get; set; }
        public virtual DateTime? End { get; set; }
        [Display(Name = "Project Manager")]
        public virtual Worker ProjectManager { get; set; }
        [Display(Name = "Lead Programmer")]
        public virtual Worker LeadProgrammer { get; set; }

        // Client information
        [Required]
        [StringLength(100)]
        public virtual string Contact { get; set; }
        [StringLength(100)]
        [DataType(DataType.EmailAddress)]
        [Display(Name="Contact Email")]
        public virtual string ContactEmail { get; set; }
        [Required]
        [StringLength(50)]
        public virtual string Unit { get; set; }

        // tracking information
        [Required]
        [Display(Name="Status Code")]
        public virtual StatusCode StatusCode { get; set; }
        public virtual DateTime DateAdded { get; set; }
        public virtual DateTime LastUpdate { get; set; }
        public virtual bool Hide { get; set; }

        // bags
        public virtual IList<ProjectWorkgroup> ProjectWorkgroups { get; set; }
        public virtual IList<ProjectTerm> ProjectTerms { get; set; }
        public virtual IList<ProjectSection> ProjectSections { get; set; }
        public virtual IList<File> Files { get; set; }
        public virtual IList<RequirementCategory> RequirementCategories { get; set; }
        public virtual IList<Requirement> Requirements { get; set; }
        public virtual IList<UseCase> UseCases { get; set; }

        #region Non-Mapped fields

        public virtual string WorkgroupNames
        {
            get { 
                var wgs = ProjectWorkgroups.Select(a => a.Workgroup.Name);
                return wgs.Count() > 0 ? string.Join(", ", wgs) : "n/a";
            }
        }

        public virtual string WorkgroupDepts
        {
            get { 
                var wgs = ProjectWorkgroups.Select(a => a.Workgroup.Department.Name).Distinct();
                return wgs.Count() > 0 ? string.Join(", ", wgs) : "n/a";
            }
        }
        #endregion

        #region Methods
        public virtual void AddWorkgroup(Workgroup workgroup)
        {
            var pworkgroup = new ProjectWorkgroup() {Project = this, Workgroup = workgroup};
            ProjectWorkgroups.Add(pworkgroup);
        }
        #endregion
    }

    public class ProjectMap : ClassMap<Project>
    {
        public ProjectMap()
        {
            Id(x => x.Id);

            Map(x => x.Name);
            References(x => x.ProjectType);
            References(x => x.Priority).Column("Priority");
            Map(x => x.Complexity);
            Map(x => x.Deadline);
            Map(x => x.ProjectedBegin);
            Map(x => x.Begin).Column("`Begin`");
            Map(x => x.ProjectedEnd);
            Map(x => x.End).Column("`End`");
            References(x => x.ProjectManager).Column("ProjectManagerId");
            References(x => x.LeadProgrammer).Column("LeadProgrammerId");

            Map(x => x.Contact);
            Map(x => x.ContactEmail);
            Map(x => x.Unit);

            References(x => x.StatusCode).Column("StatusCode");
            Map(x => x.DateAdded);
            Map(x => x.LastUpdate);
            Map(x => x.Hide);

            HasMany(x => x.ProjectWorkgroups).Inverse().Cascade.AllDeleteOrphan();
            HasMany(x => x.ProjectTerms).Inverse().Cascade.AllDeleteOrphan();
            HasMany(x => x.ProjectSections).Inverse().Cascade.AllDeleteOrphan();
            HasMany(x => x.Files).Inverse().Cascade.AllDeleteOrphan();
            HasMany(x => x.RequirementCategories).Inverse().Cascade.AllDeleteOrphan();
            HasMany(x => x.Requirements).Inverse().Cascade.AllDeleteOrphan();
            HasMany(x => x.UseCases).Inverse().Cascade.AllDeleteOrphan();
        }
    }
}
