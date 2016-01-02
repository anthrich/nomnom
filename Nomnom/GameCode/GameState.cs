using FarseerPhysics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nomnom.GameCode.Graphics;
using System.Collections.Generic;

namespace Nomnom.GameCode
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameState : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Camera2d Camera;
        Physics Physics;
        Nom Player;
        List<Nom> Noms;
        List<Nom> AiNoms;
        DebugViewXNA DebugView;

        SpriteFont font;
        Texture2D dot;

        public GameState()
        {
            Noms = new List<Nom>();
            AiNoms = new List<Nom>();
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.graphics.PreferredBackBufferWidth = 1280;
            this.graphics.PreferredBackBufferHeight = 720;
            this.graphics.ApplyChanges();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            var nomnom = Content.Load<Texture2D>("nomnom");
            font = Content.Load<SpriteFont>("font");
            dot = new Texture2D(GraphicsDevice, 1, 1);
            Color[] data = new Color[1];
            data[0] = Color.White;
            dot.SetData(data);

            if (DebugView == null)
            {
                DebugView = new DebugViewXNA(Physics.World);
                DebugView.AppendFlags(DebugViewFlags.DebugPanel);
                DebugView.AppendFlags(DebugViewFlags.PerformanceGraph);
                DebugView.DefaultShapeColor = Color.White;
                DebugView.SleepingShapeColor = Color.LightGray;
                DebugView.LoadContent(GraphicsDevice, Content);
            }
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Camera = new Camera2d(GraphicsDevice);
            Player = new Nom(Content);
            Physics = new Physics();
            AddNomToGame(Player);

            for (int i = 0; i < 10; i++)
            {
                Nom newNom = new Nom(Content);
                newNom.SetPosition(i * 50, i * 50);
                newNom.MoveToPosition(newNom.GetPosition());
                AddNomToGame(newNom);
                AiNoms.Add(newNom);
            }

            this.IsMouseVisible = true;
            base.Initialize();
        }

        private void AddNomToGame(Nom nom)
        {
            Physics.RegisterNom(nom);
            Noms.Add(nom);
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
        protected override void Update(GameTime gameTime)
        {
            if(this.IsActive) HandleInputs();
            Physics.Update(gameTime);
            Noms.ForEach(nom => nom.Update(gameTime));
            base.Update(gameTime);
        }

        private void HandleInputs()
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                Vector2 clickPos = Mouse.GetState().Position.ToVector2() + Camera.TopLeftPos;
                Player.MoveToPosition(clickPos);
            }
                
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Chocolate);
            Camera.Pos = Player.GetPosition();
            spriteBatch.Begin(SpriteSortMode.BackToFront,
                BlendState.AlphaBlend, 
                null, null, null, null, 
                Camera.GetTransformation());

            Noms.ForEach(nom => nom.Draw(spriteBatch));

            #if DEBUG
            DebugDraw();
            #endif

            spriteBatch.End();

            var transform = Matrix.CreateOrthographicOffCenter(
                ConvertUnits.ToSimUnits(Camera.TopLeftPos.X), 
                ConvertUnits.ToSimUnits(Camera.TopLeftPos.X + GraphicsDevice.Viewport.Width),
                ConvertUnits.ToSimUnits(Camera.TopLeftPos.Y + GraphicsDevice.Viewport.Height),
                ConvertUnits.ToSimUnits(Camera.TopLeftPos.Y),
                0f, 1f);
            DebugView.RenderDebugData(ref transform);

            base.Draw(gameTime);
        }

        private void DebugDraw()
        {
            spriteBatch.DrawString(font, $"PlayerPos: {Player.GetPosition()}", Camera.TopLeftPos, Color.White);
            spriteBatch.Draw(dot, Player.GetPosition(), Color.White);
        }
    }
}
