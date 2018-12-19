var app=new Vue({
    el:'#app',
    data:{
        message:"Hello World by vue!"
    }
});

var app2=new Vue({
    el:"#app2",
    data:{
        msg:"页面加载于"+new Date().toLocaleDateString()
    }
});

var app3=new Vue({
    el:"#app3",
    data:{
        seen:true
    }
});

var app4=new Vue({
    el:"#app4",
    data:{
        todos:[
            {text:'学习数据结构算法'},
            {text:'学习Dotnet Core'},
            {text:'学习Vue'},
            {text:'学习Go'}
        ]
    }
});

var app5=new Vue({
    el:"#app5",
    data:{
       message:"Hello Vue,js!"
    },
    methods:{
        reverMessage:function()
        {
            this.message="消息被改变了！"
        }
    }

});

var app6=new Vue({
    el:"#app6",
    data:{
        msg:'Hello Vue!'
    }
});