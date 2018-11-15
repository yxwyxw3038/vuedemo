<template>
        <div >
          <el-input v-show="!tempIsDisabled"  v-model="tempvalue" ></el-input>
          <div v-show="tempIsDisabled" class="cell">{{tempvalue}}</div>
         </div>
</template>
<script>
 export default {
    props: ['value','isDisabled'],
    data() {
      return {
       
        tempvalue:'',
        tempIsDisabled:false
      };
    },
      created() { 
      this.tempvalue=this.value;
      this.tempIsDisabled=this.isDisabled;
    }, 
    watch:
    {
         tempvalue(val) 
        {
            this.$emit('update:value', val);
        },
        value(val)
        {
         this.tempvalue=val;
        },
         isDisabled(val)
        {
         this.tempIsDisabled=val;
        }
    }
 };

</script>