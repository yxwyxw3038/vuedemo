<template>
<el-dialog :title="temptitle" :visible.sync="tempdialogVisible" :before-close="handleClose" @open="open" >
  <el-form :model="form" ref="form" :rules="rules" label-width="120px" >
    <el-form-item label="IP："  prop="Address">
        <el-col :span="8">
        <el-input v-model="form.Address" ></el-input>
      </el-col>
    </el-form-item>
     <el-form-item label=" 备注：" prop="Notes" >
         <el-col :span="16">
     <el-input v-model="form.Notes"  type="textarea" :rows="2" placeholder="请输入内容" ></el-input>
         </el-col>
    </el-form-item>
    
  </el-form>
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
        tempid:this.id,
        temptitle:this.name,
        tempdialogVisible:this.dialogVisible,
        tempdialogEditOrNew:this.dialogEditOrNew,
        form: {
         Id:0,
         Address:'',
         Port:'',
         CreateTime:'',
         Notes:'',
         IsAble:''
        },
         rules: {
          Address: [
            { required: true, message: '请输入IP', trigger: 'blur' }
         
          ]
      
          
        }

        }

    } ,
    store,
    mounted() {
    
    },
    methods: {
        dialogClose() {
       
        this.$emit('dialogClose');
        

        },
        submitInfo()
        {
               
       var optUrl="Sys/AddBlackList";
       if( this.tempdialogEditOrNew==="0")
       {
            optUrl="Sys/AddBlackList";
           
       }
       else{
           optUrl="Sys/UpdateBlackList";
        
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

              this.tempdialogVisible=false;
              this.dialogClose();
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
         done();
         },
        closeForm(formName) {
                    this.tempdialogVisible=false;            
        },
        GetData()
            {
                 var _self = this;
                 _self.form= {
                        Id:0,
                        Address:'',
                        Port:'',
                        CreateTime:'',
                        Notes:'',
                        IsAble:''
                  };
                if(this.dialogEditOrNew==="1")
                {
             
                this.$api.post("Sys/GetBlackListById", {  Token:this.$store.state.userInfo.Token,Id:  this.tempid}, response => {
                
                _self.tempTreeMenuData=[];
        
        
                if (response.status >= 200 && response.status < 300) {
        
                    if (response.data) {
                    var jsonData = eval("(" + response.data + ")");
        
                    if (jsonData.Code == "1") {
        
                        if (jsonData.Data) {
                        var info=JSON.parse(jsonData.Data);
                         _self.form=info;             
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
            }
            },
            open()
            {
            try{
                    this.GetData();
                }
                catch(ex)
                {
                this.$message.error("第一次打开菜单加载错误，请关闭后再试！");
                this.closeForm('form');
                }
            }
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
        }

    },
     props: ['name','id','dialogVisible','dialogEditOrNew']
}
</script>