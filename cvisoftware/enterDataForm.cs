using DataComm;
using NameplateManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cvisoftware
{
    public partial class enterDataForm : Form
    {
        public NameplateManagement.NameplateManagement nameplateCoponent = new NameplateManagement.NameplateManagement();
        DataComponent datamangeComponent = new DataComponent();
        private ControlTypeEnum controlType = new ControlTypeEnum();
        public DataComm.TestUnit [] testUnit;
        public DataComm.ProdInfoItem [] prodInfoItem ;
        public ProdInfoItem oneProdInfoItem = new ProdInfoItem();
        public DataComm.TestUnit oneTestUnit = new DataComm.TestUnit();
        //private enterDataForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load//窗体加载是触发
        public enterDataForm()
        {
            InitializeComponent();
            DataComponent.InitApplicationStartPath(Application.StartupPath);//初始化应用程序根目录
            if (DataComm.DataComponent.GetControlType() == ControlTypeEnum.RemoteControl)//判断主控还是远控
            {
                controlType = ControlTypeEnum.RemoteControl;//远控赋值
            }
            else
            {
                controlType = ControlTypeEnum.MainControl;//主控赋值
            }
            var rtn = datamangeComponent.Init(); //数据组件初始化
            
            testUnit = datamangeComponent.GetAllTestUnit();//获得所有台位
            prodInfoItem = datamangeComponent.GetProdInfoItem();//获得录入条目
            nameplateCoponent.setstartpath(Application.StartupPath);//设置路径
            for(int i=0;i<prodInfoItem.Length;i++)
            {
                oneProdInfoItem = prodInfoItem[i];//将条目信息赋值
                nameplateCoponent.setItemInfo(
                    oneProdInfoItem.itemno, 
                    oneProdInfoItem.itemname, 
                    oneProdInfoItem.defaultcontent, 
                    oneProdInfoItem.inputmode, 
                    oneProdInfoItem.selectitem, 
                    oneProdInfoItem.changeable, 
                    oneProdInfoItem.datalength, 
                    oneProdInfoItem.itemtype, 
                    oneProdInfoItem.enitemname);
            }
            for(int i=0;i<testUnit.Length;i++)
            {
                oneTestUnit = testUnit[i];
                nameplateCoponent.setMetaData(
                    oneTestUnit.PrimaryKey,
                    oneTestUnit.BelongedId,
                    oneTestUnit.TestUnitNo,
                    oneTestUnit.TestUnitName,
                    oneTestUnit.TestNow,
                    oneTestUnit.BeginDateTime,
                    oneTestUnit.EndDateTime,
                    ref oneTestUnit.ItemContent,
                    oneTestUnit.ProjectMissionBookId);
            }

            nameplateCoponent.labname = datamangeComponent.GetSystemInfo().LabName;
            nameplateCoponent.EnlabName = datamangeComponent.GetSystemInfo().EnLabName;
            nameplateCoponent.SetTestUnitPrefix(
                datamangeComponent.GetSystemInfo().TestUnitNameConfig,
                datamangeComponent.GetSystemInfo().LabCode);//将台位统称和labcode传到录入组件
            nameplateCoponent.SetRowAndColumNum(2, 2);//设置行列数
            nameplateCoponent.Dock = DockStyle.Fill;//控件填充方式，充满窗体
            //绑定事件
            nameplateCoponent.Open_Test += Open_Test;
            nameplateCoponent.Stop_Test += Stop_Test;
            nameplateCoponent.Nameplate_Manage += Nameplate_Manage;
            this.Controls.Add(nameplateCoponent);



        }

        private void Nameplate_Manage(MetaData metadata, int testunitid, string labcode)
        {
            // throw new NotImplementedException();
            datamangeComponent.ModifyTestInfo(testunitid, metadata.itemcontent);
        }

        private void Stop_Test(MetaData metadata, int testunitid, string labcode)
        {
            //throw new NotImplementedException();
            for(int i=0;i<testUnit.Length;i++)
            {
                if(testUnit[i].TestUnitNo==testunitid)
                {
                    datamangeComponent.UpdateTestUnitReportTime(testUnit[i].PrimaryKey, testUnit[i].ReportTime);//将时间+台位标号在数据库中更新
                    break;
                }
            }
            datamangeComponent.StopTest(testunitid, true);//停测该台位，数据存库
        }

        private void Open_Test(MetaData metadata, int testunitid, string labcode)
        {
            // throw new NotImplementedException();
            int startResult = datamangeComponent.StartTest(testunitid, metadata.itemcontent);
        }
    }
}
