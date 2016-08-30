using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appiume.Web.Ecommerce.Models;
using Appiume.Apm.Web.Data;

namespace Appiume.Web.Ecommerce.Orders.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Order : IMultiStore, IMultiTenancyObject, ITrackingObject<string>
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; } // not used as primary key, only for insert order in SQL pages

        /// <summary>
        /// 
        /// </summary>
        public string Avin { get; set; } // Primary Key

        /// <summary>
        /// 
        /// </summary>
        public CustomPropertyCollection CustomProperties { get; set; }
  
        /// <summary>
        /// Status   
        /// </summary>
        public OrderPaymentStatus PaymentStatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public OrderShippingStatus ShippingStatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsPlaced { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string StatusCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string StatusName { get; set; }

        /// <summary>
        /// Addresses
        /// </summary>
        public Contacts.Models.Address BillingAddress { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Contacts.Models.Address ShippingAddress { get; set; }
        
        /// <summary>
        /// Others
        /// </summary>
        public string AffiliateID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal FraudScore { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Instructions { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ShippingMethodId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ShippingMethodDisplayName { get; set; }

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
        public DateTime TimeOfOrderUtc { get; set; }
        
        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        public Order()
        {
            this.Coupons = new List<OrderCoupon>();
            this.Items = new List<LineItem>();
            this.Notes = new List<OrderNote>();
            this.Packages = new List<OrderPackage>();

            this.Avin = string.Empty;
            this.StoreId = 0;
            this.ModifiedOnUtc = DateTime.UtcNow;
            this.TimeOfOrderUtc = DateTime.UtcNow;
            this.OrderNumber = string.Empty;
            this.ThirdPartyOrderId = string.Empty;
            this.UserEmail = string.Empty;
            this.UserID = string.Empty;
            this.CustomProperties = new CustomPropertyCollection();

            this.PaymentStatus = OrderPaymentStatus.Unknown;
            this.ShippingStatus = OrderShippingStatus.Unknown;
            this.IsPlaced = false;
            this.StatusCode = string.Empty;
            this.StatusName = string.Empty;

            this.BillingAddress = new Contacts.Models.Address();
            this.ShippingAddress = new Contacts.Models.Address();

            this.TotalTax = 0m;
            this.TotalTax2 = 0m;
            this.TotalShippingBeforeDiscounts = 0m;
            // this.ShippingDiscountDetails = new List<Marketing.DiscountDetail>();
            //this.OrderDiscountDetails = new List<Marketing.DiscountDetail>();
            this.TotalHandling = 0m;

            this.AffiliateID = string.Empty;
            this.FraudScore = -1m;
            this.Instructions = string.Empty;
            this.ShippingMethodId = string.Empty;
            this.ShippingMethodDisplayName = string.Empty;
            this.ShippingProviderId = string.Empty;
            this.ShippingProviderServiceCode = string.Empty;
        }
        
        /// <summary>
        /// Totals     
        /// </summary>
        public decimal TotalTax { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal TotalTax2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool AddCouponCode(string code)
        {
            if (!CouponCodeExists(code))
            {
                this.Coupons.Add(new OrderCoupon() { CouponCode = code.Trim().ToUpper(), UserId = this.UserID, StoreId = this.StoreId, OrderAvin = this.Avin, IsUsed = false });
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool CouponCodeExists(string code)
        {
            var c = this.Coupons.Where(y => y.CouponCode.Trim().ToUpper() == code.Trim().ToUpper()).Count();
            return c > 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool RemoveCouponCode(long id)
        {
            OrderCoupon c = this.Coupons.Where(y => y.Id == id).SingleOrDefault();
            if (c != null)
            {
                this.Coupons.Remove(c);
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool RemoveCouponCodeByCode(string code)
        {
            bool result = false;
            string testCode = code.Trim().ToUpper();
            var codes = this.Coupons.Where(y => y.CouponCode == testCode).ToList();
            List<long> toRemove = new List<long>();
            foreach (OrderCoupon oc in codes)
            {
                toRemove.Add(oc.Id);
            }
            foreach (long id in toRemove)
            {
                this.RemoveCouponCode(id);
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool RemoveAllCouponCodes()
        {
            this.Coupons.Clear();
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public LineItem GetLineItem(long Id)
        {
            var li = this.Items.Where(y => y.Id == Id).SingleOrDefault();
            return li;
        }

        /// <summary>
        /// 
        /// </summary>
        public void MoveToNextStatus()
        {
            List<Orders.Models.OrderStatusCode> codes = Orders.Models.OrderStatusCode.FindAll();

            for (int i = 0; i <= codes.Count - 1; i++)
            {
                if (codes[i].Avin == this.StatusCode)
                {
                    // Found Current                    
                    if (i < codes.Count - 1)
                    {
                        this.StatusCode = codes[i + 1].Avin;
                        this.StatusName = codes[i + 1].StatusName;
                    }
                    break;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void MoveToPreviousStatus()
        {
            List<Orders.Models.OrderStatusCode> codes = Orders.Models.OrderStatusCode.FindAll();

            for (int i = 0; i <= codes.Count - 1; i++)
            {
                if (codes[i].Avin == this.StatusCode)
                {
                    // Found Current                    
                    if (i > 0)
                    {
                        this.StatusCode = codes[i - 1].Avin;
                        this.StatusName = codes[i - 1].StatusName;
                    }
                    break;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal TotalShippingBeforeDiscountsOverride
        {
            get
            {
                decimal result = -1;
                string setting = this.CustomProperties.GetProperty("appiumesoftware", "shippingoverride");
                if (setting.Trim().Length > 0)
                {
                    decimal.TryParse(setting, out result);
                }
                return result;
            }
            set
            {
                this.CustomProperties.SetProperty("appiumesoftware", "shippingoverride", value.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private decimal totalShippingBeforeDiscounts = 0;
        public decimal TotalShippingBeforeDiscounts
        {
            get
            {
                decimal totalOverride = TotalShippingBeforeDiscountsOverride;
                if (totalOverride >= 0)
                {
                    return totalOverride;
                }
                return totalShippingBeforeDiscounts;
            }
            set { totalShippingBeforeDiscounts = value; }
        }
      
        /// <summary>
        /// Calculated Properties       
        /// </summary>
        public decimal TotalOrderBeforeDiscounts
        {
            get
            {
                return this.Items.Sum(y => y.LineTotal);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal TotalQuantity
        {
            get
            {
                decimal result = 0m;
                foreach (LineItem li in Items)
                {
                    result += li.Quantity;
                }
                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal TotalQuantityShipping
        {
            get
            {
                decimal result = 0m;
                foreach (LineItem li in Items)
                {
                    if (li.ShippingSchedule > -1)
                    {
                        result += li.Quantity;
                    }
                    else
                    {
                        result += li.Quantity;
                    }
                }
                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal TotalWeight
        {
            get
            {
                decimal result = 0m;
                foreach (LineItem li in Items)
                {
                    result += li.GetTotalWeight();
                }
                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal TotalHandling { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrderNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ThirdPartyOrderId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UserEmail { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<LineItem> Items { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<OrderNote> Notes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<OrderPackage> Packages { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<OrderCoupon> Coupons { get; set; }

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