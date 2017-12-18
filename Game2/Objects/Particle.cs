using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2.Objects
{
	class Particle
	{
		public Texture2D texture;
		public bool alive;
		public float lifetime;
		public float life;
		public Vector2 velocity;
		public Vector2 position;
		public float rotationvelocity;

		public Color startcolor;
		public Color endcolor;
		public void Update(float dt, Vector2 acceleration)
		{
			life += dt;
			if (life >= lifetime)
			{
				alive = false;
			}
			else
			{
				velocity = new Vector2(velocity.X + acceleration.X * dt, velocity.Y + acceleration.Y * dt);
				Vector2 disp = new Vector2(velocity.X * dt, velocity.Y * dt);
				float rot = rotationvelocity * dt;
				position = position + disp;

				//setFillColor(lerpRGBA(startcolor, endcolor, life / lifetime));
			}
		}
	}
}
