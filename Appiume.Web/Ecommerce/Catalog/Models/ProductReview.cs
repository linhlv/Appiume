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
    public class ProductReview : IMultiStore, ITrackingObject<string>, IMultiTenancyObject, IProductRelated
    {
        /// <summary>
        /// 
        /// </summary>
        public string ProductId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime ReviewOn
        {
            get { return ReviewOnUtc.ToLocalTime(); }
            set { ReviewOnUtc = value.ToUniversalTime(); }
        }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime ReviewOnUtc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tz"></param>
        /// <returns></returns>
        public System.DateTime ReviewDateForTimeZone(TimeZoneInfo tz)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(ReviewOnUtc, tz);
        }

        /// <summary>
        /// 
        /// </summary>
        public ProductReviewRating Rating { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int RatingAsInteger => (int)Rating;

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Approved { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime ApprovedOnUtc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ApprovedBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ProductReview()
        {
            this.MerchantId = 0;
            this.StoreId = 0;
            this.CreatedOnUtc = DateTime.UtcNow;
            this.ModifiedOnUtc = DateTime.UtcNow;
            this.ReviewOnUtc = DateTime.UtcNow;
            this.Rating = ProductReviewRating.ThreeStars;
            this.Description = string.Empty;
            this.Approved = false;
            this.ProductName = string.Empty;
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
    }
}