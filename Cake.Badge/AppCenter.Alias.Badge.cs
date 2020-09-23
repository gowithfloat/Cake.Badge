// <copyright file="AppCenter.Alias.Badge.cs" company="Float">
// Copyright (c) 2020 Float, All rights reserved.
// Shared under an MIT license. See license.md for details.
// </copyright>

using System;
using Cake.Core;
using Cake.Core.Annotations;

namespace Cake.Badge
{
    /// <summary>
    /// Contains aliases for badging app icons.
    /// </summary>
    [CakeAliasCategory("badge")]
    public static class BadgeAliases
    {
        /// <summary>
        /// Badge icons in the given directory.
        /// </summary>
        /// <code>
        /// Task("Badge")
        ///   .Does( => {
        ///     Badge(new BadgeSettings { Verbose = true });
        ///   });
        /// </code>
        /// <param name="context">The current Cake context.</param>
        /// <param name="settings">Settings for badging icons.</param>
        [CakeMethodAlias]
        public static void Badge(this ICakeContext context, BadgeSettings settings)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var runner = new BadgeRunner(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            runner.Run(settings);
        }
    }
}
