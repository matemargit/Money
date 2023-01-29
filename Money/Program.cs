﻿using Microsoft.Extensions.DependencyInjection;

using Money;
using Money.Commands;
using Money.Data;
using Money.Data.DataAccess;

using Spectre.Console.Cli;

ServiceCollection registrations = new ServiceCollection();
registrations.AddSingleton<IWriteOnlyData, WriteOnlyData>();
registrations.AddSingleton<IReadonlyData, ReadOnlyData>();

ITypeRegistrar registrar = new TypeRegistrar(registrations);

CommandApp app = new CommandApp(registrar);
app.Configure(config =>
{
    config.AddCommand<AddCommand>("add");
    config.AddCommand<ExportCommand>("export");
    config.AddCommand<ImportCommand>("import");
    config.AddCommand<StatCommand>("stat");
}); 

return app.Run(args);