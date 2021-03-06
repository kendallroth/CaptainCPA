﻿/*
 * Project: CaptainCPA - ABOUT.cs
 * Purpose: Display the game's about information
 *
 * History:
 *		Doug Epp		Nov-26-2015:	Created
 *		Kendall Roth	Dec-09-2015:	Updated User Interface Design
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CaptainCPA.Scenes
{
	/// <summary>
	/// Displays the game's about information from a text file
	/// </summary>
	public class AboutScene : GameScene
	{
		private Texture2D menuImage;

		public AboutScene(Game game, SpriteBatch spriteBatch, Texture2D menuImage)
			: base(game, spriteBatch)
		{
			this.menuImage = menuImage;
		}

		/// <summary>
		/// Allows the game component to perform any initialization it needs to before starting
		/// to run.  This is where it can query for any required services and load content.
		/// </summary>
		public override void Initialize()
		{
			base.Initialize();
		}

		/// <summary>
		/// Allows the game component to update itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
		}

		/// <summary>
		/// Allows the game component to draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public override void Draw(GameTime gameTime)
		{
			spriteBatch.Begin();
			spriteBatch.Draw(menuImage, Vector2.Zero, Color.White);
			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
