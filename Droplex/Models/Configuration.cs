using System;
using System.Collections.Generic;
using System.Text;

namespace Droplex.Models
{
	public class Configuration
	{
		public string Name { get; set; }
		public string Version { get; set; }
		public int Id { get; set; }
		public string Url { get; set; }
		public string Mirror { get; set; }
		public string Args { get; set; }
	}
}
