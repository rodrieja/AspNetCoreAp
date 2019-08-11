using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class MailNewsList : IDataList
    {
        public List<MailNews> mailNewsList;

        public void CreateList()
        {
            mailNewsList = new List<MailNews>();
        }
    }
}
