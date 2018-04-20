using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PLFApp.Server.Core
{
    [Table("t_Goods")]
    public class Goods:BaseFullEntity
    {
        [Required]
        [MaxLength(30)]
        public string GoodsName { get; set; }

        [Required]
        [MaxLength(255)]
        public string GoodsPictureUrl { get; set; }

        [Required]
        public decimal GoodsPrice { get; set; }

        [Required]
        public int Inventory { get; set; }

        private int _goodsstate = (int)GoodsStateEnum.未发布;
        public int GoodsState { get => _goodsstate; set => _goodsstate = value; }

        [NotMapped]
        public GoodsStateEnum GoodsStateEnum { get => (GoodsStateEnum)_goodsstate; }
    }
}
