<template>
 
  <div class="login-wrap">
  
  
  
    <el-card id="loginCard" shadow="always" >
  
  
  
      <h3>登录界面</h3>
  
  
  
  
  
      <el-input id="user" class="logininput" type="text" placeholder="请输入用户名" v-model="username" prefix-icon="el-icon-search" clearable>
        <template slot="prepend">帐号</template>
      </el-input>
  
  
  
  
      <el-input id='password' class="logininput" type="password" placeholder="请输入密码" v-model="password" prefix-icon="el-icon-search" clearable>
        <template slot="prepend">密码</template>
      </el-input>
      
    <el-row>
        <div id="captcha">
            <p id="wait" class="show">正在加载验证码......</p>
        </div>
    </el-row>
      <el-button id="login_button" v-on:click="CheckGeetest" :loading="loading">登录</el-button>
  
    </el-card>
  
  
  
  </div>
 
</template>

<style>

  .login-wrap {
  
 
    text-align: center;
    background-repeat: no-repeat;
    background-image: url('../assets/image/login_bg.jpg');
    width:99%;
    height: 800px;
    position: absolute;
    top:50%; 
    margin-top:-400px;
  
  }
  
  
  
  #loginCard {
  
    position: absolute;
  
    top: 0px;
  
    right: 0px;
  
    left: 0px;
  
    bottom: 0px;
  
    margin: auto;
  
    height: 280px;
  
    width: 335px;
  
    text-align: center;
   
  
  }
   #captcha {
            text-align:center;
            width: 300px;
            display: inline-block;
        }
  
  
  .logininput {
  
    margin: 3px;
  
  }
  
  
  
  #login_button {
  
    margin: 5px;
  
  }
  .code {
  margin:  auto;
 
  width: 114px;
  height: 40px;
  border: 1px solid red;
}
</style>
<script>

  import store from '../store/store.js';
  import jsbase64 from 'js-base64';
  import '../assets/js/gt.js';
  export default {
  
    data() {
  
      return {
  
  
 
        loading: false,
  
        tishi: '',
  
        username: '',
  
        password: '',
  
        newUsername: '',
  
        newPassword: '',
        
        GeetestId:'',
        CaptchaObj:''
      }
    
  
    },
  
    store,
    jsbase64,
    mounted() {
     this.createdInitGeetest();
    },
    methods: {
      handler(captchaObj)
      {
      captchaObj.appendTo('#captcha');
            captchaObj.onReady(function () {
                $("#wait").hide();
            });
      this.CaptchaObj=captchaObj;
      },
      newGuid()
      {
          var guid = "";
          for (var i = 1; i <= 32; i++){
            var n = Math.floor(Math.random()*16.0).toString(16);
            guid +=   n;
            if((i==8)||(i==12)||(i==16)||(i==20))
              guid += "-";
          }
          return guid;    
      },
      createdInitGeetest()
      {
         this.GeetestId = this.newGuid();
         this.$api.post("Sys/GeetestInit", {
          GeetestId: this.GeetestId
        }, response => {
          console.log(response.status);
  
          if (response.status >= 200 && response.status < 300) {
            if (response.data) {
              var jsonData = eval("(" + response.data + ")");
              if (jsonData.Code == "1") {
                if (jsonData.Data) {
  
                  var info = eval("(" + jsonData.Data + ")");
  
                  if (info) {
                    initGeetest({
                        // 以下 4 个配置参数为必须，不能缺少
                        gt: info.gt,
                        challenge: info.challenge,
                        offline: !info.success, // 表示用户后台检测极验服务器是否宕机
                        new_captcha: info.new_captcha, // 用于宕机时表示是新验证码的宕机

                        product: "float", // 产品形式，包括：float，popup
                        width: "300px"
                        // 更多配置参数说明请参见：http://docs.geetest.com/install/client/web-front/
                    }, this.handler);
  
  
                  }
  
  
                }
  
  
  
              } else {
  
                this.$message.error(jsonData.Message);
                console.log(jsonData.Message);
  
  
  
              }
  
            }
  
  
  
          } else {
            this.$message.error(response.message); 
            console.log(response.message);
  
          }
  
        });
  
  
  
      
      
      },
      CheckGeetest()
      {
          if(!this.username)
        {
            this.$message({
            message: '用户名不能为空！',
            type: 'warning'
          });
            return;
        }
          if(!this.password)
        {
            this.$message({
            message: '密码不能为空！',
            type: 'warning'
          });
            return;
        }
         this.loading = true;
        var result = this.CaptchaObj.getValidate();
                    if (!result) {
                        this.$message({
                          message: '请完成验证!',
                          type: 'warning'
                        });
                        return;
                    };
         var _self = this;
         this.$api.post("Sys/GeetestCheck", {
          GeetestId: this.GeetestId,
          geetest_challenge : result.geetest_challenge,
           geetest_seccode : result.geetest_seccode,
            geetest_validate : result.geetest_validate
        }, response => {
  
          this.loading = false;
  
          console.log(response.status);
  
          if (response.status >= 200 && response.status < 300) {
            if (response.data) {
              var jsonData = eval("(" + response.data + ")");
              if (jsonData.Code == "1") {
                _self.login();  
              } else {
  
                this.$message.error(jsonData.Message);
                console.log(jsonData.Message);
  
  
  
              }
  
            }
  
  
  
          } else {
            this.$message.error(response.message); 
            console.log(response.message);
  
          }
  
        });

        

      },
      login() {
       
      //  if(!this.username)
      //  {
      //      this.$message({
      //     message: '用户名不能为空！',
      //     type: 'warning'
      //   });
      //     return;
      //  }
      //   if(!this.password)
      //  {
      //      this.$message({
      //     message: '密码不能为空！',
      //     type: 'warning'
      //   });
      //     return;
      //  }
        this.loading = true;
        let Base64 = jsbase64.Base64;
        var passwords = Base64.encode(this.password);
        this.$api.post("User/GetUserByAccountName", {
  
          accountName: this.username,
  
          passWord: passwords
  
        }, response => {
  
          
  
          console.log(response.status);
  
          if (response.status >= 200 && response.status < 300) {
  
  
  
            //    console.log(response.data);
  
  
  
            if (response.data) {
  
  
  
              var jsonData = eval("(" + response.data + ")");
  
  
  
              if (jsonData.Code == "1") {
  
  
  
                if (jsonData.Data) {
  
  
  
                  var info = eval("(" + jsonData.Data + ")");
  
                  // if(password==info.Password)
  
                  if (info) {
  
                    var newuserInfo = new Object();
  
                    newuserInfo.ID = info.ID;
  
                    newuserInfo.AccountName = info.AccountName;
  
                    
  
                    newuserInfo.RealName = info.RealName;
  
                   
                   
                    newuserInfo.CreateTime = info.CreateTime;
                    newuserInfo.Token=info.Token;
  
                    console.log(newuserInfo.Token);
                    //更新AUEX
                    var  info1 = JSON.stringify(newuserInfo);
                    window.localStorage.setItem('newuserInfo',info1);
                    this.$store.commit('UpdateUserInfo', newuserInfo);
  
                    // console.log($store.userInfo.AccountName);
  
                    //测试打印
  
                    //console.log(this.$store.state.userInfo.LoginDate);
  
                    setTimeout(function() {
                     this.loading = false;
                      this.$router.push('/Home')
  
                    }.bind(this), 1000)
  
  
  
                  }
                  this.loading = false;
  
  
  
  
  
  
  
                  //跳转页面
  
  
  
                }
                this.loading = false;
  
  
  
              } else {
  
  
               this.loading = false;
                this.CaptchaObj.reset();
                this.$message.error(jsonData.Message);
  
  
  
                console.log(jsonData.Message);
  
  
  
              }
  
            }
  
  
  
          } else {
            this.loading = false;
            this.CaptchaObj.reset();
            this.$message.error(response.message);
  
  
  
            console.log(response.message);
  
          }
  
        });
  
  
  
      }
  
  
    },
  
    created() {
    // var newuserInfo = new Object();
   //  window.localStorage.removeItem("newuserInfo");
     // 更新AUEX
     // this.$store.commit('UpdateUserInfo', newuserInfo);
   this.clearQx();
  
  
    }
  
  }
</script>

