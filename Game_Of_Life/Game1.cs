using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game_Of_Life
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		Environment env = new Environment();
		Texture2D yellow;
		int accumulated_time = 0; 
		const int PIXEL_SIZE = 10;

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
			// TODO: Add your initialization logic here

			// Glider 
			for (int i = 0; i < 30; i++)
			{
				env.make_alive(i, i,env.Lives);
				env.make_alive(10, i,env.Lives);
				env.make_alive(13, i, env.Lives);
				env.make_alive(5, i, env.Lives);
				env.make_alive(1, i, env.Lives);

			}
			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			yellow = Content.Load<Texture2D>(@"yellow");
			spriteBatch = new SpriteBatch(GraphicsDevice);

			//TODO: use this.Content to load your game content here 
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			// For Mobile devices, this logic will close the Game when the Back button is pressed
			// Exit() is obsolete on iOS
#if !__IOS__ && !__TVOS__
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();
#endif

			// TODO: Add your update logic here

						accumulated_time += gameTime.ElapsedGameTime.Milliseconds;
						if (accumulated_time > 100)
						{
							env.Update();
							accumulated_time = 0; 
						}
			

			//env.Update();

			base.Update(gameTime);

		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear(Color.Black);

			//TODO: Add your drawing code here
			spriteBatch.Begin();
			for (int row = 0; row < Environment.ENVIRONMENT_SIZE; row++)
			{
				for (int col = 0; col < Environment.ENVIRONMENT_SIZE; col++)
				{
					if (env.isAlive(row, col))
					{
						spriteBatch.Draw(yellow, new Rectangle(col*PIXEL_SIZE, row*PIXEL_SIZE, PIXEL_SIZE, PIXEL_SIZE)  , Color.White);
					}
				}
			}

			spriteBatch.End();
			
			base.Draw(gameTime);
		}
	}
}
