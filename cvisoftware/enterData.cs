using DataComm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NameplateManagement;
namespace cvisoftware
{
    class enterData
    {
        public NameplateManagement.NameplateManagement nameplateCoponent = new NameplateManagement.NameplateManagement();
        DataComponent datamangeComponent = new DataComponent();
        private ControlTypeEnum controlType = new ControlTypeEnum();
        public DataComm.TestUnit testUnit = new DataComm.TestUnit();
        public DataComm.ProdInfoItem prodInforItem = new DataComm.ProdInfoItem();
        public ProdInfoItem oneProdInfoItem = new ProdInfoItem();
        public DataComm.TestUnit oneTestUnit = new DataComm.TestUnit();
        // private Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load//窗体加载是触发
        //DataComm.DataComponent.InitApplicationStartPath(Application.StartupPath)//初始化应用程序根目录


    }
}
