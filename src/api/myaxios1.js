import axios from 'axios';
import jsbase64 from 'js-base64';
import qs from 'qs';
let http = axios.create({
  baseURL: 'http://127.0.0.1:8092',
  //baseURL: 'http://139.199.2.151:8092',
  withCredentials: true,
  headers: {
    'Content-Type': 'multipart/form-data'
  },
  withCredentials: true
});

function apiAxios(method, url,data, params, response) {
  http({
    method: method,
    url: url,
    data:  data,
    params: params
  }).then(function (res) {
    console.log('返回成功');
    response(res);
  }).catch(function (err) {
    console.log('返回失败');
    response(err);
  })
}

export default {
  get: function (url,data, params, response) {
    return apiAxios('GET', url,data, params, response)
  },
  post: function (url,data, params, response) {
    return apiAxios('POST', url,data, params, response)
  },
  put: function (url,data, params, response) {
    return apiAxios('PUT', url,data, params, response)
  },
  delete: function (url,data, params, response) {
    return apiAxios('DELETE', url,data, params, response)
  }
}
