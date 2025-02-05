using UnityEngine;
using QFramework;
using UnityEngine.EventSystems;
using UnityEngine.Events;
namespace QFramework.SaoLei
{
	public enum MineCellStatus
	{
		None,
		Empty,
		Number,
		ClickMine,
		MarkMine,
		MarkQuery,
		ShowMines,
		
	}
	public partial class GameMainMineCell : ViewController,IPointerClickHandler
	{
		public bool isClicked = false;
		public bool hasMine = false;
		public int rowIndex = 0;
		public int colIndex = 0;
		public int mineNumbers = 0;
		public UnityEvent leftClick;
    	public UnityEvent rightClick;
		public UnityEngine.UI.Image GameMainMineCellBg;

		public MineCellStatus currentStatus = MineCellStatus.None;
		void Start()
		{
			// Code Here
			GameMainMineCellBg = this.gameObject.GetComponent<UnityEngine.UI.Image>();
			leftClick.AddListener(new UnityAction(ButtonLeftClick));
			rightClick.AddListener(new UnityAction(ButtonRightClick));
			
		}
		public void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.button == PointerEventData.InputButton.Left)
				leftClick.Invoke();
			else if (eventData.button == PointerEventData.InputButton.Right)
				rightClick.Invoke();
		}


		private void ButtonLeftClick()
		{
			OnClickMineCell();
		}

		private void ButtonRightClick()
		{
			MarkMineCell();
		}
		
		public void SetInfo(int r,int c)
		{
			rowIndex = r;
			colIndex = c;
		}
		void OnClickMineCell()
		{
			if(isClicked)
				return;
			TypeEventSystem.Global.Send(new GameMainCellEvent()
                {
                    row = rowIndex,
					col = colIndex,
					clickType = 0
                });
		}

		void MarkMineCell()
		{
			if(isClicked)
				return;
			TypeEventSystem.Global.Send(new GameMainCellEvent()
                {
                    row = rowIndex,
					col = colIndex,
					clickType = 1
                });
		}
		//设置格子样式
		public void SetStatus(MineCellStatus status)
		{
			currentStatus = status;
			if(GameMainMineCellBg==null)
				GameMainMineCellBg = this.gameObject.GetComponent<UnityEngine.UI.Image>();

			GameMainMineCellText.text = "";
			switch(currentStatus)
			{
				case MineCellStatus.None://初始状态
					isClicked = false;
					GameMainMineCellText.color = Color.black;
					GameMainMineCellBg.color = Color.white;
				break;
				case MineCellStatus.Empty://周围没有雷
					GameMainMineCellBg.color = Color.gray;
				break;
				case MineCellStatus.Number://周围有雷
					GameMainMineCellText.text = ""+mineNumbers;
					GameMainMineCellText.color = GetNumberColor(mineNumbers);
					GameMainMineCellBg.color = Color.gray;
				break;
				case MineCellStatus.ClickMine://点到雷了
					
					GameMainMineCellText.text = "*";
					GameMainMineCellBg.color = Color.red;
				break;
				case MineCellStatus.MarkMine://标记雷
					GameMainMineCellText.text = "!";
					GameMainMineCellBg.color = Color.yellow;
				break;
				case MineCellStatus.MarkQuery://标记不确定
					GameMainMineCellText.text = "?";
					GameMainMineCellBg.color = Color.yellow;
				break;
				case MineCellStatus.ShowMines://结束时展示
					GameMainMineCellText.text = "*";
					GameMainMineCellBg.color = Color.gray;
				break;
			}
		}
		Color GetNumberColor(int number)
		{
			switch(number)
			{
				case 1:
				return new Color(0,0,1);
				case 2:
				return new Color(0,1,0);
				case 3:
				return new Color(1,0,0);
				case 4:
				return new Color(1,1,0);
				case 5:
				return new Color(1,0,1);
				case 6:
				return new Color(1,.5f,1);
				case 7:
				return new Color(1,1,1);
				case 8:
				return new Color(0,0,0);
				
			}

			return new Color(0,0,0);
		}
	}
}
