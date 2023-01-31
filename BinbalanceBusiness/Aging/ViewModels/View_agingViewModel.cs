using System;
using System.Collections.Generic;

namespace BinbalanceBusiness.Binbalance.ViewModels
{
    public partial class View_agingViewModel : Pagination
    {
        public long? rowIndex { get; set; }

        public Guid? product_Index { get; set; }

        public string product_Id { get; set; }

        public string product_Name { get; set; }

        public Guid? owner_Index { get; set; }

        public string owner_Id { get; set; }

        public string owner_Name { get; set; }

        public int? sumDay { get; set; }

        public string age { get; set; }

        public decimal? c0 { get; set; }

        public decimal? c1 { get; set; }

        public decimal? c2 { get; set; }

        public decimal? c3 { get; set; }

        public decimal? c4 { get; set; }
        public decimal? qty { get; set; }

        public string p1 { get; set; }
        public string p2 { get; set; }
        public string p3 { get; set; }
        public string p4 { get; set; }

        public int? row { get; set; }


        public class actionResultAgingViewModel
        {
            public IList<View_agingViewModel> itemsAging{ get; set; }

            public Pagination pagination { get; set; }
        }
    }


}

