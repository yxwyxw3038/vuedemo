<template>

<div>
   <el-collapse accordion id="searchDiv" >
  <el-collapse-item>
    <template slot="title" id="searchtitle"  >
     <i class="el-icon-search searchicon" ></i>
    </template>
  <div>
    <el-form :inline="true"  class="searchform">
    <el-form-item label="登录名：" class="searchinput">
    <el-input v-model="searchAccountName" placeholder="请输入菜单查询条件" class="searchinput"  size="mini"   type="text" clearable></el-input>
   
    </el-form-item>
    <el-form-item label="用户名称：" class="searchinput">
    <el-input v-model="searchRealName" placeholder="请输入菜单查询条件" class="searchinput"  size="mini"   type="text" clearable></el-input>
     </el-form-item>

     <el-form-item label="启用：" class="searchinput" clearable>
     <el-select v-model="searchIsAble" placeholder="请选择" size="mini" clearable>
    <el-option
      v-for="item in options1"
      :key="item.value"
      :label="item.label"
      :value="item.value">
    </el-option>
  </el-select>

     </el-form-item>
     <el-form-item label="改密：" class="searchinput">


     <el-select v-model="searchIfChangePwd" placeholder="请选择" size="mini" clearable>
    <el-option
      v-for="item in options1"
      :key="item.value"
      :label="item.label"
      :value="item.value">
    </el-option>
  </el-select>

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
<v-btn-group :userid="user.ID"  :menuid="this.menuid" @btnclick="mybtnclick"  :loading="loading" ></v-btn-group> 

  <el-table :data="tableData"  border :loading="loading" :row-class-name="tableRowClassName"  @selection-change="handleSelectionChange">
    <el-table-column
      type="selection"
      width="55px" fixed>
  </el-table-column>
  <el-table-column
      type="index"
       width="50px"
       :index="indexMethod"
        label="序号" 
     fixed >
    </el-table-column>
    <el-table-column prop="AccountName" label="登录名" width="150px" fixed>
  
  
  
    </el-table-column>
  
  
  
    <el-table-column prop="RealName" label="用户名称" width="200px" fixed>
  
  
  
    </el-table-column>
  
  <el-table-column prop="DepartmentName" label="所属部门" >
  
  
  
    </el-table-column>

    <el-table-column prop="RoleName" label="权限组" >
  
  
  
    </el-table-column>
  
    <el-table-column prop="MobilePhone" label="联系人手机" >
  
  
  
    </el-table-column>
  
  
  
    <el-table-column prop="Email" label="邮箱" >
     

 
    </el-table-column>
  
  
  
  
  
    <el-table-column prop="IsAble" label="启用" width="60px"  >
  
   <template slot-scope="scope">
     
      <el-checkbox v-model="scope.row.IsAble===1" disabled></el-checkbox>
    
   </template>
  
    </el-table-column>
  
  
  
    <el-table-column prop="IfChangePwd" label="改密" width="60px">
  
   <template slot-scope="scope">

    <el-checkbox v-model="scope.row.IfChangePwd===1" disabled></el-checkbox>
   </template>
  
    </el-table-column>
  
  
  
    <el-table-column prop="UpdateTime" label="修改时间" >
  
  
  
    </el-table-column>
  
  
  
    <el-table-column prop="UpdateBy" label="修改人">
  
  
  
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
      <el-dialog :title="dialogTitle" :visible.sync="dialogFormVisible" >

  <el-form :model="form" ref="form" :rules="rules" label-width="120px" >
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
    <el-form-item label="启用标记:" >
       <el-col :span="3">
      <el-switch
      style="display: block"
      v-model="form.IsAble"
      active-color="#13ce66"
      inactive-color="#ff4949"
      active-value='1'
      inactive-value='0'
      >
    </el-switch>
       </el-col>
    </el-form-item>
    <el-form-item label="改密标记:" >
       <el-col :span="3">
      <el-switch
      style="display: block"
      v-model="form.IfChangePwd"
      active-color="#13ce66"
      inactive-color="#ff4949"
     
      active-value='1'
      inactive-value='0'
      >
    </el-switch>
       </el-col>
    </el-form-item>
   <el-form-item label="备注:"  prop="Description" >
         <el-col :span="16">
     <el-input v-model="form.Description"  type="textarea" :rows="2" placeholder="请输入内容" ></el-input>
         </el-col>
    </el-form-item>
  </el-form>
  <div slot="footer" class="dialog-footer">
    <el-button @click="closeForm('form')">取 消</el-button>
    <el-button type="primary" @click="submitForm('form')">确 定</el-button>
  </div>
