
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
      
        }],
        websocket: null,
        websocketmsg:'',
        websocketdata:'',
        websocketstatus:false
 
      
      }},
     store,

  mounted() {
    if ('WebSocket' in window) {
      
     // this.initWebSocket();
     // this.timerRun();
    } else {
     
        this.$message.error('当前浏览器 Not support websocket');
    }
    },


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
      },
      initWebSocket() {
        this.websocket = new WebSocket('ws://127.0.0.1:8085/InfoSocket');
      //连接错误
      this.websocket.onerror = this.setErrorMessage

      // //连接成功
      this.websocket.onopen = this.setOnopenMessage

      //收到消息的回调
      this.websocket.onmessage = this.setOnmessageMessage

      //连接关闭的回调
      this.websocket.onclose = this.setOncloseMessage

      //监听窗口关闭事件，当窗口关闭时，主动去关闭websocket连接，防止连接还没断开就关闭窗口，server端会抛异常。
      window.onbeforeunload = this.onbeforeunload
      this.websocketstatus=true;
     

    },
    setErrorMessage() {
      this.websocketdata = "WebSocket连接发生错误" + '   状态码：' + this.websocket.readyState;
      console.log("WebSocket连接发生错误");
        this.websocketstatus=false;
    },
    setOnopenMessage() {
      this.websocketdata = "WebSocket连接成功" + '   状态码：' + this.websocket.readyState;
      //this.$message.error(this.websocketdata);
      //this.$message.error('aaa');
      console.log(this.websocketdata);
        this.websocketstatus=true;
    },
    setOnmessageMessage(event) {
      this.websocketdata = '服务端返回：' + event.data;
      console.log(this.websocketdata);
     
    },
    setOncloseMessage() {
      //this.websocketdata = "WebSocket连接关闭" + '   状态码：' + this.websocket.readyState;
      console.log("WebSocket连接关闭");
      //this.$nextTick(function () {
       setTimeout(this.initWebSocket, 15000);
        // })
    },
    onbeforeunload() {
      this.closeWebSocket();
    },
    timerRun()
    {
      
    setInterval(this.timerSend, 30000);
     
    },
    timerSend()
    {
      if(this.websocketstatus===true )
      {
        try{
              
              this.websocketmsg = 'timerSend';
              this.send();
        }
        catch(e){
            this.$message.error(e.Message);
        }
      }
      
    },
    //websocket发送消息
    send() {
      this.websocket.send(this.websocketmsg);
      this.websocketmsg = '';
    },
    closeWebSocket() {
      this.websocket.close()
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
