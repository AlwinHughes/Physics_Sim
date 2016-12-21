using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Physics_Sim
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        BaseScreen[] screens = new BaseScreen[2];
        BaseScreen current_scrren;
        bool time_frozen;
        public int width;
        public int height;
        private Vector2 screenpos;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            time_frozen = true;
            width = GraphicsDevice.PresentationParameters.Bounds.Size.X;
            height = GraphicsDevice.PresentationParameters.Bounds.Size.Y;
            Console.WriteLine(Convert.ToString(width));
            Console.WriteLine(Convert.ToString(height));
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Suvat a = new Suvat(0, graphics.GraphicsDevice);
            screens[0] = a;
            a.onLoad();
            screens[0].onLoad();
            current_scrren = a;
            screens[1] = new SmallPlanetSuvat(1, graphics.GraphicsDevice);
            Viewport viewport = graphics.GraphicsDevice.Viewport;
            screenpos.X = viewport.Width / 2;
            screenpos.Y = viewport.Height / 2;

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>

        private KeyboardState currentKeyboardState;
        private KeyboardState previousKeyboardState;

        protected override void Update(GameTime gameTime)
        {
            currentKeyboardState = Keyboard.GetState();
            // exit game
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // switch to Suvat
            if (previousKeyboardState.IsKeyDown(Keys.D1) && currentKeyboardState.IsKeyUp(Keys.D1))
            {
                current_scrren.onClose();
                screens[0].onLoad();
                current_scrren = screens[0];
            }

            //switch to small planet 
            if(previousKeyboardState.IsKeyDown(Keys.D2) && currentKeyboardState.IsKeyUp(Keys.D2))
            {
                current_scrren.onClose();
                screens[1].onLoad();
                current_scrren = screens[1];
            }

            // pause time
            if(previousKeyboardState.IsKeyDown(Keys.Space) && currentKeyboardState.IsKeyUp(Keys.Space))
            {
                time_frozen = !time_frozen;
                if (time_frozen)
                {
                    Console.WriteLine("time frozen");
                }
                else
                {
                    Console.WriteLine("time un frozen");
                }
            }

            // if time is not frozen
            if (!time_frozen)
            {
                current_scrren.phyUpdate();
            }

            previousKeyboardState = currentKeyboardState;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            foreach (Object o in current_scrren.drawUpdateObjects())
            {
                if(o != null)
                spriteBatch.Draw(o.texture, o.pos, Color.White);
            }

            foreach(Instrument i in current_scrren.drawUpdateInstrument())
            {
                if(i != null)
                    spriteBatch.Draw(i.texture, i.pos, null, null,new Vector2(0,0),(float)i.Theta, null, Color.White,SpriteEffects.None,1);
            }
            //new Rectangle((int)i.pos.X - i.width, (int)i.pos.Y - i.height, (int)i.pos.X, (int)i.pos.Y);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
