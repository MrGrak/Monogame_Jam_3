using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LowBudgetJam
{
    public class Text
    {
        public SpriteFont font;
        public String text;             //the string of text to draw
        public Vector2 position;        //the position of the text to draw
        public Color color;             //the color of the text to draw
        public float alpha = 1.0f;      //the opacity of the text
        public float rotation = 0.0f;
        public float scale = 1.0f;
        public float zDepth = 0.001f;   //the layer to draw the text to

        public Text(String Text,
            Vector2 Position, Color Color)
        {
            position = Position;
            text = Text;
            color = Color;
            font = Assets.Assets_font_sm;
        }

        public void Draw()
        {
            Data.SB.DrawString(font, text, position,
                color * alpha, rotation, Vector2.Zero,
                scale, SpriteEffects.None, zDepth);
        }

    }
}
