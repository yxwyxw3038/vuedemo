<template>
  <el-dialog :title="temptitle" :visible.sync="tempdialogVisible" :before-close="handleClose" @open="open" >
      <el-row>
          <el-col :span="8">
    <el-tree
    :default-expand-all="tempexpand"
    ref="tree"
    :data="tempTreeMenuData"
    show-checkbox
    node-key="id"
    @node-click="handleNodeClick"
    :props="defaultProps">
    </el-tree>
  
         </el-col>
         <el-col :span="16">
           <el-row>
             <el-tag type="success" :disabled="disabledTransferButton">{{transfertitle}}</el-tag>
           </el-row>
       <el-row>
    <el-transfer v-model="transferSetRoleButtonValue" :data="transferSetRoleButtonData"
     :titles="['未赋权', '已赋权']"
      :button-texts="['删除', '添加']"
      ></el-transfer>
       </el-row>
       <el-row>
      <el-button type="success" icon="el-icon-back" :disabled="disabledTransferButton" @click="handleBackClick" circle size="mini"></el-button>
           <el-button  type="success" icon="el-icon-check" :disabled="disabledTransferButton" @click="handleOkClick"  circle size="mini"></el-button>
       </el-row>

       

     </el-col>
      </el-row>
     <div slot="footer" class="dialog-footer">
    <el-button @click="closeForm('form')">取 消</el-button>
    <el-button type="primary" @click="submitForm('form')">确 定</el-button>
  </div>
 </el-dialog> 
