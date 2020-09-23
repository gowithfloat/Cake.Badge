// <copyright file="IBadgeRunner.cs" company="Float">
// Copyright (c) 2020 Float, All rights reserved.
// Shared under an MIT license. See license.md for details.
// </copyright>

using System;

namespace Cake.Badge
{
    /// <summary>
    /// An interface for the badge runner.
    /// </summary>
    public interface IBadgeRunner
    {
        /// <summary>
        /// Run the badge with an action to configure settings.
        /// </summary>
        /// <param name="configure">An action to configure settings.</param>
        /// <returns>The runner.</returns>
        IBadgeRunner Run(Action<BadgeSettings> configure = null);

        /// <summary>
        /// Run the badge with the given settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns>The runner.</returns>
        IBadgeRunner Run(BadgeSettings settings);
    }
}
