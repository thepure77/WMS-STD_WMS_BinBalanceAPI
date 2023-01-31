using System;
using System.Collections.Generic;
using System.Text;

namespace BinbalanceBusiness.Binbalance.ViewModels
{
    public class SearchMemoViewModel : Pagination
    {
        public string Memo_Index { get; set; }
        public string Memo_No { get; set; }
        public Guid? Index { get; set; }

        public DateTime? Memo_Date { get; set; }

        public DateTime? Memo_Date_TO { get; set; }

        public string memo_Date_Default { get; set; }

        public string Ref_Document_No { get; set; }

        public string DocumentRef_No1 { get; set; }


        public string DocumentRef_No2 { get; set; }

        public string DocumentRef_No3 { get; set; }

        public string DocumentRef_No4 { get; set; }

        public string DocumentRef_No5 { get; set; }

        public string Owner_Name { get; set; }

        public IList<MemoItemSearchViewModel> items { get; set; }


    }
}



