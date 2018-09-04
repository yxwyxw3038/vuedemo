<template>

<div >
   <el-form :model="form" ref="form" :rules="rules" label-width="120px"  style="width:80%" >
    <el-form-item label="登录名："  prop="AccountName">
        <el-col :span="8">
     <el-input v-model="form.AccountName" ></el-input>
      </el-col>
    </el-form-item>
    
     <el-form-item label="用户名称：" prop="RealName" >
       <el-col :span="8">
     <el-input v-model="form.RealName" ></el-input>
       </el-col>
    </el-form-item>
     
      <el-form-item label="联系人手机:"  prop="MobilePhone" >
         <el-col :span="8">
     <el-input v-model="form.MobilePhone" ></el-input>
         </el-col>
    </el-form-item>
     <el-form-item label="邮箱:" prop="Email" >
       <el-col :span="8">
     <el-input v-model="form.Email" ></el-input>
       </el-col>
    </el-form-item>
       <el-form-item label="密码:" prop="Password" >
       <el-col :span="8">
     <el-input type="password" v-model="form.Password"  auto-complete="off"></el-input>
       </el-col>
    </el-form-item>
       <el-form-item label="确认密码:" prop="DoublePassword" >
       <el-col :span="8">
        <el-input  type="password" v-model="form.DoublePassword" auto-complete="off" ></el-input>
       </el-col>
    </el-form-item>
    <el-form-item style="text-align:left" >
    <el-button @click="closeForm('form')">取 消</el-button>
    <el-button type="primary" @click="submitForm('form')">确 定</el-button>
    </el-form-item>
  </el-form>

 </div>
