using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appiume.Web.Ecommerce.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomProperty
    {
        /// <summary>
        /// 
        /// </summary>
        private string _developerId = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        private string _key = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        private string _value = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string DeveloperId
        {
            get { return _developerId; }
            set { _developerId = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetValueAsInt()
        {
            int result = 0;
            if (int.TryParse(this.Value, out result))
            {
                return result;
            }
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public void SetValueAsInt(int value)
        {
            this.Value = value.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        public CustomProperty()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public CustomProperty Clone()
        {
            return new CustomProperty(this.DeveloperId, this.Key, this.Value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="devId"></param>
        /// <param name="propertyKey"></param>
        /// <param name="propertyValue"></param>
        public CustomProperty(string devId, string propertyKey, string propertyValue)
        {
            _developerId = devId;
            _key = propertyKey;
            _value = propertyValue;
        }

    }
}