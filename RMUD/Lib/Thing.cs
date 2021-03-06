﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RMUD
{
	public class Thing : MudObject, IDescribed, IMatchable
	{
		public String Short;
		public DescriptiveText Long { get; set; }
		public String IndefiniteArticle = "a";
		public List<String> Nouns { get; set; }
		public MudObject Location;

		public Thing()
		{
			Nouns = new List<string>();
		}

		public static void Move(Thing Thing, MudObject Destination)
		{
			if (Thing.Location != null)
			{
				var container = Thing.Location as IContainer;
				if (container != null)
					container.Remove(Thing);
				Thing.Location = null;
			}

			if (Destination != null)
			{
				var destinationContainer = Destination as IContainer;
				if (destinationContainer != null)
					destinationContainer.Add(Thing);
				Thing.Location = Destination;
			}
		}
	}
}
