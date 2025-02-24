using QFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneUI : UIScene
{
    private UISceneWidget startButtonWidget;       
    private UISceneWidget optionButtonWidget;
    private UISceneWidget soundSetButtonWidget;
    private UISceneWidget exitButtonWidget;

    private GameObject noSoundSign;


    protected override void Start()
    {
        base.Start();
        startButtonWidget = GetWidget("StartButton");
        optionButtonWidget = GetWidget("OptionButton");
        soundSetButtonWidget = GetWidget("SoundSetButton");
        exitButtonWidget = GetWidget("ExitButton");


        if (startButtonWidget != null)
        {
            startButtonWidget.GetButton().onClick.AddListener(() =>
            {
                //StartCoroutine(GameMain.Instance.LoadScene("","Test"));
                UIManager.Instance.SetVisible("UIScene_MainScene", false);
                UIManager.Instance.SetCreateRoleVisible(true);
            });
        }
        if(optionButtonWidget != null)
        {
            optionButtonWidget.GetButton().onClick.AddListener(() =>
            {
            });
        }
        if(soundSetButtonWidget != null)
        {
            noSoundSign = InfoTools.FindChild(soundSetButtonWidget.transform, "NoSoundSign");
            soundSetButtonWidget.GetButton().onClick.AddListener(SoundSet);
        }
        if(exitButtonWidget != null)
        {
            exitButtonWidget.GetButton().onClick.AddListener(() =>
            {
                Application.Quit();
            });
        }
    }

    private void SoundSet() {
        if (BMusicManager.Instance.getBGMIsPlaying())
        {

            BMusicManager.Instance.StopBGM();
            noSoundSign.SetActive(true);
        }
        else
        {
            BMusicManager.Instance.PlayBGM("BattleMusic");
            noSoundSign.SetActive(false);
        }
    }

}
