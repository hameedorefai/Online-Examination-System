using DataAccess;
using System.Collections.Generic;
using System.Transactions.Configuration;
namespace BusinessLayer
{
    public class clsUser
    {
        static public UserDTO GetUserInfoByUserID(int userId)
        {
            return clsUserData.GetUserInfoByUserID(userId);
        }
        static public UserDTO GetCurrentUserInfo()
        {   if(clsGlobal.CurrentUser != null)
            return clsUserData.GetUserInfoByUserID(clsGlobal.CurrentUser.UserID);
            return null;
        }
        static public List<UserDTO> GetUsersList()
        {
            return clsUserData.GetUsersList();
        }

        static private int _AddNewUser(AddUpdateUserDTO NewUser)
        {
            NewUser.username = NewUser.username.ToLower();
            NewUser.email = NewUser.email.ToLower();

            NewUser.passwrod = clsPasswordHashing.HashPassword(NewUser.passwrod);
            return clsUserData.AddNewUser(NewUser);
        }
        public static int RegisterNewUser(AddUpdateUserDTO newUser)
        {
            // here I Will make the confirmition Email logic Insha'Allah.

            return _AddNewUser(newUser);
        }


        static private bool _UpdateUserPassword(AddUpdateUserDTO User)
        {
            User.passwrod = clsPasswordHashing.HashPassword(User.passwrod);
            return clsUserData.UpdateUserPassword(User);
        }
        static public bool UpdateUser(AddUpdateUserDTO User)
        {
            User.username = User.username.ToLower().Trim();
            User.email = User.email.ToLower().Trim();
            User.passwrod = User.passwrod.Trim();

            if (User.passwrod != "string" && User.passwrod != "")
                _UpdateUserPassword(User);
            return clsUserData.UpdateUserInfo(User);
        }

        static public bool DeleteUserByUserID(int useriD)
        {
            return clsUserData.DeleteUser(useriD);
        }

        static public (int? UserID, string ErrorMessage) Login(string username, string password)
        {
            if (clsGlobal.CurrentUser != null)
                return (null,"there is already a session running, you need to log out and try again.");


            username = username.Trim();
            password = password.Trim();

            password = clsPasswordHashing.HashPassword(password);

            var (userID, errorMessage) =clsUserData.Login(username, password);

            if (userID.HasValue)
                clsGlobal.CurrentUser = GetUserInfoByUserID(userID.Value);
            return (userID, errorMessage);
        }

        static public void CheckUserRegisterating()
        {
            if (clsGlobal.CurrentUser == null)
                clsGlobal.CurrentUser = GetUserInfoByUserID(18);// which is that stored user with name "unknown".
        }

        static public int Logout()
        {
            if(clsGlobal.CurrentUser != null)
            {
                clsGlobal.CurrentUser = null;
                return 1;
            }
            else
            {
                return -1;
            }
        }
    }
}
