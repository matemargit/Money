using System.ComponentModel;

using Spectre.Console;

namespace Money.CommandsSettings;

internal class CategoryAddSettings : CommandSettings
{
    [Description("Category name")]
    [CommandArgument(0, "[category]")]
    public string CategoryName { get; set; }

    [Description("Bach input. Allows to input multiple entries at once")]
    [CommandOption("-b|--bach")]
    public bool BatchMode { get; set; }

    public CategoryAddSettings()
    {
        CategoryName = string.Empty;
    }

    public override ValidationResult Validate()
    {
        if (string.IsNullOrEmpty(CategoryName) && !BatchMode)
            return ValidationResult.Error(Resources.ErrorCategoryNameNull);

        return ValidationResult.Success();
    }
}
