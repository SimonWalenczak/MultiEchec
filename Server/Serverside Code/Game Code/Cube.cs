using PlayerIO.GameLibrary;
using SamServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimServer
{
    internal class Cube : IFunction
    {
        public void Execute(Player player, Message message, GameCode game)
        {
            int oldPosX = message.GetInt(0);

            Console.WriteLine($"old {oldPosX}");

            game.Broadcast("Cube", player.ConnectUserId, oldPosX);
        }
    }
}