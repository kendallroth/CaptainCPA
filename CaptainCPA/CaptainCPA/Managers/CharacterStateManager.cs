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
    public class CharacterStateManager : Microsoft.Xna.Framework.GameComponent
    {
        private static Vector2 characterPosition;
        private static bool isMoving;
        private static bool facingRight;
        private static bool onGround;
        private static float speed;
        private static bool screenMoving;
        private static bool tooFarRight;

        public static bool TooFarRight
        {
            get { return CharacterStateManager.tooFarRight; }
            set { CharacterStateManager.tooFarRight = value; }
        }

        public static bool ScreenMoving
        {
            get { return CharacterStateManager.screenMoving; }
            set { CharacterStateManager.screenMoving = value; }
        }

        private static Vector2 velocity;

        public static Vector2 CharacterPosition
        {
            get { return CharacterStateManager.characterPosition; }
            set { CharacterStateManager.characterPosition = value; }
        }

        public static bool IsMoving
        {
            get { return CharacterStateManager.isMoving; }
            set { CharacterStateManager.isMoving = value; }
        }

        public static bool FacingRight
        {
            get { return CharacterStateManager.facingRight; }
            set { CharacterStateManager.facingRight = value; }
        }
        public static bool OnGround
        {
            get { return CharacterStateManager.onGround; }
            set { CharacterStateManager.onGround = value; }
        }
        public static float Speed
        {
            get { return CharacterStateManager.speed; }
            set { CharacterStateManager.speed = value; }
        }
        public static Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }
        public CharacterStateManager(Game game)
            : base(game)
        {

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
    }
}
