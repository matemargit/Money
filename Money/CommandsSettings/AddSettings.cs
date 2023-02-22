using System.ComponentModel;

using Spectre.Console;

namespace Money.CommandsSettings;

internal sealed class AddSettings : CommandSettings
{
    [Description("A short text description of the spending")]
    [CommandOption("-t|--text")]
    public string Text { get; init; }

    [Description("Spending category")]
    [CommandOption("-c|--category")]
    public string Category { get; set; }

    [Description("Amount of money spent")]
    [CommandArgument(0, "[amount]")]
    public double Amount { get; init; }

    [Description("Spending time. If not set current date will be used")]
    [CommandOption("-d|--date")]
    public DateOnly Date { get; init; }

    [Description("Batch input. Allows to input multiple entries at once")]
    [CommandOption("-b|--batch")]
    public bool BatchMode { get; set; }

    public AddSettings()
    {
        Text = string.Empty;
        Category = string.Empty;
        Date = DateTime.Now.ToDateOnly();
    }

    public override ValidationResult Validate()
    {
        if (BatchMode)
            return ValidationResult.Success();

        ValidationResultBuilder errorbuilder = new();

        return errorbuilder
            .AddIfTrue(() => Amount <= 0, Resources.ErrorAddNegativeAmmount)
            .AddIfNullOrWhiteSpace(Text, Resources.ErrorEmptyText)
            .AddIfNullOrWhiteSpace(Category, Resources.ErrorCategoryNameNull)
            .Build();
    }
}
