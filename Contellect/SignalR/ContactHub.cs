using BusinessLogicProject.ViewModels.ContactDtos;
using Microsoft.AspNetCore.SignalR;

namespace Contellect.SignlR
{
    public class ContactHub :Hub
    {
        
        public async Task JoinContactGroup(string contactId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, contactId);
        }

        public async Task LeaveContactGroup(string contactId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, contactId);
        }
    }
}
