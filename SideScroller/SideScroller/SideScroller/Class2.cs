using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SideScroller
{
    class DetailedKeyboardInfo
    {
        #region Fields

        bool pressed = false;
        bool singlePress = false;
        bool longPress = false;
        bool repeatedPress = false;

        KeyboardState currentKeyboardState;
        KeyboardState oldKeyboardState; 

        #endregion 

        #region Constructor


        public DetailedKeyboardInfo(KeyboardState keyboardInfo)
        {
            //Initializes new Keyboard State
            currentKeyboardState = keyboardInfo;
        }

        #endregion 

    }
}
