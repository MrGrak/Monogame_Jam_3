using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace LowBudgetJam
{
    public class Screen_Game : Screen
    {


        public Text text_Counter;
        public int score;

        public int framesRemaining;


        public Screen_Game()
        {
            Name = "GAME";

            text_Counter = new Text("",
                new Vector2(60, 50),
                Color.White);

        }

        public override void Open()
        {
            ParticleSystem.Reset();
            score = 0;
            //allocate 10 seconds of playtime
            framesRemaining = 60 * 7;
        }

        public override void Close(ExitAction EA)
        {
            exitAction = EA;
            displayState = DisplayState.Closing;
        }

        public override void HandleInput()
        {
            //etc
        }

        public override void Update()
        {
            if(framesRemaining > 0)
            {   //game running
                ParticleSystem.Update();
                framesRemaining--;
            }
            else if(ScreenManager.activeScreen == this)
            {   //game complete, display score screen
                ScreenManager.AddScreen(ScreenManager.Score);
                ScreenManager.Score.Open();
                ScreenManager.activeScreen = ScreenManager.Score;
            }

            text_Counter.text = "score: " + score 
                + "\ntime remaining: " + framesRemaining;






            if (displayState == DisplayState.Opening)
            {

            }
            else if (displayState == DisplayState.Opened)
            {

            }
            else if (displayState == DisplayState.Closing)
            {

            }
            else if (displayState == DisplayState.Closed)
            {

            }


        }

        public override void Draw()
        {
            Data.SB.Begin(SpriteSortMode.Deferred,
                BlendState.AlphaBlend, SamplerState.PointClamp);

            ParticleSystem.Draw();

            text_Counter.Draw();

            Data.SB.End();
        }
    }
}