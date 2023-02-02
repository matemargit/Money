﻿using System.Diagnostics.CodeAnalysis;

using MiniExcelLibs;

namespace Money.Commands
{
    internal sealed class ExportExcelCommand : AsyncCommand<ExportSetting>
    {
        private readonly IReadonlyData _readonlyData;

        public ExportExcelCommand(IReadonlyData readonlyData)
        {
            _readonlyData = readonlyData;
        }

        public override async Task<int> ExecuteAsync([NotNull] CommandContext context,
                                                     [NotNull] ExportSetting settings)
        {
            try
            {
                IList<Data.Dto.ExportRow> data = await _readonlyData.ExportAsync(settings.StartDate, settings.EndDate);

                using (FileStream srtream = File.Create(settings.FileName))
                {
                    srtream.SaveAs(data);
                }
                Ui.Success(Resources.SuccessExport, data.Count, settings.FileName);
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
