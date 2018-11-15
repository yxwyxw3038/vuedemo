<template>
<el-dialog :title="temptitle" :visible.sync="tempdialogVisible" :before-close="handleClose" @open="open" >
  <el-form :model="form" ref="form" :rules="rules" label-width="120px" >
   <el-row>
    <el-col :span="12"> 
    <el-form-item label="单据号:"  prop="No"> 
         
        <el-input v-model="form.No" :disabled="true" ></el-input>
   
    </el-form-item>
    </el-col>
    <el-col :span="12"> 
    <el-form-item label="通知类型:"  prop="ftypeid"> 
            <el-select v-model="form.ftypeid" filterable placeholder="" size="mini" :disabled="tempdialogdisabled">
                <el-option
                v-for="item in ftypeOptions"
                :key="item.value"
                :label="item.label"
                :value="item.value" size="mini" >
                </el-option>
          </el-select>
    </el-form-item>
    </el-col>
    </el-row>
    <el-row>
            <el-col :span="12"> 
              <el-form-item label="单据状态:"  prop="status"> 
            <el-select v-model="form.status" filterable placeholder="" size="mini" :disabled="true">
                <el-option
                v-for="item in statusOptions"
                :key="item.value"
                :label="item.label"
                :value="item.value" size="mini" >
                </el-option>
          </el-select>
             </el-form-item>
            </el-col>
          <el-col :span="12"> 
        <el-form-item label="通知时间:"  prop="NoticeTime">    
       
             <el-date-picker
                v-model="form.NoticeTime"
                align="right"
                type="datetime"
                default-time="23:59:59"
                placeholder="选择日期"
                :picker-options="pickerOptions1" :disabled="true" >
         </el-date-picker>
        </el-form-item>
    </el-col>
    </el-row>
    <el-row>
  
          <el-col :span="24"> 
        <el-form-item label="通知主题:"  prop="ftitle">    
            <el-input v-model="form.ftitle" :disabled="tempdialogdisabled"  ></el-input>
        </el-form-item>
    </el-col>
    </el-row>
    <el-row>
        
       <el-col :span="12">
            <el-form-item label="开始时间:"  prop="SendBeginTime">  
            <el-date-picker
                v-model="form.SendBeginTime"
                align="right"
                type="datetime"
                default-time="00:00:00"
                placeholder="选择日期"
                :picker-options="pickerOptions1" :disabled="tempdialogdisabled" >
        </el-date-picker>
          </el-form-item>
       </el-col>
       <el-col :span="12">
            <el-form-item label="结束时间:"  prop="SendEndTime">  
            <el-date-picker
                v-model="form.SendEndTime"
                align="right"
                type="datetime"
                default-time="23:59:59"
                placeholder="选择日期"
                :picker-options="pickerOptions1" :disabled="tempdialogdisabled" >
         </el-date-picker>
          </el-form-item>
       </el-col>
    </el-row>
    <el-row>
    <el-col :span="24"> 
        <el-form-item label="通知内容:"  prop="fcontent">    
            <el-input v-model="form.fcontent" :disabled="tempdialogdisabled"  type="textarea" :rows="2"  ></el-input>
        </el-form-item>
    </el-col>
    </el-row>
    
  </el-form>
  <el-button-group id="buttongroup">
     <el-button size="mini"  :disabled="btnSetUserdisabled"     @click="btnSetUserclick" :loading="buttonloading">设置通知用户</el-button>
     <el-button size="mini"  :disabled="btnSetSavedisabled"    @click="btnSaveclick" :loading="buttonloading">保存</el-button>
     <el-button size="mini"  :disabled="btnSetDeldisabled"    @click="btnDelclick" :loading="buttonloading">删除</el-button>
     <el-button size="mini"  :disabled="btnSetAcceptdisabled"    @click="btnAcceptclick" :loading="buttonloading">确认</el-button>
     <el-button size="mini"  :disabled="btnSetDeposedisabled"   @click="btnDeposeclick" :loading="buttonloading">作废</el-button>
