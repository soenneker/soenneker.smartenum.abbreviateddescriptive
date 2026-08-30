[![](https://img.shields.io/nuget/v/soenneker.smartenum.abbreviateddescriptive.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.smartenum.abbreviateddescriptive/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.smartenum.abbreviateddescriptive/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.smartenum.abbreviateddescriptive/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.smartenum.abbreviateddescriptive.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.smartenum.abbreviateddescriptive/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.smartenum.abbreviateddescriptive/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.smartenum.abbreviateddescriptive/actions/workflows/codeql.yml)

# Soenneker.SmartEnum.AbbreviatedDescriptive

A SmartEnum base class for values that need a name, integer value, abbreviation, and display description.

## Installation

```bash
dotnet add package Soenneker.SmartEnum.AbbreviatedDescriptive
```

## Defining an enum

```csharp
using Soenneker.SmartEnum.AbbreviatedDescriptive;

public sealed class ShippingMethod : AbbreviatedDescriptiveSmartEnum<ShippingMethod>
{
    public static readonly ShippingMethod Ground =
        new(nameof(Ground), 1, "GND", "Ground shipping");

    public static readonly ShippingMethod NextDay =
        new(nameof(NextDay), 2, "NDA", "Next-day air");

    private ShippingMethod(string name, int value, string abbreviation, string? description = null)
        : base(name, value, abbreviation, description)
    {
    }
}
```

Members must be exposed as static fields so they can be discovered. When a description is omitted or set to `null`, `Description` returns the member's `Name`.

## Usage

```csharp
ShippingMethod method = ShippingMethod.FromDescription("Next-day air");
ShippingMethod sameMethod = ShippingMethod.FromAbbreviation("NDA");

IReadOnlyList<string> labels = ShippingMethod.GetAllDescriptions();
```

`FromDescription` uses an ordinal, case-sensitive comparison and throws when no description matches. Use an exact stored description when converting external input. `GetAllDescriptions` returns a new list ordered by member name, so callers may modify that list without changing the enum definitions.

Descriptions remain mutable and description lookup reads their latest values. Abbreviation lookup is initialized once by the base class, so treat abbreviations as immutable after the static members are created.
