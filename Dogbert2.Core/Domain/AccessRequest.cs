using System;
using System.ComponentModel.DataAnnotations;
using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace Dogbert2.Core.Domain
{
    public class AccessRequest : DomainObject
    {
        public AccessRequest()
        {
            SetDefaults();
        }

        private void SetDefaults()
        {
            DateCreated = DateTime.Now;
            Pending = true;
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
        [Display(Name= "Last Name")]
        public virtual string LastName { get; set; }
        [Required]
        public virtual Department Department { get; set; }
        [Required]
        [StringLength(50)]
        public virtual string Email { get; set; }
        [Display(Name="Other Users")]
        [DataType(DataType.MultilineText)]
        public virtual string OtherUsers { get; set; }
        public virtual DateTime DateCreated { get; set; }
        public virtual bool Pending { get; set; }

    }

    public class AccessRequestMap : ClassMap<AccessRequest>
    {
        public AccessRequestMap()
        {
            Id(x => x.Id);

            Map(x => x.LoginId);
            Map(x => x.FirstName);
            Map(x => x.LastName);
            References(x => x.Department);
            Map(x => x.Email);
            Map(x => x.OtherUsers);
            Map(x => x.DateCreated);
            Map(x => x.Pending);
        }
    }
}
