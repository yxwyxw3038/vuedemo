


exports.install = function (Vue, options) {
    Vue.prototype.PageQx = function (){
        var newuserInfo= window.localStorage.getItem('newuserInfo');
        if(!newuserInfo)
        {
            newuserInfo= new Object();
     
  
            newuserInfo.ID ='';

            newuserInfo.AccountName  ='';
          

            newuserInfo.RealName  ='';

            newuserInfo.Token  ='';



            newuserInfo.CreateTime ='';
           
        }else{
            console.log(newuserInfo);
            newuserInfo=JSON.parse(newuserInfo);
        }
    
        this.$store.commit('UpdateUserInfo', newuserInfo);
        if(newuserInfo.Token)
        {
         //this.$store.state.userInfo.AccountName
        
         var _self = this;
         this.$api.post("User/GetUserLogininfoByToken", {Token:newuserInfo.Token}, response => {
      
      

       // console.log(response.status);

        if (response.status >= 200 && response.status < 300) {

          if (response.data) {
            var jsonData = eval("(" + response.data + ")");

            if (jsonData.Code == "1") {

             




            } else {


                _self.$message.error(jsonData.Message);
                console.log(jsonData.Message);
                _self.$router.push('/Login');


            }

          }



        } else {
            _self.$message.error(response.message);
            _self.$router.push('/Login');



        }

      });

               
        
         }
         else{
            this.$router.push('/Login');
         }
},
Vue.prototype.clearQx = function (){
    
    var newuserInfo = new Object();
    window.localStorage.removeItem("newuserInfo");
     //更新AUEX
     this.$store.commit('UpdateUserInfo', newuserInfo);
},
Vue.prototype.GetToken = function (){

    var newuserInfo= window.localStorage.getItem('newuserInfo');
    if(!newuserInfo)
    {
        return "";
    }
    else{
        newuserInfo=JSON.parse(newuserInfo);
        if(!newuserInfo.Token)
        {
            return "";
        }
        return newuserInfo.Token;

    }
},
Vue.prototype.getMyDate = function (str){  
    var oDate = new Date(str);
    var oYear = oDate.getFullYear();  
    var oMonth = oDate.getMonth();  
    var oDay = oDate.getDate(); 
    var oHour = oDate.getHours();
    var oMin = oDate.getMinutes();  
    var oSen = oDate.getSeconds(); 
    var oTime = oYear +'-'+ this.getzf(oMonth) +'-'+ this.getzf(oDay) +' '+ this.getzf(oHour) +':'+ this.getzf(oMin) +':'+this.getzf(oSen);//最后拼接时间  
    return oTime;  
},
Vue.prototype.getzf =function (num){  
    if(parseInt(num) < 10){  
        num = '0'+num;  
    }  
    return num;  
  }
};