</template> 
<script>
import store from '../store/store.js';
import jsbase64 from 'js-base64';
 export default {
  props: ['menuname','menuid','menutitle'], 
    data() {
  var validatePass = (rule, value, callback) => {
        if (value === '') {
          callback(new Error('请输入密码'));
        } else {
          if (this.form.DoublePassword !== '') {
            this.$refs.form.validateField('DoublePassword');
          }
          callback();
        }
      };
      var validatePass2 = (rule, value, callback) => {
        if (value === '') {
          callback(new Error('请再次输入密码'));
        } else if (value !== this.form.Password) {
          callback(new Error('两次输入密码不一致!'));
        } else {
          callback();
        }
      };
      return {
        user:new Object(),
       
        form: {
          ID:0,
          AccountName:'',
          Password:"",
          DoublePassword:"",
          RealName:'',
          MobilePhone:'',
          Email:'',
          Description:'',
          CreateBy:'',
          UpdateBy:'',
          IsAble:'1',
          IfChangePwd:'0'
        },
          submitform: {
          ID:0,
          AccountName:'',
          Password:"",
          DoublePassword:"",
          RealName:'',
          MobilePhone:'',
          Email:'',
          Description:'',
          CreateBy:'',
          UpdateBy:'',
          IsAble:'1',
          IfChangePwd:'0'
        },
       Beginform: {
          ID:0,
          AccountName:'',
          Password:"",
          DoublePassword:"",
          RealName:'',
          MobilePhone:'',
          Email:'',
          Description:'',
          CreateBy:'',
          UpdateBy:'',
          IsAble:'1',
          IfChangePwd:'0'
        },
       rules: {
          AccountName: [
            { required: true, message: '请输入帐号', trigger: 'blur' }
         
          ],
           RealName: [
            { required: true, message: '请输入用户名称', trigger: 'blur' }
         
          ] ,
           MobilePhone: [
            { required: true, message: '请输入手机号', trigger: 'blur' }
         
          ],
           Email: [
            { required: true, message: '请输入邮箱', trigger: 'blur' },
            { type: 'email', message: '请输入正确的邮箱地址', trigger: ['blur', 'change'] }
         
          ],
        Password: [
            { required: true, message: '请输入密码', trigger: 'blur' },
            { validator: validatePass, trigger: 'blur' }
          ],
          DoublePassword: [
            { required: true, message: '请再次输入密码', trigger: 'blur' },
            { validator: validatePass2, trigger: 'blur' }
          ]
      
          
        },
     

  
      };
  
    },
    store,
  
  
    created() {
      this.init();
    },
     methods: {
      submitInfo()
      {
        
           let Base64 = jsbase64.Base64;
          var passwords = Base64.encode(this.form.Password);
     

        var  optUrl="User/UpdateUserAllinfo";
          this.submitform={
          ID:this.Beginform.ID,
          AccountName:this.form.AccountName,
          Password:passwords,
          RealName:this.form.RealName,
          MobilePhone:this.form.MobilePhone,
          Email:this.form.Email,
          Description:this.form.Description,
          CreateBy:this.Beginform.CreateBy,
          UpdateBy:this.$store.state.userInfo.AccountName,
          IsAble:this.Beginform.IsAble,
          IfChangePwd:this.Beginform.IfChangePwd
        }
 
        

         var Jsonstr= JSON.stringify(this.submitform);

           var _self = this;
           this.$api.post(optUrl, {  Token:this.$store.state.userInfo.Token,str:Jsonstr}, response => {
        
          
    
  
          if (response.status >= 200 && response.status < 300) {
  
            if (response.data) {
              var jsonData = eval("(" + response.data + ")");
  
              if (jsonData.Code == "1") {
                    var newuserInfo = new Object();
                    newuserInfo.ID = _self.submitform.ID;
                    newuserInfo.AccountName = _self.submitform.AccountName;
                    newuserInfo.RealName = _self.submitform.RealName;
                    newuserInfo.CreateTime = _self.submitform.CreateTime;
                    newuserInfo.Token=_self.$store.state.userInfo.Token;
  
                    console.log(newuserInfo.Token);
                    //更新AUEX
                    var  info1 = JSON.stringify(newuserInfo);
                    window.localStorage.setItem('newuserInfo',info1);
                    this.$store.commit('UpdateUserInfo', newuserInfo);
                   _self.$message({
                    message: '保存成功！',
                    type: 'success'
                  });

              } else {
  
 
                _self.$message.error(jsonData.Message);
                console.log(jsonData.Message);
  
  
  
              }
  
            }
  
  
  
          } else {
            this.$message.error(response.message);
  
  
          }
  
        });

      },
         submitForm(formName) {
          if(formName==="form")
          {
        this.$refs[formName].validate((valid) => {
          if (valid) {
            
            this.submitInfo();
          } else {
            return;
          }
           });
          }
          
      },
      closeForm(formName) {
         if(formName==="form")
          {
           this.init();
         }
         
      },
         init()
         {
         var _self = this;
           this.$api.post("User/GetUserByID", {  
              Token:this.$store.state.userInfo.Token,
              Id: this.$store.state.userInfo.ID}, response => {
      
          if (response.status >= 200 && response.status < 300) {
  
            if (response.data) {
              var jsonData = eval("(" + response.data + ")");
  
              if (jsonData.Code == "1") {
  
                if (jsonData.Data) {
    
                var tempdata= eval("(" + jsonData.Data+ ")");
                 _self.Beginform= {
                    ID:tempdata.ID,
                    AccountName:tempdata.AccountName,
                    RealName:tempdata.RealName,
                    MobilePhone:tempdata.MobilePhone,
                    Email:tempdata.Email,
                    Description:tempdata.Description,
                    CreateBy:tempdata.CreateBy,
                    UpdateBy:tempdata.UpdateBy,
                    IsAble:tempdata.IsAble.toString(),
                    IfChangePwd:tempdata.IfChangePwd.toString()
                   };
                   _self.form= {
                    ID:tempdata.ID,
                    AccountName:tempdata.AccountName,
                    RealName:tempdata.RealName,
                    MobilePhone:tempdata.MobilePhone,
                    Email:tempdata.Email,
                    Description:tempdata.Description,
                    CreateBy:tempdata.CreateBy,
                    UpdateBy:tempdata.UpdateBy,
                    IsAble:tempdata.IsAble.toString(),
                    IfChangePwd:tempdata.IfChangePwd.toString()
            }
                
                
                 
                }

  
  
  
              } else {
  
 
                _self.$message.error(jsonData.Message);
                console.log(jsonData.Message);
  
  
  
              }
  
            }
  
  
  
          } else {
            _self.$message.error(response.message);
  
  
          }
  
        });
         }
     }
  };
</script>

    