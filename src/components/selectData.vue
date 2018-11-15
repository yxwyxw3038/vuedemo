<template>
<el-select v-model="selectKey" filterable placeholder="请选择" size="mini">
    <el-option
      v-for="item in selectOptions"
      :key="item.value"
      :label="item.label"
      :value="item.value" size="mini" >
    </el-option>
  </el-select>
</template>
<script>
import store from '../store/store.js';
  export default {
    props: ['FatherCode','Code'],
    data() {
      return {
        user:new Object(),
        selectKey:'',
        selectOptions:[],
        selectFatherCode:''
      };
  
    }, 
    store,
    created() {
      this.user=this.$store.state.userInfo;
      this.selectKey=this.Code;
      this.selectFatherCode=this.FatherCode;
      this.GetselectOptions();
    }, 
    methods: {
    GetselectOptions()
    {
           var _self = this;
           _self.selectOptions=[];
           this.$api.post("Sys/GetParameterSelect", {  Token:this.$store.state.userInfo.Token,Code:_self.selectFatherCode}, response => {
          if (response.status >= 200 && response.status < 300) {
  
            if (response.data) {
              var jsonData = eval("(" + response.data + ")");
  
              if (jsonData.Code == "1") {
                if (jsonData.Data) {
                  var info=JSON.parse(jsonData.Data);
                  _self.selectOptions=info;
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
    watch:
    {
        Code(val)
        {
         this.selectKey=val;
        },
        selectKey(val)
        {
            this.$emit('update:Code', val);
        },
        FatherCode(val)
        {
          this.selectFatherCode=val;
        }
    }
  
  };
</script>