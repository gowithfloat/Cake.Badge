# Cake.Badge

[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE.txt)
[![NuGet Version](https://img.shields.io/nuget/v/Cake.Badge.svg)](https://www.nuget.org/packages/Cake.Badge/)

A plugin for [Cake](https://cakebuild.net/) that integrates with [badge](https://github.com/HazAT/badge) to allow icon badging during build.

## Requirements

`badge` must be callable from the command line. I would recommend installing the [Ruby version manager](https://rvm.io/), activating Ruby 2.7.0, and installing via:

   gem install badge

## Usage

Add Cake.Badge to your build script:

Call via the `Badge` alias:

    Task("Badge")
      .Does( => {
        Badge(new BadgeSettings { Verbose = true });
    });

## License

All content in this repository is shared under an MIT license. See [license.md](./license.md) for details.
