using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appiume.Web.Ecommerce.Catalog.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class OptionList : List<Option>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Option> VariantsOnly()
        {
            return this.AsQueryable().Where(y => y.IsVariant == true).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selection"></param>
        /// <returns></returns>
        public bool ContainsVariantSelection(OptionSelection selection)
        {
            // look through a list of options to see if it contains a valid option
            // for the given selection data

            bool result = false;

            foreach (Option o in this.VariantsOnly())
            {
                if (o.Avin.Replace("-", "") == selection.OptionAvin.Replace("-", ""))
                {
                    if (o.ItemsContains(selection.SelectionData))
                    {
                        return true;
                    }
                }
            }

            return result;
        }

    }
}