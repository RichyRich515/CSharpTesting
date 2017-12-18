using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2.Objects
{
	class Player
	{
		public Texture2D texture;
		public Vector2 position;

		public Player(Texture2D texture, Vector2 position)
		{
			this.texture = texture;
			this.position = position;
		}

		public void Update(GameTime gameTime)
		{

		}
		
		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Begin();
			spriteBatch.Draw(texture, this.position, Color.White);
			spriteBatch.End();
		}
	}
}
