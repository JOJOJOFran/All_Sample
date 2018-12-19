[TOC]

# Vue简介

## 介绍

首先vue是mvvm框架结构的前端框架，什么是mvvm框架？

<http://www.ruanyifeng.com/blog/2015/02/mvcmvp_mvvm.html> 

虽然没有完全遵循 MVVM 模型，但是 Vue 的设计也受到了它的启发。因此在文档中经常会使用 vm (ViewModel 的缩写) 这个变量名表示 Vue 实例。

### vue实例

新建一个实例：

```js
var vm =new Vue({
    //选项
}) 
```

创建Vue实例时，可以传入一个选项对象。一个Vue应用可以有一个new Vue创建的根实例组成，也可以由可选的嵌套的，可复用的组件树组成。

你需要明白所有的 Vue 组件都是 Vue 实例，并且接受相同的选项对象 (一些根实例特有的选项除外)

### 数据与方法

vue的数据变动会触发绑定视图的刷新，如：

```js
// 我们的数据对象
var msg = { a: 1 }

// 该对象被加入到一个 Vue 实例中
var vm = new Vue({
  data: msg
})

// 获得这个实例上的属性
// 返回源数据中对应的字段
vm.a == data.a // => true

// 设置属性也会影响到原始数据
vm.a = 2
data.a // => 2

// ……反之亦然
data.a = 3
vm.a // => 3
```

但是，只有创建时存在的属性，才是响应式的。

```js
vm.b = 'hi' //b不会触发更新
```

Object.freeze()阻止属性响应触发视图刷新：

```js
//创建数据对象obj
var obj=new {foo:'bar'}

Object.freeze(obj);

new Vue({
   el:'#app',
   data:obj
});
```

除了数据属性，Vue 实例还暴露了一些有用的实例属性与方法。它们都有前缀 `$`，以便与用户定义的属性区分开来。例如：

```js
var data = { a: 1 }
var vm = new Vue({
  el: '#example',
  data: data
})

vm.$data === data // => true
vm.$el === document.getElementById('example') // => true

// $watch 是一个实例方法
vm.$watch('a', function (newValue, oldValue) {
  // 这个回调将在 `vm.a` 改变后调用
})
```

api地址：https://cn.vuejs.org/v2/api/#%E5%AE%9E%E4%BE%8B%E5%B1%9E%E6%80%A7

### 生命周期

创建Vue实例时，会经历一系列过程，如：设置数据监听，编译模板，将实例挂载到DOM并在数据变化时更新DOM等。

也会运行一些叫“生命周期钩子”的函数，给用户提供在不同阶段添加自己的代码的机会。

钩子函数有如：

beforeCreate,Create,beforeMount,mounted,beforeUpdate,update,beforeDestroy,destroyed

#### Tips

生命周期钩子函数的 `this` 上下文指向调用它的 Vue 实例

#### Tips

