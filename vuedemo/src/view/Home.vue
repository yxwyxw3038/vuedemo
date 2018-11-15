
<template>
 <el-row class="container" style="height: 100%">
    <v-header :user="user" ></v-header>
    <el-col :span="24" class="main" style=" margin-top: 41px;" >
       <el-row>
          <el-col :span="3">
          <v-nav :navList="menu"  @ShowUrl="OpenUrl"></v-nav>

          </el-col>
     
        
          <el-col :span="21" :offset="3" style=" margin: 0px;">
            <!-- <el-tabs v-model="editableTabsValue" type="card" editable @edit="handleTabsEdit">
            <el-tab-pane
              :key="index"
               v-for="(item, index) in editableTabs"
              :label="item.title"
              :name="item.name"
            >
              {{item.content}}
            </el-tab-pane>
          </el-tabs>  -->
          <v-ifame-tabs :tabsList.sync="editableTabs" :tabsIndex.sync="editableTabsValue" ></v-ifame-tabs>
          </el-col>
     
      </el-row>
    </el-col>
 </el-row>

</template>

<script>
 import store from '../store/store.js';
 import competence from '../utils/competence.js';
 import header from '@/components/header';
 import nav from '@/components/nav';
 import ifameTabs from '@/components/ifameTabs';
  export default {
      data() {

  
      return { user:new Object(),
               menu:{},
               bj:0,
               editableTabsValue:'0',
               editableTabs: [{
          title: '首页',
          name: '0',
          content: '/Test'
        }]
  
  
 
      
      }},
     store,
     methods: {
       
      tabsSendData(tabslist,tabsindex)
      {
        this.editableTabs=tabslist;
      this.editableTabsValue=tabsindex;
      },
       OpenUrl(msg)
       {
     

          var _self = this;
           this.$api.post("User/GetMenuById", {
          Id: msg
        }, response => {
        
        
  
          console.log(response.status);
  
          if (response.status >= 200 && response.status < 300) {
  
            if (response.data) {
              var jsonData = eval("(" + response.data + ")");
  
              if (jsonData.Code == "1") {
  
                if (jsonData.Data) {
    
              
                  var info=JSON.parse(jsonData.Data);
                   var findbj=-1;
                    for (var i = 0; i < _self.editableTabs.length; i++) {
                    if(_self.editableTabs[i].name===info.Id)
                    {
                      findbj=i;
                      break;
                    }

                  }
                  if(findbj===-1)
                  {
                    var moder={
                                title: info.Name,
                                name: info.Id,
                                content: '/Test'
                              };

                        _self.editableTabs.push(moder);
                        _self.editableTabsValue=moder.name;

                  }

                 
                }

  
  
  
              } else {
  
 
                this.$message.error(jsonData.Message);
                console.log(jsonData.Message);
  
  
  
              }
  
            }
  
  
  
          } else {
            this.$message.error(response.message);
  
  
  
            console.log(response.message);
  
          }
  
        });




         


        


       }
    ,
      GetMenu()
      {
           var _self = this;
           this.$api.post("User/GetUserMenu", {
  
          userID: this.$store.state.userInfo.ID,
  

  
        }, response => {
          _self.bj=1;
           _self.menu={};
  
          console.log(response.status);
  
          if (response.status >= 200 && response.status < 300) {
  
            if (response.data) {
              var jsonData = eval("(" + response.data + ")");
  
              if (jsonData.Code == "1") {
  
                if (jsonData.Data) {
    
              
                  var info=JSON.parse(jsonData.Data);
                  _self.menu=info;
                //  console.log( _self.menu[0].Id);

                 
                }

  
  
  
              } else {
  
 
                this.$message.error(jsonData.Message);
                console.log(jsonData.Message);
  
  
  
              }
  
            }
  
  
  
          } else {
            this.$message.error(response.message);
  
  
  
            console.log(response.message);
  
          }
  
        });
      }
     },
  
  created() {
     
        this.PageQx();
        this.user=this.$store.state.userInfo;
       console.log(this.$store.state.userInfo.LoginDate);
       this.GetMenu();

    },
    components: {
      'v-header': header,
      'v-nav': nav,
      'v-ifame-tabs':ifameTabs
              
    }
  }
 </script>
