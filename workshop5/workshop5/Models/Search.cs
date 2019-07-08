using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace workshop5.Models
{
    public class Search
    {
        [DisplayName("書名")]
        public string BookName { get; set; }
        [DisplayName("圖書類別")]
        public string BookClass { get; set; }
        [DisplayName("借閱人")]
        public string BookKeeper { get; set; }
        [DisplayName("借閱狀態")]
        public string BookStatus { get; set; }

        [DisplayName("借閱狀態")]
        public string BookStatusId { get; set; }

        [DisplayName("圖書類別ID")]
        public string BookClassId { get; set; }

        [DisplayName("借閱人ID")]
        public string BookKeeperId { get; set; }


    }
}