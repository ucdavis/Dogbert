using System;
using System.ComponentModel.DataAnnotations;
using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace Dogbert2.Core.Domain
{
    public class File : DomainObject
    {
        public File()
        {
            IsImage = false;

            DateCreated = DateTime.Now;
            LastUpdate = DateTime.Now;
        }

        [Required]
        public virtual byte[] Contents { get; set; }
        /// <summary>
        /// Used as caption for image if included into the SRS
        /// </summary>
        [StringLength(100)]
        public virtual string Caption { get; set; }
        
        // file meta data
        [Required]
        [StringLength(50)]
        public virtual string FileName { get; set; }
        [Required]
        [StringLength(5)]
        public virtual string Extension { get; set; }
        [Required]
        [StringLength(15)]
        public virtual string MimeType { get; set; }
        /// <summary>
        /// Is this a image to use in the body?
        /// </summary>
        public virtual bool IsImage { get; set; }
        /// <summary>
        /// Should the image be appended to the SRS?
        /// </summary>
        public virtual bool Append { get; set; }

        // tracking
        public virtual DateTime DateCreated { get; set; }
        public virtual DateTime LastUpdate { get; set; }

        public virtual Project Project { get; set; }
    }

    public class FileMap : ClassMap<File>
    {
        public FileMap()
        {
            Id(x => x.Id);

            Map(x => x.Contents);
            Map(x => x.Caption);

            Map(x => x.FileName);
            Map(x => x.Extension);
            Map(x => x.MimeType);
            Map(x => x.IsImage);

            Map(x => x.DateCreated);
            Map(x => x.LastUpdate);

            References(x => x.Project);
        }
    }
}
