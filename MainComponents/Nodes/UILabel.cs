using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Match3Game.Node
{
    public class UILabel : Node 
    {
        public string Text { get; protected set; }
        public SpriteFont TextFont { get; protected set ; }
        public Color TextColor { get; set; }



        public UILabel(string name = "Label") : base(name)
        {
            //SpriteFont = new SpriteFont();
            TextColor = Color.Black;
            Text = "";
        }

        public void SetText(string text)
        {
            Text = text;
            Vector2 newSize = TextFont.MeasureString(text);
            int x = (int)newSize.X;
            int y = (int)newSize.Y;

            Rectangle.Size = new Point(x, y);
            //SetSize(new Point((int)newSize.X, (int)newSize.Y));
        }

        public override void LoadContent()
        {
            base.LoadContent();
            TextFont = Global.Game.Content.Load<SpriteFont>("Arial16");
            //SetText("");
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            
            Global.SpriteBatch.DrawString(TextFont, Text, GetGlobalPosition().ToVector2(), TextColor);
        }

    }


}



