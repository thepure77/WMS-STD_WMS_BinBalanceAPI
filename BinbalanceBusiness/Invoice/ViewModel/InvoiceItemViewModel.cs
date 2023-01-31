using BinbalanceBusiness;
using BinBalanceBusiness.ViewModels;
using System;
using System.Collections.Generic;

namespace binbalanceBusiness.Binbalance.ViewModels
{

    public partial class InvoiceItemViewModel
    {
        public Guid invoiceItem_Index { get; set; }

        public Guid invoice_Index { get; set; }

        public int? item_Seq { get; set; }

        public Guid? serviceCharge_Index { get; set; }

        public string serviceCharge_Id { get; set; }


        public string serviceCharge_Name { get; set; }


        public decimal? qty { get; set; }


        public decimal? weight { get; set; }


        public decimal? volume { get; set; }


        public decimal? rT { get; set; }

        public decimal? unitCharge { get; set; }


        public decimal? rate { get; set; }


        public decimal? amount { get; set; }

        public Guid? currency_Index { get; set; }

        public Guid? memo_Index { get; set; }

        public string memo_No { get; set; }


        public string documentRef_No1 { get; set; }


        public string documentRef_No2 { get; set; }


        public string documentRef_No3 { get; set; }


        public string documentRef_No4 { get; set; }


        public string documentRef_No5 { get; set; }

        public int? document_Status { get; set; }


        public string document_Remark { get; set; }


        public string uDF_1 { get; set; }


        public string uDF_2 { get; set; }


        public string uDF_3 { get; set; }


        public string uDF_4 { get; set; }


        public string uDF_5 { get; set; }

        public Guid? ref_Process_Index { get; set; }

        public string ref_Document_No { get; set; }

        public Guid? ref_Document_Index { get; set; }

        public int? isActive { get; set; }

        public int? isDelete { get; set; }

        public int? isSystem { get; set; }

        public int? status_Id { get; set; }


        public string create_By { get; set; }

        public DateTime? create_Date { get; set; }


        public string update_By { get; set; }

        public DateTime? update_Date { get; set; }


        public string cancel_By { get; set; }

        public DateTime? cancel_Date { get; set; }

        public decimal? binBalance_QtyBal { get; set; }
        public decimal? binBalance_GrsWeightBal { get; set; }
        public decimal? binBalance_WeightBal { get; set; }
        public decimal? binBalance_NetWeightBal { get; set; }
        public decimal? binBalance_VolumeBal { get; set; }
        public decimal? volumeCal { get; set; }

    }


}

