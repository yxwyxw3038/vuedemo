<template id="templateMain">
<div id="divMain">
    <el-row id="rowMain">
        <el-col id="coltree" :span="4">
            <el-tree
                :default-expand-all="tempexpand"
                ref="tree"
                :data="tempTreeData"
                node-key="id"
                @node-click="handleNodeClick"
                :props="defaultProps">
                </el-tree>
  
         </el-col>
          <el-col id="colmain" :span="20">
           <v-btn-group :userid="user.ID"  :menuid="this.menuid" @btnclick="mybtnclick"  :loading="loading"  ></v-btn-group> 
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
                <el-table-column prop="Code" label="代码"  width="200px" fixed>
                </el-table-column>
                <el-table-column prop="CodeName" label="代码名称"   width="400px"  fixed>
                </el-table-column>
                <el-table-column prop="CreateBy" label="创建人"   >
                </el-table-column>
                <el-table-column prop="CreateTime" label="创建时间" width="250px">
                </el-table-column>  
                  <el-table-column prop="UpdateBy" label="修改人"   >
                </el-table-column>
                <el-table-column prop="UpdateTime" label="修改时间" width="250px">
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
          </el-col>
     </el-row>
     <v-parameter-edit :id="dialogid" :name="dialogTitle" :dialogVisible.sync="dialogFormVisible" :dialogEditOrNew="dialogEditOrNew" :FatherCode="dialogFatherCode"  @dialogClose="dialogClose" ></v-parameter-edit>
</div>
</template>
<script>
import store from '../store/store.js';
import btnGroup from '@/components/btnGroup';
import ParameterEdit from '@/components/ParameterBill/ParameterEdit'
export default {
    props: ['menuname','menuid','menutitle'],
     data () {
    return {
        user:new Object(),
        loading:false,
        tableData: [],
        tempexpand:true,
        tempTreeData:[],
        multipleSelection: [],
        currentCode:'',
        currentPage:1,
        currentpagesize:10,
        totalCount:400,
        defaultProps: {
          children: 'children',
          label: 'label'
        },
         form: {
         id:0,
         Code:'',
         CodeName:'',
         FatherCode:'',
         CreateTime:'',
         CreateBy:'',
         UpdateTime:'',
         UpdateBy:''
        },
       dialogTitle:'',
       dialogEditOrNew:'',
       dialogFormVisible:false,
       dialogid:0,
       dialogFatherCode:''
    }
  } ,
  store,
  created() {
      this.user=this.$store.state.userInfo;
      this.init(); 
    },
  methods: {
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
        this.initItem();
      },
      handleCurrentChange(val) {
        console.log(`当前页: ${val}`);
        this.currentPage=val;
         this.initItem();
      },
      handleSelectionChange(val) {
        this.multipleSelection = val;
        if(this.multipleSelection&&this.multipleSelection.length>0)
        {
        console.log(this.multipleSelection[0].CodeName);
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
          this.dialogTitle="新增参数";
          this.dialogEditOrNew="0";    
          this.dialogid=-1;
          this.dialogFatherCode=this.currentCode;
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
           this.dialogTitle="修改参数";
           this.dialogEditOrNew="1";
           this.dialogid=this.multipleSelection[0].id;
           this.dialogFatherCode=this.currentCode;
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
           str=str+this.multipleSelection[i].id.toString();
           }
           else{
                 str=str+this.multipleSelection[i].id.toString()+',';
           }
          

         }
           _self=this;
           this.loading=true;
           this.$api.post("Sys/DeleteParameter", {  Token:this.$store.state.userInfo.Token,str:str}, response => {
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
        initItem() {
           var BillParameterStr='';
           var BillParameter=[];
             var   parameter=new Object();
             parameter.action = "=";
             parameter.column = "FatherCode";
             parameter.logic = "AND";
             parameter.value = this.currentCode;
             BillParameter.push(parameter);
             BillParameterStr=JSON.stringify(BillParameter);
            var _self = this;
            _self.tableData = [];
            _self.loading=true;
    
            this.$api.post("Sys/GetAllParameterInfo",{
            Token:this.$store.state.userInfo.Token,
            ParameterStr: BillParameterStr,
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
                   if(jsonData.DataCount!="0")
                   {

                   
                   
                    _self.$message.error(jsonData.Message);
    
                    console.log(jsonData.Message);
                   }
                }
    
                }
    
            } else {
    
            
    
                _self.$message.error(response.message);
    
                console.log(response.message);
    
            }
    
            });
     
        },
        init() {
        var _self = this;
        this.GetData();
        },
        GetData(){
                var _self = this;
                this.loading=true;
                this.$api.post("Sys/GetTreeParameter", {  Token:this.$store.state.userInfo.Token}, response => {
                _self.loading=false;
                _self.tempTreeData=[];
        
        
                if (response.status >= 200 && response.status < 300) {
        
                    if (response.data) {
                    var jsonData = eval("(" + response.data + ")");
        
                    if (jsonData.Code == "1") {
        
                        if (jsonData.Data) {
            
                    
                        var info=JSON.parse(jsonData.Data);
                        _self.tempTreeData=info;
                        _self.initItem();
                        
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

        
                }
        
                });
            },
            handleNodeClick(data,Node) {
                if(Node.childNodes!=null&&Node.childNodes.length>0)
                {
                    
                    //var transfertitle="未选中";
                   // this.$message.error(transfertitle);
                   this.currentCode=data.id;
                    console.log(this.currentCode);
                   this.initItem();
                }
                else{
                    //var transfertitle=data.label;
                    //this.$message.error(transfertitle);
                    //this.GetButtonByMenuId(this.tempmenuId);
                     this.currentCode=data.id;
                     console.log(this.currentCode);
                     this.initItem();
                }
            }
   },
     watch:
    {
        dialogFormVisible(val) 
        {
            this.loading=val;
        }
    }  ,
     components: {
             'v-btn-group':btnGroup,
              'v-parameter-edit':ParameterEdit
    }
}
</script>
<style>
#coltree >.el-tree
{
    min-height: 700px;
    height: 100%;
}
</style>