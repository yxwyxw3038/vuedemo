const userInfo =new Object()
 
userInfo.ID='';
userInfo.AccountName='';
userInfo.Password='';
userInfo.RealName='';
userInfo.MobilePhone='';
userInfo.Email='';
userInfo.IsAble=false;
userInfo.IfChangePwd=false;
userInfo.LoginDate='';




export default {
    state:{ userInfo},
    mutations:{
        UpdateUserInfo(userInfo,n){
            this.state.userInfo=n;
         //   console.log(userInfo.AccountName);
        }
    }
}