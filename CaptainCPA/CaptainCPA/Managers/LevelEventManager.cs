/*
 * Project:	PlatformGame - LevelEventManager.cs
 * Purpose:	Manage level events for a level
 *
 * History:
 *		Kendall Roth	Nov-28-2015:	Created
 *						Dec-09-2015:	Moved update to player health to HealthManager, added player reaching level exit event
 */

using Microsoft.Xna.Framework;

namespace CaptainCPA
{
	/// <summary>
	/// Manage level events for the levels by implementing an Observer pattern
	/// </summary>
	public class LevelEventManager : Observer
	{
		public LevelEventManager(Game game)
			: base(game)
		{

		}

		public override void OnNotify(object sender, string notification, object secondarySender = null)
		{
			switch (notification)
			{				
				case "PlayerLandedOnSpike":
					((Spike)sender).Color = Color.Red;
					break;
				case "PlayerReachedExit":
					//Handle player reaching level exit

					break;
				default:
					break;
			}
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