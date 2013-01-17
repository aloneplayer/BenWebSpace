using System.Configuration;

namespace MyBeerHouse.Configuration
{
	public class PollsElement : ConfigurationElement
	{
		[ConfigurationProperty("pageSize", DefaultValue = "10")]
		public int PageSize
		{
			get { return (int)base["pageSize"]; }
			set { base["pageSize"] = value; }
		}

		[ConfigurationProperty("archiveIsPublic", DefaultValue = "false")]
		public bool ArchiveIsPublic
		{
			get { return (bool)base["archiveIsPublic"]; }
			set { base["archiveIsPublic"] = value; }
		}
	}
}
