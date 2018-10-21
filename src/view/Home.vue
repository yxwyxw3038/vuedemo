
<template>
 <el-row class="container" style="height: 100%">
    <v-header :user="user" @EditUserInfo="editUserInfo" ></v-header>
    <el-col id="homeMainLayout" :span="24" class="main" >
       <el-row id="homeRowLayout">
          <el-col :span="3">
          <v-nav :navList="menu"  @ShowUrl="OpenUrl"></v-nav>

          </el-col>
     
        
          <el-col id="homeTabLayout" :span="21" :offset="3" >
            <!-- <el-tabs v-model="editabletabsvalue" type="card" editable @edit="handleTabsEdit">
            <el-tab-pane
              :key="index"
               v-for="(item, index) in editabletabs"
              :label="item.title"
              :name="item.name"
            >
              {{item.content}}
            </el-tab-pane>
          </el-tabs>  -->
          <v-ifame-tabs :tabslist.sync="editabletabs" :tabsindex.sync="editabletabsvalue" ></v-ifame-tabs>
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
               editabletabsvalue:'0',
               editabletabs: [{
          title: '首页',
          name: '0',
          content: 'Test'
      
        }]
  
  
 
      
      }},
     store,
     methods: {
       editUserInfo()
       {
                var _self=this;
                console.log(this.$store.state.userInfo.ID);
                var titleInfo='个人信息维护';
                var idInfo='99999';
                var contentInfo='UserInfoBill'
                    var findbj=-1;
                    for (var i = 0; i < _self.editabletabs.length; i++) {
                    if(_self.editabletabs[i].name===idInfo)
                    {
                      findbj=i;
                      break;
                    }

                  }
                  if(findbj===-1)
                  {
                    var moder={
                                title: titleInfo,
                                name: idInfo,
                                content: contentInfo
                              };

                        _self.editabletabs.push(moder);
                        _self.editabletabsvalue=moder.name;

                  }
                  else{
                      _self.editabletabsvalue=idInfo;
                  }

                 
                


         
       },
      tabsSendData(tabslist,tabsindex)
      {
        this.editabletabs=tabslist;
      this.editabletabsvalue=tabsindex;
      },
       OpenUrl(msg)
       {
     
          var Token=this.GetToken();
          console.log(Token);
          var _self = this;
           this.$api.post("User/GetMenuById", { 
          Token:this.$store.state.userInfo.Token,
          Id: msg
        }, response => {
        
        
  
        //  console.log(response.status);
  
          if (response.status >= 200 && response.status < 300) {
  
            if (response.data) {
              var jsonData = eval("(" + response.data + ")");
  
              if (jsonData.Code == "1") {
  
                if (jsonData.Data) {
    
              
                  var info=JSON.parse(jsonData.Data);
                   var findbj=-1;
                    for (var i = 0; i < _self.editabletabs.length; i++) {
                    if(_self.editabletabs[i].name===info.Id.toString())
                    {
                      findbj=i;
                      break;
                    }

                  }
                  if(findbj===-1)
                  {
                    var moder={
                                title: info.Name,
                                name: info.Id.toString(),
                                content: info.LinkAddress
                              };

                        _self.editabletabs.push(moder);
                        _self.editabletabsvalue=moder.name;

                  }
                  else{
                      _self.editabletabsvalue=info.Id.toString();
                  }

                 
                }

  
  
  
              } else {
  
 
                this.$message.error(jsonData.Message);
              //  console.log(jsonData.Message);
  
  
  
              }
  
            }
  
  
  
          } else {
            this.$message.error(response.message);
  
  
  
          //  console.log(response.message);
  
          }
  
        });




         


        


       }
    ,
      GetMenu()
      {
           var _self = this;
           
           this.$api.post("User/GetUserMenu", {
          Token:this.$store.state.userInfo.Token,
          userID: this.$store.state.userInfo.ID,
  

  
        }, response => {
          _self.bj=1;
           _self.menu={};
  
         // console.log(response.status);
  
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
  
  
  
           // console.log(response.message);
  
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
