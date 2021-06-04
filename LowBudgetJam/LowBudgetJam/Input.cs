using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LowBudgetJam
{
    public static class Input
    {
        public static MouseState currentMouseState = new MouseState();
        public static MouseState lastMouseState = new MouseState();

        public static Vector2 cursorPos_Screen = new Vector2(0, 0);


        public static void Update()
        {
            lastMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            cursorPos_Screen.X = currentMouseState.X;
            cursorPos_Screen.Y = currentMouseState.Y;
        }

        public static bool IsNewLeftClick()
        {
            return (currentMouseState.LeftButton == ButtonState.Pressed &&
                    lastMouseState.LeftButton == ButtonState.Released);
        }

        public static bool IsNewRightClick()
        {
            return (currentMouseState.RightButton == ButtonState.Pressed &&
                    lastMouseState.RightButton == ButtonState.Released);
        }
    }
}
