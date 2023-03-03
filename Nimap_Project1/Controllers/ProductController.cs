using Nimap_Project1.Dal;
using Nimap_Project1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nimap_Project1.Controllers
{
    public class ProductController : Controller
    {
        ProductDal ObjProduct = new ProductDal();
        // GET: Product
        [HttpGet]
        public ActionResult InsertProduct()
        {
            try
            {
                ProductModel model = new ProductModel();
                List<CategoryModel> CatList = new List<CategoryModel>();

                if (ModelState.IsValid)
                {
                    CatList = ObjProduct.GetAllCategory();
                    List<Category> CategoryList = new List<Category>();

                    foreach (var item in CatList)
                    {
                        Category Cat = new Category();
                        Cat.CategoryName = item.CategoryName;
                        Cat.CategoryId = item.CategoryId;
                        CategoryList.Add(Cat);
                    }
                    model.CategoryList = CategoryList;
                    model.CatList = model.CategoryList;
                    return View(model);
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [HttpPost]
        public ActionResult InsertProduct(ProductModel model)
        {
            try
            {
                    if (ModelState.IsValid)
                {

                    model = ObjProduct.CreateProduct(model);

                    if (model.message == "Success")
                    {
                        ViewBag.Message = "";
                        ViewBag.Class = "";
                        TempData["Message"] = model.message;
                        TempData["Class"] = "success";
                        return RedirectToAction("GetAllProduct", "Product");
                    }
                    else
                    {
                        ViewBag.Class = "primary";
                        ViewBag.Message = model.message;
                        return RedirectToAction("GetAllProduct", "Product");
                    }
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [HttpGet]
        public ActionResult GetAllProduct(int page = 1)
        {
            int PageSize = 10;
            ProductViewModel promodel = new ProductViewModel();
            promodel = ObjProduct.GetProductsList(page, PageSize);

            return View(promodel);
        }
        [HttpGet]
        public ActionResult EditProduct(int id)
        {
            ProductModel model = new ProductModel();
            List<CategoryModel> CatList = new List<CategoryModel>();
            model = ObjProduct.GetProductbyid(id);

            CatList = ObjProduct.GetAllCategory();
            List<Category> CategoryList = new List<Category>();

            foreach (var item in CatList)
            {
                Category Cat = new Category();
                Cat.CategoryName = item.CategoryName;
                Cat.CategoryId = item.CategoryId;
                CategoryList.Add(Cat);
            }
            model.CategoryList = CategoryList;

            return View(model);
        }
        [HttpPost]
        public ActionResult EditProduct(ProductModel model)
        {
            try
            {
                model = ObjProduct.UpdateProduct(model);

                if (model.message == "Success")
                {
                    ViewBag.Message = "";
                    ViewBag.Class = "";
                    TempData["Message"] = model.message;
                    TempData["Class"] = "success";
                    return RedirectToAction("GetAllProduct", "Product");
                }
                else
                {
                    ViewBag.Class = "primary";
                    ViewBag.Message = model.message;

                    return View(model);
                }


            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [HttpGet]
        public ActionResult DeletePro(int id)
        {
            string message = string.Empty;
            message = ObjProduct.DeleteProcedure(id);
            if (message == "Successfully Deleted")
            {
                TempData["DeleteMessage"] = message;
                TempData["Class"] = "success";
                return RedirectToAction("GetAllProduct", "Product");
            }
            else
            {
                TempData["DeleteMessage"] = message;
                TempData["Class"] = "Primary";
                return RedirectToAction("GetAllProduct", "Product");
            }
        }

    }
}