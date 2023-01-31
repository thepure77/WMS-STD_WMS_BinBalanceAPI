using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BinbalanceBusiness.Binbalance.ViewModels
{
    public class MemoItemSearchViewModel
    {
        public Guid? Memo_Index { get; set; }

        public Guid? Memo_Item_Index { get; set; }

        public Guid? Index { get; set; }


        public string Memo_No { get; set; }

        public decimal? UnitCharge { get; set; }

        public string ServiceCharge_Name { get; set; }

        public decimal? Qty { get; set; }

        public decimal? Weight { get; set; }

        public decimal? Volume { get; set; }

        public decimal? Amount { get; set; }

        public decimal? Rate { get; set; }
        public decimal? Minimumrate { get; set; }

        public decimal? Pallet { get; set; }

        public decimal? RT { get; set; }

        public String ServiceCharge_Id { get; set; }

        public Guid? ServiceCharge_Index { get; set; }

        public string remark { get; set; }






    }





}



