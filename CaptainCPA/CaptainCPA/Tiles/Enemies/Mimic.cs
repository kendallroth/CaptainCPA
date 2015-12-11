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
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Mimic : PursuingEnemy
    {
        private List<Rectangle> frames;
        private Vector2 dimension;
        private int delay;
        private int delayCounter;
        private int frameIndex;
        private Texture2D bigTexture;
        private int jumpSpeed;
        public Mimic(Game game, SpriteBatch spriteBatch, Texture2D texture, TileType tileType, Color color, Vector2 position, float rotation, float scale, float layerDepth,
                            Vector2 velocity, bool onGround)
            : base(game, spriteBatch, texture, TileType.Character, color, position, rotation, scale, layerDepth, velocity, onGround)
        {
            delay = 4;
            jumpSpeed = -6;
            dimension = new Vector2(128, 128);
            bigTexture = game.Content.Load<Texture2D>("Sprites/MimicSpriteSheet");
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
            if (onGround)
            {

                velocity.Y = jumpSpeed;
                onGround = false;
            }
            if (isMoving)
            {
                delayCounter++;
                if (delayCounter % delay == 0)
                {
                    frameIndex++;
                    if (frameIndex == 7)
                        frameIndex = 0;
                }
            }

            base.Update(gameTime);
            //if (onGround)
            //    position.Y -= 5;
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            if (frameIndex >= 0)
            {
                spriteBatch.Draw(bigTexture, position, frames[frameIndex], Color.White, rotation, new Vector2(dimension.X / 2 - 20, dimension.Y / 2 -15), 0.5f, spriteEffect, layerDepth);
            }
            spriteBatch.End();
        }
        private void createFrames()
        {
            frames = new List<Rectangle>();
            for (int i = 0; i < 7; i++)
            {
                    int x = i * (int)dimension.X;
                    int y = 512;
                    Rectangle r = new Rectangle(x, y, (int)dimension.X, (int)dimension.Y);
                    frames.Add(r);
            }
        }
    }
}
