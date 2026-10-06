using System.Diagnostics.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Soenneker.Extensions.Type;
using Soenneker.SmartEnum.Abbreviated;

namespace Soenneker.SmartEnum.AbbreviatedDescriptive;

/// <summary>
/// Represents an abstract base class for abbreviated descriptive smart enums.
/// </summary>
/// <typeparam name="TEnum">The type of the enum.</typeparam>
public abstract class AbbreviatedDescriptiveSmartEnum<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields)] TEnum> : AbbreviatedSmartEnum<TEnum> where TEnum : AbbreviatedDescriptiveSmartEnum<TEnum>
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

    protected AbbreviatedDescriptiveSmartEnum(string name, int value, string abbreviation, string? description = null, bool ignoreCase = false)
        : base(name, value, abbreviation, ignoreCase)
    {
        _description = description;
    }

    private static readonly object _optionsLock = new();
    private static List<TEnum>? _registeredOptions;
    private static bool _optionsRead;

    /// <summary>Registers all lookup values, including values declared on derived types, before the first lookup.</summary>
    public static void RegisterDescriptionOptions(IEnumerable<TEnum> options)
    {
        ArgumentNullException.ThrowIfNull(options);
        List<TEnum> values = options.ToList();
        lock (_optionsLock)
        {
            if (_optionsRead) throw new InvalidOperationException("Register enum options before the first lookup.");
            _registeredOptions = values;
        }
    }

    private static List<TEnum> GetAllOptionsWithDescriptions()
    {
        lock (_optionsLock)
        {
            _optionsRead = true;
            List<TEnum> enums = (_registeredOptions ?? typeof(TEnum).GetFieldsOfType<TEnum>()).OrderBy(t => t.Name).ToList();
            if (enums.Count != 0)
                StaticIgnoreCase = enums[0].IgnoreCase;
            return enums;
        }
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
        foreach (TEnum enumValue in _enumOptionsWithDescriptions.Value)
        {
            if (enumValue.Description == description)
                return enumValue;
        }

        throw new Exception($"Description '{description}' not found in {nameof(AbbreviatedDescriptiveSmartEnum<TEnum>)}.");
    }

    /// <summary>
    /// Gets all descriptions for the enum values.
    /// </summary>
    /// <returns>A list of descriptions for all enum values.</returns>
    public static List<string> GetAllDescriptions()
    {
        List<TEnum> options = _enumOptionsWithDescriptions.Value;
        var descriptions = new List<string>(options.Count);
        foreach (TEnum option in options)
            descriptions.Add(option.Description);
        return descriptions;
    }
}