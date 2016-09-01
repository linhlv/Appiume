using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace Appiume.Web.Ecommerce.Catalog.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class OptionSelectionList : List<OptionSelection>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionBvin"></param>
        /// <returns></returns>
        public bool ContainsSelectionForOption(string optionBvin)
        {
            string cleaned = OptionSelection.CleanAvin(optionBvin);

            foreach (OptionSelection os in this)
            {
                if (os.OptionAvin == cleaned)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionId"></param>
        /// <returns></returns>
        public OptionSelection FindByOptionId(string optionId)
        {
            string cleaned = OptionSelection.CleanAvin(optionId);

            foreach (OptionSelection os in this)
            {
                if (os.OptionAvin == cleaned)
                {
                    return os;
                }
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        public void DeserializeFromXml(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return;
            }

            try
            {
                this.Clear();

                XDocument doc = XDocument.Load(new StringReader(xml));

                var selections = from sel in doc.Descendants("OptionSelection")
                                 select new
                                 {
                                     OptionBvin = sel.Element("OptionBvin").Value,
                                     SelectionData = sel.Element("SelectionData").Value
                                 };

                foreach (var selection in selections)
                {
                    OptionSelection sel = new OptionSelection();
                    sel.OptionAvin = selection.OptionBvin;
                    sel.SelectionData = selection.SelectionData;
                    this.Add(sel);
                }

            }
            catch (Exception ex)
            {
                this.Clear();
                EventLog.WriteEntry("Appiume Commerce", ex.Message + "\n" + ex.StackTrace);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string SerializeToXml()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (XmlTextWriter xw = new XmlTextWriter(sw))
                {
                    if (xw != null)
                    {
                        xw.WriteStartElement("OptionSelections");

                        foreach (OptionSelection sel in this)
                        {
                            xw.WriteStartElement("OptionSelection");
                            xw.WriteElementString("OptionBvin", sel.OptionAvin);
                            xw.WriteElementString("SelectionData", sel.SelectionData);
                            xw.WriteEndElement();
                        }

                        xw.WriteEndElement();
                    }
                }
                return sw.ToString();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="allOptions"></param>
        /// <returns></returns>
        public decimal GetPriceAdjustmentForSelections(OptionList allOptions)
        {
            decimal result = 0;

            foreach (OptionSelection selection in this)
            {
                foreach (Option opt in allOptions)
                {
                    if (opt.Items != null)
                    {
                        foreach (OptionItem oi in opt.Items)
                        {
                            string cleaned = OptionSelection.CleanAvin(oi.Avin);
                            if (cleaned == selection.SelectionData)
                            {
                                if (oi.PriceAdjustment != 0)
                                {
                                    result += oi.PriceAdjustment;
                                }
                            }
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="allOptions"></param>
        /// <returns></returns>
        public decimal GetWeightAdjustmentForSelections(OptionList allOptions)
        {
            decimal result = 0;

            foreach (OptionSelection selection in this)
            {
                foreach (Option opt in allOptions)
                {
                    if (opt.Items != null)
                    {
                        foreach (OptionItem oi in opt.Items)
                        {
                            string cleaned = OptionSelection.CleanAvin(oi.Avin);
                            if (cleaned == selection.SelectionData)
                            {
                                if (oi.WeightAdjustment != 0)
                                {
                                    result += oi.WeightAdjustment;
                                }
                            }
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool HasLabelsSelected()
        {
            foreach (OptionSelection os in this)
            {
                if (os.SelectionData == "systemlabel")
                {
                    return true;
                }
            }
            return false;
        }
    }
}