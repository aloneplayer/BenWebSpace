using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web.Configuration;

namespace StudyAbroad.Configuration
{
    public class TheStudyAbroadSection : ConfigurationSection
    {
        public readonly static TheStudyAbroadSection Current =
            (TheStudyAbroadSection)WebConfigurationManager.GetSection("theStudyAbroad");

        [ConfigurationProperty("articles", IsRequired = true)]
        public ArticlesElement Articles
        {
            get { return (ArticlesElement)base["articles"]; }
        }

        [ConfigurationProperty("pageSize", DefaultValue = "10")]
        public int PageSize
        {
            get { return (int)base["pageSize"]; }
            set { base["pageSize"] = value; }
        }
    }
}