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
    public partial class frmTask : Form
    {
        DataTable dtTaskInfo,dtSys;
        frmMain fFrmMain;


        int iCurTaskStep = 0;
        public string sChange = "0";

        public frmTask(frmMain vFrmMain)
        {
            fFrmMain = vFrmMain;

            InitializeComponent();
        }

        private void picQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmTask_Load(object sender, EventArgs e)
        {
            dtSys = DbHelper.ExecuteQueryTable(" SELECT *  FROM D_Sys_Info WHERE ItemID ='1'  ");
            if (dtSys.Rows.Count == 0)
            {
                int i = DbHelper.ExecuteNonQuery("INSERT INTO  D_Sys_Info(ItemName,ItemID,ItemVal) VALUES('当前任务','1','0')");
            }
            else
            {
                iCurTaskStep = int.Parse(dtSys.Rows[0]["ItemVal"].ToString());
                  
            }
            

            this.CreateComboBox(cmb_TubeNum, 32);
            this.CreateGridView();
            this.CreateCombox_MD();
            this.SetGridView();


        }
        private void SetGridView()
        {
            for (int i = 0; i < this.dtGridView.ColumnCount; i++)
            {
                this.dtGridView.Columns[i].Visible = false;

            }
           

            this.dtGridView.Columns["TubeNum"].Visible = true;
            this.dtGridView.Columns["TubeNum"].HeaderText = "水样位置";
            this.dtGridView.Columns["TubeNum"].Width = 130;
            this.dtGridView.Columns["TubeNum"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dtGridView.Columns["TubeNum"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.dtGridView.Columns["ExperimentNum"].Visible = true;
            this.dtGridView.Columns["ExperimentNum"].HeaderText = "实验组数";
            this.dtGridView.Columns["ExperimentNum"].Width = 130;
            this.dtGridView.Columns["ExperimentNum"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dtGridView.Columns["ExperimentNum"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.dtGridView.Columns["ConcentrationEstimate"].Visible = true;
            this.dtGridView.Columns["ConcentrationEstimate"].HeaderText = "浓度";
           this.dtGridView.Columns["ConcentrationEstimate"].Width = 100;
            this.dtGridView.Columns["ConcentrationEstimate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dtGridView.Columns["ConcentrationEstimate"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
          
            this.dtGridView.Columns["TestMan"].Visible = true;
            this.dtGridView.Columns["TestMan"].HeaderText = "实验员";
            this.dtGridView.Columns["TestMan"].Width = 120;
            this.dtGridView.Columns["TestMan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dtGridView.Columns["TestMan"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.dtGridView.Columns["WaterAddress"].Visible = true;
            this.dtGridView.Columns["WaterAddress"].HeaderText = "水源信息";
            this.dtGridView.Columns["WaterAddress"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dtGridView.Columns["WaterAddress"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dtGridView.Columns["WaterAddress"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


            this.dtGridView.Columns["DelMark"].Visible = true;
            this.dtGridView.Columns["DelMark"].HeaderText = "删除";
            this.dtGridView.Columns["DelMark"].Width = 80;
            this.dtGridView.Columns["DelMark"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dtGridView.Columns["DelMark"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;


        }
        private void CreateGridView()
        {
            this.dtGridView.DataSource = dtTaskInfo;



        }
        private void CreateCombox_MD()
        {
            ArrayList arrayList = new ArrayList();
            arrayList.Add(new CreateArrayList("未知浓度", "0"));
            arrayList.Add(new CreateArrayList("高浓度", "1"));
            arrayList.Add(new CreateArrayList("低浓度", "2"));

            this.cmb_MD.DataSource = arrayList ;

			this.cmb_MD.DisplayMember = "strName" ;
            this.cmb_MD.ValueMember = "strValue";
        }
       
        private void CreateComboBox(ComboBox comboBox, int TotalCount)
        {
            dtTaskInfo = DbHelper.ExecuteQueryTable(" SELECT [id]  ,[TubeNum]      ,[ExperimentNum]      ,(case [ConcentrationEstimate]  when '0' then '未知浓度'  when '1' then '高浓度' else '低浓度' end ) AS  ConcentrationEstimate    ,[ConcentrationMeasure]  ,[DissolutionTime]      ,[CreateDate]      ,[TestMan]      ,[MD]      ,WaterAddress ,[Mark],'删除' AS DelMark FROM D_TestInfo WHERE Mark ='0' ORDER BY id ");

            ArrayList arrayList = new ArrayList();
            DataRow[] dRow;

            int iTotal;

            if (TotalCount == -1)
                iTotal = 32;
            else
                iTotal = TotalCount;

            for (int iRow = 1; iRow <= iTotal; iRow++)
            {
                dRow = dtTaskInfo.Select("TubeNum='"+iRow.ToString()+"'");
                if (dRow.Length == 0)
                {
                    arrayList.Add(new CreateArrayList(iRow.ToString(), iRow.ToString()));
                }

            }
            comboBox.DataSource = arrayList;
            comboBox.DisplayMember = "strName";
            comboBox.ValueMember = "strValue";
        }

        private void txt_ExperimentNum_Click(object sender, EventArgs e)
        {
            frm_Num frm = new frm_Num(this.txt_ExperimentNum.Text, "1", "1", "32");
            frm.ShowDialog();
            this.txt_ExperimentNum.Text = frm.sNUM;

        }

        private void picAdd_Click(object sender, EventArgs e)
        {
            if (this.txt_Man.Text == "")
            {
                frm_Info frm = new frm_Info("实验员不能为空");
                frm.ShowDialog();
                this.txt_Man.Focus();
            }
            else if (this.txt_WaterAddress.Text == "")
            {
                frm_Info frm = new frm_Info("水源信息不能为空");
                frm.ShowDialog();
                this.txt_WaterAddress.Focus();
            }
            else
            {
                int i = DbHelper.ExecuteNonQuery("INSERT INTO  D_TestInfo(TubeNum,ExperimentNum,ConcentrationEstimate,TestMan,Mark,WaterAddress) VALUES('" + this.cmb_TubeNum.Text + "','" + this.txt_ExperimentNum.Text + "','" + this.cmb_MD.SelectedValue.ToString() + "','" + this.txt_Man.Text + "','0','"+this.txt_WaterAddress.Text+"')");



                frmLogic.TaskSet.TubeNum[int.Parse(this.cmb_TubeNum.Text) - 1] = int.Parse(this.cmb_TubeNum.Text);

                frmLogic.TaskSet.ExperimentNum[int.Parse(this.cmb_TubeNum.Text) - 1] = int.Parse(this.txt_ExperimentNum.Text);

                frmLogic.TaskSet.ConcentrationEstimate[int.Parse(this.cmb_TubeNum.Text) - 1] = (frmLogic.enumRangeState)int.Parse(this.cmb_MD.SelectedValue.ToString());


                this.CreateComboBox(cmb_TubeNum, 32);
                this.CreateGridView();

            }
        }

        private void dtGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 11 && e.RowIndex >= 0)
            {
                if (iCurTaskStep == 0)
                {
                    frm_Info_02 frm = new frm_Info_02("删除记录", "确定要删除选中记录吗？");
                    frm.ShowDialog();
                    if (frm.sChageMark == "1")
                    {
                        int i = DbHelper.ExecuteNonQuery("DELETE D_TestInfo WHERE ID = '" + this.dtGridView["id", e.RowIndex].Value.ToString() + "'");



                        frmLogic.TaskSet.TubeNum[int.Parse(this.cmb_TubeNum.Text) - 1] = 0;

                        frmLogic.TaskSet.ExperimentNum[int.Parse(this.cmb_TubeNum.Text) - 1] = 0;

                        frmLogic.TaskSet.ConcentrationEstimate[int.Parse(this.cmb_TubeNum.Text) - 1] = 0;

                        this.CreateComboBox(cmb_TubeNum, 32);
                        this.CreateGridView();

                    }
                }
                else
                {
                    frm_Info frm = new frm_Info("任务已启动，不能做删除操作了！");
                    frm.ShowDialog();
           
                }
 
            }
        }

        private void picTask_Click(object sender, EventArgs e)
        {
            Application.DoEvents();
            if (iCurTaskStep == 0)
            {
                iCurTaskStep = 1;
            }
            int i = DbHelper.ExecuteNonQuery("UPDATE  D_Sys_Info SET ItemVal ='" + iCurTaskStep.ToString() + "' WHERE ItemID ='1'");
            sChange = "1";
            this.Close();
        }
    }
}
