﻿using ConsoleBattleship.Screen;
using ConsoleBattleship.UserServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBattleship.states
{
    internal class LoginState : BaseState
    { 
        private LoginScreen screen = LoginScreen.GetScreen();
        Authentication Auth = new Authentication();
        DbHandler handler = DbHandler.Instance;
        public override void Enter(params object[] args)
        {
          screen.OnLoginAttempt += (string user, string password, bool registering, bool forgotPassword) =>
          {
              if (!registering && !forgotPassword)
              {
                 Auth.LogIn(user, password);
              }
              if (registering)
              {
                  //Auth.SignUp(user, password);
                  bool isExistingUser = handler.doesUsernameExist(user);
                  if (!isExistingUser)
                  {
                      handler.createUser(user, password);
                  }
              }
              if (forgotPassword)
              {
                  Auth.ForgotPassword(user, password);
              }
              
              bool isValid = handler.isValidUser(user, password);
              if (isValid)
              {
                  screen.Stop();
                  StateMachine.StateMachineInstance.ChangeState(StateMachine.MENU);
              }
          };
          screen.Start();
        }

        public override void Exit(params object[] args)
        {
          screen.Stop();
        }

        public override void Render(params object[] args)
        {
            //this is all the output to go on the screen.
        }

        public override void Update(params object[] args)
        {
            //function is used to update some logic on an event call, or tick rate.
            this.Render();
            //StateMachine.StateMachineInstance.ChangeState(StateMachine.StateMachineInstance.<STATE_NAME>, new object[] { <PARAMS> });
        }
    }
}
