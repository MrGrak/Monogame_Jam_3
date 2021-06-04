using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace LowBudgetJam
{
    public static class ScreenManager
    {
        public static List<Screen> screens = new List<Screen>();
        public static Screen activeScreen;

        public static Screen_Game Game = new Screen_Game();
        public static Screen_Title Title = new Screen_Title();
        public static Screen_Score Score = new Screen_Score();

        public static void AddScreen(Screen screen)
        {
            screens.Add(screen);
        }

        public static void RemoveScreen(Screen screen)
        {
            screens.Remove(screen);
        }

        public static void ExitAndLoad(Screen screenToLoad)
        {
            //remove every screen on screens list
            while (screens.Count > 0)
            { screens.Remove(screens[0]); }

            //add screen to load and open it
            AddScreen(screenToLoad);
            screenToLoad.Open();
            //set loaded screen to active ref
            activeScreen = screenToLoad;
        }

        public static void Update()
        {
            Input.Update();
            if (screens.Count > 0)
            {   //the only 'active screen' is the last one (top one)
                activeScreen = screens[screens.Count - 1];
                activeScreen.HandleInput();
                activeScreen.Update();
            }
        }

        public static void DrawActiveScreens()
        {
            Data.GDM.GraphicsDevice.SetRenderTarget(null);
            Data.GDM.GraphicsDevice.Clear(Color.Black);
            foreach (Screen screen in screens) { screen.Draw(); }
        }

    }
}