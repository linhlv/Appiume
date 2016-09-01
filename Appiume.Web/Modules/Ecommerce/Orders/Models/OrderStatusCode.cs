using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appiume.Apm.Web.Data;

namespace Appiume.Web.Ecommerce.Orders.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderStatusCode: ITrackingObject<string>, ISortable
    {
        /// <summary>
        /// 
        /// </summary>
        public const string Cancelled = "A7FFDB90-C566-4cf2-93F4-D42367F359D5";

        /// <summary>
        /// 
        /// </summary>
        public const string OnHold = "88B5B4BE-CA7B-41a9-9242-D96ED3CA3135";

        /// <summary>
        /// 
        /// </summary>
        public const string Received = "F37EC405-1EC6-4a91-9AC4-6836215FBBBC";

        /// <summary>
        /// 
        /// </summary>
        public const string ReadyForPayment = "e42f8c28-9078-47d6-89f8-032c9a6e1cce";

        /// <summary>
        /// 
        /// </summary>
        public const string ReadyForShipping = "0c6d4b57-3e46-4c20-9361-6b0e5827db5a";

        /// <summary>
        /// 
        /// </summary>
        public const string Completed = "09D7305D-BD95-48d2-A025-16ADC827582A";

        /// <summary>
        /// 
        /// </summary>
        public string Avin { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string StatusName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool SystemCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int SortOrder { get; set; }
        
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
        public OrderStatusCode()
        {
            this.Avin = string.Empty;
            this.ModifiedOnUtc = DateTime.UtcNow;
            this.StatusName = string.Empty;
            this.SystemCode = false;
            this.SortOrder = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<OrderStatusCode> FindAll()
        {
            List<OrderStatusCode> result = new List<OrderStatusCode>();

            result.Add(new OrderStatusCode() { Avin = "A7FFDB90-C566-4cf2-93F4-D42367F359D5", SystemCode = true, StatusName = "Cancelled", SortOrder = 0 });
            result.Add(new OrderStatusCode() { Avin = "88B5B4BE-CA7B-41a9-9242-D96ED3CA3135", SystemCode = true, StatusName = "On Hold", SortOrder = 1 });
            result.Add(new OrderStatusCode() { Avin = "F37EC405-1EC6-4a91-9AC4-6836215FBBBC", SystemCode = true, StatusName = "Received", SortOrder = 2 });
            result.Add(new OrderStatusCode() { Avin = "e42f8c28-9078-47d6-89f8-032c9a6e1cce", SystemCode = true, StatusName = "Ready for Payment", SortOrder = 3 });
            result.Add(new OrderStatusCode() { Avin = "0c6d4b57-3e46-4c20-9361-6b0e5827db5a", SystemCode = true, StatusName = "Ready for Shipping", SortOrder = 5 });
            result.Add(new OrderStatusCode() { Avin = "09D7305D-BD95-48d2-A025-16ADC827582A", SystemCode = true, StatusName = "Complete", SortOrder = 6 });

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bvin"></param>
        /// <returns></returns>
        public static OrderStatusCode FindByBvin(string bvin)
        {
            foreach (OrderStatusCode o in FindAll())
            {
                if (o.Avin == bvin)
                {
                    return o;
                }
            }
            return null;
        }
    }
}