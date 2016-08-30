using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Xml;

namespace Appiume.Web.Ecommerce.Shipping.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ShippableItem
    {
        /// <summary>
        /// 
        /// </summary>
        public bool IsNonShipping { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal ExtraShipFee { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Weight { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Length { get; set; }

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
        public long ShippingScheduleId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ShippingMode ShippingSource { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ShippingSourceId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool ShipSeparately { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Contacts.Models.Address ShippingSourceAddress { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ShippableItem()
        {
            IsNonShipping = false;
            ExtraShipFee = 0m;
            Weight = 0m;
            Length = 0m;
            Width = 0m;
            Height = 0m;
            ShippingScheduleId = 0;
            ShippingSource = ShippingMode.ShipFromSite;
            ShippingSourceId = string.Empty;
            ShipSeparately = false;
            ShippingSourceAddress = new Contacts.Models.Address();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        public void FromXml(string xml)
        {
            System.IO.StringReader sw = new System.IO.StringReader(xml);
            XmlReader xr = XmlReader.Create(sw);
            FromXml(ref xr);
            sw.Dispose();
            xr.Close();
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
                                case "IsNonShipping":
                                    xr.Read();
                                    this.IsNonShipping = bool.Parse(xr.ReadString());
                                    break;
                                case "ShipSeparately":
                                    xr.Read();
                                    this.ShipSeparately = bool.Parse(xr.ReadString());
                                    break;
                                case "ExtraShipFee":
                                    xr.Read();
                                    this.ExtraShipFee = decimal.Parse(xr.ReadString());
                                    break;
                                case "Weight":
                                    xr.Read();
                                    this.Weight = decimal.Parse(xr.ReadString());
                                    break;
                                case "Length":
                                    xr.Read();
                                    this.Length = decimal.Parse(xr.ReadString());
                                    break;
                                case "Width":
                                    xr.Read();
                                    this.Width = decimal.Parse(xr.ReadString());
                                    break;
                                case "Height":
                                    xr.Read();
                                    this.Height = decimal.Parse(xr.ReadString());
                                    break;
                                case "ShippingScheduleId":
                                    xr.Read();
                                    this.ShippingScheduleId = long.Parse(xr.ReadString());
                                    break;
                                case "ShippingSource":
                                    xr.Read();
                                    this.ShippingSource = (ShippingMode)int.Parse(xr.ReadString());
                                    break;
                                case "ShippingSourceId":
                                    xr.Read();
                                    this.ShippingSourceId = xr.ReadString();
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
            xw.WriteStartElement("ShippableItem");

            xw.WriteStartElement("IsNonShipping");
            xw.WriteValue(this.IsNonShipping);
            xw.WriteEndElement();


            xw.WriteStartElement("ShipSeparately");
            xw.WriteValue(this.ShipSeparately);
            xw.WriteEndElement();

            xw.WriteStartElement("ExtraShipFee");
            xw.WriteValue(this.ExtraShipFee);
            xw.WriteEndElement();

            xw.WriteStartElement("Weight");
            xw.WriteValue(this.Weight);
            xw.WriteEndElement();

            xw.WriteStartElement("Length");
            xw.WriteValue(this.Length);
            xw.WriteEndElement();

            xw.WriteStartElement("Width");
            xw.WriteValue(this.Width);
            xw.WriteEndElement();

            xw.WriteStartElement("Height");
            xw.WriteValue(this.Height);
            xw.WriteEndElement();

            xw.WriteStartElement("ShippingScheduleId");
            xw.WriteValue(this.ShippingScheduleId);
            xw.WriteEndElement();

            xw.WriteStartElement("ShippingSource");
            xw.WriteValue(this.ShippingSource);
            xw.WriteEndElement();

            xw.WriteStartElement("ShippingSourceId");
            xw.WriteValue(this.ShippingSourceId);
            xw.WriteEndElement();

            xw.WriteEndElement();
        }
    }
}