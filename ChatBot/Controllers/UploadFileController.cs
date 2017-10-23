using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using OfficeOpenXml;
using System.Text;

namespace ChatBot.Controllers
{
    [Produces("application/json")]
    [Route("api/UploadFile")]
    public class UploadFileController : Controller
    {
        public string Post()
        {
            var files = Request.Form.Files;
            if (files != null)
            {
                if (files[0].FileName.EndsWith(".xlsx"))
                {
                    var stream = files[0].OpenReadStream();

                    StreamReader reader = new StreamReader(stream);
                    ExcelPackage package = new ExcelPackage(stream);
                    StringBuilder sb = new StringBuilder();
                    if (package.Workbook.Worksheets.Count > 0)
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                        int rowCount = worksheet.Dimension.Rows;
                        int ColCount = worksheet.Dimension.Columns;
                        //bool bHeaderRow = true;
                        for (int row = 1; row < rowCount; row++)
                        {
                            for (int col = 1; col < ColCount; col++)
                            {
                                //if (bHeaderRow)
                                //{
                                if (worksheet.Cells[row, col].Value != null)
                                {
                                    sb.Append(worksheet.Cells[row, col].Value.ToString() + "\t");
                                }
                                //}
                                //else
                                //{
                                    //sb.Append(worksheet.Cells[row, col].Value.ToString() + "\t");
                                //}
                            }
                            sb.Append(Environment.NewLine);
                        }
                        //MemoryStream memstream = new MemoryStream()


                        return sb.ToString();
                    }
                }
            }
            return "";

        }
    }
}