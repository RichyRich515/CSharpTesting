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
	public class TetrisGame : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		private KeyboardState oldState;

		private SpriteFont font;

		private Tetris tetris;
		private Texture2D blockTexture;
		private Texture2D blockHighlightTexture;
		private string keystring;

		public TetrisGame()
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
			graphics.PreferredBackBufferWidth = (int)G.ScreenWidth;
			graphics.PreferredBackBufferHeight = (int)G.ScreenHeight;
			graphics.IsFullScreen = true;
			graphics.ApplyChanges();
			tetris = new Tetris
			{
				tetroTexture = blockTexture,
				highlightTexture = blockHighlightTexture,
			};
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);
			blockTexture = Content.Load<Texture2D>("images/particle");
			blockHighlightTexture = Content.Load<Texture2D>("images/Highlight");
			font = Content.Load<SpriteFont>("font/font");
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

			// TODO: make a keyboard handler
			KeyboardState newState = Keyboard.GetState(); // get the newest state
			// handle the input
			if ((oldState.IsKeyUp(Keys.Space) && newState.IsKeyDown(Keys.Space)) || (oldState.IsKeyUp(Keys.Enter) && newState.IsKeyDown(Keys.Enter)))
			{
				tetris.Slam(); // TODO: Slam
			}
			else
			{
				// Horizontal
				if ((oldState.IsKeyUp(Keys.Right) && newState.IsKeyDown(Keys.Right)) || (oldState.IsKeyUp(Keys.D) && newState.IsKeyDown(Keys.D)))
				{
					tetris.Move(1);
				}
				else if ((oldState.IsKeyUp(Keys.Left) && newState.IsKeyDown(Keys.Left)) || (oldState.IsKeyUp(Keys.A) && newState.IsKeyDown(Keys.A)))
				{
					tetris.Move(-1);
				}

				// Rotation
				if ((oldState.IsKeyUp(Keys.Up) && newState.IsKeyDown(Keys.Up)) || (oldState.IsKeyUp(Keys.W) && newState.IsKeyDown(Keys.W)))
				{
					tetris.Rotate(1);
				}

				// Speed modifying
				if ((oldState.IsKeyUp(Keys.Down) && newState.IsKeyDown(Keys.Down)) || (oldState.IsKeyUp(Keys.S) && newState.IsKeyDown(Keys.S)))
				{
					// on push
					tetris.Speed(true); 
				}
				else if ((oldState.IsKeyDown(Keys.Down) && newState.IsKeyUp(Keys.Down)) || (oldState.IsKeyDown(Keys.S) && newState.IsKeyUp(Keys.S)))
				{
					// on release
					tetris.Speed(false);
				}
			}
			
			keystring = "";
			foreach (Keys k in newState.GetPressedKeys())
			{
				keystring += k.ToString() + ", ";
			}
			if (keystring.Length > 2)
			{
				keystring = keystring.Remove(keystring.Length - 2);
			}
			oldState = newState; // set the new state as the old state for next tick

			tetris.Update(gameTime);

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);

			tetris.Draw(spriteBatch);

			spriteBatch.Begin();

			// Debug text			
			spriteBatch.DrawString(font, "uptime: " + gameTime.TotalGameTime.TotalSeconds.ToString() + "s", new Vector2(10, 0), Color.White);
			spriteBatch.DrawString(font, "keys: " + keystring, new Vector2(10, 30), Color.White);
			spriteBatch.DrawString(font, "rotation: " + tetris.rotation.ToString(), new Vector2(10, 60), Color.White);
			spriteBatch.DrawString(font, "clock: " + tetris.moveClock.ToString(), new Vector2(10, 90), Color.White);
			spriteBatch.DrawString(font, "(X, Y): (" + tetris.currentX + ", " + tetris.currentY + ")", new Vector2(10, 120), Color.White);
			spriteBatch.DrawString(font, "Field:", new Vector2(10, 150), Color.White);
			for (int i = 0; i < G.FieldHeight; i++)
			{
				for (int j = 0; j < G.FieldWidth; j++)
				{
					spriteBatch.DrawString(font, tetris.l_field[j, i].ToString(), new Vector2(10 + j * 15, 180 + i * 30), Color.White);
				}
			}

			spriteBatch.End();
			base.Draw(gameTime);
		}
	}
}
