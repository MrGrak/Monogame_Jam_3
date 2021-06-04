using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LowBudgetJam
{
    public struct SpriteStruct
    {
        public Rectangle drawRec;
        public Color drawColor;
        public Vector2 origin;
        public SpriteEffects spriteEffect;
        public int X, Y;
        public int Xoffset, Yoffset;
        public float layer;
        public float alpha;
        public float scale;
        public float rotation;
        public float layerOffset;
        public int ID; //maps to any int based enum
        public int anim_Index; //where in animFrame sprite is
        public float anim_Speed; //higher is slower, in frames
        public float anim_Counter; //counts up to speed
        public bool visible; //or active

        public static void ResetSprite(ref SpriteStruct sprite)
        {
            sprite.origin = Vector2.Zero;
            sprite.spriteEffect = SpriteEffects.None;
            sprite.alpha = 1.0f;
            sprite.scale = 1.0f;
            sprite.rotation = 0.0f;
            sprite.layerOffset = 0;
            sprite.anim_Index = 0;
            sprite.anim_Speed = 10.0f;
            sprite.anim_Counter = 0;
            sprite.visible = true;
            sprite.drawColor = Color.White;

            //defaults to cookie in atlas
            sprite.drawRec.Width = 16; sprite.drawRec.Height = 16;
            sprite.drawRec.X = 5 * sprite.drawRec.Width;
            sprite.drawRec.Y = 0 * sprite.drawRec.Height;

            sprite.layer = Data.Layer_Particles;

        }

        public static void Draw(ref SpriteStruct sprite)
        {
            if (!sprite.visible) { return; }

            Vector2 pos = new Vector2(sprite.X, sprite.Y);

            Data.SB.Draw(Assets.sheet_Main, pos, 
                sprite.drawRec,
                sprite.drawColor * sprite.alpha,
                sprite.rotation,
                sprite.origin,
                sprite.scale,
                sprite.spriteEffect,
                sprite.layer);
        }

    }
}
