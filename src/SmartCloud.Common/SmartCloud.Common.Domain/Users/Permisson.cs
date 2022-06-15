using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities;

namespace SmartCloud.Common.Users
{
    public class Permisson : Entity<Guid>
    {
        [NotMapped]
        public Guid UserId { get; set; }

        [NotMapped]
        public User User { get; set; }

        public string Name { get; set; }

        public string MenuId { get; set; }

        public string OrganizationAccounting { get; set; }

        /// <summary>
        /// 1-读  2-编辑
        /// </summary>
        public int Status { get; set; }
    }
}
