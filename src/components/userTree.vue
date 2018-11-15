<template>

<el-dialog :title="temptitle" :visible.sync="tempdialogVisible" :before-close="handleClose" @open="open" append-to-body >
    <el-row>
        <el-col :span="8">
            <el-tree
                :default-expand-all="tempexpand"
                ref="tree"
                :data="tempTreeData"
                show-checkbox
                node-key="id"
                @node-click="handleNodeClick"
                @check-change="handleCheckChange"
                :props="defaultProps">
                </el-tree>
        </el-col>
        <el-col :span="16">
             <el-table :data="tempTreeUserform"  border  :row-class-name="tableRowClassName" >
  <el-table-column
      type="index"
       width="50"
     
        label="序号" 
     fixed >
    </el-table-column>
    <el-table-column prop="UserId" label="用户ID"   width="100px"  fixed>
    </el-table-column>
     <el-table-column prop="AccountName" label="用户帐号" >
    </el-table-column>
     <el-table-column prop="RealName" label="用户名" >
    </el-table-column>
    
    
  </el-table>
        </el-col>
    </el-row>
  <div slot="footer" class="dialog-footer">
    <el-button @click="closeForm('form')">取 消</el-button>
    <el-button type="primary" @click="submitForm('form')" :disabled="tempbuttonDisabled">确 定</el-button>
  </div>
</el-dialog>
</template>
 <script>