不要在选项属性或回调上使用[箭头函数](https://developer.mozilla.org/zh-CN/docs/Web/JavaScript/Reference/Functions/Arrow_functions)，比如 `created: () => console.log(this.a)` 或 `vm.$watch('a', newValue => this.myMethod())`。因为箭头函数是和父级上下文绑定在一起的，`this` 不会是如你所预期的 Vue 实例，经常导致 `Uncaught TypeError: Cannot read property of undefined` 或 `Uncaught TypeError: this.myMethod is not a function` 之类的错误。

#### 图示：

![Vue å®ä¾çå½å¨æ](https://cn.vuejs.org/images/lifecycle.png)

### 引用

#### vue.js文件：

开发版：https://vuejs.org/js/vue.js

生产版：https://vuejs.org/js/vue.min.js

#### CDN引用:

```js
<script src="https://cdn.jsdelivr.net/npm/vue@2.5.21/dist/vue.js"></script>  
```

你可以在 [cdn.jsdelivr.net/npm/vue](https://cdn.jsdelivr.net/npm/vue/) 浏览 NPM 包的源代码。

Vue 也可以在 [unpkg](https://unpkg.com/vue@2.5.21/dist/vue.js) 和 [cdnjs](https://cdnjs.cloudflare.com/ajax/libs/vue/2.5.21/vue.js) 上获取 (cdnjs 的版本更新可能略滞后)。

请确认了解[不同构建版本](https://cn.vuejs.org/v2/guide/installation.html#%E5%AF%B9%E4%B8%8D%E5%90%8C%E6%9E%84%E5%BB%BA%E7%89%88%E6%9C%AC%E7%9A%84%E8%A7%A3%E9%87%8A)并在你发布的站点中使用**生产环境版本**，把 `vue.js` 换成 `vue.min.js`。这是一个更小的构建，可以带来比开发环境下更快的速度体验。

## XXXX

环境：node.js 

包管理工具：npm (windows安装node会自带)

脚手架工具：vue-cli

### node的安装

#### windows安装

下载地址：https://nodejs.org/en/

然后一直下一步就好了。

#### linux安装

1.直接使用下载好的包

```shell
 wget https://nodejs.org/dist/v10.9.0/node-v10.9.0-linux-x64.tar.xz    // 下载
 tar xf  node-v10.9.0-linux-x64.tar.xz       // 解压
 cd node-v10.9.0-linux-x64/                  // 进入解压目录
 ./bin/node -v                               // 执行node命令 查看版本
v10.9.0
```

2.源码安装

参考地址：http://www.runoob.com/nodejs/nodejs-install-setup.html

### vue-cli安装

```shell
#npm安装
npm i -g @vue/cli 

#yarn安装,facebook?提供的管理器
#先安装yarn
npm i -g yarn
#安装cli
yarn global add @vue/cli
```



### 常用命令

```shell
node -v #查看node版本
npm -v  #查看npm版本
npm i -g @vue/cli  #npm安装vue-cli
npm i -g yarn #安装yarn
```

## 启动

#### 构建新项目

```shell
vue create myapp
```

#### 项目结构
```shell

├── node_modules     # 项目依赖包目录
├── public
│   ├── favicon.ico  # ico图标
│   └── index.html   # 首页模板
├── src 
│   ├── assets       # 样式图片目录
│   ├── components   # 组件目录
│   ├── views        # 页面目录
│   ├── App.vue      # 父组件
│   ├── main.js      # 入口文件
│   ├── router.js    # 路由配置文件
│   └── store.js     # vuex状态管理文件
├── .gitignore       # git忽略文件
├── .postcssrc.js    # postcss配置文件
├── babel.config.js  # babel配置文件
├── package.json     # 包管理文件
└── yarn.lock        # yarn依赖信息文件
```

#### 运行项目

```shell
#启动
npm run serve
#or
yarn serve
```

# npm和yarn

## 什么是npm和yarn

npm全称，Node Package Manager,顾名思义就是node包管理工具，官方地址是： www.npmjs.com 。

yarn,是由facebook官方提供的开源包管理工具，具有速度快，安全性高，可靠性强等主要优势。

### npm常用命令

```shell
#生成package.json文件(需要手动配置)
npm init

#生成package.json文件（默认配置)
npm init -y

#一键安装package.json下的依赖包(指定目录下执行)
npm i

#在项目中安装指定依赖包（配置在dependencies）
npm i xxx

#在项目中安装指定依赖包（配置在dependencies）
npm i xxx --save

#在项目中安装指定依赖包（配置在devDependencies）
npm i xxx --save-dev

#全局安装包名为xxx的依赖包
npm i -g xxx

#运行package.json中scripts下的命令
npm run xxx

#打开xxx包的主页
npm home xxx

#打开xxx包的代码仓库
npm repo xxx

#将当前模块发布到npmjs.com，需要先登录
npm publish
```

### yarn常用命令

```shell
# 生成 package.json 文件（需要手动选择配置）
yarn init

# 生成 package.json 文件（使用默认配置）
yarn init -y

# 一键安装 package.json 下的依赖包
yarn

# 在项目中安装包名为 xxx 的依赖包（配置在 dependencies 下）,同时 yarn.lock 也会被更新
yarn add xxx

# 在项目中安装包名为 xxx 的依赖包（配置在配置在 devDependencies 下）,同时 yarn.lock 也会被更新
yarn add xxx --dev

# 全局安装包名为 xxx 的依
yarn global add xxx

# 运行 package.json 中 scripts 下的命令
yarn xxx

# 列出 xxx 包的版本信息
yarn outdated xxx

# 验证当前项目 package.json 里的依赖版本和 yarn 的 lock 文件是否匹配
yarn check

# 将当前模块发布到 npmjs.com，需要先登录
yarn publish
```

### vue-cli常用命令

```shell
vue create myapp
vue add @vue/eslint
vue add router
vue add vuex
```



## package.json

我们可以看到项目的最外层目录都会有一个package.json的配置文件，它会告诉我们包管理了哪些文件。

详细介绍地址： https://docs.npmjs.com/files/package.json

我们可以看到大概有这些内容：

```javascript
{
  "name": "myapp",                               //
  "version": "0.1.0",
  "private": true,
  "scripts": {
    "serve": "vue-cli-service serve",
    "build": "vue-cli-service build",
    "lint": "vue-cli-service lint"
  },
  "dependencies": {
    "vue": "^2.5.17",
    "vue-class-component": "^6.0.0",
    "vue-property-decorator": "^7.0.0",
    "vue-router": "^3.0.1",
    "vuex": "^3.0.1"
  },
  "devDependencies": {
    "@vue/cli-plugin-babel": "^3.2.0",
    "@vue/cli-plugin-typescript": "^3.2.0",
    "@vue/cli-service": "^3.2.0",
    "lint-staged": "^7.2.2",
    "node-sass": "^4.9.0",
    "sass-loader": "^7.0.1",
    "typescript": "^3.0.0",
    "vue-template-compiler": "^2.5.17"
  },
  "postcss": {
    "plugins": {
      "autoprefixer": {}
    }
  },
  "browserslist": [
    "> 1%",
    "last 2 versions",
    "not ie <= 8"
  ],
  "gitHooks": {
    "pre-commit": "lint-staged"
  },
  "lint-staged": {
    "*.ts": [
      "vue-cli-service lint",
      "git add"
    ],
    "*.vue": [
      "vue-cli-service lint",
      "git add"
    ]
  }
}

```



首先它必须是json（如上面），而不是js对象。

name:程序的名称，必须的，它会和version组成唯一的标识，以方便识别和发布。

version:版本号。

description:描述你的程序包，方便别人来查找它。

private:如果你的private值是true,npm将拒绝你去publish它。

scripts:npm执行的命令对应的脚本，比如 npm run serve 就是对应serve后面的命令。

dependencies  ：生产环境依赖包

devDependencies  :开发环境依赖包

browserslist:第三方插件配置，这个是针对浏览器优化做出选择，也可以单独写在.browsersliststrc的文件中



# Webpack 

官网： https://www.webpackjs.com/concepts/
