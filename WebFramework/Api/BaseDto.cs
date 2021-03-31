using Entities;
using Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace WebFramework.Api
{
    public abstract class BaseDto<TKey> 
    {
        [Display(Name = "ردیف")]
        public TKey Id { get; set; }

    }
}
