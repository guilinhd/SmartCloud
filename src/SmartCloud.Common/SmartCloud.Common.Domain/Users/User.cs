using SmartCloud.Common.Organizations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace SmartCloud.Common.Users
{
    public class User : AggregateRoot<Guid>
    {
        public string OrganizationId { get; set; } = null!;

        [NotMapped]
        public Organization Organization { get; set; }

        public int No { get; set; }

        public string Name { get; private set; } = null!;

        public string Gender { get; set; }

        public virtual string Pwd { get; set; }

        /// <summary>
        /// MD5加盐
        /// </summary>
        public virtual string PwdSalt { get; set; }

        public string Phone { get; set; }

        public string Mobile { get; set; }

        public string Fax { get; set; }

        public string Post { get; set; }

        /// <summary>
        /// 停用状态 0-正常 1-停用
        /// </summary>
        public int Disable { get; set; }

        public string Description { get; set; }

        [NotMapped]
        public List<Permisson> permissons { get; set; } = new();

        private User() { }

        internal User (
            Guid id,
            string organizationId,
            int no,
            string name,
            string pwd,
            string pwdSalt,
            string gender,
            string phone,
            string mobile,
            string fax,
            string post,
            string description
         ) : base(id)
        {
            OrganizationId = organizationId;
            No = no;
            Name = name;
            Pwd = pwd;
            PwdSalt = pwdSalt;
            Gender = gender;
            Phone = phone;
            Mobile = mobile;
            Fax = fax;
            Post = post;
            Disable = 0;
            Description = description;
        }

        internal User ChangeName([NotNull] string name)
        {
            SetName(name);
            return this;
        }

        private void SetName([NotNull] string name)
        {
            Name = Check.NotNullOrWhiteSpace(
                name,
                nameof(name)
            );
        }
    }
}
