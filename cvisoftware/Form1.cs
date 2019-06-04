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
using xCurveControl;

namespace cvisoftware
{
    public partial class Form1 : Form
    {
        internal static string labCode = "2118021108701";
        private static string restHost = "http://115.28.236.114/RestInterfaceSystem";
        /// <summary>
        /// 设置nav的接口地址
        /// </summary>
        private static string navPostURL = string.Format("{0}/testUnitNavigationInfoController/addTestUnitNavigationInfo", restHost);
        /// <summary>
        /// 设置测试单元的接口地址
        /// </summary>
        private static string testUnitPostURL = string.Format("{0}/testUnitConfigController/addTestUnitConfig", restHost);
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
        /// 添加sensorType的接口地址
        /// </summary>
        private static string addSensorTypeURL = string.Format("{0}/sensorTypeController/addSensorType", restHost);
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
        /// 存放导航信息
        /// </summary>
        ArrayList bridgeNavs = new ArrayList();
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
        /// 存放全部的传感器信息
        /// </summary>
        ArrayList sensorList = new ArrayList();
        /// <summary>
        /// 存放所有的组信息
        /// </summary>
        ArrayList groupList = new ArrayList();
        /// <summary>
        /// 配置传感器的类型
        /// </summary>
        private static string [] sensorsStringList = {"温度&`C","频率&HZ","电压&V","电流&A","功率&W","耗电量&KW*h",};
        private static int[] sensorUpLimit = { 200, 200, 500, 100, 5000, 200 };
        private static int[] sensorLowLimit = { -100, 0, 0, 0, 0, 0 };
        private static int[] typeId = { 1, 4, 3, 5, 6, 7 };
        /// <summary>
        /// 配置组信息
        /// </summary>
        private static string[] groupStringList = { "环温&Env Temp", "冷藏&Cold Room" };
        private static string[] sensorNameStringList = { "干燥过滤器" };
        /// <summary>
        /// 定义数据组件
        /// </summary>
        private DataComponent dataComponent = new DataComponent();
        /// <summary>
        /// 也许是曲线组件
        /// </summary>
        private CurveControl curveControl = new CurveControl();
        /// <summary>
        /// 控制类型
        /// </summary>
        ControlTypeEnum controlType = new ControlTypeEnum();
        /// <summary>
        /// 定义正在使用的检测单元
        /// </summary>
        TestUnit[] testingTestUnit;//= new TestUnit();
        /// <summary>
        /// 检测单元
        /// </summary>
        TestUnit[] testUnit;// = new TestUnit();
        private int subWindowNo = 1;
        public Form1()
        {
            InitializeComponent();

        }
        private void logSysInfo(string info)
        {
            string timeNow = DateTime.Now.ToLocalTime().ToString();        // 2008-9-4 20:12:12
            textBoxSysLog.AppendText("\n*******"+timeNow+"********"+"\n\n");
            textBoxSysLog.AppendText(info);
            textBoxSysLog.AppendText("\n\n");
        }
        private void sensorConfigInit(string versionNo="2.1.0")
        {
            //throw new NotImplementedException();
            Sensor[] sensors = constructSensors();
            foreach(TestUnit refrig in refrigeratorList)
            {
                for(int j=0;j<sensors.Length;j++)
                {
                    string postDataString = string.Format("testUnitNo={0}&sensorNo={1}&name={2}&englishName={3}&totalSequenceNo={4}&coordinateNo={5}&selected=1&visible=1&dotNum={6}&groupNo={7}&maxSelect=1&minSelect=1&averageSelect=1&integralAvSelect=1&diffSelect=0&isVirtual=0&state=1&commonSelect=0&labCode={8}&versionNo={9}&coordinateNoStr={10}&selectedStr=1&visibleStr=1&assemblyName=func&functionName=funcName&functionParas=&functionClass=functionClass&keyParaMeter=1", 
                        //根据No将testUnit和Sensor进行关联
                        refrig.TestUnitNo, 
                        sensors[j].SensorNo, 
                        sensors[j].Name, 
                        sensors[j].EnName, 
                        sensors[j].TotalSequenceNo, 
                        sensors[j].CoordinateNo, 
                        sensors[j].DotNum, 
                        sensors[j].GroupNo, 
                        labCode, 
                        versionNo, 
                        sensors[j].CoordinateNo);
                    //配置并回显结果
                    string res = PostData(addSensorConfigURL, postDataString);
                    // MessageBox.Show("添加传感器配置" + "\n" + postDataString + "\n" + res);
                    logSysInfo("添加传感器配置" + "\n" + postDataString + "\n" + res);
                    Trace.WriteLine("添加传感器配置"  + " " + res);
                }

               
            }

        }
        /// <summary>
        /// 为每一个冰箱生成对应的全部传感器
        /// </summary>
        /// <returns></returns>
        private Sensor[] constructSensors()
        {
            //throw new NotImplementedException();
            string[] tName = {"温度1", "温度2", "温度3", "温度4", "温度5", "温度6", "温度7", "温度8", "温度9", "温度10", "干温",
                "湿温", "频率", "电压", "电流", "功率", "耗电量"};

            string[] tEnName = {"Temperature1", "Temperature2", "Temperature3", "Temperature4", "Temperature5", "Temperature6",
                "Temperature7", "Temperature8", "Temperature9", "Temperature10", "Dry Temperature", "Wet Temperature",
                "Frequency", "Voltage", "Current", "Power", "Power Consumption"};
            //??不知道这个配置是干什么的
            int[] tSequenceNo = { 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 0, 2, 366, 363, 364, 365, 367 };

            int[] coordinateNo = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 4, 5, 6, 7 };

