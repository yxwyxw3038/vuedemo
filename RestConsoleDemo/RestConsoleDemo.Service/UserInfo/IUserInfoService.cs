using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace RestConsoleDemo.Service
{
    [ServiceContract(Namespace = "用户信息接口")]
    public interface IUserInfoService
    {
        [OperationContract, WebInvoke(UriTemplate = "/GetAllUser", Method = "GET",
         ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]

        [Description("获取全部用户信息")]
        string GetAllUser();

        [OperationContract, WebInvoke(UriTemplate = "/GetUserByID?userID={userID}", Method = "GET",
         ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        [Description("获取指定用户信息")]
        string GetUserByID(int userID);
        //[OperationContract, WebInvoke(UriTemplate = "/GetUserByAccountName?accountName={accountName}&passWord={passWord}", Method = "GET",
        // ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        [OperationContract, WebInvoke(UriTemplate = "/GetUserByAccountName", Method = "POST",
         ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        [Description("获取指定用户信息")]
        string GetUserByAccountName(Stream stream);
        [OperationContract, WebInvoke(UriTemplate = "/GetUserMenu", Method = "POST",
        ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        string GetUserMenu(Stream stream);


        [OperationContract, WebInvoke(UriTemplate = "/GetMenuById", Method = "POST",
        ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        string GetMenuById(Stream stream);


        [OperationContract, WebInvoke(UriTemplate = "/Getaa", Method = "POST",
        ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        [Description("获取指定用户信息")]
        string Getaa();
    }
}
