using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metizsoft.Model
{
    public class DynamicMenuModel
    {
        public List<MainTier> MainTier { get; set; }    
    }

    public class MainTier
    {
        public int MainTierID { get; set; }
        public long MenuID { get; set; }
        public string DisplayName { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Iconname { get; set; }
        public int SortNo { get; set; }
        public List<SubTier> SubTier { get; set; }
    }

    public class SubTier
    {
        public int MainTierID { get; set; }    
        public int SubTierID { get; set; }    
        public long MenuID { get; set; }
        public string DisplayName { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Iconname { get; set; }
        public int SortNo { get; set; }
        public List<ThirdTier> ThirdTier { get; set; }
    }

    public class ThirdTier
    {
        public int MainTierID { get; set; }
        public int SubTierID { get; set; }
        public long MenuID { get; set; }
        public string DisplayName { get; set; }
        public string Controller { get; set; }
        public string Iconname { get; set; }
        public int SortNo { get; set; }
        public string Action { get; set; }
    }

}
