﻿using System;
using System.Reflection;
using System.Web.Mvc;

namespace TCRC.Helpers
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class MultipleButtonAttribute : ActionNameSelectorAttribute
    {
        #region Members
        public string Name { get; set; }
        public string Argument { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Determines if name is valid
        /// </summary>
        /// <param name="controllerContext">The controller context</param>
        /// <param name="actionName">The action name</param>
        /// <param name="methodInfo">The method info</param>
        /// <returns></returns>
        public override bool IsValidName(ControllerContext controllerContext, string actionName, MethodInfo methodInfo)
        {
            var isValidName = false;
            var keyValue = string.Format("{0}:{1}", Name, Argument);
            var value = controllerContext.Controller.ValueProvider.GetValue(keyValue);

            if (value != null)
            {
                controllerContext.Controller.ControllerContext.RouteData.Values[Name] = Argument;
                isValidName = true;
            }

            return isValidName;
        }
        #endregion
    }
}