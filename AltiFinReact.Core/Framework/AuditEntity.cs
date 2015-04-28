using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AltiFinReact.Core.Framework
{
    public class AuditEntity : BaseEntity
    {
        /// <summary>
        /// Gets or sets DateCreated of the Entity.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public virtual DateTime? DateCreated { get; set; }

        /// <summary>
        /// Gets or sets CreatedByGuid of the Entity.
        /// </summary>
        public virtual int? CreatedById { get; set; }

        /// <summary>
        /// Gets or sets DateModified of the Entity.
        /// </summary>
        public virtual DateTime? DateModified { get; set; }

        /// <summary>
        /// Gets or sets ModifiedByGuid of the Entity.
        /// </summary>
        public virtual int? ModifiedById { get; set; }

    }
}
