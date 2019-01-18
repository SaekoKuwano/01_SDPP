using Prism.Interactivity.InteractionRequest;

namespace WpfCtr_CreateLabel.Models
{
    public class UserAuthenticationNotification : Notification
    {
        // UserID
        public string Input_UserID { get; set; }

        // PassWord
        public string Input_PassWD { get; set; }
    }
}