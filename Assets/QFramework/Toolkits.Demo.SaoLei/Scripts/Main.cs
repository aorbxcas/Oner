using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.SaoLei
{
	public class Main : MonoBehaviour {

		/// <summary>
		/// Awake is called when the script instance is being loaded.
		/// </summary>
		void Awake()
		{
			ResKit.Init();
		}
		// Use this for initialization
		void Start () {
			UIKit.Root.SetResolution(1080,1334,0);
			UIKit.OpenPanel<GameStartUIPanel>();
		}
		
		// Update is called once per frame
		void Update () {
			
		}
	}

}
