using System;
using Microsoft.AspNetCore.Mvc;

namespace ChalmersBookExchange.Controllers
{
    public interface IChatController
    {
        JsonResult ConversationWithContact(string contact);
        JsonResult SendMessage(string message, string contact);
    }
}