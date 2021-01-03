using System;
using System.Collections.Generic;
using System.Text;

namespace Droplex.Models
{
    public class Configuration
    {
		public string Name { get; set; }
		public List<Releases> Releases { get; set; }
	}

	public class Releases
	{
		public string Url { get; set; }
		public string Args { get; set; }
		public string Version { get; set; }
	}
}
