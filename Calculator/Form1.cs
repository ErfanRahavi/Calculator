using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Button = System.Windows.Forms.Button;

namespace Calculator
{
    public partial class Form1 : Form
    {
        Double result = 0;
        string operation = string.Empty;
        double enterFirstValue, enterSecondValue;
        string fstNum, secNum;
        bool enterValue = false;
        public string no1, constfun;
        public bool inputstatus;
        private double Memory;
        string path = "data_table.db";
        string cs = @"URI=file:" + Application.StartupPath + "\\data_table.db";

        SQLiteConnection con;
        SQLiteCommand cmd;
        SQLiteDataReader dr;


        public Form1()
        {
            InitializeComponent();
            btnMC.Enabled = false;
            btnMR.Enabled = false;
        }
        private void Create_db()
        {
            if (!System.IO.File.Exists(path))
            {
                SQLiteConnection.CreateFile(path);
                using (var sqlite = new SQLiteConnection(@"Data Source="+ path))
                {
                    sqlite.Open();
                    string sql = "create table test(history varchar(1000),memorycr varchar(12))";
                    SQLiteCommand command = new SQLiteCommand(sql,sqlite);
                    command.ExecuteNonQuery();
                }
            }
            else
            {
                Console.WriteLine("Database cannot create");
                return;
            }
        }
        private void data_show()
        {
            

            var con = new SQLiteConnection(cs);
            con.Open();

            string stm = "SELECT * FROM test";
            var cmd = new SQLiteCommand(stm, con);
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                dataGridView1.Rows.Insert(0, dr.GetString(0), dr.GetString(1));
                

            }
        }

        private void TxtDisplay1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button32_Click(object sender, EventArgs e)
        {
            secNum = TxtDisplay1.Text;
           
            enterSecondValue = Convert.ToDouble(TxtDisplay1.Text);

            
            TxtDisplay2.Text = $"{TxtDisplay2.Text} {TxtDisplay1.Text}=";
            if(TxtDisplay1.Text != string.Empty)
            {
                if (TxtDisplay1.Text == "0") TxtDisplay2.Text = string.Empty;
                switch (operation)
                {
                    case "+":
                        TxtDisplay1.Text = (result + Double.Parse(TxtDisplay1.Text)).ToString();
                        richTextBox1.AppendText($"{fstNum} {secNum} = {TxtDisplay1.Text} \n");
                        break;
                    case "-":
                        TxtDisplay1.Text = (result - Double.Parse(TxtDisplay1.Text)).ToString();
                        richTextBox1.AppendText($"{fstNum} {secNum} = {TxtDisplay1.Text} \n");
                        break;
                    case "*":
                        TxtDisplay1.Text = (result * Double.Parse(TxtDisplay1.Text)).ToString();
                        richTextBox1.AppendText($"{fstNum} {secNum} = {TxtDisplay1.Text} \n");
                        break;
                    case "/":
                        TxtDisplay1.Text = (result / Double.Parse(TxtDisplay1.Text)).ToString();
                        richTextBox1.AppendText($"{fstNum} {secNum} = {TxtDisplay1.Text} \n");
                        break;
                    case "Mod":
                        enterSecondValue = (result % double.Parse(TxtDisplay1.Text));
                        TxtDisplay1.Text = enterSecondValue.ToString();
                        
                        richTextBox1.AppendText($"{fstNum} {secNum} = {TxtDisplay1.Text} \n");
                        break;
                    case "Exp":
                        double i = Convert.ToDouble(TxtDisplay1.Text);
                        double j;
                        j = enterSecondValue;
                        TxtDisplay1.Text = Math.Exp(i  * Math.Log(j * 4)).ToString();
                        
                        break;
                    case "x^y":
                        result = Math.Pow(Convert.ToDouble(enterFirstValue), Convert.ToDouble(enterSecondValue));
                        TxtDisplay1.Text = result.ToString();
                        richTextBox1.AppendText($"{enterFirstValue}^{secNum} = {TxtDisplay1.Text} \n");
                        break;
                    default: TxtDisplay2.Text = $"{TxtDisplay1.Text}=";
                        break;
                }

                result = Double.Parse(TxtDisplay1.Text);
                operation = string.Empty;

                button1.Visible = true;
                
                label1.Text = "";
            }
        }

        private void BtnMathOperation_Click(object sender, EventArgs e)
        {
            if (result != 0) BtnEquals.PerformClick();
            else result = Double.Parse(TxtDisplay1.Text);

            Button button = (Button)sender;
            operation = button.Text;
            enterValue = true;
            if(TxtDisplay1.Text != "0")
            {
                TxtDisplay2.Text = fstNum = $"{result} {operation}";
                TxtDisplay1.Text = string.Empty;
            }
        }

        

        private void BtnBackSpace_Click(object sender, EventArgs e)
        {
            if (TxtDisplay1.Text.Length > 0)
                TxtDisplay1.Text = TxtDisplay1.Text.Remove(TxtDisplay1.Text.Length - 1, 1);
            if (TxtDisplay1.Text == string.Empty) TxtDisplay1.Text = "0";
        }

