using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace workshop5.Models
{
    public class Books
    {
        [DisplayName("圖書類別ID")]
        public string BookClassId { get; set; }

        [DisplayName("圖書類別")]
        public string BookClass { get; set; }

        [DisplayName("書名")]
        public string BookName { get; set; }

        [DisplayName("購書日期")]
        public string BoughtDate { get; set; }

        [DisplayName("借閱狀態")]
        public string BookStatus { get; set; }

        [DisplayName("借閱狀態ID")]
        public string BookStatusId { get; set; }


        [DisplayName("借閱人")]
        public string BookKeeper { get; set; }

        [DisplayName("借閱人ID")]
        public string BookKeeperId { get; set; }

        [DisplayName("作者")]
        public string BookAuthor { get; set; }
        [DisplayName("出版商")]
        public string BookPublisher { get; set; }
        [DisplayName("內容簡介")]
        public string BookNote { get; set; }
        [DisplayName("書本編號")]
        public int BookId { get; set; }
    }
}