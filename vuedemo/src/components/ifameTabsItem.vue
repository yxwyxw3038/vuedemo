<template>
  <component :is="comp"></component>
</template>
<script>
export default {
  data () {
    return {
      compName: this.name
    }
  },
  computed: {
    comp: function () {
      console.log(this.compName)
      
     // return require(`@/view${this.compName}.vue`)
     // var myComponent =() => import('../view'+this.compName+'.vue');
      var myComponent = resolve => require.ensure([], () => resolve(require('../view'+this.compName+'.vue')));
       return myComponent;
    }
  },
  props: ['name']
}
</script>