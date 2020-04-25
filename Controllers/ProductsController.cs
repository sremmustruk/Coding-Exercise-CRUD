using CRUD_MVC_Application.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace CRUD_MVC_Application.Controllers
{
    public class ProductsController : Controller
    {
        string connStr = @"Data Source=(local);Initial Catalog=CRUDDB;Integrated Security=True";
        [HttpGet]

        // GET: Products
        public ActionResult Index()
        {
            DataTable dtProducts = new DataTable();
            using (SqlConnection dbConn = new SqlConnection(connStr))
            {
                dbConn.Open();
                SqlDataAdapter sDa = new SqlDataAdapter("Select * from Products", dbConn);
                sDa.Fill(dtProducts);
            }
            return View(dtProducts);
        }

        //// GET: Products/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: Products/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View(new ProductsModel());
        }

        // POST: Products/Create
        [HttpPost]
        public ActionResult Create(ProductsModel productModel)
        {
            try
            {
                // TODO: Add insert logic here
                using (SqlConnection dbConn = new SqlConnection(connStr))
                {
                    dbConn.Open();
                    string sqlInsert = "Insert into Products values (@ProdDesc)";
                    SqlCommand dbCmd = new SqlCommand(sqlInsert, dbConn);
                    dbCmd.Parameters.AddWithValue("@ProdDesc", productModel.ProdDesc);
                    dbCmd.ExecuteNonQuery();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int id)
        {
            ProductsModel productsModel = new ProductsModel();
            DataTable dtProducts = new DataTable();
            using (SqlConnection dbConn = new SqlConnection(connStr))
            {
                dbConn.Open();
                string sqlSelect = "Select * from Products where ProductID = @ProductID";
                SqlDataAdapter sDa = new SqlDataAdapter(sqlSelect, dbConn);
                sDa.SelectCommand.Parameters.AddWithValue("@ProductID", id);
                sDa.Fill(dtProducts);
            }
            if (dtProducts.Rows.Count == 1)
            {
                productsModel.ProductID = Convert.ToInt32(dtProducts.Rows[0][0].ToString());
                productsModel.ProdDesc = dtProducts.Rows[0][1].ToString();
                return View(productsModel);
            }

            return RedirectToAction("Index");
        }

        // POST: Products/Edit/5
        [HttpPost]
        public ActionResult Edit(ProductsModel productModel)
        {

            // TODO: Add update logic here
            using (SqlConnection dbConn = new SqlConnection(connStr))
            {
                dbConn.Open();
                string sqlInsert = "Update Products set ProdDesc = @ProdDesc where ProductID = @ProductID";
                SqlCommand dbCmd = new SqlCommand(sqlInsert, dbConn);
                dbCmd.Parameters.AddWithValue("@ProductID", productModel.ProductID);
                dbCmd.Parameters.AddWithValue("@ProdDesc", productModel.ProdDesc);
                dbCmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");

        }

        // GET: Products/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection dbConn = new SqlConnection(connStr))
            {
                dbConn.Open();
                string sqlInsert = "delete Products where ProductID = @ProductID";
                SqlCommand dbCmd = new SqlCommand(sqlInsert, dbConn);
                dbCmd.Parameters.AddWithValue("@ProductID", id);
                dbCmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");

        }

        //// POST: Products/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
