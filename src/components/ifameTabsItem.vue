<template>
  <component :is="comp" :menuname="compName" :menuid="compId" :menutitle="comptitle" ></component>
</template>
<script>
export default {
  data () {
    return {
      compName: this.name,
      compId:this.id,
      comptitle:this.title
      
    }
  } ,
  created() {
     
        //console.log(this.name);
       // console.log(this.id);
    
    },
 
  computed: {
    comp: function () {
     // console.log(this.compName)
      
     // return require(`@/view${this.compName}.vue`)
     // var myComponent =() => import('../view'+this.compName+'.vue');
      var myComponent = resolve => require.ensure([], () => resolve(require('../view/'+this.compName+'.vue')));
       return myComponent;
    }
  },
  props: ['name','id','title']
}
</script>