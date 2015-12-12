/*
 * Project: CaptainCPA - Character.cs
 * Purpose: Character class
 *
 * History:
 *		Kendall Roth	Nov-24-2015:	Created
 *										Movement physics added
 *						Nov-26-2015:	Movement physics overhauled
 *						Nov-27-2015:	Updated physics
 *						Nov-29-2015:	Added speed, jumpspeed, lives, and losing life methods
 *						Dec-09-2015:	Added player death
 *						Dec-10-2015:	Added game over sound
 */

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;

namespace CaptainCPA
{
	/// <summary>
	/// Character tile and logic
	/// </summary>
	public class Character : MoveableTile
	{
		public const int MAX_LIVES = 3;

		private List<Rectangle> frames;
		private Vector2 dimension;
		private int delay;
		private int delayCounter;
		private int frameIndex = 0;
		private Texture2D bigTexture;
		private int lives;
		private int score;
		private float speed;
		private float jumpSpeed;
		private bool isAlive;
		private bool levelComplete;

		//Store characters's starting position
		private Vector2 startingPosition;

		public int Lives
		{
			get { return lives; }
			set { lives = value; }
		}

		public int Score
		{
			get { return score; }
			set { score = value; }
		}

		public float Speed
		{
			get { return speed; }
			set { speed = value; }
		}
		
		public float JumpSpeed
		{
			get { return jumpSpeed; }
			set { jumpSpeed = value; }
		}

		public bool IsAlive
		{
			get { return isAlive; }
			set { isAlive = value; }
		}

		public bool LevelComplete
		{
			get { return levelComplete; }
			set { levelComplete = value; }
		}

		public Vector2 StartingPosition
		{
			get { return startingPosition; }
		}

		public Character(Game game, SpriteBatch spriteBatch, Texture2D texture, TileType tileType, Color color, Vector2 position, float rotation, float scale, float layerDepth,
							Vector2 velocity, bool onGround, int lives, float speed, float jumpSpeed)
			: base(game, spriteBatch, texture, TileType.Character, color, position, rotation, scale, layerDepth, velocity, onGround)
		{
			dimension = new Vector2(64, 64);
			delay = 2;
			facingRight = true;
			//source: http://www.swingswingsubmarine.com/2010/11/25/seasons-after-fall-spritesheet-animation/
			bigTexture = game.Content.Load<Texture2D>("Sprites/braidSpriteSheet");
			createFrames();
			this.lives = lives;
			this.speed = speed;
			this.jumpSpeed = jumpSpeed;

			isAlive = true;
			startingPosition = position;
			levelComplete = false;

			CharacterStateManager.Speed = speed;
			Console.WriteLine(bounds);

			//Reset player score
			ResetScore();
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
		/// Reset player score
		/// </summary>
		private void ResetScore()
		{
			score = 0;
		}

		/// <summary>
		/// Make the character lose one life. If the number of lives is under zero, the game ends
		/// </summary>
		public void LoseLife()
		{
			if (--lives <= 0)
			{
				Die();
			}
			else
			{
				ResetPosition();
			}
		}

		/// <summary>
		/// Make the character die, and the game ends
		/// </summary>
		public void Die()
		{
			isAlive = false;
			
			//Destroy the character
			Destroy();

			//Play the game over sound effect
			SoundEffect gameOver = Game.Content.Load<SoundEffect>("Sounds/GameOver");
			gameOver.Play();
		}
		
		/// <summary>
		/// Reset the character's position
		/// </summary>
		private void ResetPosition()
		{
			position = new Vector2(startingPosition.X + origin.X, startingPosition.Y + origin.Y);
		}

		/// <summary>
		/// Allows the game component to update itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public override void Update(GameTime gameTime)
		{
			isMoving = false;
			KeyboardState ks = Keyboard.GetState();

			//Reset horizontal velocity to zero
			velocity.X = 0;

			//If the character is on the ground, reset vertical velocity to 0
			if (onGround == true)
			{
				velocity.Y = 0;
			}

			//If the Left key is pressed, subtract horizontal velocity to move left
			if (ks.IsKeyDown(Keys.Left))
			{
				isMoving = true;
				velocity.X -= speed;
				facingRight = false;
			}

			//If the Right key is pressed, add horizontal velocity to move right
			if (ks.IsKeyDown(Keys.Right))
			{
				isMoving = true;
				velocity.X += speed;
				facingRight = true;
			}

			//If the screen is moving character stays still on screen
			if (CharacterStateManager.ScreenMoving && isMoving)
			{
				//TODO: if character is to the right and wants to move left
				if (facingRight && CharacterStateManager.TooFarRight)
				{
					velocity.X = 0f;
				}
				else if (!facingRight && !CharacterStateManager.TooFarRight)
				{
					velocity.X = 0f;
				}
			}

			//If the Up key is pressed and the character is on the ground, add vertical velocity to jump (counteract gravity)
			if (ks.IsKeyDown(Keys.Up) && onGround == true)
			{
				velocity.Y = jumpSpeed;
				onGround = false;
			}

			//Debug Mode
			if (ks.IsKeyDown(Keys.Space))
			{
				Console.WriteLine("Debug Mode");
			}

			//animation, hopefully
			if (isMoving)
			{
				if (velocity.Y == 0)
				{
					delayCounter++;
					if (delayCounter % delay == 0)
					{
						frameIndex++;
						if (frameIndex == 27)
							frameIndex = 0;
					}
				}
				else
				{
					if (frameIndex != 1)
					{
						delayCounter++;
						if (delayCounter % delay == 0)
						{
							frameIndex++;
							if (frameIndex == 27)
								frameIndex = 0;
						}
					}
					else
					{
						frameIndex = 0;
					}
				}
			}
			//texture = bigTexture.GetData<Texture2D>()
			CharacterStateManager.CharacterPosition = position;
			CharacterStateManager.FacingRight = facingRight;
			CharacterStateManager.IsMoving = isMoving;
			CharacterStateManager.Velocity = velocity;
			base.Update(gameTime);
		}
		public override void Draw(GameTime gameTime)
		{
			spriteBatch.Begin();
			if (frameIndex >= 0)
			{
				spriteBatch.Draw(bigTexture, position, frames[frameIndex], Color.White, rotation, origin, 1f, spriteEffect, layerDepth);
			}
			spriteBatch.End();
			//base.Draw(gameTime);
		}
		private void createFrames()
		{
			frames = new List<Rectangle>();
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 7; j++)
				{
					int x = j * (int)dimension.X + 5 * j;
					int y = i * (int)dimension.Y + 5 * i + 5;
					Rectangle r = new Rectangle(x, y, (int)dimension.X, (int)dimension.Y);
					frames.Add(r);
				}
			}
		}
	}
}
