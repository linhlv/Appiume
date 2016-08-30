using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using Appiume.Web.Ecommerce.Models;
using Appiume.Apm.Web.Data;

namespace Appiume.Web.Ecommerce.Orders.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class LineItem : IMultiStore, IMultiTenancyObject, ITrackingObject<string>
    {
        /// <summary>
        /// 
        /// </summary>
        public long Id { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public decimal BasePricePerItem { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrderAvin { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ProductId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string VariantId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ProductSku { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ProductShortDescription { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int QuantityReturned { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int QuantityShipped { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal ShippingPortion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string StatusCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string StatusName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal TaxPortion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Catalog.Models.OptionSelectionList SelectionData { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long ShippingSchedule { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long TaxSchedule { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal ProductShippingWeight { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal ProductShippingLength { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal ProductShippingWidth { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal ProductShippingHeight { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public CustomPropertyCollection CustomProperties { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Shipping.Models.ShippingMode ShipFromMode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Contacts.Models.Address ShipFromAddress { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ShipFromNotificationId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool ShipSeparately { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal ExtraShipCharge { get; set; }
        
        /// <summary>
        /// CONSTRUCTORS
        /// </summary>
        private void Init()
        {
            this.Id = 0;
            this.StoreId = 0;
            this.ModifiedOnUtc = DateTime.UtcNow;
            this.BasePricePerItem = 0m;
            //this.DiscountDetails = new List<Marketing.DiscountDetail>();
            this.OrderAvin = string.Empty;
            this.ProductId = string.Empty;
            this.VariantId = string.Empty;
            this.ProductName = string.Empty;
            this.ProductSku = string.Empty;
            this.ProductShortDescription = string.Empty;
            this.Quantity = 1;
            this.QuantityReturned = 0;
            this.QuantityShipped = 0;
            this.ShippingPortion = 0m;
            this.StatusCode = string.Empty;
            this.StatusName = string.Empty;
            this.TaxPortion = 0m;
            this.SelectionData = new Catalog.Models.OptionSelectionList();
            this.ShippingSchedule = 0;
            this.TaxSchedule = 0;
            this.ProductShippingHeight = 0m;
            this.ProductShippingLength = 0m;
            this.ProductShippingWeight = 0m;
            this.ProductShippingWidth = 0m;
            this.CustomProperties = new CustomPropertyCollection();
            this.ShipFromAddress = new Contacts.Models.Address();
            this.ShipFromMode = Shipping.Models.ShippingMode.ShipFromSite;
            this.ShipFromNotificationId = string.Empty;
            this.ShipSeparately = false;
            this.ExtraShipCharge = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        public LineItem()
        {
            Init();
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
        /// <param name="app"></param>
        /// <returns></returns>
        public Catalog.Models.Product GetAssociatedProduct(AppiumeCommerceApplication app)
        {
            //return app.CatalogServices.Products.Find(this.ProductId);
            return null;
        }

        /// <summary>
        /// Custom Property Helpers
        /// </summary>
        /// <param name="devId"></param>
        /// <param name="propertyKey"></param>
        /// <returns></returns>
        public bool CustomPropertyExists(string devId, string propertyKey)
        {
            bool result = false;
            for (int i = 0; i <= CustomProperties.Count - 1; i++)
            {
                if (CustomProperties[i].DeveloperId.Trim().ToLower() == devId.Trim().ToLower())
                {
                    if (CustomProperties[i].Key.Trim().ToLower() == propertyKey.Trim().ToLower())
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="devId"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void CustomPropertySet(string devId, string key, string value)
        {
            bool found = false;

            for (int i = 0; i <= CustomProperties.Count - 1; i++)
            {
                if (CustomProperties[i].DeveloperId.Trim().ToLower() == devId.Trim().ToLower())
                {
                    if (CustomProperties[i].Key.Trim().ToLower() == key.Trim().ToLower())
                    {
                        CustomProperties[i].Value = value;
                        found = true;
                        break;
                    }
                }
            }

            if (found == false)
            {
                CustomProperties.Add(new CustomProperty(devId, key, value));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="devId"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string CustomPropertyGet(string devId, string key)
        {
            string result = string.Empty;

            for (int i = 0; i <= CustomProperties.Count - 1; i++)
            {
                if (CustomProperties[i].DeveloperId.Trim().ToLower() == devId.Trim().ToLower())
                {
                    if (CustomProperties[i].Key.Trim().ToLower() == key.Trim().ToLower())
                    {
                        result = CustomProperties[i].Value;
                        break;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="devId"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool CustomPropertyRemove(string devId, string key)
        {
            bool result = false;

            for (int i = 0; i <= CustomProperties.Count - 1; i++)
            {
                if (CustomProperties[i].DeveloperId.Trim().ToLower() == devId.Trim().ToLower())
                {
                    if (CustomProperties[i].Key.Trim().ToLower() == key.Trim().ToLower())
                    {
                        CustomProperties.Remove(CustomProperties[i]);
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string CustomPropertiesToXml()
        {
            string result = string.Empty;

            try
            {
                StringWriter sw = new StringWriter();
                XmlSerializer xs = new XmlSerializer(CustomProperties.GetType());
                xs.Serialize(sw, CustomProperties);
                result = sw.ToString();
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("Appiume Commerce", ex.Message + "\n" + ex.StackTrace);
                result = string.Empty;
            }
            

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool CustomPropertiesFromXml(string data)
        {
            bool result = false;

            try
            {
                StringReader tr = new StringReader(data);
                XmlSerializer xs = new XmlSerializer(CustomProperties.GetType());
                CustomProperties = (CustomPropertyCollection)xs.Deserialize(tr);
                if (CustomProperties != null)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("Appiume Commerce", ex.Message + "\n" + ex.StackTrace);
                CustomProperties = new CustomPropertyCollection();
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Calculated Properties
        /// </summary>
        public decimal AdjustedPricePerItem
        {
            get { return CalculateAdjustedPrice(); }
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal LineTotalWithoutDiscounts
        {
            get
            {
                return (this.BasePricePerItem * this.Quantity);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal LineTotal
        {
            get { return CalculateLineTotal(); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private decimal SumUpDiscounts()
        {
            //if (this.DiscountDetails == null) return 0;
            //if (this.DiscountDetails.Count < 1) return 0;
            //return this.DiscountDetails.Sum(y => y.Amount);

            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private decimal CalculateAdjustedPrice()
        {
            if ((decimal)Quantity == 0) return 0;

            decimal result = CalculateLineTotal();
            result = result / (decimal)Quantity;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private decimal CalculateLineTotal()
        {
            decimal result = BasePricePerItem * Quantity;
            result += SumUpDiscounts();
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public LineItem Clone()
        {
            return Clone(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="copyId"></param>
        /// <returns></returns>
        public LineItem Clone(bool copyId)
        {
            LineItem result = new LineItem();

            result.ModifiedOnUtc = this.ModifiedOnUtc;
            result.BasePricePerItem = this.BasePricePerItem;
            //result.DiscountDetails = DiscountDetail.ListFromXml(DiscountDetail.ListToXml(this.DiscountDetails));
            result.OrderAvin = this.OrderAvin;
            result.ProductId = this.ProductId;
            result.VariantId = this.VariantId;
            result.ProductName = this.ProductName;
            result.ProductSku = this.ProductSku;
            result.ProductShortDescription = this.ProductShortDescription;
            result.Quantity = this.Quantity;
            result.QuantityReturned = this.QuantityReturned;
            result.QuantityShipped = this.QuantityShipped;
            result.ShippingPortion = this.ShippingPortion;
            result.StatusCode = this.StatusCode;
            result.StatusName = this.StatusName;
            result.TaxPortion = this.TaxPortion;
            foreach (var x in this.SelectionData)
            {
                result.SelectionData.Add(x);
            }
            result.ShippingSchedule = this.ShippingSchedule;
            result.TaxSchedule = this.TaxSchedule;
            result.ProductShippingHeight = this.ProductShippingHeight;
            result.ProductShippingLength = this.ProductShippingLength;
            result.ProductShippingWeight = this.ProductShippingWeight;
            result.ProductShippingWidth = this.ProductShippingWidth;
            foreach (var y in this.CustomProperties)
            {
                result.CustomProperties.Add(y.Clone());
            }

            if (copyId)
            {
                result.Id = this.Id;
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool ContainsSameProduct(LineItem other)
        {
            if (other.ProductId == this.ProductId)
            {
                if (other.AdjustedPricePerItem != this.AdjustedPricePerItem)
                {
                    return false;
                }

                if (other.VariantId != string.Empty | this.VariantId != string.Empty)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public decimal GetTotalWeight()
        {
            decimal weight = this.ProductShippingWeight;
            weight *= (this.Quantity - this.QuantityShipped);
            return weight;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public virtual bool Equals(LineItem other)
        {
            return this.Id == other.Id;
        }
    }
}