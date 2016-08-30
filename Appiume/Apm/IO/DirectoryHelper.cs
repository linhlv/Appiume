using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Appiume.Apm.IO
{
    /// <summary>
    /// A helper class for Directory operations.
    /// </summary>
    public static class DirectoryHelper
    {
        /// <summary>
        /// Creates a new directory if it does not exists.
        /// </summary>
        /// <param name="directory">Directory to create</param>
        public static void CreateIfNotExists(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }
    }
}