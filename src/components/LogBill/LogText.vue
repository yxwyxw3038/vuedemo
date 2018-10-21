<template>
<div :loading="loading">
<!-- <p v-for="(subItem,index) in logText" :key="index" >{{subItem}}</p> -->
<el-table :data="tableData"  border  :row-class-name="tableRowClassName"  @selection-change="handleSelectionChange">
  <el-table-column
      type="index"
       width="50"
       :index="indexMethod"
        label="序号" 
     fixed >
    </el-table-column>
    <el-table-column prop="LogDateTime" label="时间"  width="200px">
  
  
  
    </el-table-column>
  
    <el-table-column prop="LogType" label="类型" width="100px" >
  
  
  
    </el-table-column>

      <el-table-column prop="RunClassName" label="响应程序集" width="250px">
  
  
  
    </el-table-column>

      <el-table-column prop="LogInfo" label="日志详情" >
  
  
  
    </el-table-column>
  
  
  </el-table>
    <el-pagination
      @size-change="handleSizeChange"
      @current-change="handleCurrentChange"
      :current-page="currentPage"
      :page-sizes="[10, 20, 50, 100]"
      :page-size="currentpagesize"
      layout="total, sizes, prev, pager, next, jumper"
      :total="totalCount">
    </el-pagination>
</div>
</template>
<script>

  export default {
  
    props: ['logname'],
    data () {
      return {
         tableData: [],
         logName:'',
         logData:[],
         currentPage:1,
         currentpagesize:10,
         totalCount:400,
         loading:false
      };
    },
    mounted: function () {
      this.$nextTick(function () {
        this.$on('callMethod', function () {
          console.log('监听成功');
          this.callMethod();

        })
      })
    },
     methods: {
    indexMethod(index) {
        return index +(this.currentPage-1)*this.currentpagesize +1;
       
      },
     handleSizeChange(val) {
        console.log(`每页 ${val} 条`);
        this.currentpagesize=val;
        this.currentPage=1;
        this.totalCount=400;
        this.init();
      },
      handleCurrentChange(val) {
        console.log(`当前页: ${val}`);
        this.currentPage=val;
         this.init();
      },
      handleSelectionChange(val) {
        this.multipleSelection = val;
        if(this.multipleSelection&&this.multipleSelection.length>0)
        {
        console.log(this.multipleSelection[0].Name);
        }
      },
      tableRowClassName({row, rowIndex}) {
        if (rowIndex%2 === 0) {
          return 'warning-row';
        } else if (rowIndex%2 === 1) {
          return 'success-row';
        }
        return '';
      },
      callMethod() {
        console.log('调用成功');
        this.GetData();
      },
      myclick(event) {
        console.log(event.target.title);
        var eventtitle=event.target.title
        this.$emit('btnclick',eventtitle);
        

      },
     GetData()
      {
           var _self = this;
          _self.logData = [];
          _self.tableData = [];
           _self.loading=true;
           this.$api.post("Sys/LogRead", {
          Token:this.$store.state.userInfo.Token,
          FileName:this.logName

  
        }, response => {
        
       
  
          console.log(response.status);
           _self.loading=false;
          if (response.status >= 200 && response.status < 300) {
  
            if (response.data) {
              var jsonData = eval("(" + response.data + ")");
  
              if (jsonData.Code == "1") {
  
                if (jsonData.Data) {
                  var info = eval("(" + jsonData.Data + ")");
                  for (var i = 0; i < info.length; i++) {
                    _self.logData.push(info[i]);
                  }
                 this.init();
                }
  
              } else {
  
 
                this.$message.error(jsonData.Message);
                console.log(jsonData.Message);
  
  
  
              }
  
            }
  
  
  
          } else {
             _self.loading=false;
            this.$message.error(response.message);
  
  
  
            console.log(response.message);
  
          }
  
        });
      },
       init() {
  
        var _self = this;
        _self.tableData = [];
        _self.totalCount=_self.logData.length;
        var count= (this.currentPage-1)*this.currentpagesize;
        var endCount=count+this.currentpagesize;
        if(endCount> _self.logData.length)
        {
          endCount=_self.logData.length;
        }
        for (var i = count; i < endCount; i++) {
         _self.tableData.push(_self.logData[i]);
         }
       }
  
   
  
      
     },
    created() {
    // this.GetData();
    },
    watch:
    {
            logname(val) 
            {
               this.logName=val;
               
            }
    }
    
  };
</script>
