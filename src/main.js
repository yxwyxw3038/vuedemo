// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import App from './App'
import router from './router'
import ElementUI from 'element-ui'
import ElementUItheme from 'element-theme-chalk'
import Api from  './api/myaxios'
import store from './store/store.js'
import competence from './utils/competence.js'

Vue.prototype.$api = Api
Vue.config.productionTip = false
Vue.use(ElementUI);
Vue.use(ElementUItheme);
Vue.use(competence);

/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  store,
  components: { App },
  template: '<App/>'
})
