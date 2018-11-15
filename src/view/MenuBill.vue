<template>
<div>
  <el-collapse accordion id="searchDiv" >
  <el-collapse-item>
    <template slot="title" id="searchtitle"  >
     <i class="el-icon-search searchicon" ></i>
    </template>
  <div>
    <el-form :inline="true"  class="searchform">
    <el-form-item label="菜单名称：" class="searchinput">
    <el-input v-model="searchMenuName" placeholder="请输入菜单查询条件" class="searchinput"  size="mini"   type="text" clearable></el-input>
  </el-form-item>
   <el-form-item label="上级节点：" class="searchinput">
    <el-input v-model="searchParentId" placeholder="请输入上级节点ID查询条件" class="searchinput"  size="mini"   type="text" clearable></el-input>
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
    <el-table-column prop="Id" label="菜单ID"  width="100px" fixed>
  
  
  
    </el-table-column>
  
  
  
    <el-table-column prop="Name" label="菜单名称"   width="150px"  fixed>
  
  
  
    </el-table-column>
  
  
  
    <el-table-column prop="ParentId" label="父节点ID" >
  
  
  
    </el-table-column>
  
  
  
    <el-table-column prop="Code" label="标识码" >
  
  
  
    </el-table-column>
  
  
  
  
  
    <el-table-column prop="LinkAddress" label="链接地址" >
  
  
  
    </el-table-column>
  
  
  
    <el-table-column prop="Icon" label="图标" >
  
  
  
    </el-table-column>
  
  
      <el-table-column prop="Sort" label="排序" >
  
  
  
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
    <el-form-item label="菜单名称："  prop="Name">
        <el-col :span="8">
     <el-input v-model="form.Name" ></el-input>
      </el-col>
    </el-form-item>
     <el-form-item label=" 父节点：" prop="ParentId" >
         <el-col :span="8">
   <el-cascader 
  :options="menu"
  :show-all-levels="false"
  change-on-select
   v-model="selectedOptions"