        private void BtnC_Click(object sender, EventArgs e)
        {
            TxtDisplay1.Text = "0";
            TxtDisplay2.Text = string.Empty;
            result = 0;
        }

        private void BtnCE_Click(object sender, EventArgs e)
        {
            TxtDisplay1.Text = "0";
        }

        private void BtnOperation_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            operation = button.Text;
            switch (operation)
            {
                case "√x":
                    TxtDisplay2.Text = $"√({TxtDisplay1.Text})";
                    TxtDisplay1.Text = Convert.ToString(Math.Sqrt(Double.Parse(TxtDisplay1.Text)));
                    break;
                case "x²":
                    TxtDisplay2.Text = $"({TxtDisplay1.Text})^2";
                    TxtDisplay1.Text = Convert.ToString(Convert.ToDouble(TxtDisplay1.Text) * Convert.ToDouble( TxtDisplay1.Text));
                    break;
                case "1/x":
                    TxtDisplay2.Text = $"1/({TxtDisplay1.Text})";
                    TxtDisplay1.Text = Convert.ToString(1.0 / Convert.ToDouble(TxtDisplay1.Text));
                    break;
                case "%":
                    TxtDisplay2.Text = $"%({TxtDisplay1.Text})";
                    TxtDisplay1.Text = Convert.ToString(Convert.ToDouble(TxtDisplay1.Text) / Convert.ToDouble(100));
                    break;
                case "±":
                    TxtDisplay1.Text = Convert.ToString(-1 * Convert.ToDouble(TxtDisplay1.Text));
                    break;
            }
            
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void digitCalculate_Click(object sender, EventArgs e)
        {
            Button ButtonThatWasPushed = (Button)sender;
            string ButtonText = ButtonThatWasPushed.Text;
            if (ButtonText == "MC")
            {
                TxtDisplay1.Text = "0";
                Memory = 0;
                btnMC.Enabled = false;
                btnMR.Enabled = false;
                
            }

            if (ButtonText == "MR")
            {
                TxtDisplay1.Text = Memory.ToString();
                
            }

            if (ButtonText == "MS")
            {
                Memory = Double.Parse(TxtDisplay1.Text);
                btnMC.Enabled = true;
                btnMR.Enabled = true;
                
            }

            if (ButtonText == "M-")
            {
                Memory -= Double.Parse(TxtDisplay1.Text);
                
            }

            if (ButtonText == "M+")
            {

                Memory += Double.Parse(TxtDisplay1.Text);
       
            }
        }

