using System.Configuration;

namespace MyBeerHouse.Configuration
{
	public class ArticlesElement : ConfigurationElement
	{
		[ConfigurationProperty("pageSize", DefaultValue = "10")]
		public int PageSize
		{
			get { return (int)base["pageSize"]; }
			set { base["pageSize"] = value; }
		}
	}
}
