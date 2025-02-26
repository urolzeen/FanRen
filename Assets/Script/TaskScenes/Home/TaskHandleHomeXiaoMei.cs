using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//获取3个红浆果任务
public class TaskHandleHomeXiaoMei : ITaskHandle
{

    public const int ROLE_ID = 2;

    public override Queue<TalkContentItemModel> TriggerTaskTalkData(int taskId)
    {
        Debug.Log("TriggerTaskTalkData taskId : " + taskId);
        Queue<TalkContentItemModel> allTalkContent = new Queue<TalkContentItemModel>();
        TalkContentItemModel talkContentItemModel = new TalkContentItemModel
        {
            dfAvatar = "xiaoMei",
            dfName = "小妹",
            dfTalkContent = "立哥哥，我想吃红浆果！"
        };
        allTalkContent.Enqueue(talkContentItemModel);
        talkContentItemModel = new TalkContentItemModel
        {
            dfAvatar = "hanLi",
            dfName = "韩立",
            dfTalkContent = "好，等我回来！"
        };
        allTalkContent.Enqueue(talkContentItemModel);
        return allTalkContent;
    }

    public override Queue<TalkContentItemModel> InProgressTaskTalkData(int taskId)
    {
        Debug.Log("InProgressTaskTalkData taskId : " + taskId);
        return TriggerTaskTalkData(taskId);
    }

    public override Queue<TalkContentItemModel> SubmitTaskTalkData(int taskId)
    {
        Debug.Log("SubmitTaskTalkData taskId : " + taskId);
        Queue<TalkContentItemModel> allTalkContent = new Queue<TalkContentItemModel>();
        TalkContentItemModel talkContentItemModel = new TalkContentItemModel
        {
            dfAvatar = "hanLi",
            dfName = "韩立",
            dfTalkContent = "小妹我回来了，看！红浆果！"
        };
        allTalkContent.Enqueue(talkContentItemModel);
        talkContentItemModel = new TalkContentItemModel
        {
            dfAvatar = "xiaoMei",
            dfName = "小妹",
            dfTalkContent = "谢谢立哥哥！"
        };
        allTalkContent.Enqueue(talkContentItemModel);
        return allTalkContent;
    }

    public override Queue<TalkContentItemModel> GeneralTalkData()
    {
        Debug.Log("GeneralTalkData");
        MyDBManager.GetInstance().ConnDB();
        RoleTask roleTask = MyDBManager.GetInstance().GetRoleTask(2); //2是小妹的唯一任务id，查文档（数据库）可知
        Queue<TalkContentItemModel> allTalkContent = new Queue<TalkContentItemModel>();
        if (roleTask.taskState == (int)FRTaskState.Finished) //任务已经完成
        {
            TalkContentItemModel talkContentItemModel = new TalkContentItemModel
            {
                dfAvatar = "xiaoMei",
                dfName = "小妹",
                dfTalkContent = "谢谢立哥哥！"
            };
            allTalkContent.Enqueue(talkContentItemModel);
        }
        return allTalkContent;
    }

    public override bool IsTriggerable(int taskId)
    {
        Debug.Log("IsTriggerable");
        MyDBManager.GetInstance().ConnDB();
        return MyDBManager.GetInstance().GetRoleTask(taskId).taskState == (int)FRTaskState.Untrigger;
    }

    public override bool IsSubmitable(int taskId)
    {
        Debug.Log("IsSubmitable");
        MyDBManager.GetInstance().ConnDB();
        RoleItem roleItem = MyDBManager.GetInstance().GetRoleItemInBag(2); //2是红浆果  
        return roleItem.itemCount >= 3;
    }

    public override void OnSubmitTaskComplete(int taskId)
    {
        Debug.Log("红浆果数量-3，心境+1");
        MyDBManager.GetInstance().ConnDB();
        RoleItem roleItem = MyDBManager.GetInstance().GetRoleItemInBag(2);
        MyDBManager.GetInstance().DeleteItemInBag(2, 3, roleItem.itemCount);
    }

    public override void OnTriggerTask(int taskId)
    {
        Debug.Log("需要收集红浆果数量*3");
    }
}
