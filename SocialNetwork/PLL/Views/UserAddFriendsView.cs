using SocialNetwork.BLL.Exeptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Repositories;
using SocialNetwork.PLL.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork.PLL.Views
{
    public class UserAddFriendsView 
    {
        FriendRepository friendRepository = new FriendRepository();
        UserService userService;
        public UserAddFriendsView(UserService userService)
        {
            this.userService = userService;
        }

        public void Show(User user)
        {
            Console.Write("Введите почтовый адрес друга, которого хотите добавить:");
            string emailFriend = Console.ReadLine();
            bool isFriendExist = false;
            if (!String.IsNullOrEmpty(emailFriend))
            {
                try
                {
                    user = userService.FindByEmail(emailFriend);
                    isFriendExist = true;
                }
                catch (UserNotFoundException ex) { Console.WriteLine("Пользователь с такой почтой не найден!"); }

                if (isFriendExist == true)
                {
                    FriendEntity friendEntity = new FriendEntity();
                    friendEntity.friend_id = user.Id;
                    
                    friendRepository.Create(friendEntity);
                    
                    Console.WriteLine("Друг был успешно добавлен");
                }
            } else Console.WriteLine("Вы не ввели значение!");
            
            this.userService.Update(user);

            SuccessMessage.Show("Ваш профиль успешно обновлён!");
        }

    }
}