</el-dialog>
 <el-dialog :title="dialogSetUserRoleTitle" :visible.sync="dialogSetUserRoleVisible" >
    <el-transfer v-model="transferSetUserRoleValue" :data="transferSetUserRoleData"
     :titles="['未赋权', '已赋权']"
      :button-texts="['删除', '添加']"
      ></el-transfer>
     <div slot="footer" class="dialog-footer">
    <el-button @click="closeForm('transferSetUserRole')">取 消</el-button>
    <el-button type="primary" @click="submitForm('transferSetUserRole')">确 定</el-button>
  </div>
 </el-dialog> 
  <el-dialog :title="dialogSetUserDeptTitle" :visible.sync="dialogSetUserDeptVisible" >
    <el-transfer v-model="transferSetUserDeptValue" :data="transferSetUserDeptData"
     :titles="['未赋权', '已赋权']"
      :button-texts="['删除', '添加']"
      ></el-transfer>
     <div slot="footer" class="dialog-footer">
    <el-button @click="closeForm('transferSetUserDept')">取 消</el-button>
    <el-button type="primary" @click="submitForm('transferSetUserDept')">确 定</el-button>
  </div>
 </el-dialog> 
</div>
</template>
<script>
import btnGroup from '@/components/btnGroup';
import store from '../store/store.js';
import jsbase64 from 'js-base64';

  export default {
   props: ['menuname','menuid','menutitle'], 
    data() {
  
      return {
 
        user:new Object(),
        tableData: [],
        loading:false,
        multipleSelection: [],
        currentPage:1,
        currentpagesize:10,
        totalCount:400,
        searchAccountName:'',
        searchRealName:'',
        options1: [{
          value: '1',
          label: '是'
        }, {
          value: '0',
          label: '否'
        }],
        searchIsAble:'',
        searchIfChangePwd:'',
        searchUpdateTime:'',
        UserBillParameterStr:'',
          dialogFormVisible:false,
        form: {
          ID:0,
          AccountName:'',
          Password:"",
          RealName:'',
          MobilePhone:'',
          Email:'',
          Description:'',
          CreateBy:'',
          UpdateBy:'',
          IsAble:'1',
          IfChangePwd:'0'
        },
       selectedOptions:[],
       user:[],
       dialogTitle:'',
       dialogEditOrNew:'',
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
         
          ]
      
          
        },
        dialogSetUserRoleTitle:'',
        dialogSetUserRoleVisible:false,
        transferSetUserRoleData:[],
        transferSetUserRoleValue:[],

        dialogSetUserDeptTitle:'',
        dialogSetUserDeptVisible:false,
        transferSetUserDeptData:[],
        transferSetUserDeptValue:[]
        

  
      };
  
    },
    store,
  
  
    created() {
      this.user=this.$store.state.userInfo;
      this.init();
  
    },
  
  
  
    methods: {
     submitsetuserrole()
    {
         var _self = this;
         var roleStr='';
         if(this.transferSetUserRoleValue.length>0)
         {
           for(var i=0;i<this.transferSetUserRoleValue.length;i++)
           {
             if(i===this.transferSetUserRoleValue.length-1)
             {
                  roleStr=roleStr+this.transferSetUserRoleValue[i].toString();
             }
             else{
                   roleStr=roleStr+this.transferSetUserRoleValue[i].toString()+',';
             }
           }
         }

         this.$api.post("User/SetUserRole", {  Token:this.$store.state.userInfo.Token,userId:this.multipleSelection[0].ID ,roleStr:roleStr}, response => {

          if (response.status >= 200 && response.status < 300) {
  
            if (response.data) {
              var jsonData = eval("(" + response.data + ")");
  
              if (jsonData.Code == "1") {
  
                
    
                
                 this.$message({
                    message: '保存成功！',
                    type: 'success'
                  });

                  this.dialogSetUserRoleVisible=false;
                 this.init();
               
              } 
             
              
              else {
  
 
                this.$message.error(jsonData.Message);
                console.log(jsonData.Message);
  
  
  
              }
  
            }
  
  
  
          } else {
            this.$message.error(response.message);
  
  
          }
  
        });
    },
      submitsetuserdept()
    {
         var _self = this;
         var deptStr='';
         if(this.transferSetUserDeptValue.length>0)
         {
           for(var i=0;i<this.transferSetUserDeptValue.length;i++)
           {
             if(i===this.transferSetUserDeptValue.length-1)
             {
                  deptStr=deptStr+this.transferSetUserDeptValue[i].toString();
             }
             else{
                   deptStr=deptStr+this.transferSetUserDeptValue[i].toString()+',';
             }
           }
         }

         this.$api.post("User/SetUserDept", {  Token:this.$store.state.userInfo.Token,userId:this.multipleSelection[0].ID ,deptStr:deptStr}, response => {

          if (response.status >= 200 && response.status < 300) {
  
            if (response.data) {
              var jsonData = eval("(" + response.data + ")");
  
              if (jsonData.Code == "1") {
  
                
    
                
                 this.$message({
                    message: '保存成功！',
                    type: 'success'
                  });

                  this.dialogSetUserDeptVisible=false;
                 this.init();
               
              } 
             
              
              else {
  
 
                this.$message.error(jsonData.Message);
                console.log(jsonData.Message);
  
  
  
              }
  
            }
  
  
  
          } else {
            this.$message.error(response.message);
  
  
          }
  
        });
    },
     submitInfo()
      {
        
         
        var optUrl="User/AddUser";
        if( this.dialogEditOrNew==="0")
        {
            optUrl="User/AddUser";
              this.form.CreateBy=this.$store.state.userInfo.AccountName;

                let Base64 = jsbase64.Base64;
                var passwords = Base64.encode('123456');
                this.form.Password=passwords;
        }
        else{
            optUrl="User/UpdateUser";
            this.form.UpdateBy=this.$store.state.userInfo.AccountName;
        }

         var Jsonstr= JSON.stringify(this.form);

           var _self = this;
           this.$api.post(optUrl, {  Token:this.$store.state.userInfo.Token,str:Jsonstr}, response => {
        
          
    
  
          if (response.status >= 200 && response.status < 300) {
  
            if (response.data) {
              var jsonData = eval("(" + response.data + ")");
  
              if (jsonData.Code == "1") {
  
               this.$message({
                    message: '保存成功！',
                    type: 'success'
                  });

              this.dialogFormVisible=false;
  
              this.init();
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
          else if(formName==="transferSetUserRole")
          {
            
           this.submitsetuserrole();
          }
          else if(formName==="transferSetUserDept")
          {
            this.submitsetuserdept();
           
          }
      },
      closeForm(formName) {
         if(formName==="form")
          {
           this.dialogFormVisible=false;
         }
          else if(formName==="transferSetUserRole")
          {
            this.dialogSetUserRoleVisible=false;
           
          }
          else if(formName==="transferSetUserDept")
          {
            this.dialogSetUserDeptVisible=false;
           
          }
      },
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
          var _self = this;
        switch(btnMsg)
        {
          case 'search':
           this.search();
          break;
          case 'add':
           this.add();
          break;
           case 'edit':
           this.edit();
          break;
           case 'delete':
           this.delete();
          break;
          case 'setuserrole':
          this.setuserrole();
          break;
          case 'setuserdept':
          this.setuserdept();
          break;
          default:
          this.$message.error(btnMsg);
          break;
        }

       },
       search()
       {
        this.UserBillParameterStr='';
        var UserBillParameter=[];
       
         if (this.searchAccountName == null || this.searchAccountName == undefined || this.searchAccountName == "") {

         }
         else{
           var   parameter=new Object();
             parameter.action = "in";
             parameter.column = "AccountName";
             parameter.logic = "AND";
             parameter.value = this.searchAccountName;
             UserBillParameter.push(parameter);

         }

         if (this.searchRealName == null || this.searchRealName == undefined || this.searchRealName == "") {

         }
         else{
            var parameter=new Object();
             parameter.action = "in";
             parameter.column = "RealName";
             parameter.logic = "AND";
             parameter.value = this.searchRealName;
             UserBillParameter.push(parameter);

         }

      if (this.searchIsAble == null || this.searchIsAble == undefined || this.searchIsAble == "") {

         }
         else{
         
          
           var  parameter=new Object();
             parameter.action = "=";
             parameter.column = "IsAble";
             parameter.logic = "AND";
             parameter.value = this.searchIsAble;
             UserBillParameter.push(parameter);

         }

       if (this.searchIfChangePwd == null || this.searchIfChangePwd == undefined || this.searchIfChangePwd == "") {

         }
         else{
          
            var parameter=new Object();
             parameter.action = "=";
             parameter.column = "IfChangePwd";
             parameter.logic = "AND";
             parameter.value = this.searchIfChangePwd;
             UserBillParameter.push(parameter);

         }

      if (this.searchUpdateTime == null || this.searchUpdateTime == undefined || this.searchUpdateTime == "") {

         }
         else{
           var  parameter=new Object();
             parameter.action = ">=";
             parameter.column = "UpdateTime";
             parameter.logic = "AND";
             parameter.value = this.searchUpdateTime[0].toString();
             UserBillParameter.push(parameter);

            var  parameter=new Object();
             parameter.action = "<=";
             parameter.column = "UpdateTime";
             parameter.logic = "AND";
             parameter.value = this.searchUpdateTime[1].toString();
             UserBillParameter.push(parameter);

         }
        this.UserBillParameterStr=JSON.stringify(UserBillParameter);
        this.init();
       },
       add()
       {

          var _self = this;
          this.dialogTitle="新增用户信息";
          this.dialogEditOrNew="0";
          this.form= {
          ID:0,
          AccountName:'',
          Password:"",
          RealName:'',
          MobilePhone:'',
          Email:'',
          Description:'',
          CreateBy:'',
          UpdateBy:'',
          IsAble:'1',
          IfChangePwd:'0'
        }



         this.dialogFormVisible=true;
       },
         edit()
       {
          if(!this.multipleSelection|| this.multipleSelection.length===0)
          {
            this.$message.error("请选择要修改的数据！");
            return;
          }
          if(this.multipleSelection.length>1)
          {
              this.$message.error("请只选择一条要修改的数据！");
              return;
          }
           this.dialogTitle="修改用户信息";
           this.dialogEditOrNew="1";
           var _self = this;
           this.$api.post("User/GetUserByID", {  
              Token:this.$store.state.userInfo.Token,
              Id: this.multipleSelection[0].ID}, response => {
      
          if (response.status >= 200 && response.status < 300) {
  
            if (response.data) {
              var jsonData = eval("(" + response.data + ")");
  
              if (jsonData.Code == "1") {
  
                if (jsonData.Data) {
    
                var tempdata= eval("(" + jsonData.Data+ ")");
                  _self.form= tempdata;
                   _self.form= {
                    ID:tempdata.ID,
                    AccountName:tempdata.AccountName,
                    Password:tempdata.Password,
                    RealName:tempdata.RealName,
                    MobilePhone:tempdata.MobilePhone,
                    Email:tempdata.Email,
                    Description:tempdata.Description,
                    CreateBy:tempdata.CreateBy,
                    UpdateBy:tempdata.UpdateBy,
                    IsAble:tempdata.IsAble.toString(),
                    IfChangePwd:tempdata.IfChangePwd.toString()
            }
                
                 _self.dialogTitle="修改-"+this.multipleSelection[0].RealName+"-用户信息"
                 _self.dialogFormVisible=true;
                 
                }

  
  
  
              } else {
  
 
                _self.$message.error(jsonData.Message);
                console.log(jsonData.Message);
  
  
  
              }
  
            }
  
  
  
          } else {
            _self.$message.error(response.message);
  
  
  
           // console.log(response.message);
  
          }
  
        });



         this.dialogFormVisible=true;
       },
      delete()
       {
         if(!this.multipleSelection|| this.multipleSelection.length===0)
         {
           this.$message.error("请选择要删除的数据！");
           return;
         }
        var _self = this;
           this.$confirm('此操作将永久删除数据, 是否继续?', '提示', {
          confirmButtonText: '确定',
          cancelButtonText: '取消',
          type: 'warning'
        }).then(() => {


         var str='';
        var maxid=this.multipleSelection.length-1;
         for(var i=0;i< this.multipleSelection.length;i++)
         {
          
           if(i===maxid)
           {
           str=str+this.multipleSelection[i].ID.toString();
           }
           else{
                 str=str+this.multipleSelection[i].ID.toString()+',';
           }
          

         }
           _self.loading=true;
           this.$api.post("User/DeleteUser", {  Token:this.$store.state.userInfo.Token,str:str}, response => {
           _self.loading=false;
         
  
         
  
          if (response.status >= 200 && response.status < 300) {
  
            if (response.data) {
              var jsonData = eval("(" + response.data + ")");
  
              if (jsonData.Code == "1") {
  
                 this.$message({
                  type: 'success',
                  message: '删除成功!'
                });
              this.init();
  
  
  
              } else {
  
 
                _self.$message.error(jsonData.Message);
                console.log(jsonData.Message);
  
  
  
              }
  
            }
  
  
  
          } else {
            _self.$message.error(response.message);
  
  
  
       
  
          }
  
        });


        
        }).catch(() => {
          this.$message({
            type: 'info',
            message: '已取消删除'
          });          
        });


       },
      setuserrole(){
         if(!this.multipleSelection|| this.multipleSelection.length===0)
         {
           this.$message.error("请选择要赋权的用户！");
           return;
         }
        if(this.multipleSelection.length>1)
        {
            this.$message.error("请只选择一个用户！");
            return;
        }

        var _self = this;
        _self.dialogSetUserRoleTitle="修改-"+this.multipleSelection[0].RealName+"-角色信息"
         this.$api.post("User/GetRoleByUserIdForTransfer", {  Token:this.$store.state.userInfo.Token,userId:this.multipleSelection[0].ID }, response => {

          if (response.status >= 200 && response.status < 300) {
  
            if (response.data) {
              var jsonData = eval("(" + response.data + ")");
  
              if (jsonData.Code == "1") {
  
                if (jsonData.Data) {
    
                
                  var sp=jsonData.Data.split(',');
                 
                   _self.transferSetUserRoleValue=sp;
               
               
                 
                }

  
  
  
              } 
              else if (jsonData.Code == "0") {
                _self.transferSetUserRoleValue=[];
              }
              
              else {
  
 
                _self.$message.error(jsonData.Message);
                console.log(jsonData.Message);
  
  
  
              }
  
            }
  
  
  
          } else {
            _self.$message.error(response.message);
  
  
  
           // console.log(response.message);
  
          }
  
        });


          this.$api.post("User/GetAllRoleForTransfer", {  Token:this.$store.state.userInfo.Token }, response => {

          if (response.status >= 200 && response.status < 300) {
  
            if (response.data) {
              var jsonData = eval("(" + response.data + ")");
  
              if (jsonData.Code == "1") {
  
                if (jsonData.Data) {
    
                 var tempdata= eval("(" + jsonData.Data+ ")");
                  _self.transferSetUserRoleData=tempdata;

                 
                
               
                  _self.dialogSetUserRoleVisible=true;
                 
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
      setuserdept(){
           if(!this.multipleSelection|| this.multipleSelection.length===0)
         {
           this.$message.error("请选择要赋权的用户！");
           return;
         }
        if(this.multipleSelection.length>1)
        {
            this.$message.error("请只选择一个用户！");
            return;
        }

        var _self = this;
        _self.dialogSetUserDeptTitle="修改-"+this.multipleSelection[0].RealName+"-部门信息"
         this.$api.post("User/GetDeptByUserIdForTransfer", {  Token:this.$store.state.userInfo.Token,userId:this.multipleSelection[0].ID }, response => {

          if (response.status >= 200 && response.status < 300) {
  
            if (response.data) {
              var jsonData = eval("(" + response.data + ")");
  
              if (jsonData.Code == "1") {
  
                if (jsonData.Data) {
    
                
                  var sp=jsonData.Data.split(',');
                 
                   _self.transferSetUserDeptValue=sp;
               
               
                 
                }

  
  
  
              } 
              else if (jsonData.Code == "0") {
                _self.transferSetUserDeptValue=[];
              }
              
              else {
  
 
                _self.$message.error(jsonData.Message);
                console.log(jsonData.Message);
  
  
  
              }
  
            }
  
  
  
          } else {
            _self.$message.error(response.message);
  
  
  
           // console.log(response.message);
  
          }
  
        });


          this.$api.post("User/GetAllDeptForTransfer", {  Token:this.$store.state.userInfo.Token }, response => {

          if (response.status >= 200 && response.status < 300) {
  
            if (response.data) {
              var jsonData = eval("(" + response.data + ")");
  
              if (jsonData.Code == "1") {
  
                if (jsonData.Data) {
    
                 var tempdata= eval("(" + jsonData.Data+ ")");
                  _self.transferSetUserDeptData=tempdata;

                 
                
               
                  _self.dialogSetUserDeptVisible=true;
                 
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
      init() {
  
        var _self = this;
  
  
  
        _self.tableData = [];
  
  
         _self.loading=true;
         this.$api.post("User/GetAllUserViewInfo", {
          Token:this.$store.state.userInfo.Token,
          ParameterStr: this.UserBillParameterStr,
          PageSize:this.currentpagesize,
          CurrentPage:this.currentPage

         }, response => {
         _self.loading=false;
     
  
  
  
          if (response.status >= 200 && response.status < 300) {
  
           // console.log(response.data);
  
            if (response.data) {
  
              var jsonData = eval("(" + response.data + ")");
  
              if (jsonData.Code === "1") {
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
 
  
              }
  
            }
  
          } else {
  
          
  
            _self.$message.error(response.message);
  
       
  
          }
  
        });
  
      }
  
    },
      watch:
    {
        dialogFormVisible(val) 
        {
            this.loading=val;
        },
        dialogSetUserRoleVisible(val) 
        {
            this.loading=val;
        },
        dialogSetUserDeptVisible(val)
        {
           this.loading=val;
        }
    }  ,
    components: {
             'v-btn-group':btnGroup
    } 
          
  
  };
</script>
<style>
  
</style>
