﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RMUD.Commands
{
	internal class Look : CommandFactory
	{
		public override void Create(CommandParser Parser)
		{
			Parser.AddCommand(
				new KeyWord("LOOK", false),
				new LookProcessor(),
				"Look around at your suroundings.");
		}
	}

	internal class LookProcessor : ICommandProcessor
	{
		public void Perform(PossibleMatch Match, Actor Actor)
		{
			var location = Actor.Location as Room;
			if (location == null) throw new InvalidOperationException("Error: Actor not in room.");

			if (Actor.ConnectedClient != null)
			{
				var builder = new StringBuilder();

				builder.Append(location.Short);
				builder.Append("\r\n");
				builder.Append(location.Long.Expand(Actor, location));
				builder.Append("\r\n");

				//Display objects in room
				if (location.Contents.Count > 0)
					builder.Append("Also here: " + String.Join(",", location.Contents.Select(t => t.Short)));
				else
					builder.Append("There is nothing here.");
				builder.Append("\r\n");

				//Display exits from room
				if (location.Links.Count > 0)
				{
					builder.Append("Obvious exits: ");

					for (int i = 0; i < location.Links.Count; ++i)
					{
						var l = location.Links[i];

						builder.Append(l.Direction.ToString().ToLower());

						if (l.Door != null)
						{
							builder.Append(" [through ");
							builder.Append(l.Door.Short);
							builder.Append("]");
						}

						if (i != location.Links.Count - 1)
							builder.Append(", ");
					}

					builder.AppendLine("\r\n");
				}

				Mud.SendEventMessage(Actor, EventMessageScope.Single, builder.ToString());
			}
		}
	}
}
