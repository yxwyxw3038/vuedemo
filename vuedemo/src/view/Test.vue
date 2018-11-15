<template>
<div>
   <v-btn-group :userid="user.ID"  :menuid="0" ></v-btn-group> 
  <div>
  <el-table :data="tableData" height="400px" border>
  
  
  
    <el-table-column prop="AccountName" label="登录名" width="100">
  
  
  
    </el-table-column>
  
  
  
    <el-table-column prop="RealName" label="用户名称" width="100">
  
  
  
    </el-table-column>
  
  
  
    <el-table-column prop="MobilePhone" label="联系人手机" width="100">
  
  
  
    </el-table-column>
  
  
  
    <el-table-column prop="Email" label="邮箱" width="100">
  
  
  
    </el-table-column>
  
  
  
  
  
    <el-table-column prop="IsAble" label="启用" width="60">
  
  
  
    </el-table-column>
  
  
  
    <el-table-column prop="IfChangePwd" label="改密" width="60">
  
  
  
    </el-table-column>
  
  
  
    <el-table-column prop="UpdateTime" label="修改时间" width="100">
  
  
  
    </el-table-column>
  
  
  
    <el-table-column prop="UpdateBy" label="修改人" width="100">
  
  
  
    </el-table-column>
  
  
  
  </el-table>

</div> 
</div>
</template>
<script>
import btnGroup from '@/components/btnGroup';
import store from '../store/store.js';
  export default {
  
    data() {
  
      return {
        user:new Object(),
        tableData: []
  
      };
  
    },
    store,
  
  
    created() {
      this.user=this.$store.state.userInfo;
      this.init();
  
    },
  
  
  
    methods: {
  
      init() {
  
        var _self = this;
  
  
  
        _self.tableData = [];
  
  
  
        this.$api.get("User/GetAllUser", {}, response => {
  
          console.log(response.status);
  
  
  
          if (response.status >= 200 && response.status < 300) {
  
            console.log(response.data);
  
            if (response.data) {
  
              var jsonData = eval("(" + response.data + ")");
  
              if (jsonData.Code = "1") {
  
                if (jsonData.Data) {
  
                  var info = eval("(" + jsonData.Data + ")");
                  for (var i = 0; i < info.length; i++) {
                    _self.tableData.push(info[i]);
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
  
      }
  
    },
    components: {
             'v-btn-group':btnGroup
    }
  
  };
</script>
<style>
  
</style>
