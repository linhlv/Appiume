using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Appiume.Web.Ecommerce.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomPropertyCollection : Collection<CustomProperty>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public CustomProperty this[string val]
        {
            get
            {
                foreach (CustomProperty value in this.Items)
                {
                    if (string.Compare(value.Key, val, true) == 0)
                    {
                        if (string.Compare(value.DeveloperId, "bvsoftware", true) == 0)
                        {
                            return value;
                        }
                    }
                }
                return null;
            }
            set
            {
                foreach (CustomProperty item in this.Items)
                {
                    if (string.Compare(item.Key, val, true) == 0)
                    {
                        if (string.Compare(item.DeveloperId, "bvsoftware", true) == 0)
                        {
                            item.Value = value.Value;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        /// <param name="developerId"></param>
        /// <returns></returns>
        public CustomProperty this[string val, string developerId]
        {
            get
            {
                foreach (CustomProperty value in this.Items)
                {
                    if (string.Compare(value.Key, val, true) == 0)
                    {
                        if (string.Compare(value.DeveloperId, developerId, true) == 0)
                        {
                            return value;
                        }
                    }
                }
                return null;
            }
            set
            {
                foreach (CustomProperty item in this.Items)
                {
                    if (string.Compare(item.Key, val, true) == 0)
                    {
                        if (string.Compare(item.DeveloperId, developerId, true) == 0)
                        {
                            item.Value = value.Value;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="devId"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(string devId, string key, string value)
        {
            CustomProperty item = new CustomProperty(devId, key, value);
            if (this[key, devId] == null)
            {
                this.Items.Add(item);
            }
            else
            {
                this[key, devId].Value = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="devId"></param>
        /// <param name="propertyKey"></param>
        /// <returns></returns>
        public bool Exists(string devId, string propertyKey)
        {
            bool result = false;
            for (int i = 0; i <= this.Count - 1; i++)
            {
                if (this[i].DeveloperId.Trim().ToLower() == devId.Trim().ToLower())
                {
                    if (this[i].Key.Trim().ToLower() == propertyKey.Trim().ToLower())
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
        public void SetProperty(string devId, string key, string value)
        {
            bool found = false;

            for (int i = 0; i <= this.Count - 1; i++)
            {
                if (this[i].DeveloperId.Trim().ToLower() == devId.Trim().ToLower())
                {
                    if (this[i].Key.Trim().ToLower() == key.Trim().ToLower())
                    {
                        this[i].Value = value;
                        found = true;
                        break;
                    }
                }
            }

            if (found == false)
            {
                this.Add(new CustomProperty(devId, key, value));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="devId"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetProperty(string devId, string key, int value)
        {
            SetProperty(devId, key, value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="devId"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetProperty(string devId, string key)
        {
            string result = string.Empty;

            for (int i = 0; i <= this.Count - 1; i++)
            {
                if (this[i].DeveloperId.Trim().ToLower() == devId.Trim().ToLower())
                {
                    if (this[i].Key.Trim().ToLower() == key.Trim().ToLower())
                    {
                        result = this[i].Value;
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
        public int GetPropertyAsInt(string devId, string key)
        {
            int result = 0;

            for (int i = 0; i <= this.Count - 1; i++)
            {
                if (this[i].DeveloperId.Trim().ToLower() == devId.Trim().ToLower())
                {
                    if (this[i].Key.Trim().ToLower() == key.Trim().ToLower())
                    {
                        result = this[i].GetValueAsInt();
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
        public bool Remove(string devId, string key)
        {
            bool result = false;

            for (int i = 0; i <= this.Count - 1; i++)
            {
                if (this[i].DeveloperId.Trim().ToLower() == devId.Trim().ToLower())
                {
                    if (this[i].Key.Trim().ToLower() == key.Trim().ToLower())
                    {
                        this.Remove(this[i]);
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
        public string ToXml()
        {
            string result = string.Empty;

            try
            {
                StringWriter sw = new StringWriter();
                XmlSerializer xs = new XmlSerializer(this.GetType());
                xs.Serialize(sw, this);
                result = sw.ToString();
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("Appiume Commerce", ex.Message + "\n" + ex.StackTrace);
                result = string.Empty;
            }

            return result;
        }
        public static CustomPropertyCollection FromXml(string data)
        {
            CustomPropertyCollection result = new CustomPropertyCollection();

            try
            {
                StringReader tr = new StringReader(data);
                XmlSerializer xs = new XmlSerializer(result.GetType());
                result = (CustomPropertyCollection)xs.Deserialize(tr);
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("Appiume Commerce", ex.Message + "\n" + ex.StackTrace);
                result = new CustomPropertyCollection();
            }
            return result;
        }
    }
}