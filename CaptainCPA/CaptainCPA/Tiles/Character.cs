/*
 * Project: CaptainCPA - Character.cs
 * Purpose: Character class
 *
 * History:
 *		Kendall Roth	Nov-24-2015:	Created
 *										Movement physics added
 *						Nov-26-2015:	Movement physics overhauled
 *						Nov-27-2015:	Physics adjusted again
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace CaptainCPA
{
    /// <summary>
    /// Character tile and logic
    /// </summary>
    public class Character : MoveableTile
    {
        private SpriteBatch spriteBatch;
        private List<Rectangle> frames;
        private Vector2 dimension;
        private int delay;
        private int delayCounter;
        private int frameIndex = 0;
        private Texture2D bigTexture;
        public Character(Game game, SpriteBatch spriteBatch, Texture2D texture, TileType tileType, Color color, Vector2 position, float rotation, float scale, float layerDepth,
                            Vector2 velocity, bool onGround)
            : base(game, spriteBatch, texture, TileType.Character, color, position, rotation, scale, layerDepth, velocity, onGround)
        {
            this.spriteBatch = spriteBatch;
            dimension = new Vector2(64, 64);
            delay = 2;
            facingRight = true;
            bigTexture = game.Content.Load<Texture2D>("Sprites/braidSpriteSheet");
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
                velocity.X -= 3.5f;
                facingRight = false;
            }

            //If the Right key is pressed, add horizontal velocity to move right
            if (ks.IsKeyDown(Keys.Right))
            {
                velocity.X += 3.5f;
                facingRight = true;
            }

            #region OldDebuggingMovement
            /*if (ks.IsKeyDown(Keys.Up))
			{
				velocity.Y = -4;
			}

			if (ks.IsKeyDown(Keys.Down))
			{
				velocity.Y = 4;
			}*/
            #endregion

            //If the Up key is pressed and the character is on the ground, add vertical velocity to jump (counteract gravity)
            if (ks.IsKeyDown(Keys.Up) && onGround == true)
            {
                velocity.Y = -10.0f;
                onGround = false;
            }
            CharacterStateManager.CharacterPosition = position;

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
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            if (frameIndex >= 0)
            {
                spriteBatch.Draw(bigTexture, position, frames.ElementAt<Rectangle>(frameIndex), Color.White, rotation, origin, 1f, spriteEffect, layerDepth);
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
