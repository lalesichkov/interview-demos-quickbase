

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;


public static class CountryNormalizer
{
    // Dictionary of normalized input → canonical country name
    private static readonly Dictionary<string, string> CountryMap =
        new Dictionary<string, string>
        {
            // USA variations
            { "usa", "USA" },
            { "us", "USA" },
            { "unitedstates", "USA" },
            { "unitedstatesofamerica", "USA" },
            { "u s a", "USA" },

            // UK variations
            { "uk", "UK" },
            { "unitedkingdom", "UK" },
            { "greatbritain", "UK" },
            { "britain", "UK" },
            { "u k", "UK" }
        };

    public static string Normalize(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return input;

        string key = Clean(input);

        return CountryMap.TryGetValue(key, out var value)
            ? value
            : input; // fallback if not found
    }

    private static string Clean(string input)
    {
        // Lowercase
        var normalized = input.ToLowerInvariant();

        // Remove punctuation (., etc.)
        normalized = Regex.Replace(normalized, @"[^\w\s]", "");

        // Remove extra spaces
        normalized = Regex.Replace(normalized, @"\s+", " ").Trim();

        // Optionally remove spaces entirely for matching
        normalized = normalized.Replace(" ", "");

        return normalized;
    }
}





