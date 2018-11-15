<template>
<div class="buttongroup">
<el-button-group>
<el-button size="mini" icon="el-icon-edit"> {{userid}}</el-button>

</el-button-group>
</div>
</template>
<script>
  export default {
  
     props: ['userid','menuid'],
    data () {
      return {
      };
    }
  };
</script>
<style>
.buttongroup{
  height: 30px;
  width: 100%;
  text-align: left;
  margin: 0px 10px 10px 10px;
}
</style>