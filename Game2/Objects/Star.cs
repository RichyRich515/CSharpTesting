using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2.Objects
{
	class Star
	{
		public Texture2D texture;
		public Vector2 position;

		private Vector2 velocity;
		

		public Star(Texture2D texture, Vector2 position, Vector2 velocity)
		{
			this.texture = texture;
			this.position = position;
			this.velocity = velocity;
		}

		public void Update(GameTime gameTime)
		{
			float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
			position.Y += velocity.Y * dt;
		}

		
	}
}
