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
    public class Screen_Score : Screen
    {
        Rectangle fadeRec = //fades in for screen exit
            new Rectangle(0, 0, Data.GameWidth, Data.GameHeight);
        float blackgroundAlpha = 1.0f; //starts at 100%


        public Text text_ScoreTitle;
        public Text text_Score;
        public Text text_Exit;



        public Screen_Score()
        {
            Name = "SCORE";

            text_ScoreTitle = new Text("",
                new Vector2(300 - 15, 150), Color.White);
            text_ScoreTitle.font = Assets.Assets_font_big;

            text_Score = new Text("",
                new Vector2(300 - 20, 205), Color.White);
            text_Score.font = Assets.Assets_font_big;

            text_Exit = new Text("",
                new Vector2(300, 270), Color.White);
        }

        public override void Open()
        {
            blackgroundAlpha = 0.0f;
            displayState = DisplayState.Opening;

            text_ScoreTitle.text = "YOUR SCORE:";
            text_ScoreTitle.alpha = 0.0f;

            text_Score.text = "" + ScreenManager.Game.score;
            //center score text in very hacky way
            text_Score.position.X = (int)((Data.GameWidth / 2) - (text_Score.text.Length * 10));
            text_Score.alpha = 0.0f;

            text_Exit.text = "You have fed Apos well!";
            text_Exit.text += "\nLeft click to play again";
            text_Exit.text += "\n   Right click to exit";
            text_Exit.alpha = 0.0f;
        }

        public override void Close(ExitAction EA)
        {
            exitAction = EA;
            displayState = DisplayState.Closing;
        }

        public override void HandleInput()
        {
            if(displayState == DisplayState.Opened)
            {
                if (Input.IsNewLeftClick())
                {
                    Close(ExitAction.Game);
                }
                else if (Input.IsNewRightClick())
                {
                    Close(ExitAction.Title);
                }
            }
        }

        public override void Update()
        {
            


            if(displayState == DisplayState.Opening)
            {
                if (blackgroundAlpha < 0.9f)
                { blackgroundAlpha += 0.09f; }
                else
                { displayState = DisplayState.Opened; }
            }
            else if (displayState == DisplayState.Opened)
            {
                if (text_ScoreTitle.alpha < 1.0f)
                { text_ScoreTitle.alpha += 0.01f; }
            }
            else if (displayState == DisplayState.Closing)
            {
                if (text_ScoreTitle.alpha > 0.0f)
                { text_ScoreTitle.alpha -= 0.09f; }
                else { text_ScoreTitle.alpha = 0.0f; }

                if (blackgroundAlpha < 1.0f)
                { blackgroundAlpha += 0.01f; }
                else
                { displayState = DisplayState.Closed; }
            }
            else if (displayState == DisplayState.Closed)
            {
                if(exitAction == ExitAction.Title)
                {
                    //exit to title
                    ScreenManager.ExitAndLoad(ScreenManager.Title);
                }
                else
                {
                    //restart game
                    ScreenManager.ExitAndLoad(ScreenManager.Game);
                }
            }



            text_Score.alpha = text_ScoreTitle.alpha;
            text_Exit.alpha = text_ScoreTitle.alpha;
        }

        public override void Draw()
        {
            Data.SB.Begin(SpriteSortMode.Deferred,
                BlendState.AlphaBlend, SamplerState.PointClamp);

            Data.SB.Draw(Assets.lineTexture,
                fadeRec, Color.Black * blackgroundAlpha);

            text_ScoreTitle.Draw();
            text_Exit.Draw();
            text_Score.Draw();

            Data.SB.End();
        }
    }
}