using RestConsoleDemo.BLL;

using RestConsoleDemo.Service.Helper;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace RestConsoleDemo.Service
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [JavascriptCallbackBehavior(UrlParameterName = "jsoncallback")]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class UserInfoService : IUserInfoService
    {
        public string GetAllUser()
        {

            ResponseHelper.SetHeaderInfo();

            string str = UserBill.GetAllUser();
            return str;


        }
        public string GetUserByID(int userID)
        {
            ResponseHelper.SetHeaderInfo();
            string str = UserBill.GetUserByID(userID);
            return str;
        }
        public string GetUserByAccountName(Stream stream)
        {

            string accountName = string.Empty;
            string passWord = string.Empty;
            StreamReader sr = new StreamReader(stream);


            try
            {
                string s = sr.ReadToEnd();

                ResponseHelper.SetHeaderInfo();
                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                accountName = nvc["accountName"];
                passWord = nvc["passWord"];
                string str = UserBill.GetUserByAccountName(accountName, passWord);
                return str;
                //  sr.Dispose();
                //NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                //accountName = nvc["accountName"];
                //passWord = nvc["passWord"];

            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            //  ResponseHelper.SetHeaderInfo();

            // string str = UserBill.GetUserByAccountName(accountName, passWord);
            //   return str;
        }
        public string GetUserMenu(Stream stream)
        {
            string userID = string.Empty;

            StreamReader sr = new StreamReader(stream);


            try
            {
                string s = sr.ReadToEnd();

                ResponseHelper.SetHeaderInfo();
                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                userID = nvc["userID"];
                int id = Convert.ToInt32(userID);
                string str = MenuBill.GetUserMenu(id);
                return str;
               

            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }

         public string GetMenuById(Stream stream)
        {
            string ID = string.Empty;

            StreamReader sr = new StreamReader(stream);


            try
            {
                string s = sr.ReadToEnd();

                ResponseHelper.SetHeaderInfo();
                NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(s);
                ID = nvc["Id"];
                int id = Convert.ToInt32(ID);
                string str = MenuBill.GetMenuById(id);
                return str;


            }
            catch (Exception ex)
            {
                return RestConsoleDemo.BLL.Helper.ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
        }
        public string Getaa()
        {
            string accountName = "admin"; string passWord = "admin";
            ResponseHelper.SetHeaderInfo();
            string str = UserBill.GetUserByAccountName(accountName, passWord);
            return str;
        }
    }
}
