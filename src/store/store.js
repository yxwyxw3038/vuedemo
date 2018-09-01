import Vue from 'vue';
import Vuex from 'vuex';
import userInfo_store from './userInfo_store.js'
Vue.use(Vuex);

export default new Vuex.Store({
    modules: {
        userInfo:userInfo_store
    }
});