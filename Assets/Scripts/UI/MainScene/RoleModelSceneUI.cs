using TMPro;
using QFramework;
using UnityEngine;
using UnityEngine.UI;

public class RoleModelSceneUI : UIScene
{
    private UISceneWidget JobNameWidget;
    private UISceneWidget NameWidget;
    public Text jobText;
    protected override void Start()
    {
        base.Start();
        JobNameWidget = GetWidget("JobNameText");
        NameWidget = GetWidget("Text");
        //jobText = GetComponentsInChildren<Text>(true)[0];
    }
    public void SetJobInfo(string jobId)
    {
        if (JobNameWidget != null) {
            JobNameWidget.GetComponent<TextMeshProUGUI>().text = Configuration.GetContent("Job", "Job" + jobId);
        }
    }
}