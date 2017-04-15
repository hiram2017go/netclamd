# netclamd

通过.net 实现Clamd的杀毒操作

string result = netClamd.SendSimpleCmdAndGetRes("PING"); 返回结果为PONG，则clamd安装正常，socket连接正常，可进行下一步操作。

SendCmdAndGetRes 返回文件夹下所有文件的杀毒情况，通过列表显示，无问题则返回OK，有问题返回问题名称。
