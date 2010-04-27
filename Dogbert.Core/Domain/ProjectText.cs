using System;
using System.Collections.Generic;
using NHibernate.Validator.Constraints;
using UCDArch.Core.DomainModel;
using UCDArch.Core.NHibernateValidator.Extensions;

namespace Dogbert.Core.Domain
{
    public class ProjectText : DomainObject
    {
        [Required]
        public virtual string Text { get; set; }
        [Required]
        public virtual TextType TextType { get; set; }
        [Required]
        public virtual Project Project { get; set; }

   
    }
}
