<template>
<div>
  <el-collapse accordion id="searchDiv" >
  <el-collapse-item>
    <template slot="title" id="searchtitle"  >
     <i class="el-icon-search searchicon" ></i>
    </template>
  <div>
    <el-form :inline="true"  class="searchform">
    <el-form-item label="Token：" class="searchinput">
    <el-input v-model="searchToken" placeholder="请输入Token查询条件" class="searchinput"  size="mini"   type="text" clearable></el-input>
  </el-form-item>
  <!-- <el-form-item label="用户名：" class="searchinput">
    <el-input v-model="searchAccountName" placeholder="请输入用户名查询条件" class="searchinput"  size="mini"   type="text" clearable></el-input>
  </el-form-item>
  <el-form-item label="姓名：" class="searchinput">
    <el-input v-model="searchRealName" placeholder="请输入姓名查询条件" class="searchinput"  size="mini"   type="text" clearable></el-input>
  </el-form-item> -->
    <el-form-item label="是否离线：" class="searchinput" clearable>
  <el-select v-model="searchIsLoginOut" filterable placeholder="请选择" size="mini" clearable>
    <el-option
      v-for="item in loginOutOptions"
      :key="item.value"
      :label="item.label"
      :value="item.value" size="mini" >
    </el-option>
  </el-select>
  </el-form-item>
  <el-form-item label="登录日期：" class="searchinput">
    <el-date-picker size="mini" 
      v-model="searchCreateTime"
      type="daterange"
      range-separator="至"
      start-placeholder="开始日期"
      end-placeholder="结束日期"
       value-format="yyyy-MM-dd">
    </el-date-picker>
   </el-form-item>
   <el-form-item label="修改日期：" class="searchinput">
    <el-date-picker size="mini" 
      v-model="searchUpdateTime"
      type="daterange"
      range-separator="至"
      start-placeholder="开始日期"
      end-placeholder="结束日期"
       value-format="yyyy-MM-dd">
    </el-date-picker>
   </el-form-item>
</el-form>
  </div>
  </el-collapse-item>
 
</el-collapse>
 <v-btn-group :userid="user.ID"  :menuid="this.menuid" @btnclick="mybtnclick" :loading="loading"></v-btn-group> 
  <el-table :data="tableData"  border :loading="loading" :row-class-name="tableRowClassName"  @selection-change="handleSelectionChange">
  <el-table-column
      type="index"
       width="50"
       :index="indexMethod"
        label="序号" 
     fixed >
    </el-table-column>
    <el-table-column prop="Token" label="Token"  width="320px" fixed>
  
  
  
    </el-table-column>
  
  
     <el-table-column prop="AccountName" label="用户名"   width="200px"  fixed>
  
  
  
    </el-table-column>
    <el-table-column prop="RealName" label="姓名"   width="150px"  fixed>
  
  
  
    </el-table-column>
  
     <el-table-column prop="IsLoginOut" label="是否离线"  width="100px" >
  
      <template slot-scope="scope">

        <el-checkbox v-model="scope.row.IsLoginOut===1" disabled></el-checkbox>
      </template>
  
    </el-table-column>
    <el-table-column prop="CreateTime" label="登录时间" >
  
  
  
    </el-table-column>
    <el-table-column prop="UpdateTime" label="修改时间" >
  
  
  
    </el-table-column>
    <el-table-column prop="LoginOutTime" label="离线时间" >
  
  
  
    </el-table-column>
  
  

  
  
  
  </el-table>
  <el-pagination
      @size-change="handleSizeChange"
      @current-change="handleCurrentChange"
      :current-page="currentPage"
      :page-sizes="[10, 20, 50, 100]"
      :page-size="currentpagesize"
      layout="total, sizes, prev, pager, next, jumper"
      :total="totalCount">
    </el-pagination>
 
</div> 

