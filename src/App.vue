<template>
  <div id="app">
    <router-view/>
  </div>
</template>

<script>
export default {
  name: 'App'
}
</script>

<style>
  body {
    height: 100%;
   
  }
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
   color: #2c3e50;
   height: 100%;
   width: 100%;

}
.spanFont
{
   color: #fff;
   font-size: 16px;
}
.onediv {
    background-color: rgb(29, 67, 95);


 
}
  .el-table .warning-row {
    background: oldlace;
  }

  .el-table .success-row {
    background: #f0f9eb;
  }
  #searchDiv
  {
    margin: 3px;
    text-align: left;
  }
  #searchtitle
  {
    height: 10px;
    width: 100%;
  }
  .searchicon
  {
    margin-left: 10px;
  }
   .searchform
  {
    margin-left: 10px;
  }
  .searchinput{
    margin-left: 5px;
    margin-right: 5px;
  }
</style>
