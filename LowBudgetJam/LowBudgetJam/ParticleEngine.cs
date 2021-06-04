using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace LowBudgetJam
{
    public struct Physics
    {
        public float X, Y; //current position
        public float accX, accY; //accelerations
        public float preX, preY; //last position
    }

    public enum ParticleID : byte 
    { 
        Inactive, 
        Active 
    }

    public struct Particle
    {   //uses physics struct as component
        public Physics physics;
        public Color color;
        public float alpha;
        public ParticleID Id;
        public SpriteStruct sprite;
    }

    public static class ParticleSystem
    {   //max size depends on the platform, 30k is baseline/lowend
        public const int size = 50000;
        public static Particle[] data = new Particle[size];

        static Random Rand = new Random();
        public static Texture2D texture;
        public static Rectangle drawRec = new Rectangle(0, 0, 3, 3);
        public static float gravity = 0.1f;
        public static float windCounter = 0;

        public static int deadIndex = 0; //last active index

        public static MouseState currentMouseState = new MouseState();

        public static float scaleCounter;
        public static SpriteStruct AposFace;






        static ParticleSystem()
        {
            AposFace = new SpriteStruct();
            SpriteStruct.ResetSprite(ref AposFace);
            AposFace.drawRec.Width = 16 * 3;
            AposFace.drawRec.Height = 16 * 3;
            AposFace.drawRec.X = 0 * AposFace.drawRec.Width;
            AposFace.drawRec.Y = 0 * AposFace.drawRec.Height;
            AposFace.layer = Data.Layer_Face;
        }

        public static void Reset()
        {   //acts as a constructor as well
            if (texture == null)
            {   //set up a general texture we can draw dots with, if required
                texture = new Texture2D(Data.GDM.GraphicsDevice, 1, 1);
                texture.SetData<Color>(new Color[] { Color.White });
            }

            //reset particles to inactive state
            for (int i = 0; i < size; i++)
            { 
                data[i].Id = ParticleID.Inactive;
                
                SpriteStruct Sp = new SpriteStruct();
                SpriteStruct.ResetSprite(ref Sp);
                data[i].sprite = Sp;
            }

            deadIndex = 0; //reset index too
            scaleCounter = 0.0f;
            AposFace.alpha = 0.0f;
        }

        public static void Spawn(
            ParticleID ID,          //type of particle to spawn
            float X, float Y,       //spawn x, y position 
            float accX, float accY  //initial x, y acceleration
            )
        {   //create a stack instance to work on
            Particle P = new Particle();
            //setup P based on parameters
            P.physics.X = X; P.physics.preX = X;
            P.physics.Y = Y; P.physics.preY = Y;
            P.physics.accX = accX;
            P.physics.accY = accY;
            //set defaults for particle
            P.alpha = 0.6f;
            P.color = Color.White;

            //pass active ID
            P.Id = ID;

            
            //randomly assign a food sprite
            SpriteStruct Sp = new SpriteStruct();
            SpriteStruct.ResetSprite(ref Sp);
            Sp.drawRec.X = Rand.Next(5, 13) * 16;
            Sp.drawRec.Y = Rand.Next(0, 7) * 16;
            Sp.scale = 2.0f;

            P.sprite = Sp;
            

            //save P to heap via marker
            data[deadIndex] = P;
            deadIndex++;
            //bound dead index to array size
            if (deadIndex >= size)
            { deadIndex = size - 1; }
        }

        public static void Update()
        {

            //increase wind counter
            windCounter += 0.015f;
            //get left or right horizontal value for wind
            float wind = (float)Math.Sin(windCounter) * 0.03f;
            wind = 0;

            currentMouseState = Mouse.GetState();
            //set current mouse pos
            mouseX = currentMouseState.X;
            mouseY = currentMouseState.Y;

            //store locals for this heap data
            int width = Data.GDM.PreferredBackBufferWidth;
            int height = Data.GDM.PreferredBackBufferHeight;


            #region Sim Particles

            for (int i = deadIndex - 1; i >= 0; i--)
            {   //particle active, age + check id / behavior
                //create a local copy
                Particle P = data[i];


                if (P.Id == ParticleID.Active)
                {
                    //bounce around inside window
                    if (P.physics.X < 0)
                    {
                        P.physics.accX = 1.0f;
                    }
                    else if (P.physics.X > width)
                    {
                        P.physics.accX = -1.0f;
                    }

                    if (P.physics.Y < 0)
                    {
                        P.physics.accY = 1.0f;
                    }
                    else if (P.physics.Y > height)
                    {
                        P.physics.accY = Rand.Next(1, 300) * -0.05f;
                    }

                    //bounce away from center circle
                    if (CheckPointInCircle(P.physics.X, P.physics.Y))
                    { 
                        P.Id = ParticleID.Inactive;
                        ScreenManager.Game.score++;
                    }

                    //add gravity to push down
                    P.physics.accY += gravity;
                    //add wind to push left/right
                    P.physics.accX += wind;

                    //calculate velocity using current and previous pos
                    float velocityX = (P.physics.X - P.physics.preX) * 1.0f;
                    float velocityY = (P.physics.Y - P.physics.preY) * 0.98f;

                    //store previous positions (the current positions)
                    P.physics.preX = P.physics.X;
                    P.physics.preY = P.physics.Y;

                    //set next positions using current + velocity + acceleration
                    P.physics.X = P.physics.X + velocityX + P.physics.accX;
                    P.physics.Y = P.physics.Y + velocityY + P.physics.accY;

                    //clear accelerations
                    P.physics.accX = 0; P.physics.accY = 0;

                    //write local to heap
                    data[i] = P;
                }
                else
                {
                    //deactivate particle
                    if (i < deadIndex - 1)
                    {
                        data[i] = data[deadIndex - 1];
                        deadIndex--;
                    }
                    else { deadIndex--; }
                }
            }

            #endregion


            // create particles and disallow creation from exceeding buffer limit.
            int numberOfParticlesToSpawn = 5;
            if (deadIndex + numberOfParticlesToSpawn < size)
            {
                for (int i = 0; i < numberOfParticlesToSpawn; i++)
                {   
                    Spawn(ParticleID.Active, Rand.Next(0, width), height,
                        Rand.Next(-100, 101) * 0.01f, 0);
                }
            }



            //match apos face to cursor
            scaleCounter += 0.05f;
            AposFace.scale = (float)(3.0f + Math.Sin(scaleCounter) * 0.5f);
            int offset = 25;
            AposFace.X = (int)(Input.cursorPos_Screen.X - (AposFace.scale * offset));
            AposFace.Y = (int)(Input.cursorPos_Screen.Y - (AposFace.scale * offset));

            //fade apos face in from reset
            if (AposFace.alpha < 1.0f)
            { AposFace.alpha += 0.09f; }


        }

        public static void Draw()
        {   
            int s = deadIndex; //size
            for (int i = 0; i < s; i++)
            {   //if particle is active, draw it
                if (data[i].Id > 0)
                {
                    //draw food items
                    SpriteStruct Sp = data[i].sprite;
                    Sp.X = (int)data[i].physics.X - 16;
                    Sp.Y = (int)data[i].physics.Y - 16;
                    SpriteStruct.Draw(ref Sp);

                    /*
                    //create vector2 that draw wants
                    Vector2 pos = new Vector2(data[i].physics.X, data[i].physics.Y);
                    //draw each particle as a sprite
                    Data.SB.Draw(texture,
                        pos,
                        drawRec,
                        data[i].color * data[i].alpha,
                        0,
                        Vector2.Zero,
                        1.0f, //scale
                        SpriteEffects.None,
                        i * 0.00001f);
                    */

                }   //layer particles based on index, as simple example
            }

            SpriteStruct.Draw(ref AposFace);
        }


        static int mouseX, mouseY;
        public static bool CheckPointInCircle(float px, float py)
        {
            // get distance between the point and circle's center
            // using the Pythagorean Theorem
            float distX = px - mouseX;
            float distY = py - mouseY;
            float distance = (float)Math.Sqrt((distX * distX) + (distY * distY));

            // if the distance is less than the circle's
            // radius the point is inside!
            if (distance <= 60) { return true; }
            return false;
        }




    }

}