></el-cascader>
         </el-col>
    </el-form-item>
     <el-form-item label="标识码：" prop="Code" >
       <el-col :span="8">
     <el-input v-model="form.Code" ></el-input>
       </el-col>
    </el-form-item>
     
      <el-form-item label="链接地址:" >
         <el-col :span="16">
     <el-input v-model="form.LinkAddress" ></el-input>
         </el-col>
    </el-form-item>
     <el-form-item label="图标:" prop="Icon" >
       <el-col :span="8">
     <el-input v-model="form.Icon" ></el-input>
       </el-col>
     
    </el-form-item>
    <el-form-item label="排序:" >
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
  <el-dialog :title="dialogSetMenuButtonTitle" :visible.sync="dialogSetMenuButtonVisible" >
    <el-transfer v-model="transferSetMenuButtonValue" :data="transferSetMenuButtonData"
     :titles="['未赋权', '已赋权']"
      :button-texts="['删除', '添加']"
      ></el-transfer>
     <div slot="footer" class="dialog-footer">
    <el-button @click="closeForm('transfer')">取 消</el-button>
    <el-button type="primary" @click="submitForm('transfer')">确 定</el-button>
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
        searchMenuName:"",
        searchParentId:"",
        searchUpdateTime:'',
        searchMenuName:"",
        menuBillParameterStr:'',
        date1:'',
        loading:false,
        multipleSelection: [],
        currentPage:1,
        currentpagesize:10,
        totalCount:400,
        dialogFormVisible:false,
        form: {
          ID:0,
          Name:'',
          ParentId:"0",
          Code:'',
          LinkAddress:'',
          Icon:'',
          Sort:'',
          CreateBy:''
        },
      selectedOptions:[],
       menu:[],
       dialogTitle:'',
       dialogEditOrNew:'',
       rules: {
          Name: [
            { required: true, message: '请输入菜单名称', trigger: 'blur' }
         
          ],
           ParentId: [
            { required: true, message: '请选择父节点', trigger: 'blur' }
         
          ] ,
           Code: [
            { required: true, message: '请选择标识码', trigger: 'blur' }
         
          ],
           Icon: [
            { required: true, message: '请选择图标', trigger: 'blur' }
         
          ]
      
          
        },
        dialogSetMenuButtonTitle:'',
        dialogSetMenuButtonVisible:false,
        transferSetMenuButtonData:[],
        transferSetMenuButtonValue:[]
      };
  
    },
    
    store,
  
  
    created() {
      this.user=this.$store.state.userInfo;
      this.init();
      this.GetMenu();
    
    },
  
  
  
    methods: {
    submitsetMenuButton()
    {
         var _self = this;
         var buttonStr='';
         if(this.transferSetMenuButtonValue.length>0)
         {
           for(var i=0;i<this.transferSetMenuButtonValue.length;i++)
           {
             if(i===this.transferSetMenuButtonValue.length-1)
             {
                  buttonStr=buttonStr+this.transferSetMenuButtonValue[i].toString();
             }
             else{
                   buttonStr=buttonStr+this.transferSetMenuButtonValue[i].toString()+',';
             }
           }
         }
         this.loading=true;
        // buttonStr=
         this.$api.post("User/SetMenuButton", {  Token:this.$store.state.userInfo.Token,menuId:this.multipleSelection[0].Id ,buttonStr:buttonStr}, response => {
         this.loading=false;
          if (response.status >= 200 && response.status < 300) {
  
            if (response.data) {
              var jsonData = eval("(" + response.data + ")");
  
              if (jsonData.Code == "1") {
  
                
    
                
                 this.$message({
                    message: '保存成功！',
                    type: 'success'
                  });

                  this.dialogSetMenuButtonVisible=false;
               
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
        
         if( this.selectedOptions.length>0)
         {
        
             this.form.ParentId=Number(this.selectedOptions[this.selectedOptions.length-1]);
         }
       var optUrl="User/AddMenu";
       if( this.dialogEditOrNew==="0")
       {
           optUrl="User/AddMenu";
            this.form.CreateBy=this.$store.state.userInfo.AccountName;
       }
       else{
           optUrl="User/UpdateMenu";
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
            this.submitsetMenuButton();
           
          }
      },
      closeForm(formName) {
         if(formName==="form")
          {
        this.dialogFormVisible=false;
         }
          else if(formName==="transfer")
          {
            this.dialogSetMenuButtonVisible=false;
          }
      },
      GetMenu()
      {
        var _self = this;
        this.loading=true;
        this.$api.post("User/GetCascaderMenuo", {  Token:this.$store.state.userInfo.Token}, response => {
        this.loading=false;
           _self.menu=[];
  
         // console.log(response.status);
  
          if (response.status >= 200 && response.status < 300) {
  
            if (response.data) {
              var jsonData = eval("(" + response.data + ")");
  
              if (jsonData.Code == "1") {
  
                if (jsonData.Data) {
    
              
                  var info=JSON.parse(jsonData.Data);
                  _self.menu=info;
                //  console.log( _self.menu[0].Id);

                 
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
          case 'setmenubutton':
          this.setmenubutton();
          break;
          default:
          this.$message.error(btnMsg);
          break;
        }

       },
       add()
       {

          var _self = this;
          this.dialogTitle="新增菜单";
          this.dialogEditOrNew="0";
          this.loading=true;
          this.$api.post("User/GetMenuAllCount", {  Token:this.$store.state.userInfo.Token}, response => {
          this.loading=false;
         
  
         
  
          if (response.status >= 200 && response.status < 300) {
  
            if (response.data) {
              var jsonData = eval("(" + response.data + ")");
  
              if (jsonData.Code == "1") {
  
                if (jsonData.Data) {
    
              
                  _self.form= {
                    Id:0,
                    Name:'',
                    ParentId:"0",
                    Code:'',
                    LinkAddress:'',
                    Icon:'',
                    Sort:   Number(jsonData.Data)+1,
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
  
  
  
           // console.log(response.message);
  
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
           this.dialogTitle="修改菜单";
           this.dialogEditOrNew="1";
           var _self = this;
           this.loading=true;
           this.$api.post("User/GetMenuById", {  
              Token:this.$store.state.userInfo.Token,
              Id: this.multipleSelection[0].Id}, response => {
           this.loading=false;
         
  
         
  
          if (response.status >= 200 && response.status < 300) {
  
            if (response.data) {
              var jsonData = eval("(" + response.data + ")");
  
              if (jsonData.Code == "1") {
  
                if (jsonData.Data) {
    
                var tempdata= eval("(" + jsonData.Data+ ")");
                  _self.form= tempdata;

                  if(_self.menu&&_self.menu.length>0)
                  {
                   var returnstr=   _self.GetSelectParentId(_self.menu,tempdata.ParentId.toString());
                   if(returnstr==="-1")
                   {
                      _self.selectedOptions=[0];
                   }
                   else{
                    var list=returnstr.split(',');
                    _self.selectedOptions=list;
                   }
                  // _self.selectedOptions=[tempdata.ParentId.toString()];
                  }
                 _self.dialogTitle="修改-"+this.multipleSelection[0].Name+"-菜单信息"
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
           str=str+this.multipleSelection[i].Id.toString();
           }
           else{
                 str=str+this.multipleSelection[i].Id.toString()+',';
           }
          

         }
           this.loading=true;
           this.$api.post("User/DeleteMenu", {  Token:this.$store.state.userInfo.Token,str:str}, response => {
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
      setmenubutton()
      {
         if(!this.multipleSelection|| this.multipleSelection.length===0)
         {
           this.$message.error("请选择要赋权的菜单！");
           return;
         }
        if(this.multipleSelection.length>1)
        {
            this.$message.error("请只选择一条要赋权的菜单！");
            return;
        }

        var _self = this;
        _self.dialogSetMenuButtonTitle="修改-"+this.multipleSelection[0].Name+"-按钮信息"
         this.$api.post("User/GetButtonByMenuIdnForTransfer", {  Token:this.$store.state.userInfo.Token,menuId:this.multipleSelection[0].Id }, response => {

          if (response.status >= 200 && response.status < 300) {
  
            if (response.data) {
              var jsonData = eval("(" + response.data + ")");
  
              if (jsonData.Code == "1") {
  
                if (jsonData.Data) {
    
                
                  var sp=jsonData.Data.split(',');
                 
                   _self.transferSetMenuButtonValue=sp;
               
               
                 
                }

  
  
  
              } 
              else if (jsonData.Code == "0") {
                _self.transferSetMenuButtonValue=[];
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


          this.$api.post("User/GetAllButtonForTransfer", {  Token:this.$store.state.userInfo.Token }, response => {

          if (response.status >= 200 && response.status < 300) {
  
            if (response.data) {
              var jsonData = eval("(" + response.data + ")");
  
              if (jsonData.Code == "1") {
  
                if (jsonData.Data) {
    
                 var tempdata= eval("(" + jsonData.Data+ ")");
                  _self.transferSetMenuButtonData=tempdata;

                 
                
               
                  _self.dialogSetMenuButtonVisible=true;
                 
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



      },
       search()
       {
        this.menuBillParameterStr='';
        var MenuBillParameter=[];
       
         if (this.searchMenuName == null || this.searchMenuName == undefined || this.searchMenuName == "") {

         }
         else{
           var   parameter=new Object();
             parameter.action = "in";
             parameter.column = "Name";
             parameter.logic = "AND";
             parameter.value = this.searchMenuName;
             MenuBillParameter.push(parameter);

         }
        if (this.searchParentId == null || this.searchParentId == undefined || this.searchParentId == "") {

         }
         else{
           var   parameter=new Object();
             parameter.action = "=";
             parameter.column = "ParentId";
             parameter.logic = "AND";
             parameter.value = this.searchParentId;
             MenuBillParameter.push(parameter);

         }
         
      if (this.searchUpdateTime == null || this.searchUpdateTime == undefined || this.searchUpdateTime == "") {

         }
         else{
           var  parameter=new Object();
             parameter.action = ">=";
             parameter.column = "UpdateTime";
             parameter.logic = "AND";
             parameter.value = this.searchUpdateTime[0].toString();
             MenuBillParameter.push(parameter);

            var  parameter=new Object();
             parameter.action = "<=";
             parameter.column = "UpdateTime";
             parameter.logic = "AND";
             parameter.value = this.searchUpdateTime[1].toString();
             MenuBillParameter.push(parameter);

         }
        this.menuBillParameterStr=JSON.stringify(MenuBillParameter);
        this.init();
       },
      init() {
  
        var _self = this;
  
  
  
        _self.tableData = [];
  
  _self.loading=true;
  
        this.$api.post("User/GetAllMenuInfo",{
          Token:this.$store.state.userInfo.Token,
          ParameterStr: this.menuBillParameterStr,
          PageSize:this.currentpagesize,
          CurrentPage:this.currentPage

        }, response => {
  
   
        _self.loading=false;
  
  
          if (response.status >= 200 && response.status < 300) {
  
          //  console.log(response.data);
  
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
    components: {
             'v-btn-group':btnGroup
    }
  
  };
</script>
<style>
 
</style>
