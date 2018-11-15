<template>
<div id="tabsDiv">
  <el-tabs v-model="tabsIndex" type="card" closable @edit="handleTabsEdit">
            <el-tab-pane
              :key="index"
               v-for="(item, index) in tabsList"
              :label="item.title"
              :name="item.name"
            >
            <!-- <iframe class="urliframe" :src="item.content" ></iframe>-->
            <tab-item :name="item.content" ></tab-item>
            </el-tab-pane> 

    
          </el-tabs> 
</div>
</template>
    <script>
    import tabItem from '@/components/ifameTabsItem';
    export default {
           props: ['tabsList','tabsIndex'],
     created() {
           console.log(this.tabsList.length);
     }  ,
    components: {
  
        'tab-item': tabItem
    },
  
      methods: {
        handleTabsEdit(targetName, action) {
        if (action === 'add') {
         
        }
        if (action === 'remove') {
          if(targetName==="0")
          {
          let tabs = this.tabsList;
           let activeName = "0";
          this.tabsIndex = activeName;
          this.tabsList = tabs.filter(tab => tab.name == targetName);
          this.$emit('update:tabsList', this.tabsList);
          this.$emit('update:tabsIndex', this.tabsIndex);
            return;
          }
          let tabs = this.tabsList;
          let activeName = this.tabsIndex;
          if (activeName === targetName) {
            tabs.forEach((tab, index) => {
              if (tab.name === targetName) {
                let nextTab = tabs[index + 1] || tabs[index - 1];
                if (nextTab) {
                  activeName = nextTab.name;
                }
              }
            });
          }
          
          this.tabsIndex = activeName;
          this.tabsList = tabs.filter(tab => tab.name !== targetName);
          this.$emit('update:tabsList', this.tabsList);
          this.$emit('update:tabsIndex', this.tabsIndex);
        }
      }
      }

    }
    </script>
    <style>
#tabsDiv
{
    
    height: 100%;
    min-height: 800px;
    width: 100%;
}
.urliframe
{
     height: 100%;
     min-height: 745px;
     width: 100%;
     border: 1px solid #eee;
}
</style>
    
