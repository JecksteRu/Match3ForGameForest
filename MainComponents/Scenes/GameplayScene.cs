using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Match3Game.Node;

namespace Match3Game.GameScenes
{
    public class GameplayScene : Node.Node, IScene
    {


        public GridNode GridNode;
        public GameTimer GameTimer;
        public PlayerScoreSystem PlayerScoreSystem;


        public GameplayScene() : base("GameScene")
        {
            GridNode = new GridNode();
            GameTimer = new GameTimer();
            GameTimer.TimeEnded += OnGameTimeEnds;
            PlayerScoreSystem = new PlayerScoreSystem();
            GridNode.OnScoreAdded += PlayerScoreSystem.AddScore;
            
            AddChild(PlayerScoreSystem);
            AddChild(GridNode);
            AddChild(GameTimer);
        }

        

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public override void Initialize()
        {
            base.Initialize();
            PlayerScoreSystem.SetPosition(500, 50);




        }

        public override void LoadContent()
        {
            base.LoadContent();
            
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            
            
        }

        public void OnGameTimeEnds()
        {
            PlayerScoreSystem.SetGlobalPosition(400, 200);
            GridNode.Dispose();

            Texture2D exitButtonTexture = Global.Game.Content.Load<Texture2D>("ok-button");
            UiButton ExitButton;
            ExitButton = new UiButton();
            ExitButton.SetGlobalPosition(300, 475);
            ExitButton.SetSize(200, 50);
            ExitButton.Name = "ExitButton";
            ExitButton.OnButtonPressed += () => Global.Game.ChangeScene(new MainMenu());
            ExitButton.SetTexture(exitButtonTexture);
            AddChild(ExitButton);

        }

    }

  
}
