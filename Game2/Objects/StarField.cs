using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2.Objects
{
	class StarField
	{
		private int maxStars;
		private List<Star> stars;
		private List<Texture2D> starTextures;
		public StarField(List<Texture2D> textures, int maxStars)
		{
			this.maxStars = maxStars;
			this.starTextures = textures;
			stars = new List<Star>(maxStars);
			Random rand = new Random();
			for (int i = 0; i < maxStars; ++i)
			{
				int textureidx = rand.Next(starTextures.Count);
				float xscale = (float)rand.NextDouble();
				float yscale = (float)rand.NextDouble();
				float scaleratio = (xscale + yscale) / 2.0f;
				stars.Add(new Star(starTextures[textureidx],
									new Vector2(rand.Next(0, G.ScreenWidth - starTextures[textureidx].Width),
												rand.Next(-G.ScreenHeight - starTextures[textureidx].Height, 0)),
									new Vector2(0, scaleratio * 250.0f)));
			}
		}

		public void Update(GameTime gameTime)
		{
			Random rand = new Random();
			for (int i = 0; i < stars.Count; ++i)
			{
				stars[i].Update(gameTime);
				if (stars[i].position.Y > G.ScreenHeight)
				{
					int textureidx = rand.Next(starTextures.Count);
					float xscale = (float)rand.NextDouble();
					float yscale = (float)rand.NextDouble();
					float scaleratio = (xscale + yscale) / 2.0f;
					stars[i] = new Star(starTextures[textureidx],
										new Vector2(rand.Next(0, G.ScreenWidth - starTextures[textureidx].Width),
													rand.Next(-G.ScreenHeight - starTextures[textureidx].Height, 0)),
										new Vector2(0, scaleratio * 250.0f));
				}
			}
		}
		
		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);
			foreach (Star s in stars)
			{
				spriteBatch.Draw(s.texture, s.position, Color.White);
			}
			spriteBatch.End();
		}
	}
}
