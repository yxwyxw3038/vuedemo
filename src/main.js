// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import App from './App'
import router from './router'
import ElementUI from 'element-ui'
import ElementUItheme from 'element-theme-chalk'
import $ from 'jquery';
import Api from  './api/myaxios'
import Api1 from  './api/myaxios1'
import store from './store/store.js'
import competence from './utils/competence.js'

//import VueWebsocket from "vue-websocket";
Vue.prototype.$api = Api
Vue.prototype.$api1 = Api1
Vue.config.productionTip = false
Vue.use(ElementUI);
Vue.use(ElementUItheme);
Vue.use(competence);
//Vue.use(VueWebsocket, "ws://localhost:8085/InfoSocket");

/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  store,
  components: { App },
  template: '<App/>'
});

