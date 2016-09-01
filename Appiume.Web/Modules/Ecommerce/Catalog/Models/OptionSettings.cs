using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appiume.Web.Ecommerce.Catalog.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class OptionSettings : Dictionary<string, string>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void AddOrUpdate(string name, string value)
        {
            if (this.ContainsKey(name))
            {
                this[name] = value;
            }
            else
            {
                this.Add(name, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetSettingOrEmpty(string name)
        {
            if (this.ContainsKey(name))
            {
                return this[name];
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool GetBoolSetting(string name)
        {
            if (this.ContainsKey(name))
            {
                if (this[name] == "1")
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void SetBoolSetting(string name, bool value)
        {
            if (value)
            {
                AddOrUpdate(name, "1");
            }
            else
            {
                AddOrUpdate(name, "0");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="otherSettings"></param>
        public void Merge(OptionSettings otherSettings)
        {
            foreach (KeyValuePair<string, string> kv in otherSettings)
            {
                this.AddOrUpdate(kv.Key, kv.Value);
            }
        }
    }
}