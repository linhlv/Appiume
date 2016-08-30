using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appiume.Web.Ecommerce.Catalog.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class OptionSelection
    {
        /// <summary>
        /// 
        /// </summary>
        private string _optionAvin = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        private string _selectionData = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string CleanAvin(string input)
        {
            return input.Replace("-", "");
        }

        /// <summary>
        /// 
        /// </summary>
        public string OptionAvin
        {
            get { return _optionAvin; }
            set { _optionAvin = CleanAvin(value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public string SelectionData
        {
            get { return _selectionData; }
            set { _selectionData = CleanAvin(value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public OptionSelection()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionBvin"></param>
        /// <param name="selectionData"></param>
        public OptionSelection(string optionBvin, string selectionData)
        {
            _optionAvin = CleanAvin(optionBvin);
            _selectionData = CleanAvin(selectionData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selections"></param>
        /// <returns></returns>
        public static string GenerateUniqueKeyForSelections(List<OptionSelection> selections)
        {
            string result = string.Empty;

            if (selections == null) return result;
            if (selections.Count < 1) return result;

            var sorted = selections.OrderBy(y => y.OptionAvin);
            foreach (OptionSelection s in sorted)
            {
                result += s.OptionAvin + "-" + s.SelectionData + "|";
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <param name="selections"></param>
        /// <returns></returns>
        public static bool ContainsInvalidSelectionForOptions(OptionList options, List<OptionSelection> selections)
        {
            // Checks to see if a list of selection data contains a selection 
            // that isn't a valid variant in a list of options

            bool result = false;

            foreach (OptionSelection sel in selections)
            {
                if (!options.ContainsVariantSelection(sel))
                {
                    return true;
                }
            }

            return result;
        }
    }
}