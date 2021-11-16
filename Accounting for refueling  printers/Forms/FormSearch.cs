using Microsoft.Office.Interop.Excel;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Application = System.Windows.Forms.Application;
using Excel = Microsoft.Office.Interop.Excel;


namespace Accounting_for_refueling__printers.Forms
{


    public partial class FormSearch : Form
    {
        string filter1;

        private SqlConnection sqlConnection = null;


        public FormSearch()
        {
            InitializeComponent();
        }

        private void FormSearch_Load(object sender, EventArgs e)
        {

            
            // TODO: данная строка кода позволяет загрузить данные в таблицу "databaseDataSet3.Printer". При необходимости она может быть перемещена или удалена.
            this.printerTableAdapter.Fill(this.databaseDataSet3.Printer);
            FormMainMenu formMainMenu = new FormMainMenu();

            sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Application.StartupPath + @"\Database.mdf;Integrated Security=True");
            sqlConnection.Open();



            comboBox1.Text = "";
            comboBox2.Text = "";
            filter1 = "";
            LoadTheme();

        }


        private void btnSearch_Click(object sender, EventArgs e)
        {

            if (checkBox1.Checked && !checkBox2.Checked)
            {
                DateTime date = DateTime.Parse(dateTimePicker1.Text);



                if (textBox1.Text != "")
                {
                    filter1 += $" Кабинет like '{textBox1.Text}%' and ";
                }
                if (comboBox1.Text != "")
                {
                    filter1 += $" Модель like '{comboBox1.Text}%' and ";

                }

                filter1 += $" Дата = '{date.Year}.{date.Month}.{date.Day}' and ";
                if (comboBox1.Text != "")
                {
                    filter1 += $" Операции like N'{textBox4.Text}%' and ";

                }
                if (comboBox2.Text != "")
                {
                    filter1 += $" Состояние like N'{comboBox2.Text}%' and ";

                }

                filter1 = filter1.Remove(filter1.Length - 4);
                
                SqlDataAdapter dataAdapter1 = new SqlDataAdapter($"Select Дата,Кабинет, Модель,  Операции, Состояние from Printer where {filter1}", sqlConnection);
                DataSet dataSet1 = new DataSet();
                dataAdapter1.Fill(dataSet1);
                DataGridSearch.DataSource = dataSet1.Tables[0];

                panel1.Visible = false;
                panel2.Visible = true;

            }
            else if (!checkBox1.Checked && !checkBox2.Checked)
            {


                try
                {

                    if (textBox1.Text != "")
                    {
                        filter1 += $" Кабинет like '{textBox1.Text}%' and ";
                    }
                    if (comboBox1.Text != "")
                    {
                        filter1 += $" Модель like '{comboBox1.Text}%' and ";

                    }

                    if (textBox4.Text != "")
                    {
                        filter1 += $" Операции like N'{textBox4.Text}%' and ";

                    }
                    if (comboBox2.Text != "")
                    {
                        filter1 += $" Состояние like N'{comboBox2.Text}%' and ";

                    }

                    filter1 = filter1.Remove(filter1.Length - 4);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter($"Select Дата, Кабинет, Модель,  Операции, Состояние from Printer where {filter1}", sqlConnection);
                    DataSet dataSet = new DataSet();
                    dataAdapter.Fill(dataSet);
                    DataGridSearch.DataSource = dataSet.Tables[0];
                    panel1.Visible = false;
                    panel2.Visible = true;
                }
                catch
                {

                    MessageBox.Show("Введите хотя бы один фильтр", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            else if (checkBox1.Checked && checkBox2.Checked)
            {
                DateTime date = DateTime.Parse(dateTimePicker1.Text);
                DateTime date1 = DateTime.Parse(dateTimePicker2.Text);



                if (textBox1.Text != "")
                {
                    filter1 += $" Кабинет like '{textBox1.Text}%' and ";
                }
                if (comboBox1.Text != "")
                {
                    filter1 += $" Модель like '{comboBox1.Text}%' and ";

                }

                filter1 += $" Дата between '{date.Year}.{date.Month}.{date.Day}' and '{date1.Year}.{date1.Month}.{date1.Day}' and ";
                if (comboBox1.Text != "")
                {
                    filter1 += $" Операции like N'{textBox4.Text}%' and ";

                }
                if (comboBox2.Text != "")
                {
                    filter1 += $" Состояние like N'{comboBox2.Text}%' and ";

                }

                
                filter1 = filter1.Remove(filter1.Length - 4);

                SqlDataAdapter dataAdapter = new SqlDataAdapter($"Select Дата, Кабинет, Модель, Операции, Состояние from Printer where {filter1}", sqlConnection);
                DataSet dataSetSearch = new DataSet();
                dataAdapter.Fill(dataSetSearch);
                DataGridSearch.DataSource = dataSetSearch.Tables[0];
                panel1.Visible = false;
                panel2.Visible = true;
            }



        }



        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox1.Checked = true;
                checkBox1.Enabled = false;
            }
            else
            {
                checkBox1.Checked = false;
                checkBox1.Enabled = true;

            }
        }
        void LoadTheme()
        {

            label1.ForeColor = ThemeColor.PrimaryColor;
            label2.ForeColor = ThemeColor.PrimaryColor;
            label3.ForeColor = ThemeColor.PrimaryColor;
            label4.ForeColor = ThemeColor.PrimaryColor;
            label5.ForeColor = ThemeColor.PrimaryColor;
            label6.ForeColor = ThemeColor.PrimaryColor;
            textBox1.ForeColor = ThemeColor.PrimaryColor;
            textBox4.ForeColor = ThemeColor.PrimaryColor;
            comboBox1.ForeColor = ThemeColor.PrimaryColor;
            comboBox2.ForeColor = ThemeColor.PrimaryColor;
            //btnSearch
            btnSearch.BackColor = ThemeColor.PrimaryColor;
            btnSearch.ForeColor = Color.White;
            btnSearch.FlatAppearance.BorderColor = ThemeColor.SecondaryColor;
            //btnReturn
            btnReturn.BackColor = ThemeColor.PrimaryColor;
            btnReturn.ForeColor = Color.White;
            btnReturn.FlatAppearance.BorderColor = ThemeColor.SecondaryColor;
            //btnPrint
            btnPrint.BackColor = ThemeColor.PrimaryColor;
            btnPrint.ForeColor = Color.White;
            btnPrint.FlatAppearance.BorderColor = ThemeColor.SecondaryColor;
            //Panel
            panelTool.BackColor = ThemeColor.ChangeColorBrightness(ThemeColor.PrimaryColor, -0.3);


        }


        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (panel2.Visible)
            {
                panel2.Visible = false;
                panel1.Visible = true;
                filter1 = "";
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            SqlCommand Update1 = new SqlCommand($"Update Printer SET Состояние = N'Выписано' where {filter1}", sqlConnection);
            if (Update1.ExecuteNonQuery()==1)
            {
                FormMainMenu.SelfRef.UpdateTable();
            }
            else
            {
                Update1.Cancel();
                MessageBox.Show("В изменение состояния произошла ошибка");
            }

            
                
            

            try
            {
               
                DateTime now = DateTime.Now;
                Excel.Application app = new Excel.Application();
                Workbook workbook = app.Workbooks.Add(Type.Missing);
                Worksheet worksheet = null;

                worksheet = workbook.Sheets[1];
                worksheet = workbook.ActiveSheet;
                worksheet.Name = "Exported from gridview";
                //Fill Excel.
                worksheet.Cells[1, 1] = $"Заправки принтеров за {now.ToString("Y").ToUpper()}";

                for (int i = 1; i < DataGridSearch.Columns.Count + 1; i++)
                {
                    worksheet.Cells[2, i] = DataGridSearch.Columns[i - 1].HeaderText;
                }
                worksheet.Cells[2, 6] = "Стоимость с НДС";
                worksheet.Cells[2, 7] = "Б или В/Б";
                for (int i = 0; i < DataGridSearch.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < DataGridSearch.Columns.Count; j++)
                    {
                        if (j == 0)
                        {
                            worksheet.Cells[i + 3, j + 1] = DataGridSearch.Rows[i].Cells[j].Value.ToString().Remove(DataGridSearch.Rows[i].Cells[j].Value.ToString().Length - 8);
                        }
                        else
                        {
                            worksheet.Cells[i + 3, j + 1] = DataGridSearch.Rows[i].Cells[j].Value.ToString();
                        }


                    }
                }
            //Format export in Excel.

            ((Range)worksheet.get_Range($"A1:G1")).Merge();
                ((Range)worksheet.get_Range($"A1:G{DataGridSearch.Rows.Count + 1}")).Cells.Borders.LineStyle = XlLineStyle.xlContinuous;
                ((Range)worksheet.get_Range($"A1:G2")).Cells.Font.FontStyle = "Bold";
                worksheet.Cells.Style.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                worksheet.Cells.Font.Name = "Arial";
                worksheet.Cells.Font.Size = 10;
                worksheet.Columns.AutoFit();
                app.Visible = true;


            }
            catch (Exception ex)
            {
                


                MessageBox.Show(ex.Message, "Ошибка экспорта данных Excel таблицу");
            }
            filter1 = "";

            
            

        }
    }

}