</el-button-group>
   <el-table :data="formitem"  border  :row-class-name="tableRowClassName"  @selection-change="handleSelectionChange">
  <el-table-column
      type="index"
       width="50"
     
        label="序号" 
     fixed >
    </el-table-column>
     <el-table-column prop="AccountName" label="用户名"   width="100px"  fixed>
    </el-table-column>
    <el-table-column prop="RealName" label="姓名"   width="150px"  fixed>
    </el-table-column>
     <el-table-column prop="SendFlag" label="是否发送"  width="120px" >
      <template slot-scope="scope">
        <el-select v-model="scope.row.SendFlag" filterable placeholder="" size="mini" :disabled="true">
                <el-option
                v-for="item in SendFlagOptions"
                :key="item.value"
                :label="item.label"
                :value="item.value" size="mini" >
                </el-option>
          </el-select>
      </template>
    </el-table-column>
    <el-table-column prop="SendTime" label="发送时间" >
    </el-table-column>
    <el-table-column prop="CreateTime" label="创建时间" >
    </el-table-column>
    <el-table-column prop="UpdateTime" label="修改时间" >
    </el-table-column>
  </el-table>
  <div slot="footer" class="dialog-footer">
    <el-button @click="closeForm()" :loading="dialogUserloading">关闭</el-button>
  </div>
  <v-user-tree :id="dialogUserid" :name="dialogUserrTitle" :dialogVisible.sync="dialogUserVisible" :dialogUserDefault="dialogUserDefault"  @dialogClose="dialogUserClose" @dialogSubmit="dialogUserSubmit" ></v-user-tree>
</el-dialog>
</template>
<script>
import store from '../../store/store.js';
import userTree from '@/components/userTree';

