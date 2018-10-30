//-----------------------------------------------------------------------
// <copyright file="ClFileTools.cs" company="Ideal Software">
//     Copyright (c) Ideal Software. All rights reserved.
// </copyright>
// <author>Stephane Amoa</author>
//-----------------------------------------------------------------------

namespace Tools
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;

    /// <summary>
    /// This class has helper functions related to file.
    /// </summary>
    /// <remarks>Used for file operations.</remarks>
    public static class CLFileTools
    {

        /// <summary>
        /// Build a list of objects from stream from a string
        /// </summary>
        /// <param name="xmlStream">The stream</param>
        /// <param name="fromString">Where to start to get informations </param>
        /// <remarks>The stream must be open and valid.</remarks>
        /// <returns>An object list or null</returns>
        public static List<object> BuildDatafromStream(Stream xmlStream, string fromString)
        {
            if (xmlStream == null)
            {
                return null;
            }

            var xdoc = XDocument.Load(xmlStream);
            List<object> lstObj = null;
            var datatofind = xdoc.Root.Elements(fromString);
            if ((datatofind != null) && (datatofind.Any()))
            {
                lstObj = new List<object>();
                foreach (var obj in datatofind)
                {
                    lstObj.Add(obj);
                }
            }

            return lstObj;
        }

        public static List<object> BuildDescendantsDataFromStream(Stream xmlStream, string fromString, string selectstring)
        {
            if (xmlStream == null)
            {
                return null;
            }

            if (string.IsNullOrEmpty(fromString))
            {
                return null;
            }

            var xdoc = XDocument.Load(xmlStream);
            List<object> lstObj = null;
            var datatofind = xdoc.Root.Elements(fromString).Descendants(selectstring);
            {
                lstObj = new List<object>();
                foreach (var obj in datatofind)
                {
                    lstObj.Add(obj);
                }
            }

            return lstObj;
        }

        /// <summary>
        /// Build a list of objects from stream from a string
        /// </summary>
        /// <param name="xmlStream">The stream</param>
        /// <param name="fromString">Where to start to get informations </param>
        /// <param name="whereString">Criteria</param>
        /// <param name="selectString">The selected string</param>
        /// <returns>The list of object if found otherwise null.</returns>
        /// <remarks>
        /// Null is returned when the stream is null, when nothing was found.
        /// </remarks>
        public static List<object> BuildDatafromStream(Stream xmlStream, string fromString, string whereString, string selectString)
        {
            if (xmlStream == null)
            {
                return null;
            }

            var xdoc = XDocument.Load(xmlStream);
            List<object> lstObj = null;
            var datatofind = from continent in xdoc.Descendants(fromString).Where(continent => (string)continent.Attribute("name") == whereString)
                             select continent.Descendants(selectString);

            if ((datatofind != null) && (datatofind.Any()))
            {
                lstObj = new List<object>();
                foreach (var obj in datatofind)
                {
                    var size = obj.Count();
                    for (int i = 0; i < size; i++)
                    {
                        lstObj.Add(obj.ElementAt(i));
                    }
                }
            }

            return lstObj;
        }

        /// <summary>
        /// This a dummy function used for testing.
        /// </summary>
        /// <param name="xmlStream">The stream, must be valid</param>
        /// <param name="fromString">Criteria to start</param>
        /// <param name="whereString">Criteria condition</param>
        /// <param name="selectString">The string to select</param>
        /// <returns>The list of object if found otherwise null</returns>
        /// <remarks>
        /// Null is returned when the stream is null, when nothing was found.
        /// </remarks>
        //public static List<object> DummyBuildDatafromStream(Stream xmlStream, string fromString, string whereString, string selectString)
        //{
        //    if (xmlStream == null)
        //    {
        //        return null;
        //    }

        //    var xdoc = XDocument.Load(xmlStream);
        //    List<object> lstObj = null;
        //    var datatofind = from continent in xdoc.Descendants(fromString).Where(continent => (string)continent.Element("Location").Attribute("town") == whereString)
        //                     select continent.Descendants(selectString);

        //    if ((datatofind != null) && (datatofind.Any()))
        //    {
        //        lstObj = new List<object>();
        //        foreach (var obj in datatofind)
        //        {
        //            var size = obj.Count();
        //            for (int i = 0; i < size; i++)
        //            {
        //                lstObj.Add(obj.ElementAt(i));
        //            }
        //        }
        //    }

        //    return lstObj;
        //}
    }
}

