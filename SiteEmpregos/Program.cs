using EasyAutomationFramework;
using EasyAutomationFramework.Model;
using OfficeOpenXml;
using SiteEmpregos.Driver;
using System.Data;

WebScrapper web = new WebScrapper();
DataTable vagas = web.GetData("https://www.empregos.com.br/vagas");

using (var package = new ExcelPackage())
{
    var worksheet = package.Workbook.Worksheets.Add("VagasSheet");

    worksheet.Cells.LoadFromDataTable(vagas, true);

    string dirPath = @"C:\temp\excel";
    string pathExcel = @"C:\temp\excel\Vagas.xlsx";

    if (!Directory.Exists(dirPath))
    {
        Directory.CreateDirectory(dirPath);
    }

    var fileInfo = new System.IO.FileInfo(pathExcel);
    package.SaveAs(fileInfo);
}

Console.WriteLine("Arquivo excel criado com sucesso!");
