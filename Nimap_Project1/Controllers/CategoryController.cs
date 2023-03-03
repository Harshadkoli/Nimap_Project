using Nimap_Project1.Dal;
using Nimap_Project1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nimap_Project1.Controllers
{
    public class CategoryController : Controller
    {
        CategoryDal ObjCategory = new CategoryDal();
        // GET: Category

        [HttpGet]
        public ActionResult Dashboard()
        {
            return View();
        }

        [HttpGet]
        public ActionResult InsertCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult InsertCategory(CategoryModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model = ObjCategory.RegisterCategory(model);
                    if (model.message == "Success")
                    {
                        TempData["Class"] = "success";
                        TempData["Message"] = model.message;
                        //ModelState.Clear();
                        return RedirectToAction("GetAllCategory","Category");
                    }
                    else
                    {
                        TempData["Class"] = "primary";
                        TempData["Message"] = model.message;
                        return RedirectToAction("GetAllCategory", "Category");
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
        public ActionResult GetAllCategory()
        {
            List<CategoryModel> getlist = new List<CategoryModel>();

            getlist = ObjCategory.GetCategory(getlist);

            return View(getlist);
        }
        [HttpGet]
        public ActionResult EditCat(int id)
        {
            CategoryModel model = new CategoryModel();
            model = ObjCategory.GetCategorybyid(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult EditCat(CategoryModel model)
        {
            model = ObjCategory.UpdateCategory(model);
            if (model.message == "Success")
            {
                TempData["Message"] = model.message;
                TempData["Class"] = "success";
                //ModelState.Clear();
                return RedirectToAction("GetAllCategory", "Category");
            }
            else
            {
                TempData["Message"] = model.message;
                TempData["Class"] = "primary";
                return RedirectToAction("GetAllCategory", "Category");
            }
        }
        [HttpGet]
        public ActionResult DeleteCat(int id)
        {
            string message = string.Empty;
            message = ObjCategory.DeleteCategory(id);
            if (message == "Successfully Deleted")
            {
                TempData["DeleteMessage"] = message;
                TempData["Class"] = "success";
                return RedirectToAction("GetAllCategory", "Category");
            }
            else
            {
                TempData["DeleteMessage"] = message;
                TempData["Class"] = "Primary";
                return RedirectToAction("GetAllCategory", "Category");
            }
        }
    }
}
