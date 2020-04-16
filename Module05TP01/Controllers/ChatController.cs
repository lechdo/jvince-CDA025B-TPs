using Microsoft.Ajax.Utilities;
using Module05TP01.Models;
using Module05TP01.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Module05TP01.Controllers
{
    public class ChatController : Controller
    {
        // GET: Chat
        public ActionResult Index()
        {
            return View(FakeDb.Instance.Chats);
        }

        // GET: Chat/Details/5
        public ActionResult Details(int id)
        {
            return View(FakeDb.Instance.Chats.FirstOrDefault(c => c.Id == id));
        }

        // GET: Chat/Delete/5
        public ActionResult Delete(int id)
        {
            Chat chat = FakeDb.Instance.Chats.FirstOrDefault(c => c.Id == id);
            if (chat != null)
            {
                return View(chat);
            }
            else
            {
                return RedirectToAction("Index");
            }
            
        }

        // POST: Chat/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                FakeDb.Instance.Chats.Remove(FakeDb.Instance.Chats.FirstOrDefault(c => c.Id == id));

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
