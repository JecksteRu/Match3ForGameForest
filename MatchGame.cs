using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Match3Game.GameScenes;
using Match3Game.Node;

namespace Match3Game
{
    public class MatchGame : Game
    {
        public event Action<SpriteBatch> SpriteBatchChanged;
        public IScene CurrentScene { get => currentScene; }
        private IScene currentScene;

        private SpriteBatch _spriteBatch;
        public SpriteBatch SpriteBatch 
        {
            get => _spriteBatch;
            private set 
            {
                _spriteBatch = value;
                SpriteBatchChanged?.Invoke(_spriteBatch);

            } 
        }


        private readonly GraphicsDeviceManager _graphics;

        


        public MatchGame()
        {
            Global.SetGame(this);
            SpriteBatchChanged += Global.SetSpriteBatch;
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 800,
                PreferredBackBufferHeight = 800,
                SynchronizeWithVerticalRetrace = false,
                
                
            };
            
            Content.RootDirectory = "Content";
            IsFixedTimeStep = true;
            TargetElapsedTime = TimeSpan.FromSeconds(1f / 60f);
            IsMouseVisible = true;
            currentScene =  new MainMenu();

        }

        protected override void Initialize()
        {

            base.Initialize();
            currentScene.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            Global.SetSpriteBatch(SpriteBatch);
            currentScene.LoadContent();
        }




        public void ChangeScene(IScene newScene)
        {
            if (newScene == null) { return; }
            newScene.Initialize();
            newScene.LoadContent();
            IScene previusScene = currentScene;
            currentScene = newScene;
            previusScene?.Dispose();
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
            CurrentScene?.UnloadContent();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            Global.SpriteBatch.Begin();
            base.Draw(gameTime);
            CurrentScene?.Draw(gameTime);
            Global.SpriteBatch.End();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            MouseInput.LastMouseState = MouseInput.MouseState;
            MouseInput.MouseState = Mouse.GetState();
            CurrentScene?.Update(gameTime);
        }
    }

    public static class Global
    {

        



        public static MatchGame Game { get; private set; }

        public static void SetGame(MatchGame game)
        {
            Game = game;
        }
        public static SpriteBatch SpriteBatch { get; private set; }

        public static void SetSpriteBatch(SpriteBatch spriteBatch)
        {
            SpriteBatch = spriteBatch;
        }

        

    }



}
