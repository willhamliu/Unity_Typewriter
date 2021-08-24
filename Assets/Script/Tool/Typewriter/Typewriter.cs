using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Typewriter: MonoBehaviour
{
    public static Typewriter instance;
    private bool isMerge;
    private bool isSplitFinish;
    private bool isSplitFinishPrint;
    private float printSpeed;
    private string textInfo;
    private int returnIndex;
    private int printCount;
    private Text showText;
    private TextMeshProUGUI showTextPro;
    private StringBuilder intactString;
    private StringBuilder mergeString;
    private StringBuilder printString;
    private List<string> textArraty;

    private Action showFinish;

    private void Awake()
    {
        instance = this;
        textArraty = new List<string>();
    }
    private void OnDestroy()
    {
        instance = null;
    }
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="text"></param>
    /// <param name="textInfo"></param>
    /// <param name="printSpeed"></param>
    public void Init(Text text, float printSpeed, Action showFinish)
    {
        this.printSpeed = printSpeed;
        this.showFinish = showFinish;
        showText = text;
        showTextPro = null;
    }
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="text"></param>
    /// <param name="textInfo"></param>
    /// <param name="printSpeed"></param>
    public void Init(TextMeshProUGUI text, float printSpeed, Action showFinish)
    {
        this.printSpeed = printSpeed;
        this.showFinish = showFinish;
        showTextPro = text;
        showText = null;
    }

    /// <summary>
    /// 立刻打印
    /// </summary>
    public void ImmediatePrint()
    {
        if (!isSplitFinish)
        {
            isSplitFinishPrint = true;
            return;
        }
        StopCoroutine("WaitPrint");
        if (showText != null)
            showText.text = intactString.ToString();
        if (showTextPro != null)
            showTextPro.text = intactString.ToString();
        showFinish.Invoke();
    }

    /// <summary>
    /// 开始打印
    /// </summary>
    public void StartPrint(string textInfo)
    {
        this.textInfo = textInfo;
        isSplitFinish = false;
        if (textArraty != null)
            textArraty.Clear();
        intactString = new StringBuilder();
        for (int i = 0; i < textInfo.Length; i++)
        {
         
            if (textInfo[i] != '^')
            {
                intactString.Append(textInfo[i]);
                if (!isMerge)
                    textArraty.Add(textInfo[i].ToString());
            }
            else
            {
                if (!isMerge)
                {
                    isMerge = true;
                    mergeString = new StringBuilder();
                }
                else
                {
                    textArraty.Add(mergeString.ToString());
                    isMerge = false;
                }
                continue;
            }
            if (isMerge)
            {
                mergeString.Append(textInfo[i].ToString());
            }
        }

        isSplitFinish = true;
        if (isSplitFinishPrint)
        {
            ImmediatePrint();
            isSplitFinishPrint = false;
        }
        else
        {
            printCount = textArraty.Count;
            printString = new StringBuilder();
            StartCoroutine("WaitPrint");
        }
    }

    private string PrintString()
    {
        return printString.Append(textArraty[returnIndex]).ToString();
    } 

    private IEnumerator WaitPrint()
    {
        while (returnIndex < printCount)
        {
            if(showText != null)
                showText.text = PrintString();
            if (showTextPro != null)
                showTextPro.text = PrintString();

            returnIndex++;
            if (returnIndex == printCount)
            {
                showFinish.Invoke();
            }
            yield return new WaitForSeconds(printSpeed);
        }
        returnIndex = 0;
    }
}
