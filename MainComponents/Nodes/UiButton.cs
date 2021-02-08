using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Match3Game.Node
{
    public class UiButton : Node
    {

        public event Action OnButtonPressed;

        private Texture2D backgroundTexture;

        public UiButton() : base("UiButon")
        {
            
        }

        public override void Draw(GameTime gameTime)
        {

            Global.SpriteBatch.Draw(backgroundTexture, Rectangle, Color.White);
            base.Draw(gameTime);
        }

        public void SetTexture(Texture2D texture) => backgroundTexture = texture;

        public override void Initialize()
        {
            base.Initialize();
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
            if (Rectangle.Contains(MouseInput.MouseState.Position) && MouseInput.MouseState.LeftButton == ButtonState.Pressed && MouseInput.LastMouseState.LeftButton == ButtonState.Released)
            {
                OnButtonPressed?.Invoke();
            }
        }
    }

}

