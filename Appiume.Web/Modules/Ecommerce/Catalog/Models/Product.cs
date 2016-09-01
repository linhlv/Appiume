using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using Appiume.Web.Ecommerce.Core;
using Appiume.Apm.Web.Data;

namespace Appiume.Web.Ecommerce.Catalog.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Product : IMultiStore, ITrackingObject<string>, IMultiTenancyObject, ISortable, IStock, ISearchEngineFriendlyObject, IAvin
    {
        /// <summary>
        /// 
        /// </summary>
        public string Avin { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int SortOrder { get; set; }

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
        public string Sku
        {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal Price
        {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ProductTypeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal ListPrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal SitePrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SitePriceOverrideText { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal SiteCost { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MetaKeywords
        {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string MetaDescription
        {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string MetaTitle
        {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool TaxExempt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long TaxSchedule { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ProductStatus Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ImageFileSmall { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ImageFileSmallAlternateText { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ImageFileMedium { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ImageFileMediumAlternateText { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int MinimumQty { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ShortDescription { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LongDescription { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ManufacturerId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string VendorId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool GiftWrapAllowed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal GiftWrapPrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Keywords { get; set; }

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
        public bool IsNew
        {
            get { return ((System.DateTime.Now - CreatedOnUtc).Days <= WebAppSettings.NewProductBadgeDays); }
        }

        /// <summary>
        /// 
        /// </summary>
        public string UrlSlug { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsAvailableForSale { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Featured { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool AllowReviews { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public ProductInventoryMode InventoryMode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<ProductDescriptionTab> Tabs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string TabsToXml()
        {
            string result = string.Empty;

            try
            {
                XmlWriterSettings writerSettings = new XmlWriterSettings();
                string response = string.Empty;
                StringBuilder sb = new StringBuilder();
                writerSettings.ConformanceLevel = ConformanceLevel.Fragment;
                XmlWriter xw = XmlWriter.Create(sb, writerSettings);

                xw.WriteStartElement("DescriptionTabs");
                foreach (ProductDescriptionTab t in this.Tabs)
                {
                    t.ToXmlWriter(ref xw);
                }
                xw.WriteEndElement();
                xw.Flush();
                xw.Close();
                result = sb.ToString();
            }

            catch (Exception ex)
            {
                EventLog.WriteEntry("Appiume Commerce", ex.Message + "\n" + ex.StackTrace);
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        public void TabsFromXml(string xml)
        {
            if (xml == string.Empty) return;

            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(xml);

            // add blocks from xml
            XmlNodeList tabNodes;
            tabNodes = xdoc.SelectNodes("/DescriptionTabs/ProductDescriptionTab");

            this.Tabs.Clear();
            if (tabNodes != null)
            {
                for (int i = 0; i <= tabNodes.Count - 1; i++)
                {
                    ProductDescriptionTab t = new ProductDescriptionTab();
                    t.FromXmlString(tabNodes[i].OuterXml);
                    this.Tabs.Add(t);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Shipping.Models.ShippableItem ShippingDetails { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsTaxable()
        {

            bool chargeTaxOnNonShipping = false;

            if (this.TaxExempt)
            {
                return false;
            }

            if (this.ShippingDetails.IsNonShipping)
            {
                if (!chargeTaxOnNonShipping)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        public List<ProductImage> Images { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<ProductReview> Reviews { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<ProductReview> ReviewsApproved
        {
            get
            {
                List<ProductReview> result = new List<ProductReview>();
                foreach (ProductReview p in Reviews)
                {
                    if (p.Approved) { result.Add(p); }
                }
                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public OptionList Options { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public VariantList Variants { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool HasOptions()
        {
            return this.Options.Count > 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool HasVariants()
        {
            return this.Variants.Count > 0;
        }

        /// <summary>
        /// 
        /// </summary>
        public Product()
        {
            this.ModifiedOnUtc = System.DateTime.MinValue;
            this.CreatedOnUtc = DateTime.UtcNow;
            this.Sku = string.Empty;
            this.ProductName = string.Empty;
            this.ProductTypeId = string.Empty;
            this.Images = new List<ProductImage>();
            this.ListPrice = 0m;
            this.Reviews = new List<ProductReview>();
            this.SitePrice = 0m;
            this.SitePriceOverrideText = string.Empty;
            this.SiteCost = 0m;
            this.MetaKeywords = string.Empty;
            this.MetaDescription = string.Empty;
            this.MetaTitle = string.Empty;
            this.TaxExempt = false;
            this.TaxSchedule = -1;
            this.Status = ProductStatus.Active;
            this.ImageFileSmall = string.Empty;
            this.ImageFileMedium = string.Empty;
            this.ImageFileSmallAlternateText = string.Empty;
            this.ImageFileMediumAlternateText = string.Empty;
            this.MinimumQty = 1;
            this.ShortDescription = string.Empty;
            this.LongDescription = string.Empty;
            this.ManufacturerId = string.Empty;
            this.VendorId = string.Empty;
            this.GiftWrapAllowed = false;
            this.Keywords = string.Empty;
            this.TemplateName = "";
            this.PreContentColumnId = string.Empty;
            this.PostContentColumnId = string.Empty;
            this.UrlSlug = string.Empty;
            this.InventoryMode = ProductInventoryMode.AlwayInStock;
            this.GiftWrapPrice = 0m;
            this.Options = new OptionList();
            this.Variants = new VariantList();
            this.ShippingDetails = new Shipping.Models.ShippableItem();
            this.Featured = false;
            this.AllowReviews = false;
            this.Tabs = new List<ProductDescriptionTab>();
            this.StoreId = 0;
            this.IsAvailableForSale = true;
        }


        // Pricing Functions
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool GetAdminGiftWrappable()
        {
            return WebAppSettings.GiftWrapAll;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private decimal GetAdminGiftWrapPrice()
        {
            return WebAppSettings.GiftWrapCharge;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cloneProductChoicesAndInputs"></param>
        /// <param name="cloneProductImages"></param>
        /// <returns></returns>
        public Product Clone(bool cloneProductChoicesAndInputs, bool cloneProductImages)
        {
            //Product result = new Product();

            //result.AllowReviews = this.AllowReviews;
            //result.Bvin = string.Empty;
            //result.CreatedOnUtc = DateTime.UtcNow;

            //foreach (CustomProperty prop in this.CustomProperties)
            //{
            //    result.CustomProperties.Add(prop.DeveloperId, prop.Key, prop.Value);
            //}

            //result.Featured = this.Featured;
            //result.GiftWrapAllowed = this.GiftWrapAllowed;
            //result.GiftWrapPrice = this.GiftWrapPrice;
            //if (cloneProductImages == true)
            //{
            //    result.ImageFileMedium = this.ImageFileMedium;
            //    result.ImageFileMediumAlternateText = this.ImageFileMediumAlternateText;
            //    result.ImageFileSmall = this.ImageFileSmall;
            //    result.ImageFileSmallAlternateText = this.ImageFileSmallAlternateText;


            //    foreach (var img in this.Images)
            //    {
            //        ProductImage imgClone = img.Clone();
            //        imgClone.ProductId = string.Empty;
            //        result.Images.Add(imgClone);
            //    }
            //}
            //result.InventoryMode = this.InventoryMode;
            //result.IsAvailableForSale = this.IsAvailableForSale;
            //result.Keywords = this.Keywords;
            //result.ListPrice = this.ListPrice;
            //result.LongDescription = this.LongDescription;
            //result.ManufacturerId = this.ManufacturerId;
            //result.MetaDescription = this.MetaDescription;
            //result.MetaKeywords = this.MetaKeywords;
            //result.MetaTitle = this.MetaTitle;
            //result.MinimumQty = this.MinimumQty;

            //result.PostContentColumnId = this.PostContentColumnId;
            //result.PreContentColumnId = this.PreContentColumnId;
            //result.PreTransformLongDescription = this.PreTransformLongDescription;
            //result.ProductName = this.ProductName;
            //result.ProductTypeId = this.ProductTypeId;

            //result.ShippingDetails.ExtraShipFee = this.ShippingDetails.ExtraShipFee;
            //result.ShippingDetails.Height = this.ShippingDetails.Height;
            //result.ShippingDetails.IsNonShipping = this.ShippingDetails.IsNonShipping;
            //result.ShippingDetails.Length = this.ShippingDetails.Length;
            //result.ShippingDetails.ShippingScheduleId = this.ShippingDetails.ShippingScheduleId;
            //result.ShippingDetails.ShippingSource = this.ShippingDetails.ShippingSource;
            //this.ShippingDetails.ShippingSourceAddress.CopyTo(result.ShippingDetails.ShippingSourceAddress);
            //result.ShippingDetails.ShippingSourceId = this.ShippingDetails.ShippingSourceId;
            //result.ShippingDetails.ShipSeparately = this.ShippingDetails.ShipSeparately;
            //result.ShippingDetails.Weight = this.ShippingDetails.Weight;
            //result.ShippingDetails.Width = this.ShippingDetails.Width;

            //result.ShippingMode = this.ShippingMode;
            //result.ShortDescription = this.ShortDescription;
            //result.SiteCost = this.SiteCost;
            //result.SitePrice = this.SitePrice;
            //result.SitePriceOverrideText = this.SitePriceOverrideText;
            //result.Sku = this.Sku;
            //result.Status = this.Status;
            //result.StoreId = this.StoreId;

            //foreach (ProductDescriptionTab tab in this.Tabs)
            //{
            //    result.Tabs.Add(new ProductDescriptionTab()
            //    {
            //        HtmlData = tab.HtmlData,
            //        SortOrder = tab.SortOrder,
            //        TabTitle = tab.TabTitle,
            //        LastUpdated = DateTime.UtcNow
            //    });
            //}

            //result.TaxExempt = this.TaxExempt;
            //result.TaxSchedule = this.TaxSchedule;
            //result.UrlSlug = string.Empty;
            //result.VendorId = this.VendorId;

            //if (cloneProductChoicesAndInputs == true)
            //{
            //    foreach (var opt in this.Options)
            //    {
            //        Option c = opt.Clone();
            //        result.Options.Add(c);
            //    }
            //    //result.Variants = this.Variants;
            //}

            //result.Bvin = System.Guid.NewGuid().ToString();



            //return result;

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Product Clone()
        {
            return Clone(true, true);
        }

        //public decimal GetCurrentPrice(string userBvin, decimal adjustment, Orders.LineItem li, string variantId)
        //{
        //    decimal result = this.SitePrice;

        //    // pull basic site price from product
        //    if (variantId != string.Empty)
        //    {
        //        //Catalog.Variant v = this.Variants.FindByBvin(variantId);
        //        //if (v != null)
        //        //{
        //        //    if (v.Price >= 0) result = v.Price;
        //        //}
        //    }

        //    if (li == null) return result;
        //    //if (li.AssociatedProduct == null) return result;            

        //    //if (li.AssociatedProduct.IsKit)
        //    //{
        //    //    KitSelectionData ksd = Services.KitService.GetKitSelectionData(li.KitSelections);
        //    //    if (li.KitSelections.SelectedValues.Count == 0)
        //    //    {
        //    //        result = ksd.DefaultPrice;
        //    //    }
        //    //    else
        //    //    {
        //    //        result = ksd.TotalPrice;
        //    //    }
        //    //    if (li != null)
        //    //    {
        //    //        if (li.Bvin.Trim().Length > 0)
        //    //        {
        //    //            result = ksd.TotalPrice;
        //    //        }
        //    //    }
        //    //}

        //    //else
        //    //{
        //    //    result = li.BasePrice;
        //    //    BusinessRules.ProductTaskContext c = new BusinessRules.ProductTaskContext(this);
        //    //    c.UserId = userBvin;
        //    //    c.SetProduct(li.AssociatedProduct);

        //    //    if (adjustment > 0)
        //    //    {
        //    //        c.UserPrice.AddAdjustment(new PriceAdjustment(adjustment, "Custom Adjustment"));
        //    //    }
        //    //    BusinessRules.Workflow.RunByName((BusinessRules.TaskContext)c, BusinessRules.WorkflowNames.ProductPricing);
        //    //    foreach (BusinessRules.WorkflowMessage item in c.Errors)
        //    //    {
        //    //        EventLog.LogEvent(item.Name, item.Description, Metrics.EventLogSeverity.Error);
        //    //    }
        //    //    result = c.UserPrice.PriceWithAdjustments();
        //    //}

        //    return result;
        //}
    }
}