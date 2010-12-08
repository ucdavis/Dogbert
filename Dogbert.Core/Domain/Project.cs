using System;
using System.Collections.Generic;
using NHibernate.Validator.Constraints;
using UCDArch.Core.DomainModel;
using UCDArch.Core.NHibernateValidator.Extensions;

namespace Dogbert.Core.Domain
{
    public class Project : DomainObject
    {
        public virtual IList<Requirement> Requirements { get; set; }
        public virtual IList<ProjectText> ProjectTexts { get; set; }
        public virtual IList<RequirementCategory> RequirementCategories { get; set; }
        public virtual IList<UseCase> UseCases { get; set; }
        public virtual IList<ChangeLog> ChangeLog { get; set; }
        public virtual ICollection<ProjectFile> ProjectFiles { get; set; }

        [Required]
        [Length(50)]
        public virtual string Name { get; set; }
        public virtual int Priority { get; set; }
        public virtual DateTime? Deadline { get; set; }

        [Required]
        [Length(100)]
        public virtual string Contact { get; set; }
        [Required]
        [Length(50)]
        public virtual string ContactEmail { get; set; }
        [Required]
        [Length(50)]
        public virtual string Unit { get; set; }
        
        public virtual int Complexity { get; set; }
        public virtual DateTime? ProjectedStart { get; set; }
        public virtual DateTime? ProjectedEnd { get; set; }
        public virtual DateTime? BeginDate { get; set; }
        public virtual DateTime? EndDate { get; set; }
        public virtual DateTime? DateAdded { get; set; }
        public virtual DateTime? LastModified { get; set; }
        public virtual StatusCode StatusCode { get; set; }
        public virtual User ProjectManager { get; set; }
        public virtual User LeadProgrammer { get; set; }
        public virtual ProjectType ProjectType { get; set; }

        public virtual int DesignerOrder { get; set; }
        public virtual bool DesignerShow { get; set; }

        
        public virtual string ProjectedStartString
        {
            get
            {
                return String.Format("{0:MM/dd/yyyy}", ProjectedStart).ToString();
            }
        }

        public virtual string ProjectedEndString
        {
            get
            {
                return String.Format("{0:MM/dd/yyyy}", ProjectedEnd).ToString();
            }
        }


        public virtual int? Duration {
            
                   
            get{
                DateTime start, end;  //to cast nullable date
                if ((ProjectedStart.HasValue) && (ProjectedEnd.HasValue))
                {
                    start = (DateTime)ProjectedStart;
                    end = (DateTime)ProjectedEnd;
                    return end.Month -  start.Month;
                }
                else
                    return null;
            }
        }

        public virtual string ProjectManagerString
        {
            get
            {
                if (ProjectManager == null)
                {
                    return ("N/A");
                }
                else return ProjectManager.FullName;
            }

        }


        public virtual string LeadProgrammerString
        {   get
            {
                if (LeadProgrammer == null)
                {
                    return("N/A");
                }
                else return LeadProgrammer.FullName;
            }

        }


        public virtual string StartDateString{
            get{
                if (BeginDate.HasValue){
                    return BeginDate.Value.ToShortDateString();
                }
                if (ProjectedStart.HasValue)
                {
                    return ProjectedStart.Value.ToShortDateString();
                    //return string.Format("{0} (proj.)", start.ToShortDateString());
                }
                return "N/A";
            }
        }

        public virtual string EndDateString
        {
            get{
                if (EndDate.HasValue){
                    return EndDate.Value.ToShortDateString();
                }
                if (ProjectedEnd.HasValue)
                {
                    return ProjectedEnd.Value.ToShortDateString();
                    //return string.Format("{0} (proj.)", ProjectedEnd.ToShortDateString());
                }
                return "N/A";
            }
        }
    
        public Project()
        {
            //create blank list for Ilists.
            ProjectTexts = new List<ProjectText>();
            RequirementCategories = new List<RequirementCategory>();
            Requirements = new List<Requirement>();
            UseCases = new List<UseCase>();

            DesignerOrder = 99;
            DesignerShow = true;
        }

        public virtual void AddUseCase(UseCase UseCase)
        {
            UseCase.Project = this;
            this.UseCases.Add(UseCase);
        }

        public virtual void AddRequirement(Requirement Requirement)
        {
            Requirement.Project = this;
            this.Requirements.Add(Requirement);
        }

        public virtual void AddRequirementCategory(RequirementCategory RequirementCategory)
        {
            RequirementCategory.Project = this;
            this.RequirementCategories.Add(RequirementCategory);
        }

  
        public virtual void AddProjectTexts(ProjectText ProjectText)
        {
            ProjectText.Project = this;
            this.ProjectTexts.Add(ProjectText);
        }

    }
}
