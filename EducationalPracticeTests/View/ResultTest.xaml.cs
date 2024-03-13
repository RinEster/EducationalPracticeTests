using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EducationalPracticeTests.View
{
    /// <summary>
    /// Логика взаимодействия для ResultTest.xaml
    /// </summary>
    public partial class ResultTest : UserControl
    {
        public ResultTest()
        {
            InitializeComponent();

            using (SqlConnection conn = new SqlConnection(MainWindow.connection))
            {
                conn.Open();
                string proc = "dbo.allResultTest";
                SqlCommand sqlCommand = new SqlCommand(proc, conn);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                result.ItemsSource = dataTable.DefaultView;
            }
        }
        private void SaveToPdf(DataGrid dataGrid, string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                Document document = new Document(PageSize.A4);
                PdfWriter.GetInstance(document, fs);
                document.Open();

            

                // Добавляем содержимое DataGrid
                PdfPTable table = new PdfPTable(dataGrid.Columns.Count);
                foreach (DataGridColumn column in dataGrid.Columns)
                {
                    table.AddCell(new Phrase(column.Header.ToString()));
                }

                foreach (var item in dataGrid.Items)
                {
                    foreach (DataGridColumn column in dataGrid.Columns)
                    {
                        var cellValue = column.GetCellContent(item) as TextBlock;
                        if (cellValue != null)
                        {
                            table.AddCell(new Phrase(cellValue.Text));
                        }
                    }
                }

                document.Add(table);

                document.Close();
            }

            MessageBox.Show("PDF файл успешно сохранен.");
        }

        private void toPDF_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                SaveToPdf(result, saveFileDialog.FileName); 
            }
        }
    }
}

