<template>
<div id="tabsDiv">
  <el-tabs v-model="tabsindex" type="card" closable @edit="handleTabsEdit" @tab-remove="removeTab">
            <el-tab-pane
              :key="item.name"
               v-for="item in tabslist"
              :label="item.title"
              :name="item.name"
            >
            <!-- <iframe class="urliframe" :src="item.content" ></iframe>-->
            <tab-item :name="item.content" :id="item.name" :title="item.title"  ></tab-item>
            </el-tab-pane> 

    
          </el-tabs> 
</div>
</template>
    <script>
    import tabItem from '@/components/ifameTabsItem';
    export default {
       
     props: ['tabslist','tabsindex'],
     created() {
       
            
  
     }  ,
    components: {
  
        'tab-item': tabItem
    },
  
      methods: {
        removeTab(targetName) {
          if(targetName==="0")
          {
          var tabs = this.tabslist;
           var activeName = "0";
          //this.tabsindex = activeName;
          //this.tabslist = tabs.filter(tab => tab.name == targetName);
         //this.$emit('update:tabslist', this.tabslist);
         //this.$emit('update:tabsindex', this.tabsindex);
            tabs= tabs.filter(tab => tab.name == targetName);
                this.$emit('update:tabslist', tabs);
              this.$emit('update:tabsindex',activeName);
            return;
          }
          var tabs = this.tabslist;
          var activeName = this.tabsindex;
          if (activeName === targetName) {
            tabs.forEach((tab, index) => {
              if (tab.name === targetName) {
                var nextTab = tabs[index + 1] || tabs[index - 1];
                if (nextTab) {
                  activeName = nextTab.name;
                }
              }
            });
          }
          
          //this.tabsindex = activeName;
          //this.tabslist = tabs.filter(tab => tab.name !== targetName);
         // this.$emit('update:tabslist', this.tabslist);
         //this.$emit('update:tabsindex', this.tabsindex);
             tabs= tabs.filter(tab => tab.name !== targetName);
               this.$emit('update:tabslist', tabs);
               this.$emit('update:tabsindex',activeName);
        

        },
        handleTabsEdit(targetName, action) {
        if (action === 'add') {
         
        }
        if (action === 'remove') {
        
          
      
   
            
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
    
