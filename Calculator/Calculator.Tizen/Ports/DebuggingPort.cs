/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */


using Xamarin.Forms.Platform.Tizen.Native;
using Calculator.Impl;
using Tizen;


namespace Calculator.Tizen
{
    /// <summary>
    /// Platform dependent implementation for the Logging and the Popup displaying.
    /// DebuggingPort is implementing IDebuggingAPIs which is defined in Calculator shared project.
    /// </summary>
    /// <remarks>
    /// Please refer to Xamarin Dependency Service
    /// https://developer.xamarin.com/guides/xamarin-forms/dependency-service/introduction/
    /// </remarks>
    class DebuggingPort : IDebuggingAPIs
    {
        /// <summary>
        /// A Calculator Windows reference. This is used to display a Dialog</summary>
        public static Xamarin.Forms.Platform.Tizen.Native.Window MainWindow
        {
            set;
            get;
        }

        /// <summary>
        /// A Logging Tag. </summary>
        public static string TAG = "Calculator";

        /// <summary>
        /// A method displays a debugging log. </summary>
        /// <param name="message"> A debugging message.</param>
        public void Dbg(string message)
        {
            Log.Debug(TAG, message);            
        }

        /// <summary>
        /// A method displays a error log. </summary>
        /// <param name="message"> A error message.</param>
        public void Err(string message)
        {
            Log.Error(TAG, message);
        }

        /// <summary>
        /// A method displays a dialog with a given message. </summary>
        /// <param name="message"> A debugging message.</param>
        public void Popup(string message)
        {
            if (MainWindow == null)
            {
                return;
            }
            //bool result = await Xamarin.Forms.Page.DisplayAlert("Calculator", message, "OK");
            
            Dialog toast = new Dialog(MainWindow);
            toast.Title = message;           
            toast.Timeout = 2.3;
            toast.BackButtonPressed += (s, e) =>
            {
                toast.Dismiss();
            };
            toast.Show();
        }
    }
}