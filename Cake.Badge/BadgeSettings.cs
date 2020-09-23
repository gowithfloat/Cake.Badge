// <copyright file="BadgeSettings.cs" company="Float">
// Copyright (c) 2020 Float, All rights reserved.
// Shared under an MIT license. See license.md for details.
// </copyright>

using System;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.Badge
{
    /// <summary>
    /// Settings for the Cake app icon badging tool.
    /// </summary>
    public sealed class BadgeSettings : ToolSettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether badging shows a more verbose output.
        /// </summary>
        /// <value><c>true</c> if verbose, <c>false</c> otherwise.</value>
        public bool Verbose { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether badging adds a dark badge instead of the white.
        /// </summary>
        /// <value><c>true</c> if dark, <c>false</c> otherwise.</value>
        public bool Dark { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether badging uses the word alpha instead of beta.
        /// </summary>
        /// <value><c>true</c> if alpha, <c>false</c> otherwise.</value>
        public bool Alpha { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether badging keeps/adds an alpha channel to the icons.
        /// </summary>
        /// <value><c>true</c> if alpha channel is to be kept, <c>false</c> otherwise.</value>
        public bool AlphaChannel { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether badging overlays a custom image on your icon.
        /// </summary>
        /// <value>The path to the custom image, if set.</value>
        public string Custom { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether badging removes the beta badge.
        /// </summary>
        /// <value><c>true</c> if badge will be removed, <c>false</c> otherwise.</value>
        public bool NoBadge { get; set; }

        /// <summary>
        /// Gets or sets a value indicating where badging positions the badge on the icon.
        /// </summary>
        /// <value>The badge gravity.</value>
        public Gravity BadgeGravity { get; set; } = Gravity.SouthEast;

        /// <summary>
        /// Gets or sets a value for settings to overlay a shield from shields.io on your icon.
        /// </summary>
        /// <value>The shield settings.</value>
        public ShieldSettings Shield { get; set; }

        /// <summary>
        /// Gets or sets a value for Parameters of the shield image.
        /// String of key-value pairs separated by ampersand as specified on shields.io.
        /// </summary>
        /// <value>The shield parameters.</value>
        public string ShieldParameters { get; set; }

        /// <summary>
        /// Gets or sets the timeout in seconds we should wait the get a response from shields.io.
        /// </summary>
        /// <value>The shields.io timeout.</value>
        public int? ShieldIoTimeout { get; set; }

        /// <summary>
        /// Gets or sets the position of shield on icon, relative to gravity.
        /// </summary>
        /// <value>The relative X and Y offsets.</value>
        public Tuple<float, float> ShieldGeometry { get; set; }

        /// <summary>
        /// Gets or sets the position of shield on icon.
        /// </summary>
        /// <value>The shield gravity.</value>
        public Gravity? ShieldGravity { get; set; }

        /// <summary>
        /// Gets or sets the shield image scale factor.
        /// </summary>
        /// <value>The shield scale.</value>
        public float? ShieldScale { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the shield image will no longer be resized to aspect fill the full icon.
        /// Instead it will only be shrinked to not exceed the icon graphic.
        /// </summary>
        /// <value><c>true</c> if shield will not be resized, <c>false</c> otherwise.</value>
        public bool ShieldNoResize { get; set; }

        /// <summary>
        /// Gets or sets the glob pattern for finding image files.
        /// </summary>
        /// <value>The glob pattern.</value>
        public string Glob { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether making icons to grayscale.
        /// </summary>
        /// <value><c>true</c> if icons should be grayscale, <c>false</c> otherwise.</value>
        public bool Grayscale { get; set; }

        internal void Evaluate(ProcessArgumentBuilder args)
        {
            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            if (Verbose)
            {
                args.Append("--verbose");
            }

            if (Dark)
            {
                args.Append("--dark");
            }

            if (Alpha)
            {
                args.Append("--alpha");
            }

            if (AlphaChannel)
            {
                args.Append("--alpha_channel");
            }

            if (Custom != null)
            {
                args.Append($"--custom {Custom}");
            }

            if (NoBadge)
            {
                args.Append("--no_badge");
            }

            args.Append($"--badge_gravity {BadgeGravity}");

            if (Shield is ShieldSettings shieldSettings)
            {
                args.Append($"--shield_parameters {shieldSettings.ToQueryParameters()}");
            }

            // todo: shield parameters

            if (ShieldIoTimeout != null)
            {
                args.Append($"--shield_io_timeout {ShieldIoTimeout}");
            }

            if (ShieldGeometry != null)
            {
                // probably broken (no +/-)
                args.Append($"--shield_geometry {ShieldGeometry.Item1}{ShieldGeometry.Item2}%");
            }

            if (ShieldGravity is Gravity gravity)
            {
                args.Append($"--shield_gravity {Enum.GetName(typeof(Gravity), gravity)}");
            }

            if (ShieldScale is float scale)
            {
                args.Append($"--shield_scale {scale}");
            }

            if (ShieldNoResize)
            {
                args.Append($"--shield_no_resize");
            }

            if (!string.IsNullOrWhiteSpace(nameof(Glob)))
            {
                args.Append($"--glob {Glob}");
            }

            if (Grayscale)
            {
                args.Append("--grayscale");
            }
        }
    }
}
