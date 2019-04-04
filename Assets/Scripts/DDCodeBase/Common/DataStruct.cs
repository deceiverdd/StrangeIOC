// 指令系统
public class CommandSys
{
    public NetWorkCommand cmd = NetWorkCommand.Access;
    public string softID = null;
    public string userID = null;
    public string value = null;

    public CommandSys()
    {

    }

    public CommandSys(NetWorkCommand command, string softID, string userID, string value)
    {
        this.cmd = command;
        this.softID = softID;
        this.userID = userID;
        this.value = value;
    }
}

// 玩家用户信息
public class UserInfo
{
    // 图片信息通过获取PNG图片文件流byte[]，
    // 通过Convert.ToBase64String()转换为string类型字符串。
    public string nameImage; // 签名
    public string photoImage; // 大头贴
    public int age; // 年龄
    public string nationality; // 国籍
    public string gender; // 性别
    public string weixin; //微信

    public UserInfo()
    {

    }

    public UserInfo(string nameImage, string photoImage, int age, string nationality, string gender, string weixin)
    {
        this.nameImage = nameImage;
        this.photoImage = photoImage;
        this.age = age;
        this.nationality = nationality;
        this.gender = gender;
        this.weixin = weixin;
    }
}

// 指令枚举
public enum NetWorkCommand
{
    Access,        // 请求接入指令
    GamePlay,      // 开始游戏
    GameStop,      // 停止游戏
    UserIntegral,  // 用户积分
    UserInfo,      // 用户信息
    GetUserInfo,   // 获取用户信息
    Notice         // 通知信息
}

//当前客户机连接状态
public enum ConnectState
{
    offline,//离线
    connecting,//连接中
    online,//在线
    disconnecting//断开连接中
}
