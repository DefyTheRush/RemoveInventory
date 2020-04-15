using System;
using EXILED;
using EXILED.Extensions;

namespace RemoveInventory
{
    public class RemoveInventoryHandler
    {
        public void RunOnRACommandSent(ref RACommandEvent RAComEv)
        {
            string[] Arguments = RAComEv.Command.Split(' ');
            ReferenceHub Sender = RAComEv.Sender.SenderId == "SERVER CONSOLE" || RAComEv.Sender.SenderId == "GAME CONSOLE" ? PlayerManager.localPlayer.GetPlayer() : Player.GetPlayer(RAComEv.Sender.SenderId);

            switch (Arguments[0].ToLower())
            {
                case "ri":
                case "removeinv":
                case "strip":
                    RAComEv.Allow = false;
                    if (!Sender.CheckPermission("ri.allow"))
                    {
                        RAComEv.Sender.RAMessage("You are not authorized to use this command!");
                        return;
                    }

                    try
                    {

                        if (!CheckIfIdIsValid(Int32.Parse(Arguments[1])))
                        {
                            RAComEv.Sender.RAMessage("Please enter in an ID from a player that exists in the game!");
                            return;
                        }

                        ReferenceHub ChosenPlayer = Player.GetPlayer(int.Parse(Arguments[1]));
                        ChosenPlayer.inventory.ServerDropAll();
                        RAComEv.Sender.RAMessage("All items in player \"" + ChosenPlayer.GetNickname() + "\"'s inventory has been removed!");
                    }
                    catch (Exception)
                    {
                        RAComEv.Sender.RAMessage("Please enter a valid ID!");
                        return;
                    }
                    break;
                case "clear":
                    RAComEv.Allow = false;
                    if (!Sender.CheckPermission("ri.clear.allow"))
                    {
                        RAComEv.Sender.RAMessage("You are not authorized to use this command!");
                        return;
                    }

                    try
                    {
                        if (!CheckIfIdIsValid(Int32.Parse(Arguments[1])))
                        {
                            RAComEv.Sender.RAMessage("Please enter in an ID from a player that exists in the game!");
                            return;
                        }

                        ReferenceHub ChosenPlayer = Player.GetPlayer(int.Parse(Arguments[1]));
                        ChosenPlayer.ClearInventory();
                        RAComEv.Sender.RAMessage("All items in player \"" + ChosenPlayer.GetNickname() + "\"'s inventory has been cleared!");
                    }
                    catch (Exception)
                    {
                        RAComEv.Sender.RAMessage("Please enter a valid ID!");
                        return;
                    }
                    break;

            }
        }

        private bool CheckIfIdIsValid(int id)
        {
            ReferenceHub RequestedPlayer = Player.GetPlayer(id);
            if (!(RequestedPlayer == null))
                return true;

            return false;
        }
    }
}
