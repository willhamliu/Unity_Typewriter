# 基于Unity的打字机效果

支持富文本输出

打印时，首先会拆分字符串，当需要连接字符串时，需要在要连接的字符串两端加上^号，这样^号内的数据就不会被拆分

思路来源于https://zhuanlan.zhihu.com/p/80420673

****
方法描述

1.调用下面这个方法指定输出组件，打印速度，打印完成回调  
Typewriter.instance.Init(Text text, float printSpeed, Action showFinish); 


2.初始化完成后，调用下面这个方法，传入要打印的信息即可  
  Typewriter.instance.StartPrint(testString);
  
3.当需要立即显示所有文字时，调用方法  
Typewriter.instance.ImmediatePrint();
  
  表现效果
  
  ![image](https://github.com/willhamliu/Unity_Typewriter/blob/main/Document/GIF%202021-8-24%2023-47-53.gif)
