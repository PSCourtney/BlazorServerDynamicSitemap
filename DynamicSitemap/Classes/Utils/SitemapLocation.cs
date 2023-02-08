using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace DynamicSitemap.Classes.Utils
{
    /// <summary>
    /// Represents a single URL entry in a sitemap.
    /// </summary>
    public class SitemapLocation
    {
        /// <summary>
        /// Enum that represents the possible frequency of URL changes.
        /// </summary>
        public enum eChangeFrequency
        {
            always,
            hourly,
            daily,
            weekly,
            monthly,
            yearly,
            never
        }

        /// <summary>
        /// The URL of the page.
        /// </summary>
        [XmlElement("loc")]
        public string? Url { get; set; }

        /// <summary>
        /// How frequently the page is likely to change.
        /// </summary>
        [XmlElement("changefreq")]
        public eChangeFrequency? ChangeFrequency { get; set; }

        /// <summary>
        /// Method that indicates if the ChangeFrequency property should be serialized.
        /// </summary>
        public bool ShouldSerializeChangeFrequency() { return ChangeFrequency.HasValue; }

        /// <summary>
        /// The date of last modification of the file.
        /// </summary>
        [XmlElement("lastmod")]
        public DateTime? LastModified { get; set; }

        /// <summary>
        /// Method that indicates if the LastModified property should be serialized.
        /// </summary>
        public bool ShouldSerializeLastModified() { return LastModified.HasValue; }

        /// <summary>
        /// The priority of this URL relative to other URLs on your site.
        /// </summary>
        [XmlElement("priority")]
        public double? Priority { get; set; }

        /// <summary>
        /// Method that indicates if the Priority property should be serialized.
        /// </summary>
        public bool ShouldSerializePriority() { return Priority.HasValue; }
    }
}