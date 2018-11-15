<template>
<el-dialog :title="temptitle" :visible.sync="tempdialogVisible" :before-close="handleClose" @open="open" >
  <el-form :model="form" ref="form" :rules="rules" label-width="120px" >
   
    <el-form-item label="参数:"  prop="Code"> 
         <el-col :span="12">  
        <el-input v-model="form.Code" ></el-input>
         </el-col>
    </el-form-item>
    
        <el-form-item label="参数名称:"  prop="CodeName"> 
         <el-col :span="12">  
        <el-input v-model="form.CodeName" ></el-input>
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
        user:new Object(),
        tempid:this.id,
        temptitle:this.name,
        tempdialogVisible:this.dialogVisible,
        tempdialogEditOrNew:this.dialogEditOrNew,
        tempFatherCode:this.FatherCode,
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
         rules: {
          Code: [
            { required: true, message: '请输入Code', trigger: 'blur' }
         
          ],
           CodeName: [
            { required: true, message: '请输入CodeName', trigger: 'blur' }
         
          ]
      
          
        }

        }

    } ,
    store,
    mounted() {
    
    },
    created() {
      this.user=this.$store.state.userInfo;
     },
    methods: {
        dialogClose() {
       
        this.$emit('dialogClose');
        

        },
        submitInfo()
        {
               
       var optUrl="Sys/AddParameter";
       if( this.tempdialogEditOrNew==="0")
       {
            optUrl="Sys/AddParameter";
            this.form.FatherCode=this.tempFatherCode;
            this.form.CreateBy=this.user.AccountName;
       }
       else{
           optUrl="Sys/UpdateParameter";
           this.form.UpdateBy=this.user.AccountName;
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
                        id:0,
                        Code:'',
                        CodeName:'',
                        FatherCode:'',
                        CreateTime:'',
                        CreateBy:'',
                        UpdateTime:'',
                        UpdateBy:''
                  };
                if(this.dialogEditOrNew==="1")
                {
             
                this.$api.post("Sys/GetParameterById", {  Token:this.$store.state.userInfo.Token,id:  this.tempid}, response => {
                
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
            else{
                _self.form.FatherCode=this.tempFatherCode;
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
        },
        FatherCode(val)
        {
              
        this.tempFatherCode=val;
      
        }

    },
     props: ['name','id','dialogVisible','dialogEditOrNew','FatherCode']
}
</script>