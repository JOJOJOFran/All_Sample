[TOC]

# Docker概念解析及操作手册

## 理论概念

### 简单理解基础定义

#### Docker是什么？

#### Docker的组成:五个核心资源和两个核心组件
 Docker有如下的优势：
 - 确保一致的运行环境
 - 更高效的系统资源利用
 - 更快的启动时间
 - 更轻松的部署和迁移
 - 更轻松的维护和扩展

##### 核心资源和核心组件
首先我对Docker的理解，是将它分为核心资源和核心组件，措辞可能不够恰当:

- 核心资源：Docker主要操作和管理的东西，也是Docker提供容器服务的重要基础概念，一切操作的对象主要针对它们
- 核心组件：运行Docker服务，和通过调用Docker内部接口管理Docker的命令的组件，通常由Docker CE(Docker Engine)来提供

首先来说两个核心组件：
- Docker Daemon：在操作系统的内核上以静默形式为Docker提供容器管理，编排，以及镜像，容器，网络，资料卷等资源操作的服务，并暴露一系列Restful API给外部提供操作的接口。
- Docker CLI:调用Daemon暴露的接口，提供给用户命令行工具的程序

然后是五个核心资源：

- 镜像（images）:包含虚拟环境最原始的文件系统内容以及对应容器所需和依赖的内容，可以理解成对容器的一种持久化的备份。每次修改镜像都是，在原镜像基础上叠加构建一个新的镜像，无法对原镜像进行实质意义上的修改。
- 容器 （container）：容器可以理解为一份镜像文件实例化运行在Deamon上，并且将镜像中的虚拟环境及内容运行和包裹，隔离在其中。
- 网络 （network）：我们可以对每个容器进行网络的配置，同时与其他网络环境进行隔离。更重要的是，我们可以容器间建立虚拟网络，可以形成容器间的服务访问和组合，使得容器化应用技术更加健壮。
- 数据卷（data volume）：Docker内部采用容器内部的文件资源，Docker如果挂掉了，我们可以快速的重启它，但是，一些需要持久化的数据文件是无法相应恢复的。虚拟机也是如此，虚拟机提供了挂载文件的方式，但是虚拟机挂载文件却有很多问题，比如系统的兼容性等问题。Docker通过UFS文件技术，也提供了多种方式来进行数据共享和持久化（数据库貌似不太适合使用docker,这里的争议我也没弄清楚，需谨慎），我们都称它为数据卷。
- 镜像仓库（Respository）：存放docker镜像的仓库，想象以下我们把所有的docker都存在云端的仓库，无论何时何地只要有网络，我们可以通过命令从仓库中拿到需要的镜像去部署去运行，是多么的方便。

### 深入理解概念原理

### 相关引申概念

## 操作手册

### 基础操作

#### Windows上安装

win上Docker Desktop for windows的安装没有什么好说的，下载地址：https://www.docker.com/products/docker-desktop

#### Linux上安装和卸载

Centos: https://docs.docker.com/install/linux/docker-ce/centos/#uninstall-old-versions

```shell
#安装命令
yum -y install docker-engine
#但是上面这个可能已经是老版本了，现在docker改为docker-ce
#所以先卸载以前的以及相关依赖
#保留/var/lib/docker的内容，包括images,container,volumes,networks
$ sudo yum remove docker \
                  docker-client \
                  docker-client-latest \
                  docker-common \
                  docker-latest \
                  docker-latest-logrotate \
                  docker-logrotate \
                  docker-selinux \
                  docker-engine-selinux \
                  docker-engine
#安装docker ce
#使用仓库安装,直接看文档吧
#安装docker ce
$ sudo yum install docker-ce
#安装指定颁布的docker ce
$ sudo yum install docker-ce-<VERSION STRING>
# 如 docker-ce-18.03.0.ce.
$ sudo yum install docker-ce-18.03.0.ce

#卸载Docker (Uninstall the Docker package)
$ sudo yum remove docker-ce
#镜像，容器，资料卷和自定义的配置文件宿主机上不会自动删除，如需删除
$ ###sudo #r#m #-#r#f /var/lib/docker 避免使用。。。。别删错了

```

#### 启动Docker服务

```shell
sudo systemctl start docker
sudo systemctl enable docker
```

#### 更改Daemon

```shell
#etc下找到docker目录
#然后修改daemon
vim daemon.json
#粘贴下面的内容,具体根据自己所有的镜像加速地址改：
{
"registry-mirrors": [
"https://d9fa0o6f.mirror.aliyuncs.com"
],
"insecure-registries": [],
"debug": true,
"experimental": false
}

```

#### DockerFile

Docker可以通过读取对应的Dockerfile去构建镜像。Docerfile是一个包含了能调用的拼装镜像的命令的文本文件，全名就是以Dockerfile（无后缀）放在需构建项目的根目录下

#### Docker-Compose

##### 什么是Docker-Compose

它是一个定义并运行多个容器的一个

### 操作实践记录

#### .net core api构建成镜像

编写Dockerfile

- 项目目录下新建Dockerfile文件
- 编写dockerfile

```dockerfile
From microsoft/aspnetcore-build as build-env
WORKDIR /code
COPY *.csproj ./
RUN dotnet restore
COPY . ./
RUN dotnet publish -c Release -o out
	
FROM microsoft/aspnetcore
WORKDIR /app
COPY  --from=build-env /code/out ./
	
EXPOSE 5000
ENTRYPOINT [ "dotnet","ApiWithRedis.dll" ]
```

在文件目录下，执行(后面的点不能掉)

```shell
dotnet build -t fran/webapi:tag .
```



### 踩坑记录





