using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Data.Advisor
{
    public class ReadOnlyReplicaCatalog
    {
        private static ConcurrentBag<string> Catalog { get; set; }

        static ReadOnlyReplicaCatalog()
        {
            LoadCatalog();
        }

        public static bool IsElligible(string storedProcName)
        {
            if (Catalog == null || Catalog.IsEmpty)
            {
                LoadCatalog();
            }

            return Catalog != null && Catalog.Contains(storedProcName);
        }

        private static void LoadCatalog()
        {
            Catalog = new ConcurrentBag<string>();
            
        }
    }
}
