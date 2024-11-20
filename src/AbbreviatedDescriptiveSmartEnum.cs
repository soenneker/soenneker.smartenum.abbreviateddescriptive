using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using Soenneker.Extensions.Type;
using Soenneker.SmartEnum.Abbreviated;

namespace Soenneker.SmartEnum.AbbreviatedDescriptive;

/// <summary>
/// Represents an abstract base class for abbreviated descriptive smart enums.
/// </summary>
/// <typeparam name="TEnum">The type of the enum.</typeparam>
public abstract class AbbreviatedDescriptiveSmartEnum<TEnum> : AbbreviatedSmartEnum<TEnum> where TEnum : AbbreviatedDescriptiveSmartEnum<TEnum>
{
    private string? _description;

    /// <summary>
    /// Gets or sets the description of the enum value. Returns Name if Description is null.
    /// </summary>
    public string Description
    {
        get => _description ?? Name;
        set => _description = value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AbbreviatedDescriptiveSmartEnum{TEnum}"/> class.
    /// </summary>
    /// <param name="name">The name of the enum value.</param>
    /// <param name="value">The value of the enum.</param>
    /// <param name="abbreviation">The abbreviation of the enum value.</param>
    /// <param name="description">The description of the enum value.</param>
    /// <param name="ignoreCase">A value indicating whether to ignore case when comparing abbreviations.</param>
    protected AbbreviatedDescriptiveSmartEnum(string name, int value, string abbreviation, string? description = null, bool ignoreCase = false)
        : base(name, value, abbreviation, ignoreCase)
    {
        _description = description;
    }

    /// <summary>
    /// Retrieves all available options for the enum, including descriptions.
    /// </summary>
    private static List<TEnum> GetAllOptionsWithDescriptions()
    {
        Type baseType = typeof(TEnum);

        List<TEnum> enumArray = Assembly.GetAssembly(baseType)!
            .GetTypes()
            .Where(baseType.IsAssignableFrom)
            .SelectMany(t => t.GetFieldsOfType<TEnum>())
            .OrderBy(t => t.Name)
            .ToList();

        StaticIgnoreCase = enumArray.First().IgnoreCase;

        return enumArray;
    }

    private static readonly Lazy<List<TEnum>> _enumOptionsWithDescriptions = new(GetAllOptionsWithDescriptions, LazyThreadSafetyMode.ExecutionAndPublication);

    /// <summary>
    /// Gets the enum value corresponding to the specified description.
    /// </summary>
    /// <param name="description">The description of the enum value to retrieve.</param>
    /// <returns>The enum value corresponding to the specified description.</returns>
    /// <exception cref="Exception">Thrown when the specified description is not found.</exception>
    public static TEnum FromDescription(string description)
    {
        _ = _enumOptionsWithDescriptions.Value;

        TEnum? enumValue = _enumOptionsWithDescriptions.Value
            .FirstOrDefault(enumValue => enumValue.Description == description);

        if (enumValue == null)
        {
            throw new Exception($"Description '{description}' not found in {nameof(AbbreviatedDescriptiveSmartEnum<TEnum>)}.");
        }

        return enumValue;
    }

    /// <summary>
    /// Gets all descriptions for the enum values.
    /// </summary>
    /// <returns>A list of descriptions for all enum values.</returns>
    public static List<string> GetAllDescriptions()
    {
        _ = _enumOptionsWithDescriptions.Value;

        return _enumOptionsWithDescriptions.Value
            .Select(enumValue => enumValue.Description)
            .ToList();
    }
}