using Prism.Mvvm;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Reactive.Bindings;
using System;
using WpfCtr_CreateLabel.Models;

namespace WpfCtr_CreateLabel.ViewModels
{
    public class UserAuthenticationViewModel : BindableBase, IInteractionRequestAware
    {
        //--------------------------
        // IInteractionRequestAware
        //--------------------------
        public INotification Notification { get; set; }

        public Action FinishInteraction { get; set; }

        // UserID
        public ReactiveProperty<string> UserID { get; } = new ReactiveProperty<string>();

        // PassWD
        public ReactiveProperty<string> UserIDPassWD { get; } = new ReactiveProperty<string>();

        //--------------------------
        // Button
        //--------------------------
        // [OK]
        public DelegateCommand AuthenCommand => new DelegateCommand(() =>
        {
            // UserID
            ((UserAuthenticationNotification)Notification).Input_UserID = UserID.Value;

            // PassWD
            ((UserAuthenticationNotification)Notification).Input_PassWD = UserIDPassWD.Value;

            // 初期化
            UserID.Value = null;
            UserIDPassWD.Value = null;

            // 値戻す
            FinishInteraction();
        });

        // [Cancel]
        public DelegateCommand AuthenCancelCommand { get; private set; }

        //--------------------------
        // コンストラクター
        //--------------------------
        public UserAuthenticationViewModel()
        {
            // cancel
            AuthenCancelCommand = new DelegateCommand(CancelCmd);
        }

        //--------------------------
        // Cancelボタン処理
        //--------------------------
        private void CancelCmd()
        {
            ((UserAuthenticationNotification)Notification).Input_UserID = null;

            // 初期化
            UserID.Value = null;
            UserIDPassWD.Value = null;

            // 値戻す
            FinishInteraction();
        }
    }
}