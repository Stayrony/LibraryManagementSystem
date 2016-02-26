using System.Collections.Generic;
using LMS.Service.Domain;
using Microsoft.Practices.Prism.PubSubEvents;

namespace LMS.UI.Utility
{
    public class BorrowBooksEvent :PubSubEvent<List<Book>>
    {
         
    }
}