﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResourceHelpers.cs" company="James Jackson-South">
//   Copyright (c) James Jackson-South.
//   Licensed under the Apache License, Version 2.0.
// </copyright>
// <summary>
//   Provides helper methods for working with resources.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ImageProcessor.Web.Helpers
{
    using System.IO;
    using System.Reflection;
    using System.Text;

    using ImageProcessor.Web.Caching;

    using Microsoft.IO;

    /// <summary>
    /// Provides helper methods for working with resources.
    /// </summary>
    public class ResourceHelpers
    {
        /// <summary>
        /// Converts an assembly resource into a string.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="assembly">The assembly.</param>
        /// <param name="encoding">The character encoding to return the resource in.</param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ResourceAsString(string resource, Assembly assembly = null, Encoding encoding = null)
        {
            assembly = assembly ?? Assembly.GetExecutingAssembly();
            encoding = encoding ?? Encoding.UTF8;

            using (RecyclableMemoryStream ms = new RecyclableMemoryStream(MemoryStreamPool.Shared))
            {
                using (Stream manifestResourceStream = assembly.GetManifestResourceStream(resource))
                {
                    manifestResourceStream?.CopyTo(ms);
                }

                return encoding.GetString(ms.GetBuffer()).Replace('\0', ' ').Trim();
            }
        }
    }
}
