<template>
<div>
  <el-collapse accordion id="searchDiv" >
  <el-collapse-item>
    <template slot="title" id="searchtitle"  >
     <i class="el-icon-search searchicon" ></i>
    </template>
  <div>
    <el-form :inline="true"  class="searchform">
    <el-form-item label="日志：" class="searchinput">
    <el-select v-model="searchLogName" filterable placeholder="请选择">
    <el-option
      v-for="item in logOptions"
      :key="item.value"
      :label="item.label"
      :value="item.value" size="mini" >
    </el-option>
  </el-select>
    </el-form-item>

</el-form>
  </div>
  </el-collapse-item>
 
</el-collapse>
 <v-btn-group :userid="user.ID"  :menuid="this.menuid" @btnclick="mybtnclick" ></v-btn-group> 
  <v-login-text ref="child" :logname="searchLogName"></v-login-text> 
</div>  
</template>
<script>
import btnGroup from '@/components/btnGroup';
import loginText from '../components/LoginBill/loginText';
import store from '../store/store.js';
  export default {
    props: ['menuname','menuid','menutitle'],
    data() {
  
      return {
        user:new Object(),
        logBillParameterStr:'',
        searchLogName:'',
        logOptions:[]
      };
  
    },
    
    store,
  
  
    created() {
      this.user=this.$store.state.userInfo;
      this.GetlogOptions();
 
    
    },
  
  
  
    methods: {


    GetlogOptions()
    {
           var _self = this;
           this.$api.post("Sys/GetAllLogList", {  Token:this.$store.state.userInfo.Token}, response => {
        
           _self.logOptions=[];
  
         // console.log(response.status);
  
          if (response.status >= 200 && response.status < 300) {
  
            if (response.data) {
              var jsonData = eval("(" + response.data + ")");
  
              if (jsonData.Code == "1") {
  
                if (jsonData.Data) {
    
              
                  var info=JSON.parse(jsonData.Data);
                  _self.logOptions=info;

                 
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
    },

     mybtnclick(btnMsg)
       {
         // this.$message.error(btnMsg);
        var _self = this;
        switch(btnMsg)
        {
          case 'search':
           this.search();
           break;
          default:
          this.$message.error(btnMsg);
          break;
        }

       },
       search()
       {
          // this.logBillParameterStr=this.searchLogName;
           this.$refs.child.$emit('callMethod');
       }
    },
    components: {
             'v-btn-group':btnGroup,
             'v-login-text':loginText
    }
  
  };
</script>
<style>
 
</style>
