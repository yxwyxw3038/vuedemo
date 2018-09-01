import Vue from 'vue'
import Router from 'vue-router'
import Login from '@/view/Login'
import Test from '@/view/Test'
import Home from '@/view/Home'
Vue.use(Router)


export default new Router({
  routes: [
   
    {
      path: '/Login',
      name: 'Login',
      component: Login
    },
    {
      path: '/Test',
      name: 'Test',
      component: Test
    },
    {
      path: '/Home',
      name: 'Home',
      component: Home
    },
    {
      path: '/*',
      name: 'Login',
      component: Login
    }
  ]
})
