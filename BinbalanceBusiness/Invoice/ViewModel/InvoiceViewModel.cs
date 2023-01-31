using BinbalanceBusiness;
using BinBalanceBusiness.ViewModels;
using System;
using System.Collections.Generic;

namespace binbalanceBusiness.Binbalance.ViewModels
{

    public partial class InvoiceViewModel : Pagination
    {
        public Guid invoice_Index { get; set; }

        public string invoice_No { get; set; }

        public string invoice_Date { get; set; }
        public string invoice_DateTo { get; set; }


        public string start_Date { get; set; }

        public string end_Date { get; set; }

        public string due_Date { get; set; }

        public Guid owner_Index { get; set; }

        public string owner_Id { get; set; }

        public string owner_Name { get; set; }

        public Guid documentType_Index { get; set; }

        public string documentType_Id { get; set; }

        public string documentType_Name { get; set; }

        public string tax_No { get; set; }

        public string credit_Term { get; set; }

        public Guid? currency_Index { get; set; }

        public decimal? exchange_Rate { get; set; }

        public string paymentMethod_Index { get; set; }


        public string payment_Ref { get; set; }

        public DateTime? fullPaid_Date { get; set; }

        public decimal? amount { get; set; }


        public decimal? discount_Percent { get; set; }


        public decimal? discount_Amt { get; set; }


        public decimal? total_Amt { get; set; }


        public decimal? vAT_Percent { get; set; }


        public decimal? vAT { get; set; }


        public decimal? net_Amt { get; set; }


        public string billing_Address { get; set; }


        public string billing_Tel { get; set; }


        public string billing_Fax { get; set; }


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


        public string confirm_By { get; set; }

        public DateTime? confirm_Date { get; set; }

        public List<InvoiceItemViewModel> listInvoice { get; set; }
        public List<InvoiceStorageChargeViewModel> listStorage { get; set; }
        public string key { get; set; }


    }

    public class actionResultInvoice
    {
        public string msg { get; set; }
        public Pagination pagination { get; set; }
        public IList<InvoiceViewModel> items { get; set; }
        public IList<InvoiceItemViewModel> listInvoice { get; set; }
        public IList<InvoiceStorageChargeViewModel> listStorage { get; set; }

    }
}

