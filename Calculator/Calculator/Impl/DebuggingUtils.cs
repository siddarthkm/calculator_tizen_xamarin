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

using Xamarin.Forms;

namespace Calculator.Impl
{
    /// <summary>
    /// A debugging utility class.
    /// </summary>
    public sealed class DebuggingUtils
    {
        private static IDebuggingAPIs ism;
        private static readonly DebuggingUtils instance = new DebuggingUtils();

        /// <summary>
        /// A method provides instance of DebuggingUtils. </summary>
        public static DebuggingUtils Instance
        {
            get { return instance; }
        }
        
        /// <summary>
        /// Default implementation of IDebuggingAPIs interface . 
        /// This is required for the unit testing of the Calculator application. </summary>
        private class DefaultSM : IDebuggingAPIs
        {
            public void Dbg(string message)
            {
            }

            public void Err(string message)
            {
            }

            public void Popup(string message)
            {
            }
        }

        /// <summary>
        /// DebuggingUtils constructor which set interface instance. </summary>
        private DebuggingUtils()
        {
            if (DependencyService.Get<IDebuggingAPIs>() != null)
            {
                ism = DependencyService.Get<IDebuggingAPIs>();
            }
            else
            {
                ism = new DefaultSM();
            }
        }

        /// <summary>
        /// A method displays a debugging message </summary>
        /// <param name="message"> A list of command line arguments.</param>
        public static void Dbg(string message)
        {
            ism.Dbg(message);
        }

        /// <summary>
        /// A method displays a error message </summary>
        /// <param name="message"> A list of command line arguments.</param>
        public static void Err(string message)
        {
            ism.Err(message);
        }

        /// <summary>
        /// A method displays a pop up  message </summary>
        /// <param name="message"> A list of command line arguments.</param>
        public static void Popup(string message)
        {
            ism.Popup(message);
        }
    }
}
