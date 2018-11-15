import axios from 'axios';
import qs from 'qs';
import jsbase64 from 'js-base64';
let http = axios.create({
  baseURL: 'http://127.0.0.1:8092',
  //baseURL: 'http://139.199.2.151:8092',
  withCredentials: true,
  headers: {
    'Content-Type': 'application/x-www-form-urlencoded;charset=utf-8'



  },
  transformRequest: [function (data) {
    let newData = '';
    newData = qs.stringify(data)
    let Base64 = jsbase64.Base64;
    var passwords = Base64.encode(newData);
    passwords =passwords.substr(passwords.length-1,1)+ passwords.substr(0,passwords.length-1);
    return passwords;
  }],
  withCredentials: true
});

function apiAxios(method, url, params, response) {
  http({
    method: method,
    url: url,
    data: method === 'POST' || method === 'PUT' ? params : null,
    params: method === 'GET' || method === 'DELETE' ? params : null,
  }).then(function (res) {
    console.log('返回成功');
    response(res);
  }).catch(function (err) {
    console.log('返回失败');
    response(err);
  })
}

export default {
  get: function (url, params, response) {
    return apiAxios('GET', url, params, response)
  },
  post: function (url, params, response) {
    return apiAxios('POST', url, params, response)
  },
  put: function (url, params, response) {
    return apiAxios('PUT', url, params, response)
  },
  delete: function (url, params, response) {
    return apiAxios('DELETE', url, params, response)
  }
}
