<template>
<el-dialog :title="temptitle" :visible.sync="tempdialogVisible" :before-close="handleClose" @open="open" >
  <el-form :model="form" ref="form" :rules="rules" label-width="120px" >
     <el-row>
      <el-col :span="12">
      <el-form-item label="Id:"  prop="Id">
        <el-input v-model="form.Id"  :disabled="true" ></el-input>
    </el-form-item>
    </el-col>
     <el-col :span="12">
     <el-form-item label="单据名称:" prop="CodeName" >
         <el-input v-model="form.CodeName" ></el-input>
    </el-form-item>
    </el-col>
    </el-row>
      <el-row>
    <el-col :span="12">
      <el-form-item label="Code:"  prop="Code">
        <el-input v-model="form.Code" ></el-input>
    </el-form-item>
    </el-col>
     <el-col :span="12">
     <el-form-item label="掩码:" prop="MaskInfo" >
    
      
         <v-select-data :Code.sync="form.MaskInfo" :FatherCode="FatherCode"  ></v-select-data>
    </el-form-item>
    </el-col>
    </el-row>
    <el-row>
    <el-col :span="12">
      <el-form-item label="流水长度:"  prop="EndLength">
        <el-input v-model="form.EndLength"  type="number" min="2" max="8" ></el-input>
    </el-form-item>
    </el-col>
     <el-col :span="12">
     <el-form-item label="当前单据号:" prop="CurrentBillNo" >
         <el-input v-model="form.CurrentBillNo" :disabled="true"></el-input>
    </el-form-item>
    </el-col>
    </el-row>

     <el-row>
    <el-col :span="12">
      <el-form-item label="当前流水:"  prop="CurrentId">
           <el-input v-model="form.CurrentId" :disabled="true"></el-input>
    </el-form-item>
    </el-col>
     <el-col :span="12">
     <el-form-item label="单据号生成时间:" prop="CurrentTime" >
         <el-input v-model="form.CurrentTime" :disabled="true"></el-input>
    </el-form-item>
    </el-col>
    </el-row>


  </el-form>
  <div slot="footer" class="dialog-footer">
    <el-button @click="closeForm('form')">取 消</el-button>
    <el-button type="primary" @click="submitForm('form')">确 定</el-button>
  </div>
</el-dialog>
</template>
<script>
import store from '../../store/store.js';
import selectData from '@/components/selectData'
export default {
      data () {
        return {
        user:new Object(),
        tempid:this.id,
        temptitle:this.name,
        tempdialogVisible:this.dialogVisible,
        tempdialogEditOrNew:this.dialogEditOrNew,
        FatherCode:'BillMask',
        form: {
         Id:0,
         Code:'',
         CodeName:'',
         CurrentId:0,
         CurrentBillNo:'',
         CurrentTime:'',
         MaskInfo:'yyyyMM',
         EndLength:4,
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
            { required: true, message: '请输入单据名称', trigger: 'blur' }
         
          ],
           MaskInfo: [
            { required: true, message: '请输入掩码', trigger: 'blur' }
         
          ],
           EndLength: [
            { required: true, message: '请输入流水长度', trigger: 'blur' }
         
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
               
       var optUrl="Sys/AddBillNo";
    
       if( this.tempdialogEditOrNew==="0")
       {
            optUrl="Sys/AddBillNo";
            this.form.CreateBy=this.user.AccountName;
       }
       else{
           optUrl="Sys/UpdateBillNo";
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
                       Id:0,
                        Code:'',
                        CodeName:'',
                        CurrentId:0,
                        CurrentBillNo:'',
                        CurrentTime:'',
                        MaskInfo:'yyyyMM',
                        EndLength:4,
                        CreateTime:'',
                        CreateBy:'',
                        UpdateTime:'',
                        UpdateBy:''
                  };
                if(this.dialogEditOrNew==="1")
                {
             
                this.$api.post("Sys/GetBillNoById", {  Token:this.$store.state.userInfo.Token,Id:  this.tempid}, response => {
                
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
     props: ['name','id','dialogVisible','dialogEditOrNew'],
       components: {
             'v-select-data':selectData,
      
    }
}
</script>