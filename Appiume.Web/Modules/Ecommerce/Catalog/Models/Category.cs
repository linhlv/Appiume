using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appiume.Web.Ecommerce.Core;
using Appiume.Apm.Web.Data;

namespace Appiume.Web.Ecommerce.Catalog.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Category : IMultiStore, ITrackingObject<string>, IMultiTenancyObject, ISearchEngineFriendlyObject, ISortable, IAvin
    {
        /// <summary>
        /// 
        /// </summary>
        public string Avin { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public CategorySortOrder DisplaySortOrder { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public CategorySourceType SourceType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BannerImageUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int LatestProductCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CustomPageUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool CustomPageOpenInNewWindow { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool ShowInTopMenu { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Hidden { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TemplateName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PreContentColumnId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PostContentColumnId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool ShowTitle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Criteria { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CustomPageId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PreTransformDescription { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Keywords { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool CustomerChangeableSortOrder { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private string _rewriteUrl = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public CustomPageLayoutType CustomPageLayout { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RewriteUrl
        {
            get { return _rewriteUrl; }
            set { _rewriteUrl = value.Trim().ToLowerInvariant(); }
        }

        /// <summary>
        /// 
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Category()
        {
            this.MerchantId = 0;
            this.StoreId = 0;
            this.CreatedOnUtc = DateTime.UtcNow;
            this.ModifiedOnUtc = DateTime.UtcNow;
            this.ParentId = "0";
            this.Name = string.Empty;
            this.Description = string.Empty;
            this.DisplaySortOrder = CategorySortOrder.ManualOrder;
            this.SourceType = CategorySourceType.Manual;
            this.SortOrder = 0;
            this.MetaKeywords = string.Empty;
            this.MetaDescription = string.Empty;
            this.MetaTitle = string.Empty;
            this.ImageUrl = string.Empty;
            this.BannerImageUrl = string.Empty;
            this.LatestProductCount = 0;
            this.CustomPageUrl = string.Empty;
            this.ShowInTopMenu = false;
            this.Hidden = false;
            this.TemplateName = "AP Grid";
            this.PreContentColumnId = string.Empty;
            this.PostContentColumnId = string.Empty;
            this.ShowTitle = true;
            this.Criteria = string.Empty;
            this.CustomPageId = string.Empty;
            this.PreTransformDescription = string.Empty;
            this.Keywords = string.Empty;
            this.CustomerChangeableSortOrder = true;
            this.CustomPageLayout = CustomPageLayoutType.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        public long MerchantId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long StoreId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime ModifiedOnUtc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ModifiedBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MetaKeywords { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MetaDescription { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MetaTitle { get; set; }
    }
}