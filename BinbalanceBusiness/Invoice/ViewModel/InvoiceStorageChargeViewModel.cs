using BinbalanceBusiness;
using BinBalanceBusiness.ViewModels;
using System;
using System.Collections.Generic;

namespace binbalanceBusiness.Binbalance.ViewModels
{
    public partial class InvoiceStorageChargeViewModel
    {
        public Guid? invoiceStorageCharge_Index { get; set; }

        public Guid? invoice_Index { get; set; }

        public string doc_Date { get; set; }

        public Guid serviceCharge_Index { get; set; }

        public string serviceCharge_Id { get; set; }

        public string serviceCharge_Name { get; set; }

        public decimal? qtyBal { get; set; }

        public decimal? weightBal { get; set; }

        public decimal? netWeightBal { get; set; }

        public decimal? grsWeightBal { get; set; }

        public decimal? volumeBal { get; set; }

        public decimal? rTBal { get; set; }


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
        public decimal? rT { get; set; }
        public decimal? volumeCal { get; set; }
        public decimal? amount { get; set; }



    }


}

