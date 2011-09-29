using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace Dogbert2.Core.Domain
{
    public class ChangeRequest : DomainObject
    {
        public ChangeRequest()
        {
            SetDefaults();
        }

        private void SetDefaults()
        {
            Pending = true;
            RequestedDate = DateTime.Now;
        }

        [StringLength(200)]
        [Required]
        public virtual string Name { get; set; }
        [DataType(DataType.MultilineText)]
        public virtual string Description { get; set; }
        public virtual string AffectedRole { get; set; }
        [StringLength(50)]
        [Required]
        [DisplayName("Development Time")]
        public virtual string TimeToDevelop { get; set; }

        [StringLength(100)]
        [Required]
        [DisplayName("Requested By")]
        public virtual string RequestedBy { get; set; }
        public virtual DateTime RequestedDate { get; set; }

        public virtual bool? Approved { get; set; }
        public virtual bool Pending { get; set; }

        public virtual Project Project { get; set; }

        public virtual string ChangeRequestStatus
        {
            get {

                if (Pending) return "Pending";
                else if (Approved.HasValue)
                {
                    if (Approved.Value) return "Approved";
                    else return "Denied";
                }

                return "Pending";
            }
        }
    }

    public class ChangeRequestMap : ClassMap<ChangeRequest>
    {
        public ChangeRequestMap()
        {
            Id(x => x.Id);

            Map(x => x.Name);
            Map(x => x.Description);
            Map(x => x.AffectedRole);
            Map(x => x.TimeToDevelop);
            Map(x => x.RequestedBy);
            Map(x => x.RequestedDate);
            Map(x => x.Approved);
            Map(x => x.Pending);
            References(x => x.Project);
        }
    }
}
