using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace StudyAbroad.Configuration
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