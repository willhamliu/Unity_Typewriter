using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public const string testString = "你好，^<color=green>谢</color>^^<color=green>谢</color>^，小笼包，再见！";
    public Text showText;
    void Start()
    {
        Typewriter.instance.Init(showText, 0.1f, ShowFinish);
        Typewriter.instance.StartPrint(testString);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Typewriter.instance.ImmediatePrint();
        }
    }

    public void ShowFinish()
    {
        Debug.Log("显示结束");
    }
}
