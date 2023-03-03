using Nimap_Project1.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Nimap_Project1.Dal
{
    public class ProductDal
    {
        CommonDal ObjCommon = new CommonDal();
        public List<CategoryModel> GetAllCategory()
        {
            DataTable dt = new DataTable();
            List<CategoryModel> CatList = new List<CategoryModel>();
            try
            {
                SqlDataAdapter dap;
                SqlConnection MyConn = ObjCommon.GetConnection();
                SqlCommand cmd = new SqlCommand("GetAllCategory", MyConn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 50000;
                dap = new SqlDataAdapter(cmd);
                dap.Fill(dt);

                CatList = dt.AsEnumerable().ToList().ConvertAll(x => new CategoryModel
                {
                    CategoryId = x.Field<int>("CategoryId"),
                    CategoryName = x.Field<string>("CategoryName")
                });
                return CatList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public ProductModel CreateProduct(ProductModel model)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter dap;
                SqlConnection MyConn = ObjCommon.GetConnection();
                SqlCommand cmd = new SqlCommand("insert_Product", MyConn);
                cmd.Parameters.Add("@ProductName", SqlDbType.VarChar).Value = model.ProductName;
                cmd.Parameters.Add("@Product_CatId", SqlDbType.Int).Value = model.Product_CatId;

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 50000;
                dap = new SqlDataAdapter(cmd);
                dap.Fill(dt);
                if (Convert.ToString(dt.Rows[0]["message"]) == "Success")
                {
                    model.message = Convert.ToString(dt.Rows[0]["message"]);
                }
                else
                {
                    model.message = Convert.ToString(dt.Rows[0]["message"]);

                }

                return model;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public ProductViewModel GetProductsList(int page, int PageSize)
        {
            ProductViewModel promodel = new ProductViewModel();

            DataSet ds = new DataSet();
            try
            {
                SqlDataAdapter dap;
                SqlConnection MyConn = ObjCommon.GetConnection();
                SqlCommand cmd = new SqlCommand("sp_GetAllCategoriesandProduct", MyConn);
                cmd.Parameters.Add("@OffsetValue", SqlDbType.Int).Value = (page - 1) * PageSize;
                cmd.Parameters.Add("@PagingSize", SqlDbType.Int).Value = PageSize;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 50000;
                dap = new SqlDataAdapter(cmd);
                dap.Fill(ds);

                promodel.ListProduct = ds.Tables[0].AsEnumerable().ToList().ConvertAll(x => new ProductModel
                {
                    CategoryId = x.Field<int>("CategoryId"),
                    CategoryName = x.Field<string>("CategoryName"),
                    ProductName = x.Field<string>("ProductName"),
                    ProductId = x.Field<int>("ProductId")
                });
                promodel.pager = new Pager((ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0) ? Convert.ToInt32(ds.Tables[1].Rows[0]["TotalRecords"]) : 0, page, PageSize);
                return promodel;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public ProductModel GetProductbyid(int id)
        {
            DataTable dt = new DataTable();
            ProductModel model = new ProductModel();
            try
            {
                SqlDataAdapter dap;
                SqlConnection MyConn = ObjCommon.GetConnection();
                SqlCommand cmd = new SqlCommand("Productbyid", MyConn);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                dap = new SqlDataAdapter(cmd);
                dap.Fill(dt);

                model.ProductId = Convert.ToInt32(dt.Rows[0]["ProductId"]);
                model.ProductName = Convert.ToString(dt.Rows[0]["ProductName"]);
                model.Product_CatId = Convert.ToInt32(dt.Rows[0]["Product_CatId"]);
                return model;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public ProductModel UpdateProduct(ProductModel model)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter dap;
                SqlConnection MyConn = ObjCommon.GetConnection();
                SqlCommand cmd = new SqlCommand("UpdateData", MyConn);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = model.id;
                cmd.Parameters.Add("@ProductName", SqlDbType.VarChar).Value = model.ProductName;
                cmd.Parameters.Add("@Product_CatId", SqlDbType.Int).Value = model.Product_CatId;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 50000;
                dap = new SqlDataAdapter(cmd);
                dap.Fill(dt);
                if (Convert.ToString(dt.Rows[0]["message"]) == "Success")
                {
                    model.message = Convert.ToString(dt.Rows[0]["message"]);
                }
                else
                {
                    model.message = Convert.ToString(dt.Rows[0]["message"]);

                }

                return model;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public string DeleteProcedure(int id)
        {
            string message = string.Empty;
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter dap;
                SqlConnection MyConn = ObjCommon.GetConnection();
                SqlCommand cmd = new SqlCommand("DeleteProduct", MyConn);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 50000;
                dap = new SqlDataAdapter(cmd);
                dap.Fill(dt);

                message = Convert.ToString(dt.Rows[0]["message"]);
                return message;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}