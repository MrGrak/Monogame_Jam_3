using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace LowBudgetJam
{
    public class Game1 : Game
    {
        public static Stopwatch timer = new Stopwatch();

        public Game1()
        {
            //set data game refs
            Data.GDM = new GraphicsDeviceManager(this);
            Data.GDM.GraphicsProfile = GraphicsProfile.HiDef;
            Data.GDM.PreferredBackBufferWidth = Data.GameWidth;
            Data.GDM.PreferredBackBufferHeight = Data.GameHeight;

            Data.CM = Content;
            Data.GAME = this;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize() { base.Initialize(); }

        protected override void LoadContent()
        {
            Data.SB = new SpriteBatch(GraphicsDevice);

            Assets.Constructor();

            base.LoadContent();

            ScreenManager.AddScreen(ScreenManager.Game);
            ScreenManager.Game.Open();
        }

        protected override void UnloadContent() { } //lol

        protected override void Update(GameTime gameTime)
        {
            timer.Restart();
            ScreenManager.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            ScreenManager.DrawActiveScreens();

            timer.Stop();
            //this wont work for all platforms
            Data.GAME.Window.Title =
                "Feed Apos! by @_mrgrak" +
                " screen: " + ScreenManager.activeScreen.Name +
                "." + ScreenManager.activeScreen.displayState +
                " - ticks: " + timer.ElapsedTicks +
                " - total particles: " + ParticleSystem.deadIndex +
                " / " + ParticleSystem.size;
        }
    }
}