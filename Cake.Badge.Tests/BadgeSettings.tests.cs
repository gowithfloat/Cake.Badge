// <copyright file="BadgeSettings.tests.cs" company="Float">
// Copyright (c) 2020 Float, All rights reserved.
// Shared under an MIT license. See license.md for details.
// </copyright>

using System;
using Xunit;

namespace Cake.Badge.Tests
{
    public class BadgeSettingsTests
    {
        [Fact]
        public void TestShieldGeometry()
        {
            var settings = new BadgeSettings
            {
                ShieldGeometry = new Tuple<int, int>(100, -20),
            };

            var builder = settings.Evaluate();
            var built = builder.Render();
            Assert.Contains("--shield_geometry +100-20%", built);
        }
    }
}
