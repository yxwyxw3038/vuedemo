<template>
<div id="buttongroup">
<el-button-group>
<el-button size="mini"  v-for="subItem in buttonData" :key="subItem.Code"   :icon="subItem.Icon" :title="subItem.Code"  @click="myclick" :loading="buttonloading" >{{subItem.Name}} </el-button>

</el-button-group>
</div>
</template>
<script>
import store from '../store/store.js';
  export default {
  
     props: ['userid','menuid','loading'],
    data () {
      return {
         buttonData:{},
         buttonloading:false
      };
    },
     store,
     methods: {
      myclick(event) {
        console.log(event.target.title);
        var eventtitle=event.target.title
        this.$emit('btnclick',eventtitle);
        

      },
     GetData()
      {
           var _self = this;
           this.$api.post("User/GetButtonByMenuIdAndUserId", {
          Token:this.$store.state.userInfo.Token,
          menuId:this.menuid,
          userId:this.userid

  
        }, response => {
        
           _self.buttonData={};
  
          console.log(response.status);
  
          if (response.status >= 200 && response.status < 300) {
  
            if (response.data) {
              var jsonData = eval("(" + response.data + ")");
  
              if (jsonData.Code == "1") {
  
                if (jsonData.Data) {
    
              
                  var info=JSON.parse(jsonData.Data);
                  _self.buttonData=info;
                  
                 
                }

  
  
  
              } else {
  
 
                this.$message.error(jsonData.Message);
                console.log(jsonData.Message);
  
  
  
              }
  
            }
  
  
  
          } else {
            this.$message.error(response.message);
  
  
  
            console.log(response.message);
  
          }
  
        });
      }
     },
    created() {
     this.buttonloading=this.loading;
     this.GetData();
    },
     watch:
    {
       
        loading(val) 
        {
            this.buttonloading=val;
        
        }
    }
  };
</script>
<style>
#buttongroup{
  height: 30px;
  width: 100%;
  text-align: left;
  margin: 0px 10px 10px 10px;
}
</style>