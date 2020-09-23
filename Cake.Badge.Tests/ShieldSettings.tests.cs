// <copyright file="ShieldSettings.tests.cs" company="Float">
// Copyright (c) 2020 Float, All rights reserved.
// Shared under an MIT license. See license.md for details.
// </copyright>

using System;
using System.Drawing;
using System.Threading.Tasks;
using Xunit;

namespace Cake.Badge.Tests
{
    public class ShieldSettingsTests
    {
        [Fact]
        public void TestInitNamedColor()
        {
            var settings = new ShieldSettings
            {
                Label = "label",
                Message = "message",
                Color = Color.Red,
            };

            Assert.Equal("label-message-red", settings.ToString());
        }

        [Fact]
        public void TestInitUnnamedColor()
        {
            var settings = new ShieldSettings
            {
                Label = "label",
                Message = "message",
                Color = Color.FromArgb(255, 255, 105, 180),
            };

            Assert.Equal("label-message-ff69b4", settings.ToString());
        }

        [Fact]
        public async Task TestSend()
        {
            var settings = new ShieldSettings
            {
                Label = "someuniquelabel",
                Message = "anyuniquemessage",
                Color = RandomColor(),
            };

            var svgString = await settings.DownloadSvgString();

            Assert.StartsWith("<svg xmlns", svgString);
            Assert.Contains(settings.Label, svgString);
            Assert.Contains(settings.Message, svgString);
        }

        [Fact]
        public void TestQueryParameters()
        {
            var settings = new ShieldSettings
            {
                Label = "label",
                Message = "message",
                Color = Color.FromArgb(255, 255, 105, 180),
            };

            Assert.Equal("label=label&message=message&color=ff69b4", settings.ToQueryParameters());
        }

        static Color RandomColor()
        {
            var random = new Random(DateTime.Now.Ticks.GetHashCode());
            return Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
        }
    }
}
