// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 2.0.50727.1433
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace QFramework.SaoLei
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.UI;
    
    
    // Generate Id:41f8712b-538d-4287-b31d-51acfcc2ef9a
    public partial class GameEndUIPanel
    {
        
        public const string NAME = "GameEndUIPanel";
        
        [SerializeField()]
        public UnityEngine.UI.Text GameEndResultText;
        
        [SerializeField()]
        public UnityEngine.UI.Button GameEndReplayBtn;
        
        [SerializeField()]
        public UnityEngine.UI.Button GameEndBackBtn;
        
        private GameEndUIPanelData mPrivateData = null;
        
        public GameEndUIPanelData mData
        {
            get
            {
                return mPrivateData ?? (mPrivateData = new GameEndUIPanelData());
            }
            set
            {
                mUIData = value;
                mPrivateData = value;
            }
        }
        
        protected override void ClearUIComponents()
        {
            GameEndResultText = null;
            GameEndReplayBtn = null;
            GameEndBackBtn = null;
            mData = null;
        }
    }
}
