<template>
<div id="map-container"> </div>
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
    
      init() {
  
        var _self = this;
        
  
      }
  
    }
  
  };
</script>
<style>
  
</style>
