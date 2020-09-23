// <copyright file="BadgeRunner.cs" company="Float">
// Copyright (c) 2020 Float, All rights reserved.
// Shared under an MIT license. See license.md for details.
// </copyright>

using System;
using System.Collections.Generic;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.Badge
{
    /// <summary>
    /// The runner for badging tasks.
    /// </summary>
    public class BadgeRunner : Tool<BadgeSettings>, IBadgeRunner
    {
        const string Badge = "badge";

        /// <summary>
        /// Initializes a new instance of the <see cref="BadgeRunner"/> class.
        /// </summary>
        /// <param name="fileSystem">The current file system.</param>
        /// <param name="environment">The current environment.</param>
        /// <param name="processRunner">The current process runner.</param>
        /// <param name="tools">The current tools.</param>
        public BadgeRunner(IFileSystem fileSystem, ICakeEnvironment environment, IProcessRunner processRunner, IToolLocator tools) : base(fileSystem, environment, processRunner, tools)
        {
        }

        /// <inheritdoc />
        public IBadgeRunner Run(Action<BadgeSettings> configure = null)
        {
            var settings = new BadgeSettings();
            configure?.Invoke(settings);
            return Run(settings);
        }

        /// <inheritdoc />
        public IBadgeRunner Run(BadgeSettings settings)
        {
            var args = GetSettingsArguments(settings);
            Run(settings, args);
            return this;
        }

        /// <inheritdoc />
        protected override string GetToolName() => Badge;

        /// <inheritdoc />
        protected override IEnumerable<string> GetToolExecutableNames() => new[] { Badge };

        static ProcessArgumentBuilder GetSettingsArguments(BadgeSettings settings)
        {
            var args = new ProcessArgumentBuilder();
            settings?.Evaluate(args);
            return args;
        }
    }
}
