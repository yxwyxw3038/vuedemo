<template>
<div id="menuDiv">
    <el-menu @open="handleOpen" @close="handleClose"  @select="handleSelect">
        <nav-item v-for="(item, index) in navList"  :item="item" :navIndex="String(index)" :key="item.Id"  >
        </nav-item>
    </el-menu>
   </div>
</template>
<script>
 import navItem from '@/components/navItem';
export default {
   
    props: ['navList'],
     created() {
         //  console.log(this.navList.length);
     }  ,
    components: {
  
      'nav-item': navItem
    },
     methods: {
      handleOpen(key, keyPath) {
      //  console.log(key, keyPath);
      },
      handleClose(key, keyPath) {
       // console.log(key, keyPath);
      }
      ,
      handleSelect(key, keyPath) {
        //console.log(key, keyPath);
        this.$emit('ShowUrl',key);
      }
    }
}
</script>
<style>
#menuDiv
{
    
    height: 100%;
    min-height: 800px;
    width: 100%;
    border: 1px solid #eee;
}
</style>