            int[] groupNo = { 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 1, 1, 5, 5, 5, 5, 5 };
            int length = tName.Length;
            Sensor [] sensors =new Sensor [length];
            for(int i=0;i<length;i++)
            {
                //配置每一个传感器一系列参数
                sensors[i] = new Sensor();
                sensors[i].SensorNo = i + 1;
                sensors[i].Name = tName[i];
                sensors[i].TotalSequenceNo = tSequenceNo[i];
                sensors[i].CoordinateNo = coordinateNo[i];
                sensors[i].GroupNo = groupNo[i];
                sensors[i].EnName = tEnName[i];
                sensors[i].DotNum = "0.0";
            }
            return sensors;
        }

        /// <summary>
        /// 配置传感器的名字
        /// </summary>
        /// <param name="startId"></param>
        private void sensorNameInit(int startId=1)
        {
            int id = startId;
            //传感器的名字放到全局变量中，可以方便的修改
            foreach(string sN in sensorNameStringList)
            {
                var sensorName = new SensorName();
                sensorName.Id = id++;
                sensorName.Name = sN;
                string postDataString = string.Format("labCode={0}&name={1}&id={2}", 
                    labCode, 
                    sensorName.Name, 
                    sensorName.Id);
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
            string postDataString = string.Format("itemNo={0}&itemName={1}&defaultContent={2}&inputMode={3}&queryCondition=1&selectItem={4}&print=1&display=1&changeable=1&englishName={5}&statusBar=1&englishDefaultContent={6}itemType={7}&versionNo={8}&labCode={9}",
                prodInfoItem.itemno,
                prodInfoItem.itemname,
                prodInfoItem.defaultcontent,
                prodInfoItem.inputmode,
                prodInfoItem.selectitem[0],
                prodInfoItem.enitemname,
                prodInfoItem.endefaultcontent,
                prodInfoItem.itemtype,
                versionNo,
                labCode);
            string res = PostData(addProdInfoItemURL, postDataString);
            logSysInfo("添加条目信息 " + prodInfoItem.itemname + res);
            Trace.WriteLine("添加条目信息 "+prodInfoItem.itemname+res);

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
                    group.GroupNo, 
                    labCode, 
                    group.Name, 
                    group.EnName);
                string res = PostData(addGroupURL, postDataString);
                logSysInfo("配置组信息" + group.Name + "res");
                Trace.WriteLine("配置组信息" + group.Name + "res");
            }


        }

        /// <summary>
        ///添加传感器，从sensorStringList中，按照typeId，自动设置typeid
        /// </summary>
        private void sensorTypeInit()
        {
            //throw new NotImplementedException();
            int index = 0;
            foreach(string sensor in sensorsStringList)
            {
                //从全局配置中提取信息
                string name = sensor.Split('&')[0];
                string unit = sensor.Split('&')[1];
                //进行一系列的设置
                Sensor tmpSensor = new Sensor();
                tmpSensor.TypeID = typeId[index];
                tmpSensor.TypeName = name;
                tmpSensor.Unit = unit;
                SensorLimit tmpSensorLimit = new SensorLimit();
                tmpSensorLimit.UpLimit = sensorUpLimit[index];
                tmpSensorLimit.LowLimit = sensorLowLimit[index];
                string postDataString = string.Format("sensorTypeId={0}&labCode={1}&sensorTypeName={2}&unit={3}&upLimit={4}&lowLimit={5}",
                       tmpSensor.TypeID, 
                       labCode, 
                       tmpSensor.TypeName, 
                       tmpSensor.Unit, 
                       tmpSensorLimit.UpLimit, 
                       tmpSensorLimit.LowLimit);
                //提交配置并且回显配置结果
                //对于温度，提交12次
                if(index==0)
                {
                    for(int i=0;i<12;i++)
                    {
                        //这里可能存在浅复制的问题
                        sensorList.Add(tmpSensor);
                        string res = PostData(addSensorTypeURL, postDataString);
                        //MessageBox.Show("添加传感器类型" + "\n" + postDataString + "\n" + res);
                        logSysInfo("添加传感器类型" + "\n" + postDataString + "\n" + res);
                        Trace.WriteLine(res);
                    }
                }
                else
                {
                    sensorList.Add(tmpSensor);
                    string res = PostData(addSensorTypeURL, postDataString);
                    //MessageBox.Show("添加传感器类型" + "\n" + postDataString + "\n" + res);
                    logSysInfo("添加传感器类型" + "\n" + postDataString + "\n" + res);
                    Trace.WriteLine(res);
                }
                
                //进入到下一个传感器的配置
                index++;
                

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
                ArrayList coordList =addCoordInit();
                //将生成的坐标系赋值给测试单元
                tmpRef.CoordinateInfo =(Coordinate[]) coordList.ToArray(typeof(Coordinate));
                for(int j=0;j<tmpRef.CoordinateInfo.Length;j++)
                {
                    string postDataString = string.Format("testUnitNo={0}&coordinateNo={1}&name={2}&subWindowNo={3}&highValue={4}&lowValue={5}&visible=1&sensorType={6}&englishName={7}&labCode={8}&versionNo={9}",
                    tmpRef.TestUnitNo,
                    tmpRef.CoordinateInfo[j].CoordinateNo,
                    tmpRef.CoordinateInfo[j].Name,
                    tmpRef.CoordinateInfo[j].SubWindowNo,
                    tmpRef.CoordinateInfo[j].HighValue,
                    tmpRef.CoordinateInfo[j].LowValue,
                    tmpRef.CoordinateInfo[j].SensorType,
                    tmpRef.CoordinateInfo[j].EnName,
                    labCode,
                    versionNo);
                    //配置并回显结果
                    string res = PostData(coordConfigURL, postDataString);
                    //MessageBox.Show("坐标系初始化" + "\n" + postDataString + "\n" + res);
                    logSysInfo("坐标系初始化" + "\n" + postDataString + "\n" + res);
                    Trace.WriteLine(res);
                }

            }
        }
        /// <summary>
        /// 对每一个测试单元（这里特指冰箱)添加坐标系
        /// </summary>
        /// <param name="tmpRef"></param>
        private ArrayList addCoordInit()
        {
            ArrayList coordList = new ArrayList();
            string[] tName = { "温度", "频率", "耗电量", "电压", "电流", "功率" };

            int[] highValue = { 40, 200, 30, 400, 30, 300 };

            int[] lowValue = { -40, 0, 0, 0, 0, 0 };

            int[] sensorType = { 1, 4, 7, 3, 5, 6 };

            string[] tEnName = { "Temperature", "Frequency", "Power Consumption", "Voltage", "Current", "Power" };
            for(int i=0;i<tName.Length;i++)
            {
                //新建一个坐标系
                var co = new Coordinate();
                co.CoordinateNo = i + 1;//从1开始标号
                //设置坐标系的名字
                co.Name = tName[i];
                co.EnName = tEnName[i];
                co.SubWindowNo = 1;//??不知道为什么直接设置为1
                //设置数值范围
                co.HighValue = highValue[i];
                co.LowValue = lowValue[i];
                //设置对应的传感器类型
                co.SensorType = sensorType[i];
                coordList.Add(co);
            }
            //throw new NotImplementedException();
            return coordList;
            
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
                SubWindow tmpSW = new SubWindow();
                var sub=refrig.SubWindowInfo.ToList();
                sub.Add(tmpSW);
                refrig.SubWindowInfo = sub.ToArray();
                //设置子窗口编号
                refrig.SubWindowInfo[0].SubWindowNo = 1;
                //设置子窗体名称
                refrig.SubWindowInfo[0].Name = "温度";
                //设置子窗口高度比例加权系数
                refrig.SubWindowInfo[0].Proportion = 2;
                //设置所属的主窗口
                //refrig.SubWindowInfo[0].WindowNo = (mainWindowList[i] as Window).WindowNo;
                //!!这里为什么直接全部设置为1，不是有依赖吗？？
                refrig.SubWindowInfo[0].WindowNo = 1;
                string postDataString = string.Format("subWindowNo={0}&testUnitNo={1}&name={2}&visible=1&labCode={3}&proportion={4}&windowNo={5}",
                       refrig.SubWindowInfo[0].SubWindowNo,
                       refrig.TestUnitNo,
                       refrig.SubWindowInfo[0].Name,
                       labCode,refrig.SubWindowInfo[0].Proportion,
                       refrig.SubWindowInfo[0].WindowNo);
                //设置并且回显结果
                string res = PostData(subWindowAddPostURL, postDataString);
                //MessageBox.Show("子窗体初始化" +"\n"+postDataString+"\n"+ res);
                logSysInfo("子窗体初始化" + "\n" + postDataString + "\n" + res);
                //Trace.WriteLine(res);
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
                       tmpWindow.WindowNo,
                       (refrigeratorList[i] as TestUnit).TestUnitNo,
                       tmpWindow.WindowName,
                       tmpWindow.UpperLimit,
                       tmpWindow.LowerLimit,
                       labCode);
                //保存主窗体
                mainWindowList.Add(tmpWindow);//??这里会不会存在浅拷贝的问题？
                string res = PostData(windowAddPostURL, postDataString);
                //MessageBox.Show("窗体初始化"+"\n"+postDataString+"\n"+res);
                logSysInfo("窗体初始化" + "\n" + postDataString + "\n" + res);
                //回显设置的结果
                //Trace.WriteLine(res);

            }
        }
        /// <summary>
        /// 进行导航栏的初始化
        /// </summary>
        private void navigationInit(int rootId=20)
        {
            //throw new NotImplementedException();
            var nav0 = new Navigation();
            nav0.Name = "抽样室";
            //导航信息的ID
            nav0.Id = rootId;
            //对导航信息的简述
            nav0.Description = "实验1对冰箱抽样的实验";
            //定义在导航树的位置
            nav0.BelongedId = 0;
            nav0.EnName = "SanplingUnit";
            bridgeNavs.Add(nav0);
            for(int i=0;i<2;i++)
            {
                var tmpNav = new Navigation();
                //id变大一点，以免跟跟节点重复
                tmpNav.Id = i + 5;//
                tmpNav.Name = string.Format("{0}号测试室", (i+'A').ToString());
                tmpNav.EnName = string.Format("#{0} room", (i + 'A').ToString());
                tmpNav.Description = string.Format("第{0}号测试室的检测单元的导航",(i+'A').ToString());
                tmpNav.BelongedId = nav0.Id;
                bridgeNavs.Add(tmpNav);
            }
            //定义了一个两级的导航栏 1--》5
            //构造导航栏
            
            foreach(Navigation tmp in bridgeNavs)
            {
                string postDataString = string.Format("id={0}&name={1}&description={2}&belongedId={3}&englishName={4}&labCode={5}",
                    tmp.Id,
                    tmp.Name,
                    tmp.Description,
                    tmp.BelongedId,
                    tmp.EnName,
                    labCode);
                //string res = PostData(navPostURL, postDataString);
                //MessageBox.Show("导航栏初始化"+"\n\n"+postDataString+"\n" + PostData(navPostURL, postDataString));
                logSysInfo("导航栏初始化" + "\n\n" + postDataString + "\n" + PostData(navPostURL, postDataString));
            }


        }
        public void testUnitInit()
        {
            //添加10个测试单元

            for (int i = 0; i < 10; i++)
            {
                TestUnit tmpBridge = new TestUnit();
                tmpBridge.TestUnitNo = i + 1;//监测单元编号从1号开始
                //设置监测单元名称
                tmpBridge.TestUnitName = string.Format("{0}号冰箱", (i + 1).ToString());
                tmpBridge.EnTestUnitName = string.Format("No {0} bridges", (i + 1).ToString());
                //设置检测单元所属级别
                if (i<5)
                    tmpBridge.BelongedId = (bridgeNavs[1] as Navigation).Id;//这里写死了，是不对的，以后再改
                else
                    tmpBridge.BelongedId = (bridgeNavs[2] as Navigation).Id;//这里写死了，是不对的，以后再改
                //设置是否允许借用，1为允许
                tmpBridge.IfBorrow = true;
                //设置是否默认组信息
                tmpBridge.IsGroupInfoDefault = true;
                //设置是否为差值模式？？不懂是什么了
                tmpBridge.DiffMode = 1;
                refrigeratorList.Add(tmpBridge);

            }
            //添加监测单元到数据表
            foreach (TestUnit tmp in refrigeratorList)
            {
                try
                {
                    string postDataString = string.Format("testUnitNo={0}&testUnitName={1}&belongedId={2}&ifBorrow={3}&borrowInfo={4}&isGroupInfoDefault={5}&groupInfoDefault={6}&dataBufferLength={7}&englishName={8}&diffMode={9}&labCode={10}&testType={11}", 
                        tmp.TestUnitNo, 
                        tmp.TestUnitName, 
                        tmp.BelongedId,
                        tmp.IfBorrow ? '1' : '0',
                        "1@2@3@4",
                        tmp.IsGroupInfoDefault ? '1' : '0', 
                        "1@2@3",
                        "100",
                        tmp.EnTestUnitName, 
                        tmp.DiffMode.ToString(), 
                        labCode,
                        "1");
                    //MessageBox.Show("测试单元初始化" +"\n\n"+postDataString+"\n"+ PostData(testUnitPostURL, postDataString));
                    logSysInfo("测试单元初始化" + "\n\n" + postDataString + "\n" + PostData(testUnitPostURL, postDataString));
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
            sysInfo.EnSoftwareName = "softWareforexperiment";
            sysInfo.CompanyName = "实验室";
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
            sysInfo.LabName = "崂山信南";
            //设置实验室英文名称
            sysInfo.EnLabName = "LaoShanSouth";
            //设置默认的功率
            sysInfo.DefaultNoPowerLimit = 5;

            //定义清空服务器数据库系统表的url
            //string url = "http://115.28.236.114/RestInterfaceSystem/systemInfoController/deleteAll";//不执行清空
            //string result = GetData(url);
            //定义添加系统表数据的url
            string url2 = "http://115.28.236.114/RestInterfaceSystem/systemInfoController/addSystemInfo";
            string data2 = string.Format("labCode={17}&noPowerLimit=5&softwareName={0}&companyName={1}&testUnitNum={2}&sensorNum={3}&category={4}&language={5}&testUnitNameConfig={6}&inputLink={7}&commonSensorNum={8}&englishSoftwareName={9}&englishTestUnitNameConfig={10}&displayFlag={11}&displayTimeLimit={12}&infoQueryTimeLimit={13}&testTable={14}&labName={15}&englishLabName={16}&preValue=0&cacheValue=10&loadable=1&reportPassword=123456&noPowerLimit=1&testCollectionPassword=123456&noPowerLimit=1&testCollectionPassword=123456&currentTestProdInfoItem=1.0.0&currentSensorConfig=2.1.0&currentCoorDinateConfig=3.1.0",
                sysInfo.SoftwareName,
                sysInfo.CompanyName,
                sysInfo.TestUnitNum,
                sysInfo.SensorNum,
                sysInfo.Category,
                sysInfo.Language,
                sysInfo.TestUnitNameConfig,
                sysInfo.InputLink?'1':'0',
                sysInfo.CommonSensorNum,
                sysInfo.EnSoftwareName,
                sysInfo.EnTestUnitNameConfig,
                sysInfo.DisplayFlag,
                sysInfo.DisplayTimeLimit,
                sysInfo.InfoQueryTimeLimit,
                "testdata",
                sysInfo.LabName,
                sysInfo.EnLabName,
                labCode);
            //将数据添加到数据表中
            //MessageBox.Show(data2);
            //MessageBox.Show(PostData(url2, data2));
            logSysInfo("系统初始化\n" + data2 + PostData(url2, data2));

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
            //检查labcode是不是我们想要的labcode
            if (!checkLabCode()) return;
            //系统的初始化
            systemInit();
            //导航栏配置
            navigationInit();
            //测试单元配置
            testUnitInit();
            //窗体的配置
            windowInit();
            //子窗体配置
            subWindowInit();
            //坐标系配置
            coordInit();
            //传感器配置
            sensorTypeInit();
            //groudInit();
           // prodInfoItemInit();
           //传感器名称配置
            sensorNameInit();
            //传感器参数配置
            sensorConfigInit();
        }

        private bool checkLabCode()
        {
            //throw new NotImplementedException();
            MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
            string message = string.Format("当前的LabCode设置为:{0}\n,点击确定继续，点击取消配置LabCode\n",labCode);
            DialogResult dr = MessageBox.Show(message, "LabCode确认",messButton);
            if (dr == DialogResult.OK)//如果点击“确定”按钮

            {
                return true;

            }

            else//如果点击“取消”按钮

            {
                return false;
            }
        }

        /// <summary>
        /// 显示组件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_showObj_Click(object sender, EventArgs e)
        {
            //初始化控制类型，是主控还是远控
            DataComponent.InitApplicationStartPath(Application.StartupPath);
            DataComponent.GetControlType();

            //曲线初始化
            //int rtn = dataComponent.InitCurve();

            //数据组件初始化
            int rtn = dataComponent.Init();

            //获取正在测试的监测单元

            testingTestUnit = dataComponent.GetAllTestUnit();
            //testingTestUnit = dataComponent.GetAllTestingUnit();

            //定义子窗口数、坐标系数、传感器数、组数
            int subWindowNum, coordinateNum, sensorNum, groupNum;
            int i, j;
            subWindowNum = testingTestUnit[0].SubWindowInfo.Length;
            coordinateNum = testingTestUnit[0].CoordinateInfo.Length;
            sensorNum = testingTestUnit[0].SensorInfo.Length + testingTestUnit[0].AverageValueList.Count;
            groupNum = testingTestUnit[0].GroupInfo.Length;


            //定义一个曲线组件变量
            CurveControl curveControl = new CurveControl();

            //整个曲线显示组件的窗口初始化
            curveControl.IniParentWindow(subWindowNum, coordinateNum, sensorNum, groupNum);

            //初始化子窗口接口
            for (int k1 = 0; k1 < testingTestUnit[0].SubWindowInfo.Length; k1++)
            {
                curveControl.IniSubWindow(testingTestUnit[0].SubWindowInfo[k1].SubWindowNo,
                    testingTestUnit[0].SubWindowInfo[k1].Name, testingTestUnit[0].SubWindowInfo[k1].Proportion, testingTestUnit[0].SubWindowInfo[k1].Visible);
            }

            //初始化坐标系
            List<int> initAllCoor = new List<int>();
            for (j = 0; j < coordinateNum; j++)
            {
                if (testingTestUnit[0].CoordinateInfo[j].SubWindowNo == subWindowNo)
                {
                    curveControl.InitCoordinate(testingTestUnit[0].CoordinateInfo[j].CoordinateNo,
                                                testingTestUnit[0].CoordinateInfo[j].Name,
                                                1,
                                                testingTestUnit[0].CoordinateInfo[j].Visible,
                                                testingTestUnit[0].CoordinateInfo[j].HighValue,
                                                testingTestUnit[0].CoordinateInfo[j].LowValue,
                                                testingTestUnit[0].CoordinateInfo[j].UpLimit,
                                                testingTestUnit[0].CoordinateInfo[j].LowLimit,
                                                testingTestUnit[0].CoordinateInfo[j].Unit);
                    initAllCoor.Add(testingTestUnit[0].CoordinateInfo[j].CoordinateNo);
                }
            }

            //初始化组信息
            for (i = 0; i < testingTestUnit[0].GroupInfo.Count(); i++)
            {
                Group oneGroup = new Group();
                oneGroup = testingTestUnit[0].GroupInfo[i];
                Boolean b = curveControl.SetGroupSelect(oneGroup.GroupNo, true);
                curveControl.SetGroupName(oneGroup.GroupNo, oneGroup.Name);

                curveControl.SetGroupStatisticSelect(oneGroup.GroupNo,
                                                     oneGroup.MaxSelect,
                                                     oneGroup.MinSelect,
                                                     oneGroup.AverageSelect,
                                                     oneGroup.IntegeraveSelect);
            }

            //确定传感器是否属于在此窗口内的坐标系
            int copyCoordinate = 0;
            //初始化传感器信息
            for (i = 0; i < testingTestUnit[0].SensorInfo.Length; i++)
            {
                if (testingTestUnit[0].SensorInfo[i].BeBorrowed == false && testingTestUnit[0].SensorInfo[i].Selected == true)
                {
                    for (int z1 = 0; z1 < testingTestUnit[0].SensorInfo[i].CoordinateBelong.CoordinateNo.Length; z1++)
                    {
                        if (initAllCoor.Contains(testingTestUnit[0].SensorInfo[i].CoordinateBelong.CoordinateNo[z1]))
                        {
                            copyCoordinate = testingTestUnit[0].SensorInfo[i].CoordinateBelong.CoordinateNo[z1];
                            break;
                        }
                    }

                    curveControl.IniSensorAttri(testingTestUnit[0].SensorInfo[i].SensorNo,
                                                testingTestUnit[0].SensorInfo[i].Name,
                                                copyCoordinate,
                                                testingTestUnit[0].SensorInfo[i].Visible,
                                                testingTestUnit[0].SensorInfo[i].Selected,
                                                testingTestUnit[0].SensorInfo[i].TotalSequenceNo,
                                                testingTestUnit[0].SensorInfo[i].DotNum);

                    //传感器颜色
                    curveControl.SetSensorColorNo(testingTestUnit[0].SensorInfo[i].SensorNo,
                                                  testingTestUnit[0].SensorInfo[i].ColorNo);

                    //设置传感器组号
                    curveControl.SetSensorGroupNo(testingTestUnit[0].SensorInfo[i].SensorNo,
                                                  testingTestUnit[0].SensorInfo[i].GroupNo);
                }
            }

            //添加传感器采集时间
            List<float> DotHowLong = new List<float>();
            float startLong = 0;
            for (i = 0; i < 500; i++)
            {
                startLong = (float)(i * 0.006);
                DotHowLong.Add(startLong);
            }
            curveControl.ReceiveDotHowLong(ref DotHowLong);

            //添加传感器采集数值
            List<float> DotValue = new List<float>();
            List<float> DotValue2 = new List<float>();
            List<float> DotValue3 = new List<float>();
            List<float> DotValue4 = new List<float>();
            for (i = 0; i < 500; i++)
            {
                DotValue.Add((float)(0 + i * 0.05));
                DotValue2.Add((float)(30 - i * 0.05));
                DotValue3.Add((float)(50 - i * 0.5));
                DotValue4.Add((float)(35 - i * 0.01));
            }

            curveControl.ReceiveValue(1, ref DotValue);
            curveControl.ReceiveValue(2, ref DotValue);
            curveControl.ReceiveValue(3, ref DotValue2);
            curveControl.ReceiveValue(4, ref DotValue3);
            curveControl.ReceiveValue(15, ref DotValue4);

            curveControl.EnableScroll(false, this.Height);
            //语言
            curveControl.ChangeLanguage(CurveControl.LanguageType.Chinese);

            //设置控件内的布局
            curveControl.RefreshLayout();
            //画出曲线
            curveControl.DrawPicCurve();
            curveControl.IsFirstListView = false;
            curvePanel.Controls.Add(curveControl);
            //this.Controls.Add(curveControl);
            curveControl.Dock = DockStyle.Fill;
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            string fName;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "./";
            openFileDialog.Filter = "所有文件|*.*";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                fName = openFileDialog.FileName;
                try
                {
                    System.IO.File.WriteAllText(fName, this.textBoxSysLog.Text);
                    MessageBox.Show("导出日志成功\n" + fName + "\n");
                }
                catch (Exception ee)
                {
                    MessageBox.Show("导出日志失败\n"+ee.Message+"\n");
                    //throw;
                }
                
            }
            else
            {
                return;
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            this.textBoxSysLog.Clear();
        }

        private void 修改LabCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setLabCode setL = new setLabCode();
            setL.Show();
        }

        private void 测试信息录入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            enterDataForm eform = new enterDataForm();
            eform.Show();
        }
    }
}
