using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using Excel = Microsoft.Office.Interop.Excel;

namespace Traveler.FileExporter.OptionExporter
{
    public class DeleteOption : DataCollector
    {

        int row;
        Excel.Application dataApp;
        Excel.Workbook dataWorkbook;
        Excel.Worksheet dataWorkSheet;
        Excel.Range excelRange;

        public void ExportData(TourData tourData)
        {
            row = 4;
            dataApp = new Excel.Application();
            dataWorkbook = dataApp.Workbooks.Open(@"D:\\Works\\Document\\Excel\\Traveller Data Log.xlsx");
            dataWorkSheet = dataWorkbook.Worksheets[2];
            excelRange = dataWorkSheet.UsedRange;

            for (int i = 1; i <= (GetDataSize() + 1); i++)
            {
                if (excelRange.Cells[1][row].Value == null)
                {
                    excelRange.Cells[1][row] = i;
                    excelRange.Cells[2][row] = tourData.GetId().ToString();
                    excelRange.Cells[3][row] = tourData.GetTourName();
                    excelRange.Cells[4][row] = tourData.GetDateBegin();
                    excelRange.Cells[5][row] = tourData.GetDateEnd();
                    excelRange.Cells[6][row] = tourData.GetPrice();
                    excelRange.Cells[7][row] = tourData.GetHotel();
                    excelRange.Cells[8][row] = tourData.GetMeetingLocation();
                    excelRange.Cells[9][row] = DateTime.Now;
                    break;
                }
                else
                {
                    row++;
                }
            }

            dataWorkbook.Save();
            dataWorkbook.Close();
        }

        public int GetDataSize()
        {
            int size = 0;
            row = 4;
            do
            {
                if (excelRange.Cells[1][row].Value == null)
                {
                    break;
                }
                else
                {
                    row++;
                    size++;
                }
            }
            while (true);
            return size;
        }
    }
}