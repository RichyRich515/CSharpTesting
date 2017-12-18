using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Game2.Objects;

namespace Game2
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		private Player player;
		//private StarField starfield;
		private ParticleEmitter emitter1;

		private SpriteFont font;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			base.Initialize();
			IsMouseVisible = true;
			graphics.PreferredBackBufferWidth = (int)Globals.ScreenWidth;
			graphics.PreferredBackBufferHeight = (int)Globals.ScreenHeight;
			graphics.IsFullScreen = true;
			graphics.ApplyChanges();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);
			player = new Player(Content.Load<Texture2D>("images/bluebox"), new Vector2(200, 200));
			font = Content.Load<SpriteFont>("font/font");
			//List<Texture2D> startextures = new List<Texture2D>
			//{
			//	Content.Load<Texture2D>("images/blue"),
			//	Content.Load<Texture2D>("images/red"),
			//	Content.Load<Texture2D>("images/green")
			//};
			//starfield = new StarField(startextures, 100);

			emitter1 = new ParticleEmitter(10000, Content.Load<Texture2D>("images/particle"));
			emitter1.acceleration = new Vector2(0, 0);
			emitter1.active = true;
			emitter1.looping = true;
			emitter1.burst = false;
			emitter1.directionHigh = 360;
			emitter1.directionLow = 0;
			emitter1.duration = 10;
			emitter1.emission = 10000;
			emitter1.lifeHigh = 3.0f;
			emitter1.lifeLow = 0.5f;
			emitter1.looping = true;
			emitter1.maxVelocity = 100.0f;
			emitter1.position = new Vector2(Globals.ScreenWidth / 2.0f, Globals.ScreenHeight / 2.0f);
			emitter1.spawnClock = 0;
			emitter1.startVelocityHigh = 100.0f;
			emitter1.startVelocityLow = 50.0f;
			emitter1.startColor = Color.Red;
			emitter1.endColor = new Color(Color.DarkRed, 0);
			emitter1.spawnClock = 0;
			emitter1.spawnTime = 0;
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// game-specific content.
		/// </summary>
		protected override void UnloadContent()
		{
			Content.Unload();
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			player.position = new Vector2(((Globals.ScreenWidth - player.texture.Width) / 2.0f) + ((float)Math.Sin(gameTime.TotalGameTime.TotalSeconds) * (Globals.ScreenWidth / 2.0f - player.texture.Width / 2.0f)), 500);
			
			player.Update(gameTime);
			//starfield.Update(gameTime);
			emitter1.Update(gameTime);
			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);

			//starfield.Draw(spriteBatch);

			player.Draw(spriteBatch);
			emitter1.Draw(spriteBatch, gameTime);

			spriteBatch.Begin();

			spriteBatch.DrawString(font, "Player pos: " + player.position.ToString(), new Vector2(10, 10), Color.White);
			spriteBatch.DrawString(font, "Game time: " + gameTime.TotalGameTime.TotalSeconds.ToString() + "s", new Vector2(10, 40), Color.White);

			spriteBatch.End();
			base.Draw(gameTime);
		}
	}
}
