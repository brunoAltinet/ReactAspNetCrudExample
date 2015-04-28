using System;

namespace AltiFinReact.Core.Framework
{
    public class BaseEntity
    {
        protected bool Equals(BaseEntity other)
        {
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public int? Id { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return (Id!=null && GetType() == obj.GetType() && ((BaseEntity)obj).Id == Id);
        }


        public virtual int CompareTo(object obj)
        {
            return Equals(obj)
                ? 0
                : obj is BaseEntity
                ? Id.GetValueOrDefault().CompareTo(((BaseEntity) obj).Id.GetValueOrDefault()):
                String.Compare(ToString(), obj.ToString(), StringComparison.Ordinal);
        }

        public virtual int CompareTo(BaseEntity other)
        {
            return CompareTo((object)other);
        }
    }
}
