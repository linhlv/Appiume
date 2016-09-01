using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using Appiume.Apm.Web.Data;
using System.Diagnostics;

namespace Appiume.Web.Ecommerce.Catalog.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ProductDescriptionTab: ITrackingObject<string>, ISortable
    {
        /// <summary>
        /// 
        /// </summary>
        private string _TabTitle = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        private string _HtmlData = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        private int _SortOrder = 1;

        /// <summary>
        /// 
        /// </summary>
        public string TabTitle
        {
            get { return _TabTitle; }
            set { _TabTitle = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string HtmlData
        {
            get { return _HtmlData; }
            set { _HtmlData = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreatedOnUtc
        {
            get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatedBy
        {
            get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime ModifiedOnUtc
        {
            get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string ModifiedBy
        {
            get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int SortOrder
        {
            get { return _SortOrder; }
            set { _SortOrder = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public virtual bool FromXmlString(string x)
        {
            System.IO.StringReader sw = new System.IO.StringReader(x);
            XmlReader xr = XmlReader.Create(sw);
            bool result = FromXml(ref xr);
            sw.Dispose();
            xr.Close();
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xr"></param>
        /// <returns></returns>
        public bool FromXml(ref System.Xml.XmlReader xr)
        {
            bool results = false;

            try
            {
                while (xr.Read())
                {
                    if (xr.IsStartElement())
                    {
                        if (!xr.IsEmptyElement)
                        {
                            switch (xr.Name)
                            {
                                case "TabTitle":
                                    xr.Read();
                                    this.TabTitle = xr.ReadString();
                                    break;
                                case "HtmlData":
                                    xr.Read();
                                    this.HtmlData = xr.ReadString();
                                    break;
                                case "SortOrder":
                                    xr.Read();
                                    this.SortOrder = int.Parse(xr.ReadString());
                                    break;
                            }
                        }
                    }
                }

                results = true;
            }

            catch (XmlException XmlEx)
            {
                EventLog.WriteEntry("Appiume Commerce", XmlEx.Message + "\n" + XmlEx.StackTrace);
                results = false;
            }

            return results;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xw"></param>
        public void ToXmlWriter(ref System.Xml.XmlWriter xw)
        {
            if (xw != null)
            {
                xw.WriteStartElement("ProductDescriptionTab");
                xw.WriteElementString("TabTitle", this.TabTitle);
                xw.WriteElementString("HtmlData", this.HtmlData);
                xw.WriteElementString("SortOrder", this.SortOrder.ToString());
                xw.WriteEndElement();
            }
        }
    }
}