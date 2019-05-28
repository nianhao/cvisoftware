using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace cvisoftware
{
    public partial class setLabCode : Form
    {
        private static string dataCommonConfigFileName="E:\\Workspaces\\VisualStudio\\cvisoftware\\cvisoftware\\bin\\Debug\\datacommconfig.xml";
        public setLabCode()
        {
            InitializeComponent();
            this.textBoxLabCode.Text = Form1.labCode;
        }
        private void synXMLFile()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(dataCommonConfigFileName);
            XmlNode configNode = xmlDoc.SelectSingleNode("config");
            XmlNode labCodeNode = configNode.SelectSingleNode("labcode");
            string labCode = labCodeNode.InnerText;
            if(labCode!=Form1.labCode)
            {
                labCodeNode.InnerText = Form1.labCode;
                MessageBox.Show("检测到xml配置文件中labcode和系统配置的LabCode不一致，正在进行修改");
            }
            try
            {
                xmlDoc.Save(dataCommonConfigFileName);
                MessageBox.Show("修改成功,完成同步");
            }
            catch (Exception e )
            {
                MessageBox.Show("修改失败\n" + e.Message + "\n");
                //throw;
            }
            this.Dispose();
        }
        private void buttonCommit_Click(object sender, EventArgs e)
        {
            Form1.labCode = this.textBoxLabCode.Text;
            synXMLFile();
        }

        private void buttonCancle_Click(object sender, EventArgs e)
        {
            this.Dispose();
            return;
        }
    }
}
