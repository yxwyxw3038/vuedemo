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
  
  
  
      <el-button id="login_button" v-on:click="login" :loading="loading">登录</el-button>
  
  
  
  
  
  
  
  
  
  
  
    </el-card>
  
  
  
  </div>
</template>

<style>
  .login-wrap {
  
    text-align: center;
  
    background-repeat: no-repeat;
  
    background-image: url('../assets/image/login_bg.jpg');
  
  }
  
  
  
  #loginCard {
  
    position: absolute;
  
    top: 0px;
  
    right: 0px;
  
    left: 0px;
  
    bottom: 0px;
  
    margin: auto;
  
    height: 250px;
  
    width: 300px;
  
    text-align: center;
  
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
  export default {
  
    data() {
  
      return {
  
  
 
        loading: false,
  
        tishi: '',
  
        username: '',
  
        password: '',
  
        newUsername: '',
  
        newPassword: ''
  
      }
  
    },
  
    store,
    jsbase64,
    methods: {
  
      login() {
  
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
                this.$message.error(jsonData.Message);
  
  
  
                console.log(jsonData.Message);
  
  
  
              }
  
            }
  
  
  
          } else {
            this.loading = false;
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

