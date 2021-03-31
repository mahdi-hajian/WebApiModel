using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Common
{
    // برای کلاس هایی که میخاهیم کلیدش ایدی نباشد
    // کار اصلی این اینه که هرکلاسی که از این ارثبری کند برایش جدول ساخته شود و دی بی ست<> برایش نوشته شود
    public interface IEntity
    {
    }

    public abstract class BaseEntity<TKey> : IEntity
    {
        public TKey Id { get; set; }
    }
    public abstract class BaseEntity : BaseEntity<int>
    {
    }
}
