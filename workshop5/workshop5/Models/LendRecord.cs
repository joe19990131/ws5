using System.ComponentModel;

namespace workshop5.Models
{
    public class LendRecord
    {
        [DisplayName("借閱日期")]
        public string LendDate { get; set; }
        [DisplayName("借閱人編號")]
        public string KeeperId { get; set; }
        [DisplayName("英文姓名")]
        public string UserEname { get; set; }
        [DisplayName("中文姓名")]
        public string UserCname { get; set; }
    }
}