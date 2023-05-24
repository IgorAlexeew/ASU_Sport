using ASUSport.DTO;
using ClosedXML.Excel;
using System.IO;

namespace ASUSport.Helpers
{
    public class ExcelHelper
    {
        /// <summary>
        /// Создание Excel документа с информацией о событиях для заданных даты и спортивного объекта
        /// </summary>
        /// <param name="data"></param>
        public static byte[] GetEventsWithCLients(EventsWithClientsDTO data)
        {
            var workbook = new XLWorkbook();
            var workSheet = workbook.Worksheets.Add("EventsWithCLients");

            workSheet.Cell(1, "A").Value = "Название спортивного объекта";
            workSheet.Cell(1, "A").Style.Font.Bold = true;
            workSheet.Cell(1, "B").Value = data.SportObject;

            workSheet.Cell(2, "A").Value = "Дата";
            workSheet.Cell(2, "A").Style.Font.Bold = true;
            workSheet.Cell(2, "B").Value = data.Date;

            int currentRow = 2;

            foreach (var item in data.EventParticipants)
            {
                currentRow += 2;

                var range = workSheet.Range("A" + currentRow.ToString() + ":B" + currentRow.ToString());
                range.Style.Border.TopBorder = XLBorderStyleValues.Thin;

                workSheet.Cell(currentRow, "A").Value = "Время занятия";
                workSheet.Cell(currentRow, "A").Style.Font.Bold = true;
                workSheet.Cell(currentRow, "B").Value = item.Timestamp;
                currentRow++;

                workSheet.Cell(currentRow, "A").Value = "Название секции";
                workSheet.Cell(currentRow, "A").Style.Font.Bold = true;
                workSheet.Cell(currentRow, "B").Value = item.SectionName;
                currentRow++;

                workSheet.Cell(currentRow, "A").Value = "Тренер";
                workSheet.Cell(currentRow, "A").Style.Font.Bold = true;
                if (item.Trainer != null)
                {
                    string trainer = item.Trainer.LastName + " " + item.Trainer.FirstName + " " + item.Trainer.MiddleName;
                    workSheet.Cell(currentRow, "B").Value = trainer;
                }
                currentRow++;

                workSheet.Cell(currentRow, "A").Value = "Клиенты";
                workSheet.Cell(currentRow, "A").Style.Font.Bold = true;

                foreach (var client in item.Clients)
                {
                    currentRow++;
                    string name = client.LastName + " " + client.FirstName + " " + client.MiddleName;
                    workSheet.Cell(currentRow, "B").Value = name;
                    workSheet.Cell(currentRow, "B").Style.Font.Italic = true;
                    if (client.DateOfBirth != null)
                    {
                        System.Console.WriteLine(client.DateOfBirth);
                        currentRow++;
                        workSheet.Cell(currentRow, "B").Value = client.DateOfBirth;
                    }
                    if (client.PhoneNumber != null)
                    {
                        currentRow++;
                        workSheet.Cell(currentRow, "B").Value = client.PhoneNumber;
                    }
                }
            }

            workSheet.Columns("A").AdjustToContents();
            workSheet.Columns("B").AdjustToContents();

            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                var content = stream.ToArray();

                return content;
            }
        }
    }
}
