<template>
<div id="map-container"> 
  <input class="file" name="file" type="file" accept="image/png,image/gif,image/jpeg" @change="update"/>
</div>
</template>
<script>
import store from '../store/store.js';

  export default {
  
    data() {
  
      return {
        user:new Object(),
        tableData: []
  
      };
  
    },
    store,
  
  
    created() {
      this.user=this.$store.state.userInfo;
      this.init();
  
    },
  
  
  
    methods: {
    update(e)
    {
        let file = e.target.files[0];
        let param = new FormData(); //创建form对象
        param.append('file',file);//通过append向form对象添加数据
        console.log(param.get('file')); //FormData私有类对象，访问不到，可以通过get判断值是否传进去


         this.$api1.post("File/UpLoadFile",param,{
          Token:this.$store.state.userInfo.Token,
          DirName:'测试',
          No:'B00001'
        }, response => {
 
          if (response.status >= 200 && response.status < 300) {
            if (response.data) {
               var jsonData = eval("(" + response.data + ")");
  
              if (jsonData.Code == "1") {
                this.$message.error(jsonData.Data);
              }
              else{
                this.$message.error(jsonData.Message);
              }
           
  
          }

          }
  
        });


    },

      init() {
  
        var _self = this;
        
  
      }
  
    }
  
  };
</script>
<style>
  
</style>
