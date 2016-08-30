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
    public class OrderPackage : IMultiStore, IMultiTenancyObject, ITrackingObject<string>
    {
        /// <summary>
        /// 
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<OrderPackageItem> Items { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Width { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Height { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Length { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Shipping.Models.LengthType SizeUnits { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Weight { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Shipping.Models.WeightType WeightUnits { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ShippingProviderId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ShippingProviderServiceCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TrackingNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool HasShipped { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime ShipDateUtc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal EstimatedShippingCost { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ShippingMethodId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public CustomPropertyCollection CustomProperties { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public OrderPackage()
        {
            this.Id = 0;
            this.StoreId = 0;
            this.ModifiedOnUtc = DateTime.UtcNow;
            this.Items = new List<OrderPackageItem>();
            this.Description = string.Empty;
            this.OrderId = string.Empty;
            this.Width = 0m;
            this.Height = 0m;
            this.Length = 0m;
            this.SizeUnits = Shipping.Models.LengthType.Inches;
            this.Weight = 0m;
            this.WeightUnits = Shipping.Models.WeightType.Pounds;
            this.ShippingProviderId = string.Empty;
            this.ShippingProviderServiceCode = string.Empty;
            this.ShippingMethodId = string.Empty;
            this.TrackingNumber = string.Empty;
            this.HasShipped = false;
            this.ShipDateUtc = DateTime.MinValue;
            this.EstimatedShippingCost = 0m;
            this.CustomProperties = new CustomPropertyCollection();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public OrderPackage Clone()
        {
            var memoryStream = new System.IO.MemoryStream();
            var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            formatter.Serialize(memoryStream, this);
            memoryStream.Position = 0;
            OrderPackage newPackage = (OrderPackage)formatter.Deserialize(memoryStream);
            return newPackage;
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
        /// <returns></returns>
        public string ItemsToXml()
        {
            string result = string.Empty;

            try
            {
                StringWriter sw = new StringWriter();
                XmlSerializer xs = new XmlSerializer(Items.GetType());
                xs.Serialize(sw, Items);
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
        public bool ItemsFromXml(string data)
        {
            bool result = false;

            try
            {
                StringReader tr = new StringReader(data);
                XmlSerializer xs = new XmlSerializer(Items.GetType());
                Items = (List<OrderPackageItem>)xs.Deserialize(tr);
                if (Items != null)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("Appiume Commerce", ex.Message + "\n" + ex.StackTrace);
                Items = new List<OrderPackageItem>();
                result = false;
            }

            return result;
        }
    }
}