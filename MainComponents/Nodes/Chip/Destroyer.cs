using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Match3Game.Node
{
    public class Destroyer : Node
    {

        private const int MoveSpeed = 1000;
        
        private Cell StartCell;
        private Texture2D Texture;
        private Point Direction;
        private Vector2 SpriteOrigin;
        private float Rotation;
        private SpriteEffects SpriteEffects;
        private Point OriginOfDestroyer; //Если не понятно, то это положение носа уничтожителя
        
        public Destroyer(Cell startCell, Point direction) : base("Destroyer")
        {
            StartCell = startCell;
            Direction = direction;
            if (direction.X != 0)
            {
                Rotation = MathHelper.ToRadians(90);
                SpriteOrigin = new Vector2(0, GridNode.cellSize);
                if (direction.X > 0)
                {
                    SpriteEffects = SpriteEffects.None;
                    OriginOfDestroyer = new Point(75, 35);
                }
                else if(direction.X < 0)
                {
                    SpriteEffects = SpriteEffects.FlipVertically;
                    OriginOfDestroyer = new Point(0, 35);
                }


            }
            else if (direction.Y != 0)
            {
                Rotation = MathHelper.ToRadians(0);
                SpriteOrigin = Vector2.Zero;
                if (direction.Y > 0)
                {
                    SpriteEffects = SpriteEffects.FlipVertically;
                    OriginOfDestroyer = new Point(35, 0);
                }
                else if(direction.Y < 0)
                {
                    SpriteEffects = SpriteEffects.None;
                    OriginOfDestroyer = new Point(35, 75);
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            MoveAndDestroy(gameTime);
        }

        public override void LoadContent()
        {
            base.LoadContent();
            Texture = Global.Game.Content.Load<Texture2D>("destroyer");
            SetPosition(StartCell.GetPosition());
            SetSize(75, 75);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            Global.SpriteBatch.Draw(Texture, Rectangle, null, Color.Red, Rotation, SpriteOrigin, SpriteEffects, 0);
        }

        private void MoveAndDestroy(GameTime gameTime)
        {
            Vector2 vector = new Vector2(Direction.X * MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds, Direction.Y * MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            Point newOffSet = new Point((int)vector.X, (int)vector.Y);
            SetPosition( GetPosition() + newOffSet);
            Cell cell = (GetParent() as GridNode).GetCellInPoint(GetGlobalPosition() + OriginOfDestroyer);
            if (cell is null)
            {
                (GetParent() as GridNode).Destroyers.Remove(this);
                Dispose();
                return;
            }
            cell.DestroyCurrentChip();
        }



    }


}




