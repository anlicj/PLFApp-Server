using System;
using System.Collections.Generic;
using System.Text;

namespace PLFApp.Service.Dto
{
    public class HomeTopScrollImage
    {
        /// <summary>
        /// 图片路径
        /// </summary>
        public string ImageSrc { get; set; }
        /// <summary>
        /// 图片链接
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// 鼠标悬停标题
        /// </summary>
        public string ImageTitle { get; set; }
        /// <summary>
        /// 滚动图序号
        /// </summary>
        public int SortCode { get; set; }
    }
}
