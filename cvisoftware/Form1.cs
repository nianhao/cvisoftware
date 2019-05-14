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
        /// <summary>
        /// 设置nav的接口地址
        /// </summary>
        private static string navPostURL = string.Format("{0}/testUnitNavigationInfoController/addTestUnitNavigationInfo", restHost);
        /// <summary>
        /// 设置测试单元的接口地址
        /// </summary>
        private static string testUnitPostURL = string.Format("{0}/testUnitNavigationInfoController/addTestUnitConfig", restHost);
        /// <summary>
        /// 设置主窗体的接口地址
        /// </summary>
        private static string windowAddPostURL = string.Format("{0}/windowConfigController/addWindowConfig", restHost);
        /// <summary>
        /// 设置子窗体的接口地址
        /// </summary>
        private static string subWindowAddPostURL = string.Format("{0}/subWindowConfigController/addSubWindowConfig", restHost);
        /// <summary>
        /// 设置坐标系的接口地址
        /// </summary>
        private static string coordConfigURL = string.Format("{0}/coordinateConfigController/addCoordinateConfig", restHost);
        /// <summary>
        /// 添加sensor的接口地址
        /// </summary>
        private static string addSensorURL = string.Format("{0}/sensorTypeController/addSensorType", restHost);
        /// <summary>
        /// 添加组配置的接口地址
        /// </summary>
        private static string addGroupURL = string.Format("{0}/groupConfigController/addGroupConfig", restHost);
        /// <summary>
        /// 添加条目信息的接口地址
        /// </summary>
        private static string addProdInfoItemURL = string.Format("{0}/testProdInfoItemController/addTestProdInfoItem",restHost);
        /// <summary>
        /// 添加传感器名称的接口地址
        /// </summary>
        private static string addSensorNameURL = string.Format("{0}/sensorNameController/addSensorName",restHost);
        /// <summary>
        /// 添加传感器配置的接口地址
        /// </summary>
        private static string addSensorConfigURL = string.Format("{0}/sensorConfigController/addSensorConfig", restHost);
        /// <summary>
        /// 存放所有的测试单元
        /// </summary>
        ArrayList refrigeratorList = new ArrayList();
        /// <summary>
        /// 存放所有的主窗口
        /// </summary>
        ArrayList mainWindowList = new ArrayList();
        /// <summary>
        /// 存放所有的子窗口
        /// </summary>
        ArrayList subWindowList = new ArrayList();
        /// <summary>
        /// 存放所有的组信息
        /// </summary>
        ArrayList groupList = new ArrayList();
        /// <summary>
        /// 配置传感器的类型
        /// </summary>
        private static string [] sensorsStringList = {"温度&`C"};
        /// <summary>
        /// 配置组信息
        /// </summary>
        private static string[] groupStringList = { "环温&Env Temp", "冷藏&Cold Room" };
        private static string[] sensorNameStringList = { "干燥过滤器" };
        /// <summary>
        /// 定义数据组件
        /// </summary>
        DataComponent datamangeComponent; 
        /// <summary>
        /// 控制类型
        /// </summary>
        ControlTypeEnum controlType = new ControlTypeEnum();
        /// <summary>
        /// 定义正在使用的检测单元
        /// </summary>
        TestUnit testingTestUnit = new TestUnit();
        /// <summary>
        /// 检测单元
        /// </summary>
        TestUnit testUnit = new TestUnit();
        public Form1()
        {
            InitializeComponent();

        }

        private void sensorConfigInit(string versionNo="2.1.0")
        {
            //throw new NotImplementedException();
            foreach(TestUnit refrig in refrigeratorList)
            {
                //这里应该有17个传感器,这里先搞一个试一下 
                Sensor sensor = new Sensor();
                sensor.SensorNo = 1;
                sensor.Name = "温度1";
                sensor.EnName = "Temp1";
                //对所有的物理及逻辑传感器进行总排序
                sensor.TotalSequenceNo = 7;
                //坐标系序号
                sensor.CoordinateNo = 1;
                //所属的组号
                sensor.GroupNo = 1;
                //小数位精度
                sensor.DotNum = "0.0";
                string postDataString = string.Format("testUnitNo={0}&sensorNo={1}&name{2}&englishName={3}&totalSequenceNo={4}&coordinateNo={5}&selected=1&visible=1&dotNum={6}&groupNo={7}&maxSelect=1&minSelect=1&averageSelect=1&integralAvSelect=1&diffSelect=1&isVirtual=0&state=1&commonSelect=0&labCode={8}&versionNo={9}&coordinateNoStr={10}&selectedStr=1&visibleStr=1"
                    ,refrig.TestUnitNo,sensor.SensorNo,sensor.Name,sensor.EnName,sensor.TotalSequenceNo,sensor.CoordinateNo,sensor.DotNum,sensor.GroupNo,labCode,versionNo,sensor.CoordinateNo);
                //配置并回显结果
                string res = PostData(addSensorConfigURL, postDataString);
                Trace.WriteLine("添加传感器配置" + sensor.Name + " " + res);
               
            }

        }
        /// <summary>
        /// 配置传感器的名字
        /// </summary>
        /// <param name="startId"></param>
        private void sensorNameInit(int startId=1)
        {
            int id = startId;
            foreach(string sN in sensorNameStringList)
            {
                var sensorName = new SensorName();
                sensorName.Id = id++;
                sensorName.Name = sN;
                string postDataString = string.Format("labCode={0}&name={1}&id={2}", labCode, sensorName.Name, sensorName.Id);
            }
            //throw new NotImplementedException();
        }
        /// <summary>
        /// 录入条目信息配置
        /// </summary>
        private void prodInfoItemInit(string versionNo="1.1.0")
        {
            //throw new NotImplementedException();
            var prodInfoItem = new ProdInfoItem();
            //录入条目编号
            prodInfoItem.itemno = 1;
            //录入条目 信息名称
            prodInfoItem.itemname = "试品编号";
            prodInfoItem.enitemname = "Product No";
            //录入条目缺省内容
            prodInfoItem.defaultcontent = "/";
            prodInfoItem.endefaultcontent = "/";
            //录入条目信息类型 1为文本类型
            prodInfoItem.inputmode = 1;
            //录入条目类型
            prodInfoItem.itemtype = 1;
            //下拉框的内容
            prodInfoItem.selectitem[0] = "";
            //添加到条目信息表
            string postDataString = string.Format("itemNo={0}&itemName={1}&defaultContent={2}&inputMode={3}&queryCondition=1&selectItem={4}&print=1&display=1&changeable=1&englishName={5}&statusBar=1&englishDefaultContent={6}itemType={7}&versionNo={8}&labCode={9}"
                ,prodInfoItem.itemno,prodInfoItem.itemname,prodInfoItem.defaultcontent,prodInfoItem.inputmode,prodInfoItem.selectitem[0],prodInfoItem.enitemname,prodInfoItem.endefaultcontent,prodInfoItem.itemtype,versionNo,labCode);
            string res = PostData(addProdInfoItemURL, postDataString);
            Trace.WriteLine("添加条目信息"+prodInfoItem.itemname+res);

        }

        /// <summary>
        /// 配置组信息
        /// </summary>
        /// <param name="noStart"></param>
        private void groudInit(int noStart=1)
        {
            int groupNo = noStart;
            // throw new NotImplementedException();
            foreach(string groupName in groupStringList)
            {
                string chName = groupName.Split('&')[0];
                string enName = groupName.Split('&')[1];
                Group group = new Group();
                group.GroupNo = groupNo++;
                group.Name = chName;
                //??不知道文档中的传感器类型怎么设置
                //group.SensorType = 1;
                //设置组的英文名称
                group.EnName = enName;
                groupList.Add(group);
                string postDataString = string.Format("groupNo={0}&labCode={1}&name={2}&englishName={3}&sensorType=1&deleteable=0&visible=1&minselect=1&maxSelect=1&averageSelect=1&integerAveSelect=1",
                    group.GroupNo, labCode, group.Name, group.EnName);
                string res = PostData(addGroupURL, postDataString);
                Trace.WriteLine("配置组信息" + group.Name + "res");
            }


        }

        /// <summary>
        ///添加传感器，从sensorStringList中，按照id=1，自动设置typeid
        /// </summary>
        private void sensorInit()
        {
            //throw new NotImplementedException();
            int id = 1;
            foreach(string sensor in sensorsStringList)
            {
                //从全局配置中提取信息
                string name = sensor.Split('&')[0];
                string unit = sensor.Split('&')[1];
                //进行一系列的设置
                Sensor tmpSensor = new Sensor();
                tmpSensor.TypeID = id++;
                tmpSensor.TypeName = name;
                tmpSensor.Unit = unit;
                SensorLimit tmpSensorLimit = new SensorLimit();
                tmpSensorLimit.UpLimit = 200;
                tmpSensorLimit.LowLimit = -100;
                string postDataString = string.Format("sensorTypeId={0}&labCode={1}&sensorTypeName={2}&unit={3}&upLimit={4}&lowLimit={5}",
                    tmpSensor.TypeID, labCode, tmpSensor.TypeName, tmpSensor.Unit, tmpSensorLimit.UpLimit, tmpSensorLimit.LowLimit);
                //提交配置并且回显配置结果
                string res = PostData(addSensorURL, postDataString);
                Trace.WriteLine(res);

            }
        }

        /// <summary>
        /// 坐标系设置
        /// </summary>
        /// <param name="versionNo">版本号</param>
        private void coordInit(string versionNo="3.1.0")
        {
            //throw new NotImplementedException();
            for(int i=0;i<refrigeratorList.Count;i++)
            {
                TestUnit tmpRef = refrigeratorList[i] as TestUnit;
                tmpRef.CoordinateInfo[0] = new Coordinate();
                //设置坐标系的编号
                tmpRef.CoordinateInfo[0].CoordinateNo = 1;
                //设置坐标系的名称
                tmpRef.CoordinateInfo[0].Name = "温度";
                //设置存放的子窗口的编号
                tmpRef.CoordinateInfo[0].SubWindowNo = tmpRef.SubWindowInfo[0].SubWindowNo;
                //设置坐标系的上限
                tmpRef.CoordinateInfo[0].HighValue = 40;
                //设置坐标系的下限
                tmpRef.CoordinateInfo[0].LowValue = -40;
                //设置传感器类型
                tmpRef.CoordinateInfo[0].SensorType = 1;
                //设置坐标系的英文名称
                tmpRef.CoordinateInfo[0].EnName = "Temp";
                string postDataString = string.Format("testUnitNo={0}&coordinateNo={1}&name={2}&subWindowNo={3}&highValue={4}&lowValue={5}&visible=1&sensorType={6}&englishName={7}&labCode={8}&versionNo={9}",
                    tmpRef.TestUnitNo,tmpRef.CoordinateInfo[0].CoordinateNo,tmpRef.CoordinateInfo[0].Name,tmpRef.CoordinateInfo[0].SubWindowNo,tmpRef.CoordinateInfo[0].HighValue,tmpRef.CoordinateInfo[0].LowValue,tmpRef.CoordinateInfo[0].SensorType,tmpRef.CoordinateInfo[0].EnName,labCode,versionNo);
                //配置并回显结果
                string res = PostData(coordConfigURL, postDataString);
                Trace.WriteLine(res);
            }
        }

        /// <summary>
        ///设置子窗口
        /// </summary>
        private void subWindowInit()
        {
            // throw new NotImplementedException();
            for(int i=0;i<refrigeratorList.Count;i++)
            {
                var refrig = refrigeratorList[i] as TestUnit;
                //先对每个testUnit设置一个子窗体看看效果
                refrig.SubWindowInfo[0] = new SubWindow();
                //设置子窗口编号
                refrig.SubWindowInfo[0].SubWindowNo = 1;
                //设置子窗体名称
                refrig.SubWindowInfo[0].Name = "温度";
                //设置子窗口高度比例加权系数
                refrig.SubWindowInfo[0].Proportion = 1;
                //设置所属的主窗口
                refrig.SubWindowInfo[0].WindowNo = (mainWindowList[i] as Window).WindowNo;
                string postDataString = string.Format("subWindowNo={0}&testUnitNo={1}&name={2}&visible=1&labCode={3}&proportion={4}&windowNo={5}",
                    refrig.SubWindowInfo[0].SubWindowNo,refrig.TestUnitNo,refrig.SubWindowInfo[0].Name,labCode,refrig.SubWindowInfo[0].Proportion,refrig.SubWindowInfo[0].WindowNo);
                //设置并且回显结果
                string res = PostData(subWindowAddPostURL, postDataString);
                Trace.WriteLine(res);
            }
        }

        /// <summary>
        /// 根据testUnit的数量，添加主窗体，并且根据index索引一一关联
        /// </summary>
        /// <param name="upperLimit"></param>
        /// <param name="lowerLimit"></param>
        private void windowInit(int upperLimit=500,int lowerLimit=0)
        {
            //throw new NotImplementedException();
            for(int i=0;i<refrigeratorList.Count;i++)
            {
                Window tmpWindow = new Window();
                tmpWindow.WindowNo = i + 1;//NO编号从1开始
                //设置窗体名称
                tmpWindow.WindowName = string.Format("主窗口{0}", tmpWindow.WindowNo.ToString());
                //设置曲线显示的默认显示时间上限
                tmpWindow.UpperLimit = upperLimit;
                //设置曲线显示的默认显示时间的下限
                tmpWindow.LowerLimit = lowerLimit;
                //构造post数据，配置主窗体
                string postDataString = string.Format("windowNo={0}&testUnitNo={1}&windowName={2}&visible=1&upperLimit={3}&lowerLimit={4}&labCode={5}",
                                                        tmpWindow.WindowNo,(refrigeratorList[i] as TestUnit).TestUnitNo,tmpWindow.WindowName,tmpWindow.UpperLimit,tmpWindow.LowerLimit,labCode);
                //保存主窗体
                mainWindowList.Add(tmpWindow);//??这里会不会存在浅拷贝的问题？
                string res = PostData(windowAddPostURL, postDataString);
                //回显设置的结果
                Trace.WriteLine(res);

            }
        }
        /// <summary>
        /// 进行导航栏的初始化
        /// </summary>
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
                try
                {
                    string postDataString = string.Format("testUnitNo={0}&testUnitName={1}&belongedId={2}&ifBorrow={3}&isGroupInfoDefault={4}&englishName={5}&diffMode={6}&&labCode={7}"
                   , tmp.TestUnitNo, tmp.TestUnitName, tmp.BelongedId, tmp.BorrowInfo.ToString(), tmp.IsGroupInfoDefault.ToString(), tmp.EnTestUnitName, tmp.DiffMode.ToString(), labCode);

                }
                catch (Exception e)
                {
                    Trace.WriteLine(e.Message);
                    //throw;
                }
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
        /// <summary>
        /// 全部初始化，调用所有配置函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_InitAll_Click(object sender, EventArgs e)
        {
            systemInit();
            navigationInit();
            windowInit();
            subWindowInit();
            coordInit();
            sensorInit();
            groudInit();
            prodInfoItemInit();
            sensorNameInit();
            sensorConfigInit();
        }
        /// <summary>
        /// 显示组件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_showObj_Click(object sender, EventArgs e)
        {
            try
            {
                datamangeComponent = new DataComponent();
            }
            catch (Exception ee)
            {

                //throw;
                Trace.WriteLine(ee.Message);
                Trace.WriteLine(ee.StackTrace);
            }
            
            DataComm.DataComponent.InitApplicationStartPath(Application.StartupPath);
        }
    }
}
