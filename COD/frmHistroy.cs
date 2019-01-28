using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DMSql;
using System.Collections;

namespace COD
{
    public partial class frmHistroy : Form
    {
        DataTable dtTaskInfo;
        public frmHistroy()
        {
            InitializeComponent();
        }

        private void frmHistroy_Load(object sender, EventArgs e)
        {

            this.lbl_Title.Left = (this.Width - this.lbl_Title.Width) / 2;
            this.CreateGridView();
            this.SetGridView();

        }

        private void CreateGridView()
        {
            dtTaskInfo = DbHelper.ExecuteQueryTable(" SELECT *  FROM D_TestResult WHERE 1=1   ");
            this.dtGridView.DataSource = dtTaskInfo;

        }
        private void SetGridView()
        {
            for (int i = 0; i < this.dtGridView.ColumnCount; i++)
            {
                this.dtGridView.Columns[i].Visible = false;

            }


            this.dtGridView.Columns["DigestnID"].Visible = true;
            this.dtGridView.Columns["DigestnID"].HeaderText = "消解管编号";
            this.dtGridView.Columns["DigestnID"].Width = 150;
            this.dtGridView.Columns["DigestnID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dtGridView.Columns["DigestnID"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.dtGridView.Columns["SampleFrom"].Visible = true;
            this.dtGridView.Columns["SampleFrom"].HeaderText = "水样源";
            this.dtGridView.Columns["SampleFrom"].Width = 150;
            this.dtGridView.Columns["SampleFrom"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dtGridView.Columns["SampleFrom"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.dtGridView.Columns["NumWithInGroup"].Visible = true;
            this.dtGridView.Columns["NumWithInGroup"].HeaderText = "组内编号";
            this.dtGridView.Columns["NumWithInGroup"].Width = 200;
            this.dtGridView.Columns["NumWithInGroup"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dtGridView.Columns["NumWithInGroup"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
           // this.dtGridView.Columns["NumWithInGroup"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            this.dtGridView.Columns["enumRangeState"].Visible = true;
            this.dtGridView.Columns["enumRangeState"].HeaderText = "试剂量程";
            this.dtGridView.Columns["enumRangeState"].Width = 200;
            this.dtGridView.Columns["enumRangeState"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dtGridView.Columns["enumRangeState"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.dtGridView.Columns["ConcentrationValue"].Visible = true;
            this.dtGridView.Columns["ConcentrationValue"].HeaderText = "浓度值";
            this.dtGridView.Columns["ConcentrationValue"].Width = 200;
            this.dtGridView.Columns["ConcentrationValue"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dtGridView.Columns["ConcentrationValue"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.dtGridView.Columns["Data_Time"].Visible = true;
            this.dtGridView.Columns["Data_Time"].HeaderText = "实验时间";
            this.dtGridView.Columns["Data_Time"].Width = 200;
            this.dtGridView.Columns["Data_Time"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dtGridView.Columns["Data_Time"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;


        }

        private void pic_Back_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
