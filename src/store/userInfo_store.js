const userInfo =new Object()
 
userInfo.ID='';
userInfo.AccountName='';

userInfo.RealName='';

userInfo.CreateTime='';
userInfo.Token='';




export default {
    state:{ userInfo},
    mutations:{
        UpdateUserInfo(userInfo,n){
            this.state.userInfo=n;
         //   console.log(userInfo.AccountName);
        }
    }
}