import store from '@/store/store.js';
export default {
      data () {
        return {
        tempexpand:false,
        tempid:this.id,
        temptitle:this.name,
        tempdialogVisible:this.dialogVisible,
        tempdialogEditOrNew:this.dialogEditOrNew,
        tempTreeData:[],
        tempTreeUserDefault:[],
        tempTreeUserform:[],
        tempbuttonDisabled:false,
        defaultProps: {
                children: 'children',
                label: 'label'
                }
        }

    } ,
    store,
    mounted() {
    this.GetData();
    },
    methods: {
            tableRowClassName({row, rowIndex}) {
            if (rowIndex%2 === 0) {
            return 'warning-row';
            } else if (rowIndex%2 === 1) {
            return 'success-row';
            }
            return '';
            },
            submitForm(formName) {

            this.submitInfo();
            
            },
            submitInfo()
            {
                 this.tempTreeUserDefault=this.$refs.tree.getCheckedKeys();
                var  tempTreeUser=this.transformUserInfo( this.tempTreeUserDefault);
                console.log(tempTreeUser);
                this.$emit('dialogSubmit',tempTreeUser);
                this.tempdialogVisible=false; 
            },
            handleCheckChange(data,checked,indeterminate)
            {
                
                var stsz=data.id.split('-');
                if(stsz.length===3)
                {
                    if(checked)
                    {
                         var tempform= {
                            UserId:stsz[2],
                            AccountName:data.label,
                            RealName:data.RealName
                            };
                        if(this.tempTreeUserform!=null&&this.tempTreeUserform.length>0)
                        {
                            var bj=false;
                            for(var i=0;i<this.tempTreeUserform.length;i++)
                            {
                                if(this.tempTreeUserform[i].UserId===stsz[2])
                                {
                                    bj=true;
                                    break;
                                }
                            }
                            if(!bj)
                            {
                                 this.tempTreeUserform.push(tempform);
                            }

                        }
                        else{

                            
                            this.tempTreeUserform.push(tempform);

                        }
                     
                    }
                    else{
                            var tempTreeUser=this.$refs.tree.getCheckedKeys();
                             if(tempTreeUser!=null&&tempTreeUser.length>0)
                             {
                                    var bj=false;
                                    for(var i=0;i<tempTreeUser.length;i++)
                                    {
                                           var stsz1=tempTreeUser[i].split('-');
                                           if(stsz1.length===3)
                                           {
                                               if(stsz1[2]===stsz[2])
                                               {
                                                bj=true;
                                                break;
                                               }

                                           }

                                    }

                                    if(!bj)
                                    {
                                        var tempsz=[];

                                        if(this.tempTreeUserform!=null&&this.tempTreeUserform.length>0)
                                        {
                                            
                                              for(var i=0;i<this.tempTreeUserform.length;i++)
                                             {
                                                if(this.tempTreeUserform[i].UserId===stsz[2])
                                                {

                                                }
                                                else
                                                {
                                                    tempsz.push(this.tempTreeUserform[i]);
                                                }
                                             }
                                             this.tempTreeUserform=tempsz;

                                        }
                                    
                                    }

                             }
                             else{
                                 this.tempTreeUserform=[];
                             }

                    }
                }
                
            },
          handleNodeClick(data,Node) {
            if(Node.childNodes!=null&&Node.childNodes.length>0)
            {
               console.log(Node.childNodes);
            }
            else{
               
                console.log(data.label);
               // this.GetButtonByMenuId(this.tempmenuId);
            }
            },
            dialogClose() {
        
            this.$emit('dialogClose');
            

            },
       
         handleClose(done) {
         this.tempdialogVisible=false;     
         done();
         },
        closeForm(formName) {
                    this.tempdialogVisible=false;            
        },
            GetData()
            {
                var _self = this;
                this.$api.post("User/GetTreeUser", {  Token:this.$store.state.userInfo.Token}, response => {
                
                _self.tempTreeData=[];
        
        
                if (response.status >= 200 && response.status < 300) {
        
                    if (response.data) {
                    var jsonData = eval("(" + response.data + ")");
        
                    if (jsonData.Code == "1") {
        
                        if (jsonData.Data) {
            
                    
                        var info=JSON.parse(jsonData.Data);
                        _self.tempTreeData=info;

                        
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
            open()
            {
            try{
                    //this.GetData();
                }
                catch(ex)
                {
                this.$message.error("第一次打开菜单加载错误，请关闭后再试！");
                this.closeForm('form');
                }
            },
            getTreeKey(treeData,key)
            {
               var tempsz=[];
              for(var i=0;i<=treeData.length-1;i++)
             {
                var temp=this.getTreechildrenKey(treeData[i].children,key);
                if(temp!=null&&temp.length>0)
                {
                 for(var j=0;j<=temp.length-1;j++)
                 {
                     tempsz.push(temp[j]);
                 }
                }
             }
              return tempsz;
            },
            getTreechildrenKey(Data,key)
            {
                var tempsz=[];
                if(Data==null) return '';
                if(Data.length<=0) return '';
                for(var i=0;i<=Data.length-1;i++)
                {
              
                            var strid=Data[i].id;
                            var stsz=strid.split('-');
                            if(stsz.length===3)
                            {
                                if(stsz[2]===key)
                                {
                                    tempsz.push(strid);
                                }
                            };
                         var temp=  this.getTreechildrenKey(Data[i].children,key);
                         if(temp!=null&&temp.length>0)
                        {
                            for(var j=0;j<=temp.length-1;j++)
                            {
                                tempsz.push(temp[j]);
                            }
                        }

                }
                return tempsz;
            },
            getTreeLabel(treeData,key)
            {
              for(var i=0;i<=treeData.length-1;i++)
             {
                var temp=this.getTreechildrenLabel(treeData[i].children,key);
                if(temp)
                {
                    return temp;
                }
             }
            },
            getTreechildrenLabel(Data,key)
            {
                if(Data==null) return '';
                if(Data.length<=0) return '';
                for(var i=0;i<=Data.length-1;i++)
                {
              
                            var strid=Data[i].id;
                            if(strid===key)
                            {
                                var stsz=strid.split('-');
                                if(stsz.length===3)
                                {
                                    return stsz[2]+':'+ Data[i].label+':'+Data[i].RealName;   
                                }
                            }
                         var temp=  this.getTreechildrenLabel(Data[i].children,key);
                         if(temp)
                         {
                             return temp;
                         }

                }
                return '';
            },

            transformDefault(val)
            {
             if(val!=null&&val.length>0)
             {
              var temp=[];
               if(this.tempTreeData!=null&&this.tempTreeData.length>0)
               {
                  this.tempbuttonDisabled=false;
                  
                 for(var j=0;j<=val.length-1;j++)
                    {
                        var temp1 =this.getTreeKey(this.tempTreeData,val[j]);
                        if(temp1!=null&&temp1.length>0)
                        {
                             for(var k=0;k<=temp1.length-1;k++)
                            {
                                    temp.push(temp1[k]);
                            }
                        }
                    }




               }
               else{
                  this.tempbuttonDisabled=true;
               }
               return temp;

             }
             else{

             } return [];

            },
            transformUserInfo(val)
            {
             if(val!=null&&val.length>0)
             {
               var temp=[];
                for(var j=0;j<=val.length-1;j++)
                {
                      var temp1 =this.getTreeLabel(this.tempTreeData,val[j]);
                      if(temp1)
                      {
                      var tempsz=temp1.split(':');
                      var tempform= {
                        UserId:tempsz[0],
                        AccountName:tempsz[1],
                        RealName:tempsz[2]
                        }
                        if(tempform)
                        {
                             temp.push(tempform);
                        }
                      };
                }
                 return temp;

             }
             else{
                 return [];
             }
            },
            getFromUser(val)
            {
              if(val!=null&&val.length>0)
             {
                   var temp=[];
                    for(var j=0;j<=val.length-1;j++)
                    {
                        var temp1 =this.getTreeUser(this.tempTreeData,val[j]);
                        if(temp1)
                        {
                        var tempsz=temp1.split(':');
                        var tempform= {
                            UserId:tempsz[0],
                            AccountName:tempsz[1],
                            RealName:tempsz[2]
                            }
                            if(tempform)
                            {
                                temp.push(tempform);
                            }
                        };
                    }
                    this.tempTreeUserform= temp;

                }
                else{
                    this.tempTreeUserform=[];
                }
            },
            getTreeUser(treeData,key)
            {
                        for(var i=0;i<=treeData.length-1;i++)
                        {
                            var temp=this.getTreechildrenUser(treeData[i].children,key);
                            if(temp)
                            {
                                return temp;
                            }
                        }
            },
            getTreechildrenUser(Data,key)
            {
                if(Data==null) return '';
                if(Data.length<=0) return '';
                for(var i=0;i<=Data.length-1;i++)
                {
              
                            var strid=Data[i].id;
                            
                                var stsz=strid.split('-');
                                if(stsz.length===3)
                                {
                                    if(stsz[2]==key)
                                    {
                                      return stsz[2]+':'+ Data[i].label+':'+Data[i].RealName;   
                                    }
                                }
                            
                         var temp=  this.getTreechildrenLabel(Data[i].children,key);
                         if(temp)
                         {
                             return temp;
                         }

                }
                return '';
            },
    },
     watch:
    {
        tempdialogVisible(val) 
        {
            this.$emit('update:dialogVisible', val);
        },
        dialogVisible(val) 
        {
            this.tempdialogVisible=val;
        
        },
        dialogEditOrNew(val)
        {
           this.tempdialogEditOrNew=val;
        },
        name(val) 
        {
            this.temptitle=val;
        },
        id(val)
        {
        this.tempid=val;
        },
        dialogUserDefault(val)
        {
        
          this.tempTreeUserDefault=this.transformDefault(val);
          console.log(this.tempTreeUserDefault);
          if(this.tempTreeUserDefault!=null&&this.tempTreeUserDefault.length>0)
          {
          this.$refs.tree.setCheckedKeys(this.tempTreeUserDefault);
          }else{
         this.$refs.tree.setCheckedKeys(this.tempTreeUserDefault);
          };
          this.getFromUser(val);
        }

    },
     props: ['name','id','dialogVisible','dialogUserDefault']
}
 </script>