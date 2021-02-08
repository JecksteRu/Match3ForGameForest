using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Match3Game.Node;




namespace Match3Game.GameScenes
{

    public interface IUIClickable
    {
        event Action OnButtonPressed;
    }

    

    public class MainMenu : Node.Node, IScene
    {

        

        public MainMenu()
            : base("MainMenu")
        {

            UiButton BeginGameButton;
            UiButton ExitButton;
            
            
            BeginGameButton = new UiButton();
            BeginGameButton.SetPosition(300, 400);
            BeginGameButton.SetSize(200, 50);
            BeginGameButton.Name = "BeginGameButton";
            BeginGameButton.OnButtonPressed += () => Global.Game.ChangeScene(new GameplayScene());
            AddChild(BeginGameButton);



            ExitButton = new UiButton();
            ExitButton.SetPosition(300, 475);
            ExitButton.SetSize(200, 50);
            ExitButton.Name = "ExitButton";
            ExitButton.OnButtonPressed += () => Global.Game.Exit() ;
            AddChild(ExitButton);


        }

        public MainMenu(string name) : base(name)
        {

        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void LoadContent()
        {
            base.LoadContent();
            Texture2D beginButtonTexture = Global.Game.Content.Load<Texture2D>("BeginGame");
            Texture2D exitButtonTexture = Global.Game.Content.Load<Texture2D>("ExitGame");
            GetNode<UiButton>("BeginGameButton").SetTexture(beginButtonTexture);
            GetNode<UiButton>("ExitButton").SetTexture(exitButtonTexture);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            
            
            
            base.Draw(gameTime);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        
    }


}
