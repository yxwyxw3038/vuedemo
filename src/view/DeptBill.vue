<template>
<div>
  <el-collapse accordion id="searchDiv" >
  <el-collapse-item>
    <template slot="title" id="searchtitle"  >
     <i class="el-icon-search searchicon" ></i>
    </template>
  <div>
    <el-form :inline="true"  class="searchform">
    <el-form-item label="部门名称：" class="searchinput">
    <el-input v-model="searchDeptName" placeholder="请输入部门查询条件" class="searchinput"  size="mini"   type="text" clearable></el-input>
  </el-form-item>
  <el-form-item label="上级部门：" class="searchinput">
    <el-input v-model="searchParentId" placeholder="请输入上级部门ID查询条件" class="searchinput"  size="mini"   type="text" clearable></el-input>
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
      type="selection"
      width="55px" fixed>
  </el-table-column>
  <el-table-column
      type="index"
       width="50"
       :index="indexMethod"
        label="序号" 
     fixed >
    </el-table-column>
    <el-table-column prop="Id" label="部门ID"  width="100px" fixed>
  
  
  
    </el-table-column>
  
  
  
    <el-table-column prop="DepartmentName" label="部门名称"   width="250px"  fixed>
  
  
  
    </el-table-column>
  
     <el-table-column prop="ParentId" label="上级主管部门"   >
  
  
  
    </el-table-column>
  
    <el-table-column prop="Sort" label="排序"   >
  
  
  
    </el-table-column>

  
    <el-table-column prop="UpdateTime" label="修改时间" >
  
  
  
    </el-table-column>
  
  
  
    <el-table-column prop="UpdateBy" label="修改人" >
  
  
  
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
    <el-form-item label="部门名称："  prop="DepartmentName">
        <el-col :span="8">
     <el-input v-model="form.DepartmentName" ></el-input>
      </el-col>
    </el-form-item>
    <el-form-item label="主管部门：" prop="ParentId" >
   <el-col :span="8">
   <el-cascader 
  :options="dept"
  :show-all-levels="false"
  change-on-select
   v-model="selectedOptions"
   ></el-cascader>
   </el-col>
    </el-form-item>
    <el-form-item label="排序:" prop="Sort">
       <el-col :span="8">
     <el-input v-model="form.Sort"  type="number" min="0" ></el-input>
       </el-col>
    </el-form-item>
  </el-form>
  <div slot="footer" class="dialog-footer">
    <el-button @click="closeForm('form')">取 消</el-button>
    <el-button type="primary" @click="submitForm('form')">确 定</el-button>
  </div>