export default {
      data () {
        return {
        user:new Object(),
        tempid:this.id,
        temptitle:this.name,
        tempdialogVisible:this.dialogVisible,
        tempdialogEditOrNew:this.dialogEditOrNew,
        tempdialogdisabled:true,
        tempFromModify:false,
        tempdialogSubmit:false,
        buttonloading:false,
        btnSetUserdisabled:true,
        btnSetSavedisabled:true,
        btnSetDeldisabled:true,
        btnSetAcceptdisabled:true,
        btnSetDeposedisabled:true,

        dialogUserloading:false,
        dialogUserid:0,
        dialogUserrTitle:'设置通知人员',
        dialogUserVisible:false,
        dialogUserDefault:[],
        multipleSelection: [],
        pickerOptions1: {
         
          shortcuts: [{
            text: '今天',
            onClick(picker) {
              picker.$emit('pick', new Date());
            }
          }, {
            text: '昨天',
            onClick(picker) {
              const date = new Date();
              date.setTime(date.getTime() - 3600 * 1000 * 24);
              picker.$emit('pick', date);
            }
          }, {
            text: '明天',
            onClick(picker) {
              const date = new Date();
              date.setTime(date.getTime() + 3600 * 1000 * 24);
              picker.$emit('pick', date);
            }
          }]
        },
         SendFlagOptions:[{
          value: 0,
          label: '未发送'
        }, {
          value: 1,
          label: '已发送'
        }],

         ftypeOptions:[{
          value: 0,
          label: '系统通知'
        }, {
          value: 1,
          label: '消息通知'
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
    
        form: { 
          
            Code :'',
            No :'',
            ftypeid :'',
            ftitle :'',
            fcontent :'',
            CreateTime :'',
            CreateBy :'',
            UpdateTime :'',
            UpdateBy :'',
            status :0,
            NoticeTime :'',
            SendBeginTime :'',
            SendEndTime :''
              
                
        },
        formitem:[],
         rules: {
          ftitle: [
            { required: true, message: '请输入通知主题', trigger: 'blur' }
         
          ],
          fcontent: [
            { required: true, message: '请输入通知内容', trigger: 'blur' }
         
          ]
      
          
        }

        }

    } ,
    store,
    mounted() {
        
    },
    created() {
      this.tempFromModify=false,
      this.user=this.$store.state.userInfo;
     },
    methods: {
      
        dialogUserSubmit(val)
        {
          if(val)
          {
            if(this.formitem!=null&&this.formitem.length>0)
            {
             
                    var delsz=[];
                    for(var i=0;i<=this.formitem.length-1;i++)
                    {
                         var bj=false;
                          for(var j=0;j<=val.length-1;j++)
                          {
                              if(this.formitem[i].UserId===val[j].UserId)
                              {
                                bj=true;
                                break;
                              }
                          }
                          if(!bj)
                          {
                           delsz.push(this.formitem[i].UserId);
                          }

                    }
                     if(delsz!=null&&delsz.length>0)
                    {
                      var tempsz=[];
                       for(var i=0;i<=this.formitem.length-1;i++)
                      {
                          var bj=false;
                          for(var j=0;j<=delsz.length-1;j++)
                            {
                              if(this.formitem[i].UserId===delsz[j])
                              {
                                bj=true;
                                break;
                              }
                             
                            }

                            if(!bj)
                            {
                             tempsz.push(this.formitem[i]);
                            }
                      }
                      this.formitem=tempsz;


                    }
                    
            }


             for(var j=0;j<=val.length-1;j++)
            {
              var item={
                     AccountName : val[j].AccountName,
                     CreateBy :this.user.AccountName,
                     CreateTime : '',
                     id : 0,
                     NoticeCode :'',
                     SendFlag : 0,
                     SendTime :'',
                     UpdateBy : this.user.AccountName,
                     UpdateTime : '',
                     UserId : val[j].UserId,
                     RealName :val[j].RealName

                 };
               if(this.formitem!=null&&this.formitem.length>0)
               {
                   var bj=false;
                    for(var i=0;i<=this.formitem.length-1;i++)
                    {
                      if(this.formitem[i].UserId===val[j].UserId)
                      {
                        bj=true;
                        break;
                      }
                      
                    }
                    if(!bj)
                    {
                      this.formitem.push(item);
                    }

               }
               else{
                
                   this.formitem.push(item);
               }


            }
           
          }
          else{
              this.formitem=[];
          }
        },
        dialogUserClose()
        {
      
        
        },
        btnSetUserclick()
        {
        
          if(this.formitem!=null&& this.formitem.length>0)
          {
           var strtemp='';
           for(var i=0; i<this.formitem.length;i++)
           {
             strtemp=strtemp+this.formitem[i].UserId+',';
           }
           strtemp=strtemp.substr(0,strtemp.length-1);
           this.dialogUserDefault=strtemp.split(',');
          }
          else{
              this.dialogUserDefault=[];
          }
          
          console.log("11");
          this.dialogUserVisible=true;
        },
         btnSaveclick()
        {
           this.$refs['form'].validate((valid) => {
            if (valid) {
                
                this.submitInfo();
            } else {
                return;
            }
            });
        },
         btnDelclick()
        {
        var _self = this;
                this.$confirm('此操作将永久删除数据, 是否继续?', '提示', {
                confirmButtonText: '确定',
                cancelButtonText: '取消',
                type: 'warning'
              }).then(() => {
                var str=_self.form.Code;
             
            
                _self.buttonloading=true;
                this.$api.post("Sys/DeleteNotice", {  Token:this.$store.state.userInfo.Token,str:str}, response => {
                _self.buttonloading=false;
              
        
              
        
                if (response.status >= 200 && response.status < 300) {
        
                  if (response.data) {
                    var jsonData = eval("(" + response.data + ")");
        
                    if (jsonData.Code == "1") {
        
                      this.$message({
                        type: 'success',
                        message: '删除成功!'
                      });
                    this.tempdialogSubmit=true;
                    this.closeForm();
        
        
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
         btnAcceptclick()
        {
               var _self = this;
                this.$confirm('是否确认?', '提示', {
                confirmButtonText: '确定',
                cancelButtonText: '取消',
                type: 'warning'
              }).then(() => {
                var strCode=_self.form.Code;
                var status=_self.form.status;
            
                _self.buttonloading=true;
                this.$api.post("Sys/UpdateNoticeStatus", {  Token:this.$store.state.userInfo.Token,Code:strCode,oldStatus:status,newStatus:5}, response => {
                _self.buttonloading=false;
              
        
              
        
                if (response.status >= 200 && response.status < 300) {
        
                  if (response.data) {
                    var jsonData = eval("(" + response.data + ")");
        
                    if (jsonData.Code == "1") {
        
                      this.$message({
                        type: 'success',
                        message: '确认成功!'
                      });
                    _self.tempid=strCode;
                    _self.temptitle='修改通知';
                    _self.tempdialogSubmit=true;
                    _self.tempFromModify=false;
                    _self.GetData();
        
        
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
                  message: '已取消确认'
                });          
              });
        },
         btnDeposeclick()
        {
        var _self = this;
                this.$confirm('是否作废?', '提示', {
                confirmButtonText: '确定',
                cancelButtonText: '取消',
                type: 'warning'
              }).then(() => {
                var strCode=_self.form.Code;
                var status=_self.form.status;
            
                _self.buttonloading=true;
                this.$api.post("Sys/UpdateNoticeStatus", {  Token:this.$store.state.userInfo.Token,Code:strCode,oldStatus:status,newStatus:-1}, response => {
                _self.buttonloading=false;
              
        
              
        
                if (response.status >= 200 && response.status < 300) {
        
                  if (response.data) {
                    var jsonData = eval("(" + response.data + ")");
        
                    if (jsonData.Code == "1") {
        
                      this.$message({
                        type: 'success',
                        message: '作废成功!'
                      });
                    _self.tempid=strCode;
                    _self.temptitle='修改通知';
                    _self.tempdialogSubmit=true;
                    _self.tempFromModify=false;
                    _self.GetData();
        
        
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
                  message: '已取消作废'
                });          
              });
        },
        SetCbStatus()
        {
           if(this.form===null||this.form.status===null)
           {
              //console.log("!0");
              this.btnSetUserdisabled=true;
              this.btnSetSavedisabled=true;
              this.btnSetDeldisabled=true;
              this.btnSetAcceptdisabled=true;
              this.btnSetDeposedisabled=true;
              this.tempdialogdisabled=true;
           }
          
           else 
           {
             
              if(this.form.status===0)
              {
                   this.tempdialogdisabled=false;
                if(this.tempFromModify)
                {
                     //console.log("0  Modify");
                          this.btnSetUserdisabled=false;
                          this.btnSetSavedisabled=false;
                          this.btnSetDeldisabled=true;
                          this.btnSetAcceptdisabled=true;
                          this.btnSetDeposedisabled=true;
                }
                else{
                     //console.log("0 not Modify");
                         this.btnSetUserdisabled=false;
                          this.btnSetSavedisabled=true;
                          this.btnSetDeldisabled=false;
                          this.btnSetAcceptdisabled=false;
                          this.btnSetDeposedisabled=false;
                }
              }
              else{
                  this.tempdialogdisabled=true;
                 // console.log("!0");
                  this.btnSetUserdisabled=true;
                  this.btnSetSavedisabled=true;
                  this.btnSetDeldisabled=true;
                  this.btnSetAcceptdisabled=true;
                  this.btnSetDeposedisabled=true;
              }

           }
        },
         handleSelectionChange(val) {
        this.multipleSelection = val;
        if(this.multipleSelection&&this.multipleSelection.length>0)
        {
            console.log(this.multipleSelection[0].No);
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
        dialogClose() {
        this.tempFromModify=false;
        this.$emit('dialogClose');
        

        },
       getMyDate(str){  
            var oDate = new Date(str);
            var oYear = oDate.getFullYear();  
            var oMonth = oDate.getMonth();  
            var oDay = oDate.getDate(); 
            var oHour = oDate.getHours();
            var oMin = oDate.getMinutes();  
            var oSen = oDate.getSeconds(); 
            var oTime = oYear +'-'+ this.getzf(oMonth) +'-'+ this.getzf(oDay) +' '+ this.getzf(oHour) +':'+ this.getzf(oMin) +':'+this.getzf(oSen);//最后拼接时间  
            return oTime;  
        },
        //补0操作
        getzf(num){  
          if(parseInt(num) < 10){  
              num = '0'+num;  
          }  
          return num;  
        },
        submitInfo()
        {
               
       var optUrl="Sys/AddNotice";
       if( this.tempdialogEditOrNew==="0")
       {
            optUrl="Sys/AddNotice";
            this.form.CreateBy=this.user.AccountName;
       }
       else{
           optUrl="Sys/UpdateNotice";
           this.form.UpdateBy=this.user.AccountName;
       }

       var savesz={
         Main:this.form,
         Item:this.formitem

       }
         var Jsonstr= JSON.stringify(savesz);

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
              if(_self.tempdialogEditOrNew==="0")
              {
                _self.tempdialogEditOrNew="1";
               
              }
              _self.tempid=jsonData.Data;
              _self.temptitle='修改通知';
              _self.tempdialogSubmit=true;
              _self.tempFromModify=false;
              _self.GetData();
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
        submitForm(formName) {

            this.$refs[formName].validate((valid) => {
            if (valid) {
                
                this.submitInfo();
            } else {
                return;
            }
            });
            
        },
         handleClose(done) {
         this.tempdialogVisible=false;  
         if(this.tempdialogSubmit)
         {
           this.dialogClose();
         }   
         done();
         },
        closeForm() {
         this.tempdialogVisible=false; 
          if(this.tempdialogSubmit)
         {
           this.dialogClose();
         }       
           
        },
        GetData()
            {
                 var _self = this;
                  this.tempFromModify=false;
                  var datetime = new Date();
                  _self.form={ 
                  
                          Code :'',
                          No :'',
                          ftypeid :'',
                          ftitle :'',
                          fcontent :'',
                          CreateTime :this.getMyDate(datetime.getTime()),
                          CreateBy :this.user.AccountName,
                          UpdateTime :this.getMyDate(datetime.getTime()),
                          UpdateBy :this.user.AccountName,
                          status :0,
                          NoticeTime :this.getMyDate(datetime.getTime()),
                          SendBeginTime :this.getMyDate(datetime.getTime()),
                          SendEndTime :this.getMyDate(datetime.getTime() + 3600 * 1000 * 24)
                      
                        
                };
                _self.formitem=[];
                _self.dialogUserDefault=[];
                //console.log("测试先后");
                if(_self.tempdialogEditOrNew==="1"||_self.tempdialogEditOrNew==="2")
                {
             
                _self.$api.post("Sys/GetNoticeById", {  Token:this.$store.state.userInfo.Token,Code:  _self.tempid}, response => {
                if (response.status >= 200 && response.status < 300) {
                 
                    if (response.data) {
                    var jsonData = eval("(" + response.data + ")");
        
                    if (jsonData.Code == "1") {
        
                        if (jsonData.Data) {
                        var info=JSON.parse(jsonData.Data);
                         _self.form=info; 
                        _self.$nextTick(function() {
                         _self.tempFromModify=false;
                        });     
                        }       
        
                    } else { 
                        _self.$message.error(jsonData.Message);
                        console.log(jsonData.Message);
                    }
                    //子项数据
                     
                      _self.$api.post("Sys/GetNoticeItemById", {  Token:this.$store.state.userInfo.Token,Code:  _self.tempid}, response => {
                            if (response.status >= 200 && response.status < 300) {
                      
                                if (response.data) {
                                var jsonDataItem = eval("(" + response.data + ")");
                    
                                if (jsonDataItem.Code == "1") {
                    
                                    if (jsonDataItem.Data) {
                                    var infoitem=JSON.parse(jsonDataItem.Data);
                                    _self.formitem=infoitem;   
                                    _self.$nextTick(function() {
                                        _self.tempFromModify=false;
                                    });
                                            
                                    }       
                    
                                } else { 
                                     if(jsonDataItem.DataCount==0)
                                     {
                                     }
                                     else{
                                    _self.$message.error(jsonDataItem.Message);
                                    console.log(jsonDataItem.Message);
                                     }
                                }
                                }
                            }
                                });


                    }
                } else {
                    _self.$message.error(response.message);
                }
                
        
                });
            }
            else{
              _self.$nextTick(function() {
                 //console.log("测试先后2");
                 _self.tempFromModify=true;
                 //_self.SetCbStatus();
                 // console.log("测试先后3");
            });
            }
            
            },
            open()
            {
            try{
                    this.tempdialogSubmit=false;
                    this.GetData();
                }
                catch(ex)
                {
                this.$message.error("第一次打开菜单加载错误，请关闭后再试！");
                this.closeForm();
                }
            },
            fromModify()
            {
                      //console.log(this);
                      //console.log("Modify状态变化！true");
                      this.tempFromModify=true;
            },
    },
     watch:
    {
        form:{
                // 深度观察
                handler:'fromModify',
                deep:true
            },
        formitem:{
               handler:'fromModify',
               deep:true
            },
        
         tempFromModify(val)
         {
           console.log("Modify状态变化！");
           //console.log(val);
           this.SetCbStatus();

         },
        tempdialogVisible(val) 
        {
            this.$emit('update:dialogVisible', val);
        },
        dialogVisible(val) 
        {
            this.tempdialogVisible=val;
        
        },
        tempdialogEditOrNew(val)
        {
            this.$emit('update:dialogEditOrNew', val);
        },
        dialogEditOrNew(val)
        {
           this.tempdialogEditOrNew=val;
           if(val==='0' || val=='1')
           {
               this.tempdialogdisabled=false;
           }
           else{
               this.tempdialogdisabled=true;
           }
        },
         temptitle(val) 
        {
            this.$emit('update:name', val);
        },
        name(val) 
        {
            this.temptitle=val;
        },
        id(val)
        {
        this.tempid=val;
        },
         dialogUserVisible(val) 
        {
            this.dialogUserloading=val;
        }

    },
     props: ['name','id','dialogVisible','dialogEditOrNew']
     ,
      components: {
            
             'v-user-tree':userTree
    }
}
</script>