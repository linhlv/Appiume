using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Appiume.Apm.Web.Core.Ping
{
    /// <summary>
    /// The trackback message.
    /// </summary>
    public class TrackbackMessage
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackbackMessage"/> class.
        /// </summary>
        /// <param name="title">
        /// The publishable item.
        /// </param>
        /// <param name="urlToNotifyTrackback">
        /// The URL to notify trackback.
        /// </param>
        /// <param name="itemUrl">
        /// The item Url.
        /// </param>
        public TrackbackMessage(string title, Uri urlToNotifyTrackback, Uri itemUrl)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentNullException("title");
            }

            this.Title = title;
            this.PostUrl = itemUrl;
            this.Excerpt = title;
            this.SystemName = "appiumeCommerce";
            this.UrlToNotifyTrackback = urlToNotifyTrackback;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the name of the blog.
        /// </summary>
        /// <value>The name of the blog.</value>
        public string SystemName { get; set; }

        /// <summary>
        ///     Gets or sets the excerpt.
        /// </summary>
        /// <value>The excerpt.</value>
        public string Excerpt { get; set; }

        /// <summary>
        ///     Gets or sets the post URL.
        /// </summary>
        /// <value>The post URL.</value>
        public Uri PostUrl { get; set; }

        /// <summary>
        ///     Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>
        ///     Gets or sets the URL to notify trackback.
        /// </summary>
        /// <value>The URL to notify trackback.</value>
        public Uri UrlToNotifyTrackback { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </returns>
        public override string ToString()
        {
            return string.Format(
                CultureInfo.InvariantCulture,
                "title={0}&url={1}&excerpt={2}&blog_name={3}",
                this.Title,
                this.PostUrl,
                this.Excerpt,
                this.SystemName);
        }

        #endregion
    }
}