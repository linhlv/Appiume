using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appiume.Apm.Web.Data;

namespace Appiume.Web.Ecommerce.Catalog.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Option : IMultiTenancyObject, IMultiStore
    {
        /// <summary>
        /// 
        /// </summary>
        public string Avin { get; set; }

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
        public virtual OptionTypes OptionType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool NameIsHidden { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsVariant { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsShared { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public OptionSettings Settings { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<OptionItem> Items { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Option()
        {
            this.Avin = string.Empty;
            this.StoreId = 0;
            this.Name = string.Empty;
            this.NameIsHidden = false;
            this.IsVariant = false;
            this.IsShared = false;
            this.Settings = new OptionSettings();
            this.Items = new List<OptionItem>();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Option Factory(OptionTypes type)
        {
            Option result = new Option();
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        public void LoadItemsFromList(List<OptionItem> items)
        {
            if (items != null)
            {
                var parts = (from i in items
                             where i.OptionAvin.Replace("-", "") == this.Avin.Replace("-", "")
                             orderby i.SortOrder
                             select i).ToList();
                if (parts != null)
                {
                    Items.Clear();
                    Items.AddRange(parts);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns></returns>
        public bool AddItem(string itemName)
        {
            OptionItem oi = new OptionItem();
            oi.Name = itemName;
            return AddItem(oi);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool AddItem(OptionItem item)
        {
            if (item == null) return false;
            item.OptionAvin = this.Avin;
            this.Items.Add(item);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemBvin"></param>
        /// <returns></returns>
        public bool ItemsContains(string itemBvin)
        {
            // check to see if this option contains a specific item
            foreach (OptionItem oi in this.Items)
            {
                if (oi.Avin.Replace("-", "") == itemBvin.Replace("-", "")) return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Option Clone()
        {
            Option result = Option.Factory(this.OptionType);
            result.Avin = string.Empty;
            result.IsShared = this.IsShared;
            result.IsVariant = this.IsVariant;
            foreach (OptionItem oi in this.Items)
            {
                result.Items.Add(oi.Clone());
            }
            result.Name = this.Name;
            result.NameIsHidden = this.NameIsHidden;
            foreach (var set in this.Settings)
            {
                result.Settings.AddOrUpdate(set.Key, set.Value);
            }
            result.StoreId = this.StoreId;

            return result;
        }
    }
}