        //btnClearHistory
        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            if(label1.Text == "")
            {
                label1.Text = "There's no history yet";
            }
            button1.Visible = false;
            richTextBox1.ScrollBars = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Create_db();
            data_show();
            this.Width = 507; 
            TxtDisplay1.Width = 350;
            
        }

        private void standardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Width = 507;
            TxtDisplay1.Width = 350;
            

        }

        private void scientificToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Width = 780;
            TxtDisplay1.Width = 778;
            TxtDisplay2.Width = 778;
            
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnPi_Click(object sender, EventArgs e)
        {
            TxtDisplay1.Text = "3.141592653589976323";
        }

        private void btnLog_Click(object sender, EventArgs e)
        {
            fstNum = TxtDisplay1.Text;
            double logg = Convert.ToDouble(TxtDisplay1.Text);
            logg = Math.Log10(logg);
            TxtDisplay1.Text = Convert.ToString(logg);
            richTextBox1.AppendText($"log({fstNum}) = {TxtDisplay1.Text} \n");
            label1.Text = "";

        }

        private void btnx3_Click(object sender, EventArgs e)
        {
            fstNum = TxtDisplay1.Text;
            double x, q, p, m;

            q = Convert.ToDouble(TxtDisplay1.Text);
            p = Convert.ToDouble(TxtDisplay1.Text);
            m = Convert.ToDouble(TxtDisplay1.Text);

            x = (q * p * m);
            TxtDisplay1.Text = Convert.ToString(x);
            richTextBox1.AppendText($"{fstNum}^3 = {TxtDisplay1.Text} \n");
            label1.Text = "";
        }

        private void btnSinh_Click(object sender, EventArgs e)
        {
            fstNum = TxtDisplay1.Text;
            double sh = Convert.ToDouble(TxtDisplay1.Text);
            sh = Math.Sinh(sh);
            TxtDisplay1.Text = Convert.ToString(sh);
            richTextBox1.AppendText($"sinh({fstNum}) = {TxtDisplay1.Text} \n");
            label1.Text = "";
        }

        private void btnSin_Click(object sender, EventArgs e)
        {
            fstNum = TxtDisplay1.Text;
            double sin = Convert.ToDouble(TxtDisplay1.Text);
            sin = Math.Sin(sin);
            TxtDisplay1.Text = Convert.ToString(sin);
            richTextBox1.AppendText($"sin({fstNum}) = {TxtDisplay1.Text} \n");
            label1.Text = "";
        }

        private void btnCosh_Click(object sender, EventArgs e)
        {
            fstNum = TxtDisplay1.Text;
            double cosh = Convert.ToDouble(TxtDisplay1.Text);
            cosh = Math.Cosh(cosh);
            TxtDisplay1.Text = Convert.ToString(cosh);
            richTextBox1.AppendText($"cosh({fstNum}) = {TxtDisplay1.Text} \n");
            label1.Text = "";
        }

        private void btnCos_Click(object sender, EventArgs e)
        {
            fstNum = TxtDisplay1.Text;
            double cos = Convert.ToDouble(TxtDisplay1.Text);
            cos = Math.Cos(cos);
            TxtDisplay1.Text = Convert.ToString(cos);
            richTextBox1.AppendText($"cos({fstNum}) = {TxtDisplay1.Text} \n");
            label1.Text = "";
        }

        private void btnTanh_Click(object sender, EventArgs e)
        {
            fstNum = TxtDisplay1.Text;
            double tanh = Convert.ToDouble(TxtDisplay1.Text);
            tanh = Math.Tanh(tanh);
            TxtDisplay1.Text = Convert.ToString(tanh);
            richTextBox1.AppendText($"tanh({fstNum}) = {TxtDisplay1.Text} \n");
            label1.Text = "";
        }

        private void btnlnx_Click(object sender, EventArgs e)
        {
            fstNum = TxtDisplay1.Text;
            double lnx = Convert.ToDouble(TxtDisplay1.Text);
            lnx = Math.Log(lnx);
            TxtDisplay1.Text = Convert.ToString(lnx);
            richTextBox1.AppendText($"ln({fstNum}) = {TxtDisplay1.Text} \n");
            label1.Text = "";
        }

        private void btnxy(object sender, EventArgs e)
        {
            
            enterFirstValue = double.Parse(TxtDisplay1.Text);
            enterSecondValue = double.Parse(TxtDisplay1.Text);
            if (TxtDisplay1.Text.Length == 0)
            {

                TxtDisplay1.Text = "";

            }
            
            else
            {
                operation = "x^y";
                TxtDisplay1.Text = String.Empty;
            }
            

        }
        //btn e^x
        private void button21_Click(object sender, EventArgs e)
        {
            enterFirstValue = Convert.ToDouble(TxtDisplay1.Text);
            double ex = 2.718281828459045235;
            TxtDisplay1.Text = Math.Pow(Convert.ToDouble(ex), Convert.ToDouble(enterFirstValue)).ToString();
            richTextBox1.AppendText($" e^{enterFirstValue} = {TxtDisplay1.Text} \n");
        }

        private void btnSaveData_Click(object sender, EventArgs e)
        {
            

            var con = new SQLiteConnection(cs);
            con.Open();
            var cmd = new SQLiteCommand(con);

            try
            {
                cmd.CommandText = "INSERT INTO test(history,memorycr) VALUES(@history,@memorycr)";

                string HISTORY = richTextBox1.Text;
                string MEMORYCR = Memory.ToString();

                cmd.Parameters.AddWithValue("@history", HISTORY);
                cmd.Parameters.AddWithValue("@memorycr", MEMORYCR);

                dataGridView1.ColumnCount = 2;
                dataGridView1.Columns[0].Name = "History";
                dataGridView1.Columns[1].Name = "Memorycr";
                string[] row = new string[] { HISTORY, MEMORYCR };
                dataGridView1.Rows.Add(row);

                cmd.ExecuteNonQuery();
            }
            catch(Exception)
            {
                Console.WriteLine("cannot insert data");
                return;
            }
            

        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            
            this.Width = 1031;
            TxtDisplay1.Width = 778;
            TxtDisplay2.Width = 778;
        }

        //btn n!
        private void button23_Click(object sender, EventArgs e)
        {
            fstNum = TxtDisplay1.Text;
            int var1 = 1;
            for (int i = 1; i <= Convert.ToInt16(TxtDisplay1.Text); i++)
            {
                var1 = var1 * i;
            }
            TxtDisplay1.Text = Convert.ToString(var1);
            inputstatus = false;
            richTextBox1.AppendText($"fact({fstNum}) = {TxtDisplay1.Text} \n");
            label1.Text = "";

        }

        private void btnTan_Click(object sender, EventArgs e)
        {
            fstNum = TxtDisplay1.Text;
            double tan = Convert.ToDouble(TxtDisplay1.Text);
            tan = Math.Tan(tan);
            TxtDisplay1.Text = Convert.ToString(tan);
            richTextBox1.AppendText($"tan({fstNum}) = {TxtDisplay1.Text} \n");
            label1.Text = "";
        }

        private void BtnNum_Click(object sender, EventArgs e)
        {
            if (TxtDisplay1.Text == "0" || enterValue) TxtDisplay1.Text = string.Empty;

            enterValue = false;
            Button button = (Button)sender;
            if(button.Text == ".")
            {
                if (!TxtDisplay1.Text.Contains("."))
                    TxtDisplay1.Text = TxtDisplay1.Text + button.Text;
            }
           else TxtDisplay1.Text = TxtDisplay1.Text + button.Text;
        }
    }
}
