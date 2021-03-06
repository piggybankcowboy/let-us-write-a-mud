﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RMUD.Commands
{
	internal class ReportError : ICommandProcessor
	{
		public String Message;

		public ReportError(String Message)
		{
			this.Message = Message;
		}

		public void Perform(PossibleMatch Match, Actor Actor)
		{
			if (Actor.ConnectedClient != null)
				Actor.ConnectedClient.Send(Message);
		}
	}
}
