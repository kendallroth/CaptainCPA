/*
 * Project:	PlatformGame - Monstar.cs
 * Purpose:	Type of pursuing enemy
 *
 * History:
 *		Doug Epp		Dec-11-2015:	Created
 */

using System.Collections.Generic;
using CaptainCPA.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CaptainCPA.Tiles.Enemies
{
	/// <summary>
	/// Type of pursuing enemy
	/// </summary>
	public class Monstar : PursuingEnemy
	{
		private Texture2D bigTexture;
		private List<Rectangle> frames;
		private Vector2 dimension;
		private int delay;
		private int delayCounter;
		private int frameIndex;


		public Monstar(Game game, SpriteBatch spriteBatch, Texture2D texture, Color color, Vector2 position, float rotation, float scale, float layerDepth,
							Vector2 velocity, bool onGround)
			: base(game, spriteBatch, texture, color, position, rotation, scale, layerDepth, velocity, onGround)
		{
			delay = 2;
			dimension = new Vector2(128, 128);
			bigTexture = game.Content.Load<Texture2D>("Sprites/MonstarSpriteSheet");
			createFrames();
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
			if (isMoving)
			{
				delayCounter++;
				if (delayCounter % delay == 0)
				{
					frameIndex++;
					if (frameIndex == 16)
						frameIndex = 0;
				}
			}

			base.Update(gameTime);
		}

		/// <summary>
		/// Allows the game component to draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public override void Draw(GameTime gameTime)
		{
			spriteBatch.Begin();
			if (frameIndex >= 0)
			{
				spriteBatch.Draw(bigTexture, position, frames[frameIndex], Color.White, rotation, new Vector2(dimension.X / 2, dimension.Y / 2 - 15), 0.5f, spriteEffect, layerDepth);
			}
			spriteBatch.End();
		}

		/// <summary>
		/// Create the animation frames for a texture image
		/// </summary>
		private void createFrames()
		{
			//TODO: move createFrames into enemy or pursuingEnemy
			frames = new List<Rectangle>();
			for (int i = 0; i < 2; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					int x = j * (int)dimension.X;
					int y = i * (int)dimension.Y;
					Rectangle r = new Rectangle(x, y, (int)dimension.X, (int)dimension.Y);
					frames.Add(r);
				}
			}
		}
	}
}