</template>
<script>
import store from '../../store/store.js';
export default {
  data () {
    return {
     tempexpand:true,
     tempid:this.id,
  
     temptitle:this.name,
     tempdialogVisible:this.dialogVisible,
     tempTreeMenuData:[],
     tempTreeMenuDefault:[],
        defaultProps: {
          children: 'children',
          label: 'label'
        },
     transferSetRoleButtonValue:[],
     transferSetRoleButtonData:[],
     tempmenuId:-1,
     disabledTransferButton:true,
     transfertitle:"未选中菜单"
    }
  } ,
  store,
   methods: {
     submitInfo()
     {
         var _self = this;
         var menuStr='';
         var sp=_self.$refs.tree.getCheckedKeys();
         if(sp.length>0)
         {
           for(var i=0;i<sp.length;i++)
           {
             if(i===sp.length-1)
             {
                  menuStr=menuStr+sp[i].toString();
             }
             else{
                   menuStr=menuStr+sp[i].toString()+',';
             }
           }
         }

         this.$api.post("User/SetMenuRole", {  Token:this.$store.state.userInfo.Token,roleId:this.id,menuStr:menuStr}, response => {

          if (response.status >= 200 && response.status < 300) {
  
            if (response.data) {
              var jsonData = eval("(" + response.data + ")");
  
              if (jsonData.Code == "1") {
  
                
    
                
                 this.$message({
                    message: '保存成功！',
                    type: 'success'
                  });

                   this.tempdialogVisible=false;
                   this.tempTreeMenuDefault=[];
              
               
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
     handleOkClick()
     {
       var _self = this;
         var buttonStr='';
         if(this.transferSetRoleButtonValue.length>0)
         {
           for(var i=0;i<this.transferSetRoleButtonValue.length;i++)
           {
             if(i===this.transferSetRoleButtonValue.length-1)
             {
                  buttonStr=buttonStr+this.transferSetRoleButtonValue[i].toString();
             }
             else{
                   buttonStr=buttonStr+this.transferSetRoleButtonValue[i].toString()+',';
             }
           }
         }

         this.$api.post("User/SetButtonByMenuIdRoleId", {  Token:this.$store.state.userInfo.Token,menuId:this.tempmenuId,roleId:this.id,buttonStr:buttonStr}, response => {

          if (response.status >= 200 && response.status < 300) {
  
            if (response.data) {
              var jsonData = eval("(" + response.data + ")");
  
              if (jsonData.Code == "1") {
                 var sp=_self.$refs.tree.getCheckedKeys();
                 var bj=0
                 if(sp)
                 {
                   for(var i=0;i<sp.length;i++)
                   {
                     if(sp[i].toString()==_self.tempmenuId.toString())
                     {
                       bj=1;
                       break;
                     }
                   }
                 }
                 if(bj===0)
                 {
                   sp.push(this.tempmenuId);
                   _self.tempTreeMenuDefault=sp;
               
                  _self.$refs.tree.setCheckedKeys(_self.tempTreeMenuDefault);
                 }
                
                
                 this.$message({
                    message: '保存成功！',
                    type: 'success'
                  });

                 
               
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
     handleBackClick()
     {
      this.GetButtonByMenuId(this.tempmenuId);
     },
    handleNodeClick(data,Node) {
       if(Node.childNodes!=null&&Node.childNodes.length>0)
       {
          this.transferSetRoleButtonValue=[];
          this.transferSetRoleButtonData=[];
          this.tempmenuId=-1;
          this.disabledTransferButton=true;
          this.transfertitle="未选中菜单";
       }
       else{
       this.transfertitle=data.label;
        this.disabledTransferButton=false;
        this.tempmenuId=data.id;
        this.GetButtonByMenuId(this.tempmenuId);
       }
      },
     GetButtonByMenuId(Id)
     {
         var _self = this;
       
         this.$api.post("User/GetAllButtonByMenuIdForTransfer", {  Token:this.$store.state.userInfo.Token,menuId:Id}, response => {

          if (response.status >= 200 && response.status < 300) {
  
            if (response.data) {
              var jsonData = eval("(" + response.data + ")");
  
              if (jsonData.Code == "1") {
  
                if (jsonData.Data) {
    
                  var tempdata= eval("(" + jsonData.Data+ ")");
                  _self.transferSetRoleButtonData=tempdata;
               
               
               
                 
                }

  
  
  
              } 
            
              
              else {
  
 
                _self.$message.error(jsonData.Message);
                console.log(jsonData.Message);
  
  
  
              }
  
            }
  
  
  
          } else {
            _self.$message.error(response.message);
  
  
  
  
          }
  
        });


          this.$api.post("User/GetButtonByMenuIdRoleIdForTransfer", {  Token:this.$store.state.userInfo.Token,menuId:Id,roleId:this.id }, response => {

          if (response.status >= 200 && response.status < 300) {
  
            if (response.data) {
              var jsonData = eval("(" + response.data + ")");
  
              if (jsonData.Code == "1") {
  
                if (jsonData.Data) {
    
                  var sp=jsonData.Data.split(',');
                 
                   _self.transferSetRoleButtonValue=sp;

                 
                
               
                  _self.dialogSetMenuButtonVisible=true;
                 
                }

  
  
  
              } 
              else if (jsonData.Code == "0") {
                 _self.transferSetRoleButtonValue=[];
              }
              else {
  
 
                _self.$message.error(jsonData.Message);
                console.log(jsonData.Message);
  
  
  
              }
  
            }
  
  
  
          } else {
            _self.$message.error(response.message);
  
  

  
          }
  
        });
     },
     handleClose(done) {
          this.transferSetRoleButtonValue=[];
          this.transferSetRoleButtonData=[];
          this.tempmenuId=-1;
          this.disabledTransferButton=true;
           this.transfertitle="未选中菜单";
     done();
     },
   submitForm(formName) {

          this.submitInfo();
          
      },
      closeForm(formName) {
                this.tempTreeMenuDefault=[];
                this.tempdialogVisible=false;
                this.transferSetRoleButtonValue=[];
                this.transferSetRoleButtonData=[];
                this.tempmenuId=-1;
                this.disabledTransferButton=true;
                this.transfertitle="未选中菜单";
                    
        
      },
      GetData()
      {
  var _self = this;
           this.$api.post("User/GetTreeMenu", {  Token:this.$store.state.userInfo.Token}, response => {
        
           _self.tempTreeMenuData=[];
  
  
          if (response.status >= 200 && response.status < 300) {
  
            if (response.data) {
              var jsonData = eval("(" + response.data + ")");
  
              if (jsonData.Code == "1") {
  
                if (jsonData.Data) {
    
              
                  var info=JSON.parse(jsonData.Data);
                  _self.tempTreeMenuData=info;

                 
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
       GetCheckedData(id)
       {
         // this.$refs.tree.setCheckedKeys([]);
          var _self = this;
            this.$api.post("User/GetMenuByRoleIdForTree", {  Token:this.$store.state.userInfo.Token,Id:id }, response => {

          if (response.status >= 200 && response.status < 300) {
  
            if (response.data) {
              var jsonData = eval("(" + response.data + ")");
  
              if (jsonData.Code == "1") {
  
                if (jsonData.Data) {
    
                
                  var sp=jsonData.Data.split(',');
                 
                   _self.tempTreeMenuDefault=sp;
               
                  _self.$refs.tree.setCheckedKeys(_self.tempTreeMenuDefault);
                 
                }
                else{
                  _self.$refs.tree.setCheckedKeys([]);
                }

  
  
  
              } 
              else if (jsonData.Code == "0") {
                _self.tempTreeMenuDefault=[];
                 _self.$refs.tree.setCheckedKeys([]);
              }
              
              else {
                _self.tempTreeMenuDefault=[];
                _self.$refs.tree.setCheckedKeys([]);
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
  open()
  {
    try{
    this.GetCheckedData(this.tempid);
    }
    catch(ex)
    {
       this.$message.error("第一次打开菜单加载错误，请关闭后再试！");
       this.closeForm('form');
    }
  }


   },

  created() {
   
        this.GetData();
        //this.GetCheckedData(this.tempid);
        //console.log(this.name);
       // console.log(this.id);
    
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
       name(val) 
      {
        this.temptitle=val;
      },
      id(val)
      {
      
      
       this.tempid=val;
      // this.GetCheckedData(val);
       
      
      }

  },
  
  props: ['name','id','dialogVisible']
}
</script>