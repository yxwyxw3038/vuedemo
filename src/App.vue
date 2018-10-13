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
   height: 100%;
   width: 100%;

}

#homeMainLayout{
   margin-top: 36px;
}
#homeTabLayout{
   margin: 0px;

}
#menuDiv
{
    
    height: 100%;
    min-height: 800px;
    background:  rgb(229, 237, 242);
    width: 100%;
    border: 1px solid #eee;
}
.submenuDiv
{
  
    height: 100%;
    width: 100%;
    text-align: left;
    background:  rgb(229, 237, 242);
}
#tabsDiv
{
    
    height: 100%;
    min-height: 800px;
    width: 100%;
    background:  rgb(246, 252, 255);
}
#buttongroup
{
   background:  rgb(246, 252, 255);
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
  .elCollapseDiv
  {
       background:  rgb(246, 252, 255);
  }
 
  #searchDiv
  {
    margin: 3px;
    text-align: left;
    background:  rgb(246, 252, 255);
  }
  #searchtitle
  {
    height: 10px;
    width: 100%;
    background:  rgb(246, 252, 255);
  }
  .searchicon
  {
    margin-left: 10px;
  }
   .searchform
  {
    margin-left: 10px;
    /*background:  rgb(246, 252, 255);*/
  }
  .searchinput{
    margin-left: 5px;
    margin-right: 5px;
  }
</style>
