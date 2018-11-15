<template>
<div>
  <el-collapse accordion id="searchDiv" >
  <el-collapse-item>
    <template slot="title" id="searchtitle"  >
     <i class="el-icon-search searchicon" ></i>
    </template>
  <div>
    <el-form :inline="true"  class="searchform">
    <el-form-item label="通知单号：" class="searchinput">
    <el-input v-model="searchNo" placeholder="请输入通知单号查询条件" class="searchinput"  size="mini"   type="text" clearable></el-input>
  </el-form-item>

    <el-form-item label="状态：" class="searchinput">
   <el-select v-model="searchstatus" filterable placeholder="" size="mini" clearable >
        <el-option
          v-for="item in statusOptions1"
          :key="item.value"
          :label="item.label"
          :value="item.value" size="mini" >
         </el-option>
          </el-select>
  </el-form-item>

   <el-form-item label="创建日期：" class="searchinput">
    <el-date-picker size="mini" 
      v-model="searchCreateTime"
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
    <el-table-column prop="No" label="通知单号"  width="150px" fixed>
    </el-table-column>
      <el-table-column prop="status" label="状态"   width="100px"  fixed>
        <template slot-scope="scope">
          <el-select v-model="scope.row.status" filterable placeholder="" size="mini" :disabled="true">
        <el-option
          v-for="item in statusOptions"
          :key="item.value"
          :label="item.label"
          :value="item.value" size="mini" >
         </el-option>
          </el-select>
       </template>

    </el-table-column>
    <el-table-column prop="ftypeid" label="通知类型"   width="120px"  fixed>
        <template slot-scope="scope">
          <el-select v-model="scope.row.ftypeid" filterable placeholder="" size="mini" :disabled="true">
        <el-option
          v-for="item in ftypeOptions"
          :key="item.value"
          :label="item.label"
          :value="item.value" size="mini" >
         </el-option>
          </el-select>
       </template>

    </el-table-column>
    <el-table-column prop="ftitle" label="通知主题"   >
    </el-table-column>
    <el-table-column prop="CreateTime" label="创建时间" >
    </el-table-column>  
     <el-table-column prop="SendBeginTime" label="通知发送时间" >
    </el-table-column>  
     <el-table-column prop="SendEndTime" label="通知结束时间" >
    </el-table-column>  
    <el-table-column prop="SendCount" label="已通知数"   >
    </el-table-column>
    <el-table-column prop="Count" label="通知总量"   >
    </el-table-column>
    <el-table-column
      fixed="right"
      label="操作"
      width="130">
      <template slot-scope="scope">

        <el-button  @click="handleLookClick(scope.row)"  type="text" size="mini">查看</el-button>
       
      </template>
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

  <v-notice-edit :id="dialogid" :name.sync="dialogTitle" :dialogVisible.sync="dialogFormVisible" :dialogEditOrNew.sync="dialogEditOrNew"  @dialogClose="dialogClose" ></v-notice-edit>
</div> 
</template>
<script>
import btnGroup from '@/components/btnGroup';
import store from '../store/store.js';
import NoticeEdit from '@/components/NoticeBill/NoticeEdit'
  export default {
    props: ['menuname','menuid','menutitle'],
    data() {
  
      return {
        user:new Object(),
        tableData: [],
        searchNo:"",
        searchstatus:"",
        searchCreateTime:'',
        loading:false,
        multipleSelection: [],
        currentPage:1,
        currentpagesize:10,
        totalCount:400,
        dialogFormVisible:false,
        ftypeOptions:[{
          value: 0,
          label: '系统通知'
        }, {
          value: 1,
          label: '消息通知'
        }],
         statusOptions1:[{
          value: '0',
          label: '录入'
        }, {
          value: '5',
          label: '审核'
        },
         {
          value: '-1',
          label: '作废'
        }],
         statusOptions:[{
          value: 0,
          label: '录入'
        }, {
          value: 5,
          label: '审核'
        },
         {
          value: -1,
          label: '作废'
        }],
       selectedOptions:[],
       dialogTitle:'',
       dialogEditOrNew:'',
       dialogid:'',
       BillParameterStr:''
      
      };
  
    },
    
    store,
  
  
    created() {
      this.user=this.$store.state.userInfo;
      this.init();
    
    
    },
  
  
  
    methods: {
      handleLookClick(row) {
        console.log(row);
          var _self = this;
          this.dialogTitle="查看通知";
          this.dialogEditOrNew="2";    
          this.dialogid=row.Code;
          this.dialogFormVisible=true;
      },
    
    dialogClose()
    {
   
    this.init();
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
        console.log(this.multipleSelection[0].Address);
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
          this.dialogTitle="新增通知";
          this.dialogEditOrNew="0";    
          this.dialogid='';
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
           this.dialogTitle="修改通知";
           this.dialogEditOrNew="1";
           this.dialogid=this.multipleSelection[0].Code;
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
           str=str+this.multipleSelection[i].Code;
           }
           else{
                 str=str+this.multipleSelection[i].Code+',';
           }
          

         }
           this.loading=true;
           this.$api.post("Sys/DeleteNotice", {  Token:this.$store.state.userInfo.Token,str:str}, response => {
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
        this.BillParameterStr='';
        var BillParameter=[];
       
         if (this.searchNo == null || this.searchNo == undefined || this.searchNo == "") {

         }
         else{
           var   parameter=new Object();
             parameter.action = "in";
             parameter.column = "No";
             parameter.logic = "AND";
             parameter.value = this.searchNo;
             BillParameter.push(parameter);

         }
         
      if (this.searchCreateTime == null || this.searchCreateTime == undefined || this.searchCreateTime == "") {

         }
         else{
           var  parameter=new Object();
             parameter.action = ">=";
             parameter.column = "CreateTime";
             parameter.logic = "AND";
             parameter.value = this.searchCreateTime[0].toString();
             BillParameter.push(parameter);

            var  parameter=new Object();
             parameter.action = "<=";
             parameter.column = "CreateTime";
             parameter.logic = "AND";
             parameter.value = this.searchCreateTime[1].toString();
             BillParameter.push(parameter);

         }

          if (this.searchstatus == null || this.searchstatus == undefined || this.searchstatus == "") {
              console.log(this.searchstatus);
         }
         else{
          
            var parameter=new Object();
             parameter.action = "=";
             parameter.column = "status";
             parameter.logic = "AND";
             parameter.value = this.searchstatus;
             BillParameter.push(parameter);

         }


        this.BillParameterStr=JSON.stringify(BillParameter);
        this.init();
       },
      init() {
  
        var _self = this;
        _self.tableData = [];
        _self.loading=true;
  
        this.$api.post("Sys/GetAllNoticeInfo",{
          Token:this.$store.state.userInfo.Token,
          ParameterStr: this.BillParameterStr,
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
 
    },
      watch:
    {
        dialogFormVisible(val) 
        {
            this.loading=val;
        }
    }  ,
    
    filters: {
              ftypeidfilters: function (value) {
                var returnvalue='';
                        switch(value)
                        {
                            case 0:
                            returnvalue='系统通知';
                            break;
                            case 1:
                            returnvalue='消息通知';
                            break;
                            default:
                            returnvalue='';
                            break;
                            

                        };
                return returnvalue;
              }
                   
                },
    components: {
             'v-btn-group':btnGroup,
             'v-notice-edit':NoticeEdit
    }
  
  };
</script>