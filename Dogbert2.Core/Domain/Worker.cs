using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;
using System.Linq;

namespace Dogbert2.Core.Domain
{
    public class Worker : DomainObject
    {
        public Worker()
        {
            SetDefaults();
        }

        public Worker(string login, string firstName, string lastName)
        {
            LoginId = login;
            FirstName = firstName;
            LastName = lastName;

            SetDefaults();
        }

        private void SetDefaults()
        {
            IsActive = true;

            WorkgroupWorkers = new List<WorkgroupWorker>();
            ProjectManagers = new List<Project>();
            LeadProgrammers = new List<Project>();
        }

        [Required]
        [StringLength(10)]
        [Display(Name="Login Id")]
        public virtual string LoginId { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name="First Name")]
        public virtual string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public virtual string LastName { get; set; }
        public virtual bool IsActive { get; set; }

        //public virtual IList<Workgroup> Workgroups { get; set; }
        public virtual IList<WorkgroupWorker> WorkgroupWorkers { get; set; }

        /// <summary>
        /// Projects this user is a project manager for
        /// </summary>
        public virtual IList<Project> ProjectManagers { get; set; }
        /// <summary>
        /// Projects this user is a lead programmer for
        /// </summary>
        public virtual IList<Project> LeadProgrammers { get; set; }

        #region Calculated Fields
        [Display(Name="Workgroups")]
        public virtual string WorkgroupNames { 
            get {
                var workgroups = WorkgroupWorkers.Select(a => a.Workgroup);
                var names = string.Join(", ", workgroups.Select(a => a.Name));
                return !string.IsNullOrWhiteSpace(names) ? names : "n/a";
            }
        }

        public virtual string FullName { 
            get { return string.Format("{0} {1}", FirstName, LastName); }
        }
        #endregion
    }

    public class WorkerMap : ClassMap<Worker>
    {
        public WorkerMap()
        {
            Id(x => x.Id);

            Map(x => x.LoginId);
            Map(x => x.FirstName);
            Map(x => x.LastName);
            Map(x => x.IsActive);

            //HasManyToMany(x => x.Workgroups)
            //    .ParentKeyColumn("WorkerId").ChildKeyColumn("WorkgroupId")
            //    .Table("WorkgroupsXWorkers").Cascade.SaveUpdate();
            HasMany(x => x.WorkgroupWorkers).Inverse();
            HasMany(x => x.ProjectManagers).KeyColumn("ProjectManagerId").Inverse();
            HasMany(x => x.LeadProgrammers).KeyColumn("LeadProgrammerId").Inverse();
        }
    }
}
