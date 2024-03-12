using Infrastructure.Model;
using Infrastructure.Models;

namespace Infrastructure.ViewModels
{
    public class MessageViewModel
    {
        public MessageModel? Message { get; set; }

        public ApplicationModel? Application { get; set; }
    }
}
