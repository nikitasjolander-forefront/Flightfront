using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace FlightFront.Infrastructure.Validators;

public static class SearchQueryValidator
{
    private const int MinQueryLength = 3;
    private static readonly string[] ForbiddenNameSubstrings = ["air"];

    public static ValidationResult ValidateQuery(string? query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return ValidationResult.Invalid("Query is required");

        if (query.Length < MinQueryLength)
            return ValidationResult.Invalid($"Query must be at least {MinQueryLength} characters");

        return ValidationResult.Valid();
    }

    public static ValidationResult ValidateNameQuery(string? query)
    {
        var baseValidation = ValidateQuery(query);
        if (!baseValidation.IsValid)
            return baseValidation;

        foreach (var forbidden in ForbiddenNameSubstrings)
        {
            if (query!.Contains(forbidden, StringComparison.OrdinalIgnoreCase))
                return ValidationResult.Invalid($"Query cannot contain '{forbidden}'");
        }

        return ValidationResult.Valid();
    }
}

public class ValidationResult
{
    public bool IsValid { get; private init; }
    public string? ErrorMessage { get; private init; }

    public static ValidationResult Valid() => new() { IsValid = true };
    public static ValidationResult Invalid(string errorMessage) => new() { IsValid = false, ErrorMessage = errorMessage };
}
