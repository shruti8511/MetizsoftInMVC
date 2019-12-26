using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metizsoft.Service.Models
{
    
    public class ProductTypeViewModel
    {
        public int ProductTypeId { get; set; }
        public string ProductType { get; set; }      
    }

    public class ProductMst_mst
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public bool Active { get; set; }
    }
    public class ProductCaseResponse
    {
        public int ProductCaseId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
    }

    public class ProductCaseMst_mst
    {

        public int ProductCaseId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string CaseName { get; set; }
        public string Name { get; set; }
        public decimal PTS { get; set; }
        public string MeasureIn { get; set; }
        public string StrangthIn { get; set; }       
        public string Pack { get; set; }
        public string PTR { get; set; }
        public string MRP { get; set; }
        public string STDRate { get; set; }
        public string BrandName { get; set; }
        public string ProductTypeId { get; set; }
        public int ProductType { get; set; }
        public string year { get; set; }

        public int SrNo { get; set; }
       
        public string jan { get; set; }
        public string Feb { get; set; }
        public string Mar { get; set; }
        public string Apr { get; set; }
        public string May { get; set; }
        public string Jun { get; set; }
        public string July { get; set; }
        public string Aug { get; set; }
        public string Sep { get; set; }
        public string Oct { get; set; }
        public string Nov { get; set; }
        public string Dec { get; set; }

    }
    public class EcxelRecord
    {

        // public int ProductCaseId { get; set; }
        //   public int ProductId { get; set; }
        public int SrNo { get; set; }
        public string ProductName { get; set; }
        //  public string CaseName { get; set; }
        //  public string Name { get; set; }
        public string MeasureIn { get; set; }
        public decimal PTS { get; set; }
        //  public string year { get; set; }


        //  public string Ptc { get; set; }
        public string jan { get; set; }
        public string Feb { get; set; }
        public string Mar { get; set; }
        public string Apr { get; set; }
        public string May { get; set; }
        public string Jun { get; set; }
        public string July { get; set; }
        public string Aug { get; set; }
        public string Sep { get; set; }
        public string Oct { get; set; }
        public string Nov { get; set; }
        public string Dec { get; set; }

    }
    public class RxTypemodel_mst
    {
        public int RxTypeId { get; set; }
        public int RxType { get; set; }
        public string Name { get; set; }
    }

    public class ProductTypeResponse
    {
        public int ProductTypeId { get; set; }
        public string ProductType { get; set; }
    }
    public class ProductCaseMst_mstForAPI
    {

        //public int ProductCaseId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        //public string CaseName { get; set; }
        //public string Name { get; set; }
        //public string MeasureIn { get; set; }
        //public string StrangthIn { get; set; }
        //public string year { get; set; }
        //public int SrNo { get; set; }
        //public Decimal PTS { get; set; }
        //public float jan { get; set; }
        //public float Feb { get; set; }
        //public float Mar { get; set; }
        //public float Apr { get; set; }
        //public float May { get; set; }
        //public float Jun { get; set; }
        //public float July { get; set; }
        //public float Aug { get; set; }
        //public float Sep { get; set; }
        //public float Oct { get; set; }
        //public float Nov { get; set; }
        //public float Dec { get; set; }

    }
  
}
