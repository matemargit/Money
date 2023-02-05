﻿using System.Diagnostics.CodeAnalysis;

using MiniExcelLibs;

using Money.Data.Dto;

namespace Money.Commands
{
    internal sealed class CreateExcelTemplateCommand : Command<CreateExcelTemplateSettings>
    {
        public override int Execute(CommandContext context,
                                    CreateExcelTemplateSettings settings)
        {
            try
            {
                List<DataRow> data = new List<DataRow>();

                using (FileStream srtream = File.Create(settings.FileName))
                {
                    srtream.SaveAs(data);
                }
                Ui.Success(Resources.SuccessCreatedImportTemplate, settings.FileName);
                return Constants.Success;
            }
            catch (Exception ex)
            {
                Ui.PrintException(ex);
                return Constants.IoError;
            }
        }
    }
}
