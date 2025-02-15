using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

public class PlayerUIManager : MonoBehaviour,IController
{
    public GameObject HPBar;
    public GameObject SPBar;


    private string playerID;
    private Slider HPSlider;
    private Slider SPSlider;
    private PlayerDataModel playerDataModel;


    public IArchitecture GetArchitecture()
    {
        return Oner.Interface;
    }

    void Start()
    {
        HPSlider = HPBar.GetComponent<Slider>();
        SPSlider = SPBar.GetComponent<Slider>();
        playerDataModel = this.GetModel<PlayerDataModel>();
    }

    void Update()
    {
        HPSlider.value = (float)playerDataModel.HP / (float)playerDataModel.MaxHP;
        SPSlider.value = (float)playerDataModel.SP / (float)playerDataModel.MaxSP;
    }
}
