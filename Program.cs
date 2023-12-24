using Sharable.Models;
using Sharable.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

builder.Services.AddSingleton<IAuthService, AuthService>();
builder.Services.AddSingleton<ISourceSettingService, SourceSettingService>();
builder.Services.AddSingleton<IDataTableProcessor, DataTableProcessor>();
builder.Services.AddSingleton<IExcelService, ExcelService>();
builder.Services.AddSingleton<IGameInfoService, GameInfoService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
