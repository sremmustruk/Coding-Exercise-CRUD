using System.ComponentModel;

namespace CRUD_MVC_Application.Models
{
    public class ProductsModel
    {
        public int ProductID { get; set; }

        [DisplayName("Product Description")]
        public string ProdDesc { get; set; }


    }
}