</el-dialog>
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
        searchDeptName:"",
        searchUpdateTime:'',
        searchParentId:"",
        loading:false,
        multipleSelection: [],
        currentPage:1,
        currentpagesize:10,
        totalCount:400,
        dialogFormVisible:false,
        form: {
         Id:0,
         DepartmentName:'',
         ParentId:0,
         Sort:0,
         CreateTime:'',
         CreateBy:'',
         UpdateTime:'',
         UpdateBy:''
        },
       selectedOptions:[],
       dialogTitle:'',
       dialogEditOrNew:'',
       deptBillParameterStr:'',
       rules: {
          DepartmentName: [
            { required: true, message: '请输入部门名称', trigger: 'blur' }
         
          ],
          ParentId: [
            { required: true, message: '请选择上级主管部门', trigger: 'blur' }
         
          ]
          
          
        },
        dept:[]
      };
  
    },
    
    store,
  
  
    created() {
      this.user=this.$store.state.userInfo;
      this.init();
      this.GetDept();
    
    },
  
  
  
    methods: {

      submitInfo()
      {
         if( this.selectedOptions.length>0)
         {
        
             this.form.ParentId=Number(this.selectedOptions[this.selectedOptions.length-1]);
         }
        
       var optUrl="User/AddDept";
       if( this.dialogEditOrNew==="0")
       {
           optUrl="User/AddDept";
            this.form.CreateBy=this.$store.state.userInfo.AccountName;
       }
       else{
           optUrl="User/UpdateDept";
           this.form.UpdateBy=this.$store.state.userInfo.AccountName;
       }
         var Jsonstr= JSON.stringify(this.form);

           var _self = this;
           this.loading=true;
           this.$api.post(optUrl, {  Token:this.$store.state.userInfo.Token,str:Jsonstr}, response => {
           this.loading=false;
          
    
  
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
  
  
  
           // console.log(response.message);
  
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
          else if(formName==="transfer")
          {
        
           
          }
      },
      closeForm(formName) {
         if(formName==="form")
          {
        this.dialogFormVisible=false;
         }
          else if(formName==="transfer")
          {
        
          }
      },
      GetDept()
      {
        var _self = this;
           this.$api.post("User/GetCascaderDept", {  Token:this.$store.state.userInfo.Token}, response => {
        
           _self.dept=[];
  
  
          if (response.status >= 200 && response.status < 300) {
  
            if (response.data) {
              var jsonData = eval("(" + response.data + ")");
  
              if (jsonData.Code == "1") {
  
                if (jsonData.Data) {
    
              
                  var info=JSON.parse(jsonData.Data);
                  _self.dept=info;
              

                 
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
          case 'add':
           this.add();
          break;
           case 'edit':
           this.edit();
          break;
           case 'delete':
           this.delete();
          break;
          default:
          this.$message.error(btnMsg);
          break;
        }

       },
       add()
       {

          var _self = this;
          this.dialogTitle="新增部门";
          this.dialogEditOrNew="0";
          
          this.$api.post("User/GetDeptAllCount", {  Token:this.$store.state.userInfo.Token}, response => {
        
         
  
         
  
          if (response.status >= 200 && response.status < 300) {
  
            if (response.data) {
              var jsonData = eval("(" + response.data + ")");
  
              if (jsonData.Code == "1") {
  
                if (jsonData.Data) {
              
                  _self.form= {
                    Id:0,
                    DepartmentName:'',
                    ParentId:0,
                    Sort:Number(jsonData.Data)+1,
                    CreateTime:'',
                    CreateBy:'',
                    UpdateTime:'',
                    UpdateBy:''
                  };
                }
              } else {
                _self.$message.error(jsonData.Message);
                console.log(jsonData.Message);
              }
            }
          } else {
            this.$message.error(response.message);
          } 
        });
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
           this.dialogTitle="修改部门信息";
           this.dialogEditOrNew="1";
          var _self = this;
           this.$api.post("User/GetDeptById", {  
              Token:this.$store.state.userInfo.Token,
              Id: this.multipleSelection[0].Id}, response => {
        
         
  
         
  
          if (response.status >= 200 && response.status < 300) {
  
            if (response.data) {
              var jsonData = eval("(" + response.data + ")");
  
              if (jsonData.Code == "1") {
  
                if (jsonData.Data) {
    
                var tempdata= eval("(" + jsonData.Data+ ")");
                  _self.form= tempdata;
                    if(_self.dept&&_self.dept.length>0)
                  {
                   var returnstr=   _self.GetSelectParentId(_self.dept,tempdata.ParentId.toString());
                   if(returnstr==="-1")
                   {
                      _self.selectedOptions=[0];
                   }
                   else{
                    var list=returnstr.split(',');
                    _self.selectedOptions=list;
                   }
                }
             
                 _self.dialogTitle="修改-"+this.multipleSelection[0].DepartmentName+"-部门信息"
                 _self.dialogFormVisible=true;
                 
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
           str=str+this.multipleSelection[i].Id.toString();
           }
           else{
                 str=str+this.multipleSelection[i].Id.toString()+',';
           }
          

         }
           this.loading=true;
           this.$api.post("User/DeleteDept", {  Token:this.$store.state.userInfo.Token,str:str}, response => {
           this.loading=false;
         
  
         
  
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
     
       search()
       {
        this.deptBillParameterStr='';
        var DeptBillParameter=[];
       
         if (this.searchDeptName == null || this.searchDeptName == undefined || this.searchDeptName == "") {

         }
         else{
           var   parameter=new Object();
             parameter.action = "in";
             parameter.column = "DepartmentName";
             parameter.logic = "AND";
             parameter.value = this.searchDeptName;
             DeptBillParameter.push(parameter);

         }
      if (this.searchParentId == null || this.searchParentId == undefined || this.searchParentId == "") {

         }
         else{
           var   parameter=new Object();
             parameter.action = "=";
             parameter.column = "ParentId";
             parameter.logic = "AND";
             parameter.value = this.searchParentId;
             DeptBillParameter.push(parameter);

         }
      if (this.searchUpdateTime == null || this.searchUpdateTime == undefined || this.searchUpdateTime == "") {

         }
         else{
           var  parameter=new Object();
             parameter.action = ">=";
             parameter.column = "UpdateTime";
             parameter.logic = "AND";
             parameter.value = this.searchUpdateTime[0].toString();
             DeptBillParameter.push(parameter);

            var  parameter=new Object();
             parameter.action = "<=";
             parameter.column = "UpdateTime";
             parameter.logic = "AND";
             parameter.value = this.searchUpdateTime[1].toString();
             DeptBillParameter.push(parameter);

         }
        this.deptBillParameterStr=JSON.stringify(DeptBillParameter);
        this.init();
       },
      init() {
  
        var _self = this;
        _self.tableData = [];
        _self.loading=true;
  
        this.$api.post("User/GetAllDeptInfo",{
          Token:this.$store.state.userInfo.Token,
          ParameterStr: this.deptBillParameterStr,
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
  
      },
            GetSelectParentId(list,ParentId)
      {

          for(var i=0;i<list.length;i++)
          {
                if(list[i].value===ParentId)
                {
                  return list[i].value;
                }
                if(list[i].children)
                {
                if(list[i].children.length>0)
                {
                  var returnStr=this.GetSelectParentId(list[i].children,ParentId);
                  if( returnStr==="-1")
                  {

                  }
                  else{

                  return  list[i].value+','+returnStr;
                  }
                }
                }
          }
          return "-1";
      }  
    },
    watch:
    {
        dialogFormVisible(val) 
        {
            this.loading=val;
        }
    },
    components: {
             'v-btn-group':btnGroup
         
    }
  
  };
</script>
<style>
 
</style>