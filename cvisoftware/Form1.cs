using DataComm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace cvisoftware
{
    public partial class Form1 : Form
    {
        private static string labCode = "2018021108701";
        private static string restHost = "http://115.28.236.114/RestInterfaceSystem";
        private static string navPostURL = string.Format("{0}/testUnitNavigationInfoController/addTestUnitNavigationInfo", restHost);
        private static string testUnitPostURL = string.Format("{0}/testUnitNavigationInfoController/addTestUnitConfig", restHost);
        private static string windowAddPostURL = string.Format("{0}/windowConfigController/addWindowConfig", restHost);
        /// <summary>
        /// 存放所有的测试单元
        /// </summary>
        ArrayList refrigeratorList = new ArrayList();
        public Form1()
        {
            InitializeComponent();
            systemInit();
            navigationInit();
            windowInit();
        }

        private void windowInit()
        {
            //throw new NotImplementedException();

        }

        private void navigationInit()
        {
            //throw new NotImplementedException();
            var nav0 = new Navigation();
            nav0.Name = "抽样室";
            //导航信息的ID
            nav0.Id = 1;
            //对导航信息的简述
            nav0.Description = "实验1对冰箱抽样的实验";
            //定义在导航树的位置
            nav0.BelongedId = 0;
            nav0.EnName = "SanplingUnit";
            //添加10个冰箱,都添加到id=1上面去
            ArrayList bridgeNavs = new ArrayList();
            bridgeNavs.Add(nav0);
            for(int i=0;i<1;i++)
            {
                var tmpNav = new Navigation();
                //id变大一点，以免跟跟节点重复
                tmpNav.Id = i + 5;//
                tmpNav.Name = string.Format("{0}号测试室", i.ToString());
                tmpNav.EnName = string.Format("#{0} room",i.ToString());
                tmpNav.Description = string.Format("第{0}号测试室的检测单元的导航",i.ToString());
                tmpNav.BelongedId = 1;
                bridgeNavs.Add(tmpNav);
            }
            //定义了一个两级的导航栏 1--》5
            //构造导航栏
            
            foreach(Navigation tmp in bridgeNavs)
            {
                string postDataString = string.Format("id={0}&name={1}&description={2}&belongedId={3}&englishName={4}&labCode={5}",
                    tmp.Id,tmp.Name,tmp.Description,tmp.BelongedId,tmp.EnName,labCode);
                string res = PostData(navPostURL, postDataString);
            }
            //添加10个测试单元
            
            for(int i=0;i<10;i++)
            {
                TestUnit tmpBridge = new TestUnit();
                tmpBridge.TestUnitNo = i + 1;//监测单元编号从1号开始
                //设置监测单元名称
                tmpBridge.TestUnitName = string.Format("{0}号冰箱", (i + 1).ToString());
                tmpBridge.EnTestUnitName = string.Format("No {0} bridges", (i + 1).ToString());
                //设置检测单元所属级别
                tmpBridge.BelongedId = 5;//这里写死了，是不对的，以后再改
                //设置是否允许借用，1为允许
                tmpBridge.IfBorrow = true;
                //设置是否默认组信息
                tmpBridge.IsGroupInfoDefault = true;
                //设置是否为差值模式？？不懂是什么了
                tmpBridge.DiffMode = 1;
                refrigeratorList.Add(tmpBridge);
                
            }
            //添加监测单元到数据表
            foreach(TestUnit tmp in refrigeratorList)
            {
                string postDataString = string.Format("testUnitNo={0}&testUnitName={1}&belongedId={3}&ifBorrow={4}&isGroupInfoDefault={5}&englishName={6}&diffMode={7}&&labCode={8}"
                    , tmp.TestUnitNo, tmp.TestUnitName, tmp.BelongedId, tmp.BorrowInfo.ToString(), tmp.IsGroupInfoDefault.ToString(),tmp.EnTestUnitName,tmp.DiffMode.ToString(),labCode);
            }

        }

        public void systemInit()
        {
            SystemInfo sysInfo = new SystemInfo();
            sysInfo.SoftwareName = "实验1软件";
            sysInfo.EnSoftwareName = "softWare for experiment";
            sysInfo.CompanyName = "OUC-MML";
            sysInfo.EnTestUnitNameConfig = "Fridge";
            //设置检测单元总数，总计10台冰箱
            sysInfo.TestUnitNum = 10;
            //设置传感器总数 10*17=170
            sysInfo.SensorNum = 170;
            //系统类别，2表示为冰箱
            sysInfo.Category = 2;
            //双语显示
            sysInfo.Language = 0;
            sysInfo.TestUnitNameConfig = "冰箱";
            //设置是否调用传感器配置,true表示调用
            sysInfo.InputLink = true;
            //设置公用传感器数量
            sysInfo.CommonSensorNum = 0;
            //数据录入显示方式，1为检测单元在右边列表显示
            sysInfo.DisplayFlag = 1;
            //曲线显示时间上线
            sysInfo.DisplayTimeLimit = 5000;
            //设置历史查询中曲线显示时间上限
            sysInfo.InfoQueryTimeLimit = 24;
            //设置实验室名称
            sysInfo.LabName = "21180211087-01";
            //设置实验室英文名称
            sysInfo.EnLabName = "21180211087-01";
            //设置默认的功率
            sysInfo.DefaultNoPowerLimit = 5;

            //定义清空服务器数据库系统表的url
            //string url = "http://115.28.236.114/RestInterfaceSystem/systemInfoController/deleteAll";//不执行清空
            //string result = GetData(url);
            //定义添加系统表数据的url
            string url2 = "http://115.28.236.114/RestInterfaceSystem/systemInfoController/addSystemInfo";
            string data2 = string.Format("labCode={17}&noPowerLimit=5&softwareName={0}&companyName={1}&testUnitNum={2}&sensorNum={3}&category={4}&language={5}&testUnitNameConfig={6}&inputLink={7}&commonSensorNum={8}&englishSoftwareName={9}&englishTestUnitNameConfig={10}&displayFlag={11}&displayTimeLimit={12}&infoQueryTimeLimit={13}&testTable={14}&labName={15}&englishLabName={16}",
                sysInfo.SoftwareName,sysInfo.CompanyName,sysInfo.TestUnitNum,sysInfo.SensorNum,sysInfo.Category,sysInfo.Language,sysInfo.TestUnitNameConfig,sysInfo.InputLink,sysInfo.CommonSensorNum,sysInfo.EnSoftwareName,sysInfo.EnTestUnitNameConfig,sysInfo.DisplayFlag,sysInfo.DisplayTimeLimit,sysInfo.InfoQueryTimeLimit,null,sysInfo.LabName,sysInfo.EnLabName,labCode);
            //将数据添加到数据表中
            string result2 = PostData(url2, data2);

        }

        private string PostData(string url2, string data2)
        {
            //throw new NotImplementedException();
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.DefaultConnectionLimit = 200;
            //创建httprequest对象
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url2);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
            request.Timeout = 10000;
            //将url编码后的字符转换成字节
            var postDataBytes = Encoding.UTF8.GetBytes(data2);
            request.ContentLength = postDataBytes.Length;
            try
            {
                var stream = request.GetRequestStream();
                stream.Write(postDataBytes, 0, postDataBytes.Length);
                stream.Close();
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                return responseString;
            }
            catch (Exception e)
            {

                Trace.WriteLine(e.StackTrace);
                Trace.WriteLine(e.Message);
                return e.Message;
            }
        }

        private string GetData(string url)
        {
            // throw new NotImplementedException();
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.DefaultConnectionLimit = 200;
            //创建httprequest对象
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
            request.Timeout = 10000;
            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                return responseString;
            }
            catch (Exception e)
            {

                //throw;
                Trace.WriteLine(e.StackTrace);
                Trace.WriteLine(e.Message);
                return e.Message;
            }

        }
    }
}