</template>
<script>
import btnGroup from '@/components/btnGroup';
import store from '../store/store.js';
  export default {
    props: ['menuname','menuid','menutitle'],
    data() {
  
      return {
        user:new Object(),
        tableData: [],
        loginOutOptions:[{
          value: '0',
          label: '否'
        }, {
          value: '1',
          label: '是'
        }],
        searchToken:"",
        searchUpdateTime:'',
        searchCreateTime:'',
        searchAccountName:"",
        searchRealName:"",
        searchIsLoginOut:'',
        loading:false,
        currentPage:1,
        currentpagesize:10,
        totalCount:400,

       loginBillParameterStr:'',
      
      };
  
    },
    
    store,
  
  
    created() {
      this.user=this.$store.state.userInfo;
      this.init();
 
    
    },
  
   filters: {
                IsLoginOutCapitalize: function (val) {

                    //var value1 = value == "true" ? "../../Content/Image/icon/chk_checked.gif" : "../../Content/Image/icon/chk_unchecked.gif";

                    var value1 = val==1 ? "是":"否";
                    return value1;
                }
            },
  
    methods: {

     
    indexMethod(index) {
        return index +(this.currentPage-1)*this.currentpagesize +1;
       
      },
     handleSizeChange(val) {
        console.log(`每页 ${val} 条`);
        this.currentpagesize=val;
        this.currentPage=1;
        this.totalCount=400;
        this.init();
      },
      handleCurrentChange(val) {
        console.log(`当前页: ${val}`);
        this.currentPage=val;
         this.init();
      },
      handleSelectionChange(val) {
        this.multipleSelection = val;
        if(this.multipleSelection&&this.multipleSelection.length>0)
        {
        console.log(this.multipleSelection[0].Name);
        }
      },
      tableRowClassName({row, rowIndex}) {
        if (rowIndex%2 === 0) {
          return 'warning-row';
        } else if (rowIndex%2 === 1) {
          return 'success-row';
        }
        return '';
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
        this.loginBillParameterStr='';
        var loginBillParameter=[];
       
         if (this.searchToken == null || this.searchToken == undefined || this.searchToken == "") {

         }
         else{
           var   parameter=new Object();
             parameter.action = "in";
             parameter.column = "Token";
             parameter.logic = "AND";
             parameter.value = this.searchToken;
             loginBillParameter.push(parameter);

         }
      if (this.searchAccountName == null || this.searchAccountName == undefined || this.searchAccountName == "") {

         }
         else{
           var   parameter=new Object();
             parameter.action = "in";
             parameter.column = "AccountName";
             parameter.logic = "AND";
             parameter.value = this.searchAccountName;
             loginBillParameter.push(parameter);

         }

      if (this.searchRealName == null || this.searchRealName == undefined || this.searchRealName == "") {

         }
         else{
           var   parameter=new Object();
             parameter.action = "in";
             parameter.column = "RealName";
             parameter.logic = "AND";
             parameter.value = this.searchRealName;
             loginBillParameter.push(parameter);

         }
      if (this.searchIsLoginOut == null || this.searchIsLoginOut == undefined || this.searchIsLoginOut == "") {

         }
         else{
           var   parameter=new Object();
             parameter.action = "=";
             parameter.column = "IsLoginOut";
             parameter.logic = "AND";
             parameter.value = this.searchIsLoginOut;
             loginBillParameter.push(parameter);

         }
      if (this.searchUpdateTime == null || this.searchUpdateTime == undefined || this.searchUpdateTime == "") {

         }
         else{
           var  parameter=new Object();
             parameter.action = ">=";
             parameter.column = "UpdateTime";
             parameter.logic = "AND";
             parameter.value = this.searchUpdateTime[0].toString();
             loginBillParameter.push(parameter);

            var  parameter=new Object();
             parameter.action = "<=";
             parameter.column = "UpdateTime";
             parameter.logic = "AND";
             parameter.value = this.searchUpdateTime[1].toString();
             loginBillParameter.push(parameter);

         }
if (this.searchCreateTime == null || this.searchCreateTime == undefined || this.searchCreateTime == "") {

         }
         else{
           var  parameter=new Object();
             parameter.action = ">=";
             parameter.column = "CreateTime";
             parameter.logic = "AND";
             parameter.value = this.searchCreateTime[0].toString();
             loginBillParameter.push(parameter);

            var  parameter=new Object();
             parameter.action = "<=";
             parameter.column = "CreateTime";
             parameter.logic = "AND";
             parameter.value = this.searchCreateTime[1].toString();
             loginBillParameter.push(parameter);

         }
         



         
        this.loginBillParameterStr=JSON.stringify(loginBillParameter);
        this.init();
       },
      init() {
  
        var _self = this;
        _self.tableData = [];
        _self.loading=true;
  
        this.$api.post("Sys/GetAllLoginViewInfo",{
          Token:this.$store.state.userInfo.Token,
          ParameterStr: this.loginBillParameterStr,
          PageSize:this.currentpagesize,
          CurrentPage:this.currentPage

        }, response => {
  
   
        _self.loading=false;
  
  
          if (response.status >= 200 && response.status < 300) {
  
  
            if (response.data) {
  
              var jsonData = eval("(" + response.data + ")");
  
              if (jsonData.Code == "1") {
                if(jsonData.DataCount)
                {
                  _self.totalCount=Number(jsonData.DataCount);
                }
                if (jsonData.Data) {
  
                  var info = eval("(" + jsonData.Data + ")");
                  for (var i = 0; i < info.length; i++) {
                    _self.tableData.push(info[i]);
                  }
                }
  
              } else {
                
                 if(jsonData.DataCount)
                {
                  _self.totalCount=Number(jsonData.DataCount);
                  if(_self.totalCount===0)
                  {
                      _self.totalCount=1;
                  }
                    if(_self.totalCount!=null&&  _self.totalCount>0)
                  {
                    if(_self.totalCount%_self.currentpagesize===0)
                    {
                      _self.currentPage=_self.currentPage-1;
                      if(_self.currentPage<=0)
                      {
                        _self.currentPage=1;
                      }
                      this.init();
                    }
                  }
                }
  
                _self.$message.error(jsonData.Message);
  
                console.log(jsonData.Message);
  
              }
  
            }
  
          } else {
  
          
  
            _self.$message.error(response.message);
  
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