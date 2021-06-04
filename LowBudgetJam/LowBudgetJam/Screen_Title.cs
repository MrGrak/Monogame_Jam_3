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
    public class Screen_Title : Screen
    {

        public Text text_Title;
        public Text text_Play;
        public float scaleCounter;
        public SpriteStruct AposFace;


        public Screen_Title()
        {
            Name = "TITLE";

            AposFace = new SpriteStruct();
            SpriteStruct.ResetSprite(ref AposFace);
            AposFace.drawRec.Width = 16 * 3;
            AposFace.drawRec.Height = 16 * 3;
            AposFace.drawRec.X = 0 * AposFace.drawRec.Width;
            AposFace.drawRec.Y = 0 * AposFace.drawRec.Height;
            AposFace.layer = Data.Layer_Particles;
            AposFace.X = -1000;
            AposFace.Y = 110; 

            text_Title = new Text("FEED APOS!",
                new Vector2(-1000, 180), Color.White);
            text_Title.font = Assets.Assets_font_big;
            
            text_Play = new Text(
                "left click to start new game\n        right click to exit",
                new Vector2(270, 280), Color.White);

        }

        public override void Open()
        {
            text_Title.alpha = 0.0f;
            text_Play.alpha = 0.0f;
            AposFace.alpha = 0.0f;

            scaleCounter = 0.0f;
            displayState = DisplayState.Opening;
        }

        public override void Close(ExitAction EA)
        {
            exitAction = EA;
            displayState = DisplayState.Closing;
        }

        public override void HandleInput()
        {
            if (displayState == DisplayState.Opened)
            {
                if (Input.IsNewLeftClick())
                {
                    Close(ExitAction.Game);
                }
                else if (Input.IsNewRightClick())
                {
                    Close(ExitAction.Exit);
                }
            }
        }

        public override void Update()
        {
            if (displayState == DisplayState.Opening)
            {
                if (text_Title.alpha < 1.0f)
                { text_Title.alpha += 0.05f; }
                else
                { displayState = DisplayState.Opened; }
            }
            else if (displayState == DisplayState.Opened)
            {
                
            }
            else if (displayState == DisplayState.Closing)
            {
                if (text_Title.alpha > 0.0f)
                { text_Title.alpha -= 0.07f; }
                else 
                { 
                    text_Title.alpha = 0.0f;
                    displayState = DisplayState.Closed;
                }
            }
            else if (displayState == DisplayState.Closed)
            {
                if (exitAction == ExitAction.Game)
                {
                    ScreenManager.ExitAndLoad(ScreenManager.Game);
                }
                else
                {
                    Data.GAME.Exit();
                }
            }

            text_Play.alpha = text_Title.alpha;
            AposFace.alpha = text_Title.alpha;

            scaleCounter += 0.05f;
            text_Title.scale = (float)(1.5f + Math.Sin(scaleCounter) * 0.2f);
            text_Title.position.X = 380 - (text_Title.scale * 80);

            AposFace.scale = (float)(2.0f + Math.Sin(scaleCounter) * 0.2f);
            AposFace.X = (int)(420 - (AposFace.scale * 32));
        }

        public override void Draw()
        {
            Data.SB.Begin(SpriteSortMode.Deferred,
                BlendState.AlphaBlend, SamplerState.PointClamp);

            SpriteStruct.Draw(ref AposFace);
            text_Title.Draw();
            text_Play.Draw();

            Data.SB.End();
        }
    }
}