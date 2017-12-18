using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2.Objects
{
	class ParticleEmitter
	{
		private int poolsize;
		List<Particle> particles;
		
		// TODO: more shapes???
		Texture2D texture;

		public float duration;

		public bool active; // if not active, don't draw or update
		public bool looping; // restart after duration?
		public bool warm; // start particles in play already?

		public bool burst;

		// if burst, particles per burst
		// otherwise particles per second
		public int emission;
		public float spawnTime;
		public float spawnClock;

		public Vector2 position; // position in world space that particle emitter exists

		// How long each particle lives, random val between low and high
		public float lifeLow;
		public float lifeHigh;

		// selects random velocity between low and high
		public float startVelocityLow;
		public float startVelocityHigh;

		public Vector2 acceleration;
		public float maxVelocity;

		// Selects random value between range at birth of particle
		public float directionLow;
		public float directionHigh;

		// TODO: make this a vector of colors, so "rainbow" blending is possible
		// Born with start color then linear RGBA blend to end color over time
		public Color startColor;
		public Color endColor;
		
		// initializes the internal vector
		public ParticleEmitter(int maxsize, Texture2D texture)
		{
			this.texture = texture;
			poolsize = maxsize;
			particles = new List<Particle>();
			for (int i = 0; i < poolsize; ++i)
			{
				particles.Add(new Particle());
			}
		}

		// delta time
		public void Update(GameTime gameTime)
		{ 
			if (active)
			{
				float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
				bool spawn = false;
				int spawns = 0; // for burst

				spawnClock += dt;
				// TODO: looping and not burst
				if (looping && spawnClock >= spawnTime)
				{
					spawnClock = 0;
					spawn = true;
				}
				for (int i = 0; i < poolsize; ++i)
				{
					if (particles[i].alive)
					{
						particles[i].Update(dt, acceleration);
					}
					else if (spawn)
					{
						particles[i].alive = true;
						particles[i].texture = texture;
						particles[i].position = position;
						float dir = Globals.randomf(directionLow, directionHigh);
						float vel = Globals.randomf(startVelocityLow, startVelocityHigh);
						particles[i].velocity = new Vector2(vel * (float)Math.Cos(Globals.ToRadian(dir)), vel * (float)Math.Sin(Globals.ToRadian(dir)));
						particles[i].lifetime = Globals.randomf(lifeLow, lifeHigh);
						particles[i].life = 0;
						particles[i].startcolor = startColor;
						particles[i].endcolor = endColor;
						if (burst)
						{
							if (++spawns > emission)
							{
								spawn = false;
							}
						}
						else
						{
							spawn = false;
						}
					}
				}
			}
		}

		public void Boom()
		{
			//int spawns = 0;
			for (int i = 0; i < poolsize; ++i)
			{
				if (!particles[i].alive)
				{
					particles[i].alive = true;
					particles[i].texture = texture;
					particles[i].position = position;
					float dir = Globals.randomf(directionLow, directionHigh);
					float vel = Globals.randomf(startVelocityLow, startVelocityHigh);
					particles[i].velocity = new Vector2(vel * (float)Math.Cos(Globals.ToRadian(dir)), vel * (float)Math.Sin(Globals.ToRadian(dir)));
					particles[i].lifetime = Globals.randomf(lifeLow, lifeHigh);
					particles[i].life = 0;
					particles[i].startcolor = startColor;
					particles[i].endcolor = endColor;
					break;
				}
			}
		}

		// TODO: this
		public void Init()
		{
			if (burst)
			{
				if (looping)
				{
					spawnTime = duration;
					spawnClock = spawnTime;
				}
				else
				{
					//boom();
				}
			}
			else
			{
				spawnTime = 1.0f / emission;
				spawnClock = 0.0f;
			}
		}
		
		public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
			if (active)
			{
				spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);
				foreach (Particle p in particles)
				{
					if (p.alive)
					{
						Color c = Color.Lerp(p.startcolor, p.endcolor, p.life / p.lifetime);
						spriteBatch.Draw(p.texture, p.position, c);
					}
						
				}
				spriteBatch.End();
			}
		}
	}
}
