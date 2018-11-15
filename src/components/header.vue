<template>
  <div>
    <header class="onediv">
     
      <ul class="header-operations">
        <li>
           <i class="el-icon-bell"></i>
           <span class="spanFont">欢迎您：</span>
           <el-dropdown @command="handleCommand">
  <span class="el-dropdown-link spanFont">
    {{user.RealName}}<i class="el-icon-arrow-down el-icon--right"></i>
  </span>
  <el-dropdown-menu slot="dropdown"  style="background-color: rgb(29, 67, 95) " >
    <el-dropdown-item command="userinfo" ><span class="spanFont">个人信息</span></el-dropdown-item>
    <el-dropdown-item  command="logout" ><span class="spanFont">退出系统</span></el-dropdown-item>
  </el-dropdown-menu>
</el-dropdown>
        </li>
       
      </ul></header>
  </div>
</template>
<script>
  export default {
    props: {
       user: {
      }
    },
    data () {
      return {
      };
    },
    methods: {
      userInfo()
      {
        this.$emit('EditUserInfo');
      },
      logOut()
      {
           var _self = this;
           this.$api.post("User/LoginOut", {  Token:this.$store.state.userInfo.Token,accountName:this.user.AccountName}, response => {
        
          
    
  
          if (response.status >= 200 && response.status < 300) {
  
            if (response.data) {
              var jsonData = eval("(" + response.data + ")");
  
              if (jsonData.Code == "1") {
  
                _self.clearQx();
                 _self.PageQx();
  
              } else {
  
 
                this.$message.error(jsonData.Message);
                console.log(jsonData.Message);
  
  
  
              }
  
            }
  
  
  
          } else {
            this.$message.error(response.message);  
          }
  
        });
      },
      handleCommand(command) {
        if (command === 'logout') {
         this.logOut();
        }
         if (command === 'userinfo') {
           this.userInfo();
        }
       
      }
    }
  };
</script>
<style>
  header {
  
    height: 40px;
    position: absolute;
    width: 100%;
    top: 0;
    left: 0;
     /* padding: 0 20px;  */
    z-index: 999;
    box-sizing: border-box;
    position: fixed;
  }
  .header-logo {
    display: inline-block;
    vertical-align: middle;
  }
  .header-operations {
    display: inline-block;
    float: right; 
    padding-right: 30px;
    height: 100%;
    margin:0px;
  }

  .header-operations li {
    color: #fff;
     size: 16;
    display: inline-block;
    vertical-align: middle;
    cursor: pointer;
  }
  .header-operations li:first-child{
    cursor: default
  }
  .header-opacity{
    opacity: .7;
  }
  .header-operations:after, header:after {
    display: inline-block;
    content: "";
    height: 100%;
    vertical-align: middle;
  }
</style>
