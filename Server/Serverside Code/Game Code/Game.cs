using System;
using System.Collections.Generic;
using PlayerIO.GameLibrary;

namespace SimServer {

	[RoomType("SimServer")]
	public class GameCode : Game<Player> {

        private Dictionary<string, IFunction> _functions = new Dictionary<string, IFunction>();

        // This method is called when an instance of your the game is created
        public override void GameStarted()
        {
            // anything you write to the Console will show up in the 
            // output window of the development server
            Console.WriteLine("Game is started: " + RoomId);

            _functions.Add("MOVE", new Move());
			_functions.Add("Cube", new Cube());
        }

		private void Resetgame() {
		}

		public override void GameClosed() {
			Console.WriteLine("RoomId: " + RoomId);
		}

		public override void UserJoined(Player player) {
			foreach(Player pl in Players) {
				if(pl.ConnectUserId != player.ConnectUserId) {
					pl.Send("PlayerJoined", player.ConnectUserId);
					player.Send("PlayerJoined", pl.ConnectUserId);
				}
			}
		}

		public override void UserLeft(Player player) {
			Broadcast("PlayerLeft", player.ConnectUserId);
		}

        public override void GotMessage(Player player, Message message)
        {
            if (!_functions.TryGetValue(message.Type, out IFunction func))
                return;

            func.Execute(player, message, this);
        }
    }
}