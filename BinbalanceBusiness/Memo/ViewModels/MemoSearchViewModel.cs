using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BinbalanceBusiness.Binbalance.ViewModels
{
    public class MemoSearchViewModel
    {
        public Guid memo_Index { get; set; }

        public string memo_No { get; set; }
        public Guid? Index { get; set; }
        public Guid? Process { get; set; }

        public string memo_Date { get; set; }

        public string ref_Document_No { get; set; }

        public string documentRef_No1 { get; set; }


        public string documentRef_No2 { get; set; }

        public string documentRef_No3 { get; set; }

        public string documentRef_No4 { get; set; }

        public string documentRef_No5 { get; set; }

        public string owner_Name { get; set; }

        public Guid? owner_Index { get; set; }
        public string owner_Id { get; set; }

        public Guid? documentType_Index { get; set; }
        
        public string documentType_Id { get; set; }
        
        public string documentType_Name { get; set; }

        public string remark { get; set; }

        public decimal? Total_Amount { get; set; }





        public IList<MemoItemSearchViewModel> listItemViewModels { get; set; }

        public class actionResultViewModel
        {
                public IList<MemoSearchViewModel> items { get; set; }
                public Pagination pagination { get; set; }
            
        }
       

    }


        
     
    
}



