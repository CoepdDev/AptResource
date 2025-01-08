using AptEMS.Models;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
 

public class ImageController : Controller
{
    private AptEmsContext db = new AptEmsContext();

    // Display uploaded images
    public ActionResult Index()
    {
        var images = db.UploadedImages.ToList();
        return View(images);
    }

    // Upload image
    [HttpPost]
    public ActionResult Upload(HttpPostedFileBase file)
    {
        if (file != null && file.ContentLength > 0)
        {
            string fileName = Path.GetFileName(file.FileName);
            string filePath = Path.Combine(Server.MapPath("~/Uploads"), fileName);

            // Save the file
            file.SaveAs(filePath);

            // Save the file info to the database
            UploadedImages image = new UploadedImages
            {
                FileName = fileName,
                FilePath = "/Uploads/" + fileName,
                UploadDate = DateTime.Now
            };
            db.UploadedImages.Add(image);
            db.SaveChanges();
        }
        return RedirectToAction("Index");
    }

    // Re-upload (add new)
    [HttpPost]
    public ActionResult Reupload(int id, HttpPostedFileBase file)
    {
        var image = db.UploadedImages.Find(id);
        if (image != null && file != null && file.ContentLength > 0)
        {
            // Delete the old file
            string oldFilePath = Server.MapPath(image.FilePath);
            if (System.IO.File.Exists(oldFilePath))
            {
                System.IO.File.Delete(oldFilePath);
            }

            // Save the new file
            string newFileName = Path.GetFileName(file.FileName);
            string newFilePath = Path.Combine(Server.MapPath("~/Uploads"), newFileName);
            file.SaveAs(newFilePath);

            // Update database record
            image.FileName = newFileName;
            image.FilePath = "/Uploads/" + newFileName;
            db.SaveChanges();
        }
        return RedirectToAction("Index");
    }

    // Delete image
    public ActionResult Delete(int id)
    {
        var image = db.UploadedImages.Find(id);
        if (image != null)
        {
            // Delete the file
            string filePath = Server.MapPath(image.FilePath);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            // Remove record from the database
            db.UploadedImages.Remove(image);
            db.SaveChanges();
        }
        return RedirectToAction("Index");
    }
    [HttpPost]
    public ActionResult ToggleActive(int id)
    {
        var image = db.UploadedImages.Find(id);
        if (image != null)
        {
            image.IsActive = !image.IsActive; // Toggle IsActive status
            db.SaveChanges();
        }
        return RedirectToAction("Index");
    }

}
