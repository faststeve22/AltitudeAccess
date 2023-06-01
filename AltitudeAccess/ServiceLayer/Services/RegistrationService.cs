using AltitudeAccess.DataAccessLayer.DAOInterfaces;
using AltitudeAccess.PresentationLayer.DTOs;
using AltitudeAccess.ServiceLayer.Common;
using AltitudeAccess.ServiceLayer.Models;
using AltitudeAccess.ServiceLayer.ServiceInterfaces;
using System.Transactions;

namespace AltitudeAccess.ServiceLayer.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IUserDAO _userDAO;
        private readonly IPasswordManagementService _passwordManagementService;
        private readonly IValidationService _validationService;
        private readonly IPasswordDAO _passwordDAO;
        private readonly IUserApplicationRoleDAO _userApplicationRoleDAO;
        private readonly IMessageService _eventMessagehandler;

        public RegistrationService(IUserDAO userDAO, IPasswordManagementService passwordManagementService, IValidationService validationService, IPasswordDAO passwordDAO, IUserApplicationRoleDAO userApplicationRoleDAO, IMessageService eventMessagehandler)
        {
            _userDAO = userDAO;
            _passwordManagementService = passwordManagementService;
            _validationService = validationService;
            _passwordDAO = passwordDAO;
            _userApplicationRoleDAO = userApplicationRoleDAO;
            _eventMessagehandler = eventMessagehandler;
        }

        public User RegisterNewUser(RegistrationRequestDTO request)
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    ValidateUsernameAndPassword(request);
                    CheckUsernameAvailability(request);
                    User CreatedUser = AddNewUser(request);
                    _eventMessagehandler.PublishEventMessage(CreatedUser);
                    CreatePasswordHash(CreatedUser, request);
                    AddUserApplicationRole(request, CreatedUser);
                    scope.Complete();
                    return CreatedUser;
                }
                catch (Exception ex)
                {
                    throw;
                }

            }

        }

        public User AddNewUser(RegistrationRequestDTO request)
        {
            User NewUser = new User(request);
            try
            {
                User CreatedUser = AddUser(NewUser);
                return CreatedUser;
            }
            catch (Exception ex)
            {
                throw new UserRegistrationException("Failed to add new user.", ex);
            }
        }

        public bool ValidateUsernameAndPassword(RegistrationRequestDTO request)
        {
            if (!_validationService.IsValidUsername(request.UserName))
            {
                throw new ArgumentException("Username does not match the required format.");
            }

            if (!_validationService.IsValidPassword(request.Password))
            {
                throw new ArgumentException("Password does not match the required format.");
            }

            return true;
        }


        public void CheckUsernameAvailability(RegistrationRequestDTO request)
        {
            if (!_userDAO.IsUserNameAvailable(request.UserName))
            {
                throw new UsernameNotAvailableException("Username isn't available. Please choose another username.");
            }
        }

        public void CreatePasswordHash(User createdUser, RegistrationRequestDTO request)
        {
            string PasswordHash = _passwordManagementService.HashPassword(createdUser, request.Password);
            try
            {
                _passwordDAO.AddPassword(createdUser.UserId, PasswordHash);
            }
            catch (Exception ex)
            {
                throw new UserRegistrationException("Pasword hash failed", ex);
            }
        }

        public void AddUserApplicationRole(RegistrationRequestDTO request, User user)
        {
            UserApplicationRole UserApplicationRole = new UserApplicationRole(request, user);
            try
            {
                _userApplicationRoleDAO.AddUserApplicationRole(UserApplicationRole);
            }
            catch (Exception ex)
            {
                throw new UserRegistrationException("Failed to add user application role", ex);
            }
        }
        public User AddUser(User user)
        {
            return _userDAO.AddUser(user);
        }

    }
}
