using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cvisoftware
{
    class interfaceURL
    {
        public static string host = "http://115.28.236.114/RestInterfaceSystem";
        /// <summary>
        /// URL：删除系统信息
        /// </summary>
        public static string delSysInfoURL = host + "/systemInfoController/deleteAll";
        /// <summary>
        /// URL：删除导航栏信息
        /// </summary>
        public static string delNavURL = host + "/testUnitNavigationInfoController/deleteAll";
        /// <summary>
        /// URL：删除测试单元
        /// </summary>
        public static string delTestUnitURL = host + "/testUnitConfigController/deleteAll";
        /// <summary>
        /// URL：删除窗体
        /// </summary>
        public static string delWindowURL = host + "/windowConfigController/deleteAll";
        /// <summary>
        /// URL：删除子窗体
        /// </summary>
        public static string delSubWindowURL = host + "/subWindowConfigController/deleteAll ";
        /// <summary>
        /// URL：删除坐标系
        /// </summary>
        public static string delCoordinateURL = host + "/coordinateConfigController/deleteAll";
        /// <summary>
        /// URL：删除传感器名字
        /// </summary>
        public static string delSensorNameURL = host + "/sensorNameController/deleteAll";
        /// <summary>
        /// URL：删除传感器类型
        /// </summary>
        public static string delSensorTypeURL = host + "/sensorTypeController/deleteAll";
        /// <summary>
        /// URL:删除传感器配置
        /// </summary>
        public static string delSensorConfigURL = host + "/sensorConfigController/deleteAll";

        public static Hashtable getAllDelURL()
        {
            Hashtable delURLS = new Hashtable();
            delURLS.Add("delSysInfoURL",delSysInfoURL);
            delURLS.Add("delTestUnitURL",delTestUnitURL);
            delURLS.Add("delCoordinateURL",delCoordinateURL);
            delURLS.Add("delWindowURL", delWindowURL);
            delURLS.Add("delSubWindowURL", delSubWindowURL);
            delURLS.Add("delSensorNameURL", delSensorNameURL);
            delURLS.Add("delSensorTypeURL", delSensorTypeURL);
            delURLS.Add("delSensorConfigURL", delSensorConfigURL);
            return delURLS;
        }
        
    }
}
