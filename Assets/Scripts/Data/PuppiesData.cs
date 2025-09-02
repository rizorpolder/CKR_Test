using System;
using System.Collections.Generic;

namespace Data
{
	[Serializable]
	public class PuppiesData
	{
		public List<PuppiesEntry> data;
	}

	[Serializable]
	public class PuppiesEntry
	{
		public string id;
		public PuppiesEntryAttributes attributes;
	}

	[Serializable]
	public class PuppiesEntryAttributes
	{
		public string name;
		public string description;
	}
}