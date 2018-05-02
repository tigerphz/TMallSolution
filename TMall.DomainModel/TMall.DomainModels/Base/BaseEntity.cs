using System;
using TMall.Infrastructure.Utility;

namespace TMall.DomainModels.Base
{
    public abstract class BaseEntity : IEntity
    {
        public BaseEntity()
        {
            CreateDate = DateTime.Now;
            ModifyDate = DateTime.Now;
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            var entity = obj as BaseEntity;
            if (entity == null) return false;
            if (!this.GetType().Equals(obj.GetType())) return false;
            if (!SysNo.Equals(entity.SysNo)) return false;
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public Guid SysNo { get; set; }

        //[Timestamp]
        //public byte[] RowVersion { get; set; }

        public Guid? CreateUserSysNo { get; set; }

        public DateTime CreateDate { get; set; }

        public Guid? ModifyUserSysNo { get; set; }

        public DateTime ModifyDate { get; set; }

        public string CreateUserName { get; set; }

        public string ModifyUserName { get; set; }
    }
}
