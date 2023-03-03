using Nimap_Project1.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Nimap_Project1.Dal
{
    public class CategoryDal
    {
        CommonDal ObjCommon = new CommonDal();
        public CategoryModel RegisterCategory(CategoryModel model)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter dap;
                SqlConnection MyConn = ObjCommon.GetConnection();
                SqlCommand cmd = new SqlCommand("In_category", MyConn);
                cmd.Parameters.Add("@CategoryName", SqlDbType.VarChar).Value = model.CategoryName;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 50000;
                dap = new SqlDataAdapter(cmd);
                dap.Fill(dt);

                model.message = dt.Rows[0]["message"].ToString();
                return model;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public List<CategoryModel> GetCategory(List<CategoryModel> getlist)
        {
            CategoryModel model = new CategoryModel();
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter dap;
                SqlConnection MyConn = ObjCommon.GetConnection();
                SqlCommand cmd = new SqlCommand("GetCategory", MyConn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 50000;
                dap = new SqlDataAdapter(cmd);
                dap.Fill(dt);

                getlist = dt.AsEnumerable().ToList().ConvertAll(x => new CategoryModel
                {
                    CategoryId = x.Field<int>("CategoryId"),
                    CategoryName = x.Field<string>("CategoryName")
                });
                return getlist;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public CategoryModel GetCategorybyid(int id)
        {
            DataTable dt = new DataTable();
            CategoryModel model = new CategoryModel();
            try
            {
                SqlDataAdapter dap;
                SqlConnection MyConn = ObjCommon.GetConnection();
                SqlCommand cmd = new SqlCommand("Categorybyid", MyConn);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                dap = new SqlDataAdapter(cmd);
                dap.Fill(dt);

                model.CategoryName = Convert.ToString(dt.Rows[0]["CategoryName"]);
                return model;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public CategoryModel UpdateCategory(CategoryModel model)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter dap;
                SqlConnection MyConn = ObjCommon.GetConnection();
                SqlCommand cmd = new SqlCommand("UpdateCategory", MyConn);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = model.id;
                cmd.Parameters.Add("@CategoryName", SqlDbType.VarChar).Value = model.CategoryName;
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
        public string DeleteCategory(int id)
        {
            string message = string.Empty;
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter dap;
                SqlConnection MyConn = ObjCommon.GetConnection();
                SqlCommand cmd = new SqlCommand("DeleteCategory", MyConn);
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