using System.Text;
using System.Text.Json.Nodes;
using Microsoft.Kiota.Abstractions;
using Sharable.Models;
using Syncfusion.XlsIO;

public class ExcelService(IGameInfoService gameInfoService) : IExcelService
{

    private readonly IGameInfoService _gameInfoService = gameInfoService;

    public async Task<XGPGame[]> ParseXGPStream(Stream stream)
    {
        var excelEngine = new ExcelEngine();
        var excel = excelEngine.Excel;
        var jsonStream = new MemoryStream();
        var masterList = excel.Workbooks.Open(stream, ExcelOpenType.Automatic).Worksheets[0];
        var result = new List<XGPGame>();

        foreach (var row in masterList.Rows.Skip(2))
        {
            try
            {
                result.Add(new XGPGame
                {
                    CnName = row.Cells[0].DisplayText,
                    EnName = row.Cells[0].DisplayText,
                    Genre = row.Cells[11].DisplayText,
                    Metacritic = row.Cells[9].DisplayText == "" ? null : int.Parse(row.Cells[9].DisplayText),
                    Platform = row.Cells[1].DisplayText,
                    Status = row.Cells[3].DisplayText,
                    AddTS = DateTime.Parse(row.Cells[4].DisplayText),
                    ReleaseTS = DateTime.Parse(row.Cells[7].DisplayText)
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        result = await _gameInfoService.Parse(result.Where(game => game.Status != "Removed").ToList());

        jsonStream.Dispose();
        excelEngine.Dispose();

        return [.. result.Where(game => game.Status != "Removed")];
    }
}