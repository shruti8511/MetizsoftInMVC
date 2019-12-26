namespace Metizsoft.Service
{

    using Metizsoft.Data.Modal;
    using System.Collections.Generic;
    using Metizsoft.Service.Interface;
    using Metizsoft.Data;
    using Metizsoft.Service.Models;
    using Metizsoft.Data.ViewModal;

    public interface IProduct
    {

        bool AddProduct(Product_Mst objproduct);
        bool DeleteProduct(long ProductId, bool IsActive);


        List<ProductCase_Mst> GetAllProductNameDropDown();
        bool AddProductcase(ProductCase_Mst objProductcase);
        List<ProductCase_Mst> GetAllProductName();
        List<ProductCase_Mst> GetAllProductCaseList();
        bool DeleteProductCase(long ProductCaseId, bool IsActive);

        ProductMst_mst GetProductByProductId(int ProductId);

        ProductCaseMst_mst GetProductCaseByProductCaseId(int ProductCaseId);

        List<RxTypemodel_mst> GetAllSpecialityName();

        List<RxTypemodel_mst> GetNameByRxType(int RxType);

        List<Product_Mst> GetAllProductList();

        List<Product_Mst> GetAllProductList(long GroupID);

        List<Product_Mst> GetAllProductList(long GroupID, long GroupTypeID);

        List<ProductTypeResponse> GetAllProductTypeName();

        List<ProductListResponse> GetProductReport();

        List<ProductTargetViewModel> GetProductTargetReport();

        long GetLastProductCode();
    }
}
