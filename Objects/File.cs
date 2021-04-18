using FSTEC.Properties;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FSTEC
{
    public class FileExcel
    {
        private static List<Danger> dangers;
        public static List<Danger> Dangers
        {
            get
            {
                if (dangers == null)
                {
                    try
                    {
                        dangers = ParseFileExcel();
                    }
                    catch
                    {
                        dangers = new List<Danger>();
                    }

                }

                return dangers;
            }
        }

        internal static bool CheckExist()
        {
            return File.Exists(Settings.Default.LocalFilePath);
        }
        internal static List<Danger> ParseFileExcel(string sheetName = "Sheet", int headerLine = 2)
        {
            List<Danger> dangers_local = new List<Danger>();
            if (CheckExist())
                using (var package = new ExcelPackage(new FileInfo(Settings.Default.LocalFilePath), true))
                {
                    var worksheet = package.Workbook.Worksheets.Where(sheet => sheet.Name == sheetName).First();
                    for (int row = worksheet.Dimension.Start.Row + headerLine; row <= worksheet.Dimension.End.Row; row++)
                    {
                        Dictionary<string, string> values = new Dictionary<string, string>();
                        for (int column = worksheet.Dimension.Start.Column; column <= worksheet.Dimension.End.Column; column++)
                        {
                            var param = worksheet.Cells[headerLine, column].Value;
                            var value = worksheet.Cells[row, column].Value;
                            values.Add(param == null ? "" : param.ToString(),
                                       value == null ? "" : value.ToString()
                                       );
                        }
                        dangers_local.Add(new Danger(values));
                    }
                }
            dangers = dangers_local;
            return dangers_local;
        }

        internal static bool SaveFile(string savePath)
        {
            if (CheckExist())
            {
                File.Copy(Settings.Default.LocalFilePath, savePath, true);
                return true;
            }
            return false;
        }
    }
}
