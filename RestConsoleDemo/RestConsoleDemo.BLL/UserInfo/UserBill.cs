using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RestConsoleDemo.BLL.Helper;
using RestConsoleDemo.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestConsoleDemo.BLL
{
    public static class UserBill
    {


        public static string GetUserByID(int userID)
        {
            string str = string.Empty;
            try
            {

                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
                tbUser temp = new tbUser();
                AchieveDBEntities myDbContext = new AchieveDBEntities();
                List<tbUser> templist = myDbContext.tbUser.Where(p => p.ID == userID).ToList();
                if (templist != null && templist.Count > 0)
                {
                    temp = templist[0];


                    str = JsonConvert.SerializeObject(temp, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("1", "取数成功", str);
                }
                else
                {
                    str = JsonConvert.SerializeObject(temp, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("-1", "用户ID不存在", str);
                }
            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str;

        }
        public static string GetUserByAccountName(string accountName,string passWord)
        {
            string str = string.Empty;
            try
            {
                string md5passWord = Md5Helper.GetMD5String(passWord);
                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
                tbUser temp = new tbUser();
                AchieveDBEntities myDbContext = new AchieveDBEntities();
                List<tbUser> templist = myDbContext.tbUser.Where(p => p.AccountName == accountName).ToList();
                if (templist != null && templist.Count > 0)
                {
                    temp = templist[0];
                    if (temp.Password == md5passWord)
                    {
                        str = JsonConvert.SerializeObject(temp, Formatting.Indented, timeFormat);
                        str = ResponseHelper.ResponseMsg("1", "取数成功", str);
                    }
                    else
                    {
                        str = JsonConvert.SerializeObject(temp, Formatting.Indented, timeFormat);
                        str = ResponseHelper.ResponseMsg("-1", "密码错误", str);
                    }

                }
                else
                {
                    str = JsonConvert.SerializeObject(temp, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("-1", "用户不存在", str);
                }
               
            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str;

        }

        public static string GetAllUser()
        {


            string str = string.Empty;
            try
            {
                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
                AchieveDBEntities myDbContext = new AchieveDBEntities();
                List<tbUser> templist = myDbContext.tbUser.ToList();
                str = JsonConvert.SerializeObject(templist, Formatting.Indented, timeFormat);
                str = ResponseHelper.ResponseMsg("1", "取数成功", str);
            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
            return str;
        }

    }
}
