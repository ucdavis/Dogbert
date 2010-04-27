using System;
using Dogbert.Core.Abstractions;
using NHibernate.Validator.Constraints;
using UCDArch.Core.DomainModel;
using UCDArch.Core.NHibernateValidator.Extensions;

namespace Dogbert.Core.Domain
{
    public class ProjectFile : DomainObject
    {
        public ProjectFile()
        {
            SetDefaults();
        }
        public ProjectFile(string fileName)
        {
            FileName = fileName;
            SetDefaults();
        }

        private void SetDefaults()
        {
            DateAdded = SystemTime.Now();
            DateChanged = SystemTime.Now();
        }


        [Required]
        [Length(200)]
        public virtual string FileName { get; set; }

        [NotNull]
        public virtual FileType Type { get; set; }                

        [NotNull]
        public virtual Project Project { get; set; }
        public virtual DateTime DateAdded { get; set; }
        public virtual DateTime DateChanged { get; set; }
        [Required]
        public virtual string FileContentType { get; set; }
        [NotNull]
        public virtual byte[] FileContents { get; set; }


    }
}