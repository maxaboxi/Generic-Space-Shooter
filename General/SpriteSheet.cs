using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace spacerpg.General
{
    class SpriteSheet
    {
        private readonly Texture2D _texture;
        private readonly SpriteBatch _spriteBatch;
        private readonly int _columns;
        private readonly int _rows;

        public int CellWidth { get; }
        public int CellHeight { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="texture">Texture</param>
        /// <param name="spriteBatch">Sprite batch</param>
        /// <param name="cellWidth">Cell width</param>
        /// <param name="cellHeight">Cell height</param>
        public SpriteSheet(SpriteBatch spriteBatch, Texture2D texture, int cellWidth, int cellHeight)
        {
            // Texture is the whole sprite sheet
            // cell height and width determine the size of a individual picture
            _spriteBatch = spriteBatch;
            _texture = texture;
            CellWidth = cellWidth;
            CellHeight = cellHeight;

            _columns = texture.Width / cellWidth;
            _rows = texture.Height / cellHeight;
        }

        /// <summary>
        /// Draw a sprite on screen.
        /// </summary>
        /// <param name="position">Screen position</param>
        /// <param name="frame">Frame</param>
        /// <param name="color">Colour</param>
        public void Draw(Vector2 position, int frame, Color color)
        {
            // Select picture to draw from spritesheet
            // frame = picture wanted
            // Columns and rows are from the spritesheet (i.e. shipsall has 3 columns and 3 rows)
            if (frame < 0 || frame >= _columns * _rows)
            {
                throw new ArgumentOutOfRangeException($"Frame: {frame} is too large");
            }

            var column = frame % _columns;
            var row = frame / _columns;
            var x = column * CellWidth;
            var y = row * CellHeight;

            _spriteBatch.Draw(_texture, position, new Rectangle(x, y, CellWidth, CellHeight), color);
        }
    }
}
