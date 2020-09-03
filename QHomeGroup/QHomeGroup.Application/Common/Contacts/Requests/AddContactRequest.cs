using QHomeGroup.Data.Enum;

namespace QHomeGroup.Application.Common.Contacts.Requests
{
    public class AddContactRequest
    {
        public string Name { set; get; }

        public string Phone { set; get; }

        public string Email { set; get; }

        public string Address { set; get; }

        public string Content { set; get; }

        public Status Status { set; get; }
    }
}
