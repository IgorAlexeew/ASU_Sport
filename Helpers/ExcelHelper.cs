using ASUSport.DTO;
using Excel = Microsoft.Office.Interop.Excel;

namespace ASUSport.Helpers
{
    public class ExcelHelper
    {
        /// <summary>
        /// Создание Excel документа с информацией о событиях для заданных даты и спортивного объекта
        /// </summary>
        /// <param name="data"></param>
        public static void GetEventsWithCLients(EventsWithClientsDTO data)
        {
            var excelApp = new Excel.Application();
            excelApp.Visible = true;
            excelApp.Workbooks.Add();
            Excel._Worksheet workSheet = (Excel.Worksheet)excelApp.ActiveSheet;

            workSheet.Cells[1, "A"] = "Название спортивного объекта";
            workSheet.Cells[1, "A"].Font.Bold = true;
            workSheet.Cells[1, "B"] = data.SportObject;

            workSheet.Cells[2, "A"] = "Дата";
            workSheet.Cells[2, "A"].Font.Bold = true;
            workSheet.Cells[2, "B"] = data.Date;

            int currentRow = 2;
            Excel.Range workSheet_range = null;
            Excel.Range c1 = null;
            Excel.Range c2 = null;

            foreach (var item in data.EventParticipants)
            {
                currentRow += 2;

                c1 = workSheet.Cells[currentRow, "A"];
                c2 = workSheet.Cells[currentRow, "B"];
                workSheet_range = workSheet.get_Range(c1, c2);
                workSheet_range.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;

                workSheet.Cells[currentRow, "A"] = "Время занятия";
                workSheet.Cells[currentRow, "A"].Font.Bold = true;
                workSheet.Cells[currentRow, "B"] = item.Timestamp;
                currentRow++;

                workSheet.Cells[currentRow, "A"] = "Название секции";
                workSheet.Cells[currentRow, "A"].Font.Bold = true;
                workSheet.Cells[currentRow, "B"] = item.SectionName;
                currentRow++;

                workSheet.Cells[currentRow, "A"] = "Тренер";
                workSheet.Cells[currentRow, "A"].Font.Bold = true;
                if (item.Trainer != null)
                {
                    string trainer = item.Trainer.FirstName + " " + item.Trainer.MiddleName + " " + item.Trainer.LastName;
                    workSheet.Cells[currentRow, "B"] = trainer;
                }
                currentRow++;

                workSheet.Cells[currentRow, "A"] = "Клиенты";
                workSheet.Cells[currentRow, "A"].Font.Bold = true;

                foreach (var client in item.Clients)
                {
                    currentRow++;
                    string name = client.FirstName + " " + client.MiddleName + " " + client.LastName;
                    workSheet.Cells[currentRow, "B"] = name;
                }

                /*c1 = workSheet.Cells[currentRow, "A"];
                c2 = workSheet.Cells[currentRow, "B"];
                workSheet_range = workSheet.get_Range(c1, c2);
                workSheet_range.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;*/
            }

            workSheet.Columns[1].AutoFit();
            workSheet.Columns[2].AutoFit();
        }
    }
}
