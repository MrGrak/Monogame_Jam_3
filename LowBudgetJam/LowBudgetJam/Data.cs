using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace LowBudgetJam
{
    public static class Data
    {
        public static GraphicsDeviceManager GDM;
        public static ContentManager CM;
        public static SpriteBatch SB;
        public static Game1 GAME;

        public static int GameWidth = 800;
        public static int GameHeight = 500;

        public static float Layer_Bkg = 0.999999f; //furthest layer
        public static float Layer_Particles = 0.800000f;
        public static float Layer_Face = 0.100000f;
        public static float Layer_UI = 0.000005f;
        public static float Layer_Debug = 0.000001f; //closest layer
    }
}
