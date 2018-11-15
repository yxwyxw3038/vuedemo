using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RestConsoleDemo.BLL.Helper;
using RestConsoleDemo.BLL.Model;
using RestConsoleDemo.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestConsoleDemo.BLL
{
    public static class MenuBill
    {
        public static string GetUserMenu(int userID)
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
                    var query = from ur in myDbContext.tbUserRole
                                join rmb in myDbContext.tbRoleMenu on ur.RoleId equals rmb.RoleId
                                join m in myDbContext.tbMenu on rmb.MenuId equals m.Id
                                where ur.UserId == userID
                                && m.ParentId==0
                                orderby m.ParentId, m.Sort
                                select m;
                    List<MenuTreeModel> list = new List<MenuTreeModel>();
                    if (query != null)
                    {
                        foreach (var tp in query)
                        {
                            MenuTreeModel m = new MenuTreeModel();
                            m.Id = tp.Id.ToString();
                            m.Icon = tp.Icon;
                            m.MenuName = tp.Name;
                            m.ParentId = tp.ParentId.ToString();
                            m.Url = tp.LinkAddress;
                            m.Node = GetChildMenu(userID,tp.Id);
                            list.Add(m);
                        }
                    }

                    str = JsonConvert.SerializeObject(list, Formatting.Indented, timeFormat);
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

        public static string GetMenuById(int Id)
        {
            string str = string.Empty;
            try
            {

                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
                tbMenu temp = new tbMenu();
                AchieveDBEntities myDbContext = new AchieveDBEntities();
                List<tbMenu> templist = myDbContext.tbMenu.Where(p => p.Id == Id).ToList();
                if (templist != null && templist.Count > 0)
                {

                    temp = templist[0];
                    str = JsonConvert.SerializeObject(temp, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("1", "取数成功", str);
                }
                else
                {
                    str = JsonConvert.SerializeObject(temp, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("-1", "菜单不存在", str);
                }
            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str;
        }


        public static List<MenuTreeModel> GetChildMenu(int userID,int ParentId)
        {
            AchieveDBEntities myDbContext = new AchieveDBEntities();
            List<MenuTreeModel> listTemp = new List<MenuTreeModel>();
            var query = from ur in myDbContext.tbUserRole
                        join rmb in myDbContext.tbRoleMenu on ur.RoleId equals rmb.RoleId
                        join m in myDbContext.tbMenu on rmb.MenuId equals m.Id
                        where ur.UserId == userID
                        && m.ParentId == ParentId
                        orderby m.ParentId, m.Sort
                        select m;

            if (query != null)
            {
                foreach (var tp in query)
                {
                    MenuTreeModel m = new MenuTreeModel();
                    m.Id = tp.Id.ToString();
                    m.Icon = tp.Icon;
                    m.MenuName = tp.Name;
                    m.ParentId = tp.ParentId.ToString();
                    m.Url = tp.LinkAddress;
                    m.Node = GetChildMenu(userID, tp.Id);
                    listTemp.Add(m);
                }
            }

            return listTemp;

        }
    }
}
