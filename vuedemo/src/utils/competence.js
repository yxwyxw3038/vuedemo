


exports.install = function (Vue, options) {
    Vue.prototype.PageQx = function (){
        var newuserInfo= window.localStorage.getItem('newuserInfo');
        if(!newuserInfo)
        {
            newuserInfo= new Object();
     
  
            newuserInfo.ID ='';

            newuserInfo.AccountName  ='';
            newuserInfo.Password  ='';

            newuserInfo.RealName  ='';

            newuserInfo.MobilePhone  ='';

            newuserInfo.Email  ='';

            newuserInfo.IsAble = false;

            newuserInfo.IfChangePwd = false;

            newuserInfo.LoginDate ='';
           
        }else{
            newuserInfo=JSON.parse(newuserInfo);
        }
       
        this.$store.commit('UpdateUserInfo', newuserInfo);
        if(!this.$store.state.userInfo.AccountName)
  {
        this.$router.push('/Login');
  }
},
Vue.prototype.clearQx = function (){
    
    var newuserInfo = new Object();
    window.localStorage.removeItem("newuserInfo");
     //更新AUEX
     this.$store.commit('UpdateUserInfo', newuserInfo);
}
};
