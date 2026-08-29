[![](https://img.shields.io/nuget/v/soenneker.smartenum.abbreviateddescriptive.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.smartenum.abbreviateddescriptive/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.smartenum.abbreviateddescriptive/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.smartenum.abbreviateddescriptive/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.smartenum.abbreviateddescriptive.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.smartenum.abbreviateddescriptive/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.smartenum.abbreviateddescriptive/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.smartenum.abbreviateddescriptive/actions/workflows/codeql.yml)

# Soenneker.SmartEnum.AbbreviatedDescriptive

Represents an abstract base class for abbreviated descriptive smart enums.

## Install

```bash
dotnet add package Soenneker.SmartEnum.AbbreviatedDescriptive
```

## What you get

- `AbbreviatedDescriptiveSmartEnum<TEnum>` — Represents an abstract base class for abbreviated descriptive smart enums.

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `AbbreviatedDescriptiveSmartEnum<TEnum>.Description` | Gets or sets the description of the enum value. Returns Name if Description is null. | Gets or sets the description of the enum value. Returns Name if Description is null. |
| `AbbreviatedDescriptiveSmartEnum<TEnum>.FromDescription(description)` | Gets the enum value corresponding to the specified description. | The enum value corresponding to the specified description. |

## Important behavior

- `AbbreviatedDescriptiveSmartEnum<TEnum>.FromDescription(description)`: Thrown when the specified description is not found.
