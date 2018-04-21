using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PLFApp.Server.Core
{
    [Table("t_GoodsCategory")]
    public class GoodsCategory:BaseEntity
    {
        public GoodsCategory()
        {
            this.Goodses = new HashSet<Goods>();
        }

        [Required]
        [MaxLength(10)]
        public string GoodsCategoryName { get; set; }

        [Required]
        [MaxLength(255)]
        public string CategoryImageSrc { get; set; }

        [Required]
        public bool IsShowOnHomePage { get; set; }

        public virtual ICollection<Goods> Goodses { get; set; }
    }
}