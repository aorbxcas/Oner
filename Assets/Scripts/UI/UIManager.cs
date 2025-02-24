using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using QFramework;

public class UIName
{
	public const string UIStandAlone = "UIScene_StandAlone";
	public const string UIRoleName = "UIScene_RoleName";
	public const string UIResetRole = "UIScene_ResetRole";
	public const string UIMessageBox = "UIScene_MessageBox";

	public const string UIRoleModel = "UIScene_RoleModel";
	public const string UIRoleInfo = "UIScene_RoleInfo";

	public const string UIPlayerInfo = "UIScene_PlayerInfo";
	public const string UIMoeny = "UIScene_Moeny";
	public const string UIOther = "UIScene_Other";
	public const string UIBattle = "UIScene_Battle";
	public const string UIMenu = "UIScene_Menu";
	public const string UIRole = "UIScene_Role";
	public const string UISkillSelect = "UIScene_SkillSelect";
	public const string UIBackpack = "UIScene_Backpack";
	public const string UIStore = "UIScene_Store";
	public const string UIMessage = "UIScene_Message";

	public const string UIBattleHead = "UIScene_BattleHead";
	public const string UIMiniMap = "UIScene_MiniMap";
	public const string UIPopup = "UIScene_Popup";
	public const string UIBattleOver = "UIScene_BattleOver";
	public const string UIBattleOption = "UIScene_BattleOption";
	public const string UIVictory =	"UIBattle_Victory";

	public const string UIMainScene = "UIScene_MainScene";
	public const string UIRegister = "UIScene_Register";
	public const string UILogin = "UIScene_Login";
//	public const string UIMessage = "UIScene_MessageBox";
}
public class UIManager : MonoSingleton<UIManager> {
	
	private Dictionary<string ,UIScene> mUIScene = new Dictionary<string, UIScene>();
	//private Dictionary<UIAnchor.Side , GameObject> mUIAnchor = new Dictionary<UIAnchor.Side, GameObject>();

	public override void OnSingletonInit()
	{
		//mUIAnchor.Clear();
		//Object[] objs = FindObjectsOfType(typeof(UIAnchor));
		//if (objs != null)
		//{
		//	foreach(Object obj in objs)
		//	{
		//		UIAnchor uiAnchor = obj as UIAnchor;
		//		if (!mUIAnchor.ContainsKey(uiAnchor.side))
		//			mUIAnchor.Add(uiAnchor.side, uiAnchor.gameObject);
		//	}
		//}
		mUIScene.Clear();
		Object[] uis = FindObjectsOfType(typeof(UIScene));
		if (uis != null)
		{
			foreach (Object obj in uis)
			{
				UIScene ui = obj as UIScene;
				ui.SetVisible(false);
				mUIScene.Add(ui.gameObject.name, ui);
			}
		}
		SetStandAloneVisible(true);

    }

	public void SetVisible (string name, bool visible)
	{
		if (visible && !IsVisible(name))
		{
			OpenScene(name);
		}
		else if (!visible && IsVisible(name))
		{
			CloseScene(name);
		}
	}

	public bool IsVisible (string name)
	{
		UIScene ui = GetUI(name);
		if (ui != null)
			return ui.IsVisible();
		return false;
	}
    private UIScene GetUI(string name)
    {
        UIScene ui;
        return mUIScene.TryGetValue(name, out ui) ? ui : null;
    }

    public T GetUI<T>(string name) where T : UIScene
    {
        return GetUI(name) as T;
    }

    private bool isLoaded(string name)
	{
		if (mUIScene.ContainsKey(name))
		{
			return true;
		}
		return false;
	}

	private void OpenScene(string name)
	{
		if (isLoaded(name))
		{
			mUIScene[name].SetVisible(true);
		}
	}
	private void CloseScene(string name)
	{
		if (isLoaded(name))
		{
			mUIScene[name].SetVisible(false);
		}
	}

	//显示开始游戏场景一级界面
	public void SetStandAloneVisible(bool visible)
	{
		SetVisible(UIName.UIMainScene, visible);
	}

	//显示创建角色场景
	public void SetCreateRoleVisible(bool visible)
	{
		SetVisible(UIName.UIRoleModel, visible);
		SetVisible(UIName.UIRoleInfo, visible);
	}

	//显示主城场景一级界面
	public void SetMainCityVisible(bool visible)
	{
		SetVisible(UIName.UIPlayerInfo, visible);
		SetVisible(UIName.UIMoeny, visible);
		SetVisible(UIName.UIOther, visible);
		SetVisible(UIName.UIMenu, visible);
	}

	//显示战斗场景一级界面
	public void SetBattleVisible(bool visible)
	{
		SetVisible(UIName.UIBattleHead, visible);
		SetVisible(UIName.UIMiniMap, visible);
		SetVisible(UIName.UIBattleOption, visible);
	}

	//显示登录场景一级界面
	public void SetLoginVisible(bool visible)
	{
		SetVisible(UIName.UIMainScene, visible);
	}

}
