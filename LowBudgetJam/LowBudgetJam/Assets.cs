using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LowBudgetJam
{
    public static class Assets
    {
        public static Texture2D lineTexture;
        public static SpriteFont Assets_font_sm;
        public static SpriteFont Assets_font_big;

        public static Texture2D sheet_Main;

        //music
        public static SoundEffectInstance music_current;

        private static SoundEffect music_titleSrc;
        public static SoundEffectInstance music_title;
        private static SoundEffect music_gameSrc;
        public static SoundEffectInstance music_game;
        private static SoundEffect music_scoreSrc;
        public static SoundEffectInstance music_score;
        public static float fadeSpeed = 0.05f;

        //soundfx
        private static SoundEffect sfx_eatingSrc;
        public static SoundEffectInstance sfx_eating;




        public static void Constructor()
        {
            //load game fonts
            Assets_font_sm = Data.CM.Load<SpriteFont>("comicSans_12pt");
            Assets_font_big = Data.CM.Load<SpriteFont>("comicSans_24pt");

            //setup texture to use for drawing xna rectangles
            lineTexture = new Texture2D(Data.GDM.GraphicsDevice, 1, 1);
            lineTexture.SetData<Color>(new Color[] { Color.White });

            //load sprite atlas
            sheet_Main = Data.CM.Load<Texture2D>(@"sheet_main");

            /*
            //load music tracks
            music_titleSrc = Data.CM.Load<SoundEffect>(@"music_title");
            music_title = music_titleSrc.CreateInstance();
            music_gameSrc = Data.CM.Load<SoundEffect>(@"music_game");
            music_game = music_gameSrc.CreateInstance();
            music_scoreSrc = Data.CM.Load<SoundEffect>(@"music_score");
            music_score = music_scoreSrc.CreateInstance();
            */

        }

    }
}