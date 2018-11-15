using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestConsoleDemo
{
    class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            Console.Title = "ConsoleDemo";
            logger.Log(LogLevel.Info, string.Format("程序启动！"));
            LoadAssembly();
            Console.Read();
        }
        public static void LoadAssembly()
        {

            int num = 0;
            //DLL所在的绝对路径 
            Assembly assembly = Assembly.LoadFrom(AppDomain.CurrentDomain.BaseDirectory + "RestConsoleDemo.Service.dll");
            Type[] types = assembly.GetTypes();
            foreach (Type type in types)
            {
                string tempName = type.Name;
                if (tempName.IndexOf("Service") > 0 && tempName.Substring(0, 1) != "I")
                {
                    //为每个服务创建一个线程
                    Thread t = new Thread(new ParameterizedThreadStart(MyThread));
                    t.Start(tempName);
                    num++;
                }
            }

           
        }
        private static void MyThread(object name)
        {
            string tempName = name.ToString();
            try
            {
                Assembly assembly = Assembly.LoadFrom(AppDomain.CurrentDomain.BaseDirectory + "RestConsoleDemo.Service.dll");

              

                Type personType = assembly.GetType(string.Format("RestConsoleDemo.Service.{0}", tempName));

                ServiceHost host = new ServiceHost(personType);
                host.Opened += delegate
                {
                  
                    logger.Log(LogLevel.Info, string.Format("{0}服务已加载！", personType.ToString()));
                };

                host.Open();
            }
            catch(Exception ex)
            {
                logger.Log(LogLevel.Error, string.Format("启动{0}对应REST服务失败，原因：{1}...", tempName, ex.Message));
            }
        }
    }
}
