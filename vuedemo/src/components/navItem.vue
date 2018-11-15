<template>
<div class="submenuDiv" >
    <el-submenu v-if="item.Node && item.Node.length" :index="item.Id"> 
        <!-- 创建菜单分组 -->
        <template slot="title">
            <i :class="item.Icon"></i>
            
          <span>  {{ item.MenuName }}</span>
            </template>
        <!-- 递归调用自身，直到subItem不含有子节点 -->
        <nav-item v-for="(subItem,i) in item.Node" :key="navIndex+'-'+i" :navIndex="navIndex+'-'+i"  :item="subItem" >
        </nav-item>
    </el-submenu>

    <el-menu-item v-else :index="item.Id"> <i :class="item.Icon"></i>{{ item.MenuName }}</el-menu-item>
    </div>
</template>
<script>

    export default {
        // 递归组件必须有name
        name: 'navItem',
        props: ['item','navIndex'],
      
    }
</script>
<style>
.submenuDiv
{
  
    height: 100%;
    width: 100%;
    text-align: left;
}
span
{
    text-align: left;
}
</style>