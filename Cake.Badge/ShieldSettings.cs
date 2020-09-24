// <copyright file="ShieldSettings.cs" company="Float">
// Copyright (c) 2020 Float, All rights reserved.
// Shared under an MIT license. See license.md for details.
// </copyright>

using System;
using System.Drawing;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace Cake.Badge
{
    /// <summary>
    /// A type for settings to be sent to shields.io.
    /// </summary>
    public sealed class ShieldSettings
    {
        /// <summary>
        /// Gets or sets the style of the shield.
        /// </summary>
        /// <value>The shield style.</value>
        public ShieldStyle Style { get; set; }

        /// <summary>
        /// Gets or sets the shield label (left side).
        /// </summary>
        /// <value>The shield label.</value>
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the shield message (right side).
        /// </summary>
        /// <value>The shield message.</value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the shield color on the right part.
        /// </summary>
        /// <value>The shield color.</value>
        public Color Color { get; set; }

        /// <summary>
        /// Gets or sets a named logo from https://simpleicons.org/ or a base64-encoded image.
        /// </summary>
        /// <value>The logo.</value>
        public string Logo { get; set; }

        /// <summary>
        /// Gets or sets the logo color; only supported for named colors.
        /// </summary>
        /// <value>The logo color.</value>
        public Color? LogoColor { get; set; }

        /// <summary>
        /// Gets or sets the horizontal space to give to the logo.
        /// </summary>
        /// <value>The logo space.</value>
        public int? LogoWidth { get; set; }

        /// <summary>
        /// Gets or sets the URL for clicking on the left or right side of the badge.
        /// </summary>
        /// <value>The URLs in left, right order.</value>
        public Tuple<Uri, Uri> Link { get; set; }

        /// <summary>
        /// Gets or sets the color of the label on the left part.
        /// </summary>
        /// <value>The label color.</value>
        public Color? LabelColor { get; set; }

        /// <summary>
        /// Gets or sets the HTTP cache lifetime.
        /// </summary>
        /// <value>The cache lifetime.</value>
        public int? CacheSeconds { get; set; }

        /// <summary>
        /// Converts the settings to URL query parameters.
        /// </summary>
        /// <returns>The query parameter string.</returns>
        public string ToQueryParameters()
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            queryString.Add("label", Label);
            queryString.Add("message", Message);
            queryString.Add("color", ColorToString(Color));

            if (!string.IsNullOrWhiteSpace(Logo))
            {
                queryString.Add("logo", Logo);
            }

            if (LogoColor is Color logoColor)
            {
                queryString.Add("logoColor", ColorToString(logoColor));
            }

            if (LogoWidth is int width)
            {
                queryString.Add("logoWidth", $"{width}");
            }

            if (Link is Tuple<Uri, Uri> uriPair)
            {
                if (uriPair.Item1 is Uri lhs)
                {
                    if (uriPair.Item2 is Uri rhs)
                    {
                        queryString.Add("link", $"{HttpUtility.UrlEncode(lhs.ToString())}&link={HttpUtility.UrlEncode(rhs.ToString())}");
                    }
                    else
                    {
                        queryString.Add("link", HttpUtility.UrlEncode(lhs.ToString()));
                    }
                }
            }

            if (LabelColor is Color labelColor)
            {
                queryString.Add("labelColor", ColorToString(labelColor));
            }

            if (CacheSeconds is int cacheSeconds)
            {
                queryString.Add("cacheSeconds", $"{cacheSeconds}");
            }

            return queryString.ToString();
        }

        /// <summary>
        /// Using the given settings parameters, retrieves the SVG as a string.
        /// </summary>
        /// <returns>The SVG string.</returns>
        public Task<string> DownloadSvgString()
        {
            using (var client = new WebClient())
            {
                client.QueryString.Add("label", Label);
                client.QueryString.Add("message", Message);
                client.QueryString.Add("color", ColorToString(Color));

                if (!string.IsNullOrWhiteSpace(Logo))
                {
                    client.QueryString.Add("logo", Logo);
                }

                if (LogoColor is Color logoColor)
                {
                    client.QueryString.Add("logoColor", ColorToString(logoColor));
                }

                if (LogoWidth is int width)
                {
                    client.QueryString.Add("logoWidth", $"{width}");
                }

                if (Link is Tuple<Uri, Uri> uriPair)
                {
                    if (uriPair.Item1 is Uri lhs)
                    {
                        if (uriPair.Item2 is Uri rhs)
                        {
                            client.QueryString.Add("link", $"{HttpUtility.UrlEncode(lhs.ToString())}&link={HttpUtility.UrlEncode(rhs.ToString())}");
                        }
                        else
                        {
                            client.QueryString.Add("link", HttpUtility.UrlEncode(lhs.ToString()));
                        }
                    }
                }

                if (LabelColor is Color labelColor)
                {
                    client.QueryString.Add("labelColor", ColorToString(labelColor));
                }

                if (CacheSeconds is int cacheSeconds)
                {
                    client.QueryString.Add("cacheSeconds", $"{cacheSeconds}");
                }

                return client.DownloadStringTaskAsync("https://img.shields.io/static/v1");
            }
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return string.Join("-", new[] { Label, Message, ColorToString(Color) });
        }

        static string ColorToString(Color color)
        {
            if (color.IsNamedColor)
            {
#pragma warning disable CA1308 // Normalize strings to uppercase
                return color.Name.ToLowerInvariant();
#pragma warning restore CA1308 // Normalize strings to uppercase
            }
            else
            {
                var colors = new string[3]
                {
                    color.R.ToString("x2", CultureInfo.InvariantCulture),
                    color.G.ToString("x2", CultureInfo.InvariantCulture),
                    color.B.ToString("x2", CultureInfo.InvariantCulture),
                };

                return string.Join(string.Empty, colors);
            }
        }
    }
}
