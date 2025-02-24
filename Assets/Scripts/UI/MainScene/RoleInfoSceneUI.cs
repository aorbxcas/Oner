using UnityEngine.UI;
using TMPro;
using System.Diagnostics;
using UnityEngine;

public class RoleInfoSceneUI : UIScene
{
    RoleModelSceneUI infoRoleScene;
    private UISceneWidget[] selectorJobWidgets;
    private string jobid;
    private TextMeshProUGUI JobInfoText;
    protected override void Start()
    {
        base.Start();
        selectorJobWidgets = GetComponentsInChildren<ToggleWidget>(true);
        InitWidget();

    }

    protected override void InitWidget()
    {
        infoRoleScene = UIManager.Instance.GetUI<RoleModelSceneUI>(UIName.UIRoleModel);
        JobInfoText = GetWidget("JobInfoText").GetComponent<TextMeshProUGUI>();

        for (int i = 0; i < selectorJobWidgets.Length; i++)
        {
            Toggle mJobToggle = GetWidget("JobToggle" + (i + 1)).GetComponent<Toggle>();
            if(mJobToggle != null) mJobToggle.onValueChanged.AddListener((isOn) => UpdateJobToggle(mJobToggle, isOn));
        }
    }
    private void UpdateJobToggle(Toggle mJobToggle, bool isOn)
    {
        if (!isOn) return;
        string jobButtonName = mJobToggle.gameObject.name;
        jobid = jobButtonName.Substring(jobButtonName.Length - 1);
        //Debug.Log("jobid:" + jobid);
        if (infoRoleScene != null)
            infoRoleScene.SetJobInfo(jobid);
        SetJobInfo();
    }
    private void SetJobInfo()
    {
        JobInfoText.text = Configuration.GetContent("Job", "Description" + jobid);
    